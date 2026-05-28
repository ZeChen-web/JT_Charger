using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using log4net;
using Service.Charger.Client;

namespace Service.Charger.MyTask;

[Scope]
public class QueryBatteryInfoTask : ITask
{
    private volatile bool _stop;
    private static readonly ILog Log = LogManager.GetLogger(typeof(QueryBatteryInfoTask));

    public string Name()
    {
        return "QueryBatteryInfo";
    }

    public int Interval()
    {
        return 1000;
    }

    public void Handle()
    {
        try
        {
            foreach (var (key, client) in ClientMgr.Dictionary)
            {
                client.SendQueryBattery();
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }

    }

    public bool Stoped()
    {
        return _stop;
    }

    public void Stop()
    {
        _stop = true;
    }

    public void ResetStop()
    {
        _stop = false;
    }
}
