using System.Windows;
using Autofac;
using HybirdFrameworkCore.Autofac;
using log4net;
using Service.System;

namespace WpfStarter;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(MainWindow));

    private readonly SysConfigService _sysConfigService;

    public MainWindow()
    {
        InitializeComponent();
        _sysConfigService = AppInfo.Container.Resolve<SysConfigService>();
    }

    private void TestClick(object sender, RoutedEventArgs e)
    {
        var sysUsers = _sysConfigService.Query();
        MessageBox.Show($"count={sysUsers.Count}");
        Log.Info($"count={sysUsers.Count}");
    }
}