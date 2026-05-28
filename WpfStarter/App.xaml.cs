using System.Windows;
using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Configuration;
using SqlSugar;
using SqlSugar.IOC;

namespace WpfStarter;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var cb = new ContainerBuilder();
        cb.Register(c =>
        {
            var db = new SqlSugarClient(new ConnectionConfig
            {
                ConfigId = AppSettingsConstVars.ConfigId,
                ConnectionString = AppSettingsConstVars.DbSqlConnection, // 设置数据库连接字符串
                DbType = AppSettingsConstVars.DbDbType == IocDbType.MySql.ToString()
                    ? DbType.MySql
                    : DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute // 如果使用实体类的属性进行主键标识，请设置为 InitKeyType.Attribute
            });
            return db;
        }).As<ISqlSugarClient>().InstancePerLifetimeScope();

        cb.RegisterModule(new AutofacModuleRegister());

        var baseType = typeof(Window);
        cb.RegisterAssemblyTypes(typeof(App).Assembly)
            .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
            .InstancePerDependency();

        // cb.RegisterType(typeof(MainWindow)).SingleInstance();

        // 构建容器
        var Container = cb.Build();
        AppInfo.Container = Container.BeginLifetimeScope("root");
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = AppInfo.Container.Resolve<MainWindow>();
        mainWindow.Show();
    }
}