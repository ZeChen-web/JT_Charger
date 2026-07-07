using HybirdFrameworkCore.AutoTask;
using log4net;
using Repository.Station;

namespace Service.MyTask;

public class AutoStopCharge: ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(AutoStopCharge));
    private volatile bool _stop;
    
    public BinInfoRepository binInfoRepository { get; set; }
    public string Name()
    {
        return "AutoStopCharge";
    }

    public int Interval()
    {
        return 1000 * 30;
    }

    public void Handle()
    {
        
        
        
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