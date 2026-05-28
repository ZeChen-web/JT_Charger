using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Job;
using log4net;
using Repository.System;

namespace Service.Job;

[Scope]
public class TestJob : AbstractCronJob
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(TestJob));

    private SysConfigRepository SysConfigRepository = AppInfo.Container.Resolve<SysConfigRepository>();

    protected override Task Handle()
    {
        Log.Info("work");
        return Task.CompletedTask;
    }

    protected override string Key()
    {
        return "test-job";
    }

    protected override string Cron()
    {
        return "0/2 * * * * ?";
    }
}
