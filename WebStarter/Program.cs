using Autofac;
using Autofac.Extensions.DependencyInjection;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.AutoTask;
using HybirdFrameworkCore.Configuration;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Job;
using HybirdFrameworkCore.Redis;
using log4net;
using Service.ChargerV14D.Server;
using Service.RealTime;
using SqlSugar;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);
var log = LogManager.GetLogger(typeof(Program));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
{
    cb.Register(c =>
    {
        var db = new SqlSugarScope(new ConnectionConfig
        {
            ConfigId = AppSettingsConstVars.ConfigId,
            ConnectionString = AppSettingsConstVars.DbSqlConnection,
            DbType = AppSettingsConstVars.DbDbType == IocDbType.MySql.ToString() ? DbType.MySql : DbType.SqlServer,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        }, c =>
        {
            c.Aop.OnLogExecuting = (sql, pars) =>
            {
                string nativeSql = UtilMethods.GetNativeSql(sql, pars);
                log.Info(nativeSql);
            };
        });
        return db;
    }).As<ISqlSugarClient>().InstancePerLifetimeScope();


    cb.RegisterModule(new AutofacModuleRegister());
});

//redis
var redisConnectionString = AppSettingsHelper.GetContent("Redis", "Connection");
var instanceName = AppSettingsHelper.GetContent("Redis", "InstanceName"); //默认数据库
var defaultDb = int.Parse(AppSettingsHelper.GetContent("Redis", "DefaultDB") ?? "0");
if (redisConnectionString != null && instanceName != null)
    builder.Services.AddSingleton(new RedisHelper(redisConnectionString, instanceName, defaultDb));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Logging.AddLog4Net("log4net.config");
builder.Services.AddSwaggerGen(c =>
{
    var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);
    //c.IncludeXmlComments(Path.Combine(basePath, Assembly.GetExecutingAssembly().GetName().Name+".xml"), true);
    c.IncludeXmlComments(Path.Combine(basePath, "WebStarter.xml"), true);
    c.IncludeXmlComments(Path.Combine(basePath, "Entity.xml"), true);
    c.IncludeXmlComments(Path.Combine(basePath, "HybirdFrameworkCore.xml"), true);
});
//跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy
    (name: "myCors",
        builde =>
        {
            builde.WithOrigins("*", "*", "*")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});
builder.Services.AddControllers().AddJsonOptions(configure =>
{
    configure.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseAuthorization();
app.UseCors("myCors");
app.MapControllers();

var list = AppSettingsHelper.GetContent("Kestrel", "Endpoints", "http", "Url");
foreach (var s in list.Split(";"))
{
    log.Info($"use url={s}");
    app.Urls.Add(s);
}


AppInfo.Container = app.Services.GetAutofacRoot();

var chargerPorts = AppSettingsHelper.GetContent("ChargerServer", "Ports");
if (!string.IsNullOrWhiteSpace(chargerPorts))
{
    foreach (var portStr in chargerPorts.Replace("，", ",").Split(',', StringSplitOptions.RemoveEmptyEntries))
    {
        if (int.TryParse(portStr.Trim(), out var port))
        {
            log.Info($"启动ChargerServer监听端口: {port}");
            ServerMgr.InitServer(port);
        }
    }
}
else
{
    log.Warn("未配置ChargerServer监听端口，跳过启动");
}

TaskInit.Init();
QuartzSchedulerFactory.Init();
app.Lifetime.ApplicationStopping.Register(QuartzSchedulerFactory.Shutdown);

bool.TryParse(AppSettingsHelper.GetContent("SignalR", "Enabled"), out var signalrEnabled);
if (signalrEnabled)
{
    RealtimeClient.Init();
}

app.Run();
