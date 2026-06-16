using Autofac;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
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

    /// <summary>
    /// 记录每台充电机最后一次查询电池信息时间
    /// Key: ChargerNo_GunNo
    /// </summary>
    private static readonly Dictionary<string, DateTime> QueryTimes = new();

    private readonly BinInfoRepository BinInfoRepository =
        AppInfo.Container.Resolve<BinInfoRepository>();

    public string Name()
    {
        return nameof(BatteryExitTask);
    }

    /// <summary>
    /// 任务每5秒执行一次
    /// </summary>
    public int Interval()
    {
        return 1000 * 5;
    }

    public void Handle()
    {
        var binInfos = BinInfoRepository.QueryByClauseToList(i => i.CacheBinFlag == 0);

        foreach (var binInfo in binInfos)
        {
            var client = V14DClientMgr.GetBySn(
                binInfo.ChargerNo,
                binInfo.ChargerGunNo);

            if (client == null)
            {
                Log.Info($"充电机 {binInfo.ChargerNo}-{binInfo.ChargerGunNo} 未连接");
                continue;
            }

            /*var key = $"{binInfo.ChargerNo}_{binInfo.ChargerGunNo}";

            // 每10秒查询一次电池信息
            if (!QueryTimes.TryGetValue(key, out var lastTime) ||
                DateTime.Now - lastTime >= TimeSpan.FromSeconds(10))
            {
                client.SendV14DBatteryInfoQueryCmd(binInfo.ChargerNo);

                QueryTimes[key] = DateTime.Now;

                Log.Debug($"发送电池信息查询：{binInfo.ChargerNo}-{binInfo.ChargerGunNo}");
            }*/

            // 每5秒发送一次在位信号
            client.SendBatteryInBinSignal(
                binInfo.ChargerNo,
                Convert.ToByte(binInfo.ChargerGunNo),
                (byte)binInfo.Exists);
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