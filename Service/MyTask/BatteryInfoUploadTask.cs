/*
using Autofac;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using HybirdFrameworkCore.Redis;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Server;
using Service.Init;
using Service.Swap.Dto;
using SqlSugar;
using SqlSugar.Extensions;

namespace Service.MyTask;
[Scope]
public class BatteryInfoUploadTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BatteryInfoUploadTask));
    private RedisHelper RedisHelper { get; set; } = AppInfo.Container.Resolve<RedisHelper>();
    private BinInfoRepository BinInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();

    private static bool _stop;

    public string Name()
    {
        return nameof(BatteryInfoUploadTask);
    }

    public int Interval()
    {
        return 1000 * 1;
    }

    public void Handle()
    {
        Log.Info("begin start BatteryInfoUploadTask uoload");
        DateTime now = DateTime.Now;
        List<BinInfo> binInfos = BinInfoRepository.Query();
        List<SingleBatInfo> batInfos = binInfos.Where(it => it.Exists == 1).Select(it =>
        {
            Log.Info("start BatteryInfoUploadTask uoload bininfo select");
            V14DChargerClient? client = V14DClientMgr.GetBySn(it.ChargerNo,it.ChargerGunNo);
            SingleBatInfo batInfo = new SingleBatInfo()
            {
                bn = it.BatteryNo,
                bt = now,
                sd = it.ChargerNo,
                cno = Convert.ToInt32(it.ChargerNo.Substring(1)),
                el = 0,
                hc = Convert.ToInt32(it.ChargeStatus),
                hst=Convert.ToSingle(client?.BatteryStatusReport?.MaxBatteryTemperature),
                hsv = Convert.ToSingle(client?.BatteryStatusReport?.MaxCellVoltage),
                lst = Convert.ToSingle(client?.BatteryStatusReport?.MinBatteryTemperature),
                lsv = Convert.ToSingle(client?.BatteryStatusReport?.MinCellVoltage),
                soc = Convert.ToSingle(it.Soc),
                soe = Convert.ToSingle(it.Soe),
                soh = Convert.ToSingle(it.Soh)
            };
            return batInfo;
        }).ToList();
        BatDataInfo batDataInfo = new BatDataInfo
        {
            batn = batInfos.Count,
            sn = StaticStationInfo.StationNo,
            datainfo = batInfos
        };

        try
        {
            Log.Info("start BatteryInfoUploadTask send redis");
            RedisHelper.PublishAsync("BatteryInfoUploadTask", JsonConvert.SerializeObject(batDataInfo));
        }
        catch (Exception e)
        {
            Log.Info($"BatteryInfoUploadTask send redis error{e}");
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
*/
