using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using HybirdFrameworkCore.Entity;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Server;

namespace Service.MyTask;

/// <summary>
/// 电池在位信号
/// </summary>
[Scope]
public class BatteryExitTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BatteryExitTask));
    private static bool _stop;

    public string Name()
    {
        return nameof(BatteryExitTask);
    }

    public int Interval()
    {
        return 1000 * 5;
    }
    
    
    private BinInfoRepository BinInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();
    

    public void Handle()
    {
        var bininfos = BinInfoRepository.Query();
        foreach (var binInfo in bininfos)
        {
            var client = V14DClientMgr.GetBySn(binInfo.ChargerNo,binInfo.ChargerGunNo);
            if (client == null)
                Log.Info($"充电机 {binInfo.ChargerNo} 未连接");
            else
            {
                client.SendV14DBatteryInfoQueryCmd(binInfo.ChargerNo);
                client.SendBatteryInBinSignal(binInfo.ChargerNo,Convert.ToByte(binInfo.ChargerGunNo),(byte)binInfo.Exists);
            }
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