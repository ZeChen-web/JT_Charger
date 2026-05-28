using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Configuration;
using log4net;
using log4net.Config;
using SqlSugar;
using SqlSugar.IOC;

namespace WinFormStarter
{
}


internal static class Program
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
    public static IContainer Container { get; private set; }

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.xml"));
        Application.ThreadException += Application_ThreadException;
        // 创建容器
        var builder = new ContainerBuilder();
        // 注册 SqlSugar 配置
        builder.Register(c =>
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = AppSettingsConstVars.DbSqlConnection, // 设置数据库连接字符串
                DbType = AppSettingsConstVars.DbDbType == IocDbType.MySql.ToString() ? DbType.MySql : DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute // 如果使用实体类的属性进行主键标识，请设置为 InitKeyType.Attribute
            });
            return db;
        }).As<ISqlSugarClient>().InstancePerLifetimeScope();

        //获取所有窗体类型
        var baseType = typeof(Form);
        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(t => baseType.IsAssignableFrom(t) && t != baseType).AsImplementedInterfaces()
            .InstancePerDependency()
            .Named(t => t.Name, typeof(Form));
        builder.RegisterModule(new AutofacModuleRegister());
        // 构建容器
        Container = builder.Build();
        AppInfo.Container = Container.BeginLifetimeScope("root");
        Application.Run(AppInfo.Container.ResolveNamed<Form>("Form2"));
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs ex)
    {
        Log.Error($"system error {ex.Exception}");
        var result = MessageBox.Show("系统发生错误，您需要退出系统吗？", "异常", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes) Application.Exit();
    }
}