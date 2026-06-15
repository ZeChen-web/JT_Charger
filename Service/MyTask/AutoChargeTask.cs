using Common.Const;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Utils;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.ChargerV14D.Server;
using Service.Init;

namespace Service.Charger;

[Scope]
public class AutoChargeTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(AutoChargeTask));
    private volatile bool _stop;

    public ElecPriceModelVersionRepository elecPriceModelVersionRepository { get; set; }
    public ElecPriceModelVersionDetailRepository elecPriceModelVersionDetailRepository { get; set; }
    public BatteryOpModelRepository batteryOpModelRepository { get; set; }
    public BatteryOpModelDetailRepository batteryOpModelDetailRepository { get; set; }
    public BinInfoRepository binInfoRepository { get; set; }
    public EquipInfoRepository EquipInfoRepository { get; set; }

    public string Name()
    {
        return "AutoChargeTask";
    }

    public int Interval()
    {
        return 1000 * 15;
    }

    public void Handle()
    {
        try
        {
            DateTime now = DateTime.Now;


            List<BinInfo> allBinInfos = binInfoRepository.Query();
            if (allBinInfos.Count < 0)
            {
                Log.Info("lack of binInfos");
                return;
            }

            List<EquipInfo> chargerList =
                EquipInfoRepository.QueryListByClause(it => it.TypeCode == (int)EquipmentType.Charger && it.Status==1);
            HashSet<string> autoChargeSet =
                chargerList.Where(it => it.AutoCharge == 1).Select(it => it.Code).ToHashSet();


            var binInfos = allBinInfos.Where(it => autoChargeSet.Contains(it.ChargerNo)).ToList();

            if (ObjUtils.IsEmpty(binInfos))
            {
                Log.Info("there is no auto charger");
                return;
            }

            /*#region 电价模型

            int ceid = StaticStationInfo.Ceid;
            ElecPriceModelVersion elecPriceModelVersion =
                elecPriceModelVersionRepository.QueryByClause(i => i.Version == ceid);
            if (elecPriceModelVersion == null)
            {
                Log.Info("lack of effective elecPriceModelVersion");
                return;
            }

            List<ElecPriceModelVersionDetail> elecPriceModelVersionDetails =
                elecPriceModelVersionDetailRepository.QueryListByClause(it =>
                    it.Version == elecPriceModelVersion.Version);
            /*ElecPriceModelVersionDetail? elecPriceModelVersionDetail = elecPriceModelVersionDetails.Where(i =>
                i.StartHour <= now.Hour && i.StartMinute <= now.Minute
                                        && i.EndHour > now.Hour &&
                                        i.EndMinute > now.Minute).FirstOrDefault();#1#
            ElecPriceModelVersionDetail? elecPriceModelVersionDetail = null;

            foreach (var VARIABLE in elecPriceModelVersionDetails)
            {
                // 构造开始和结束的DateTime对象，使用当前日期的年月日
                DateTime startTime = new DateTime(now.Year, now.Month, now.Day, VARIABLE.StartHour,
                    VARIABLE.StartMinute, 0);
                DateTime endTime = new DateTime(now.Year, now.Month, now.Day, VARIABLE.EndHour, VARIABLE.EndMinute, 0);
                if (DateTime.Now >= startTime && DateTime.Now <= endTime)
                {
                    elecPriceModelVersionDetail = VARIABLE;
                }
            }

            if (elecPriceModelVersionDetail == null)
            {
                Log.Info("lack of effective elecPriceModelVersionDetail");
                return;
            }

            #endregion*/

            #region 运营模型

            int oid = int.Parse(StaticStationInfo.Oid);
            BatteryOpModel batteryOpModel = batteryOpModelRepository.QueryByClause(d => d.ModelId == oid);
            if (batteryOpModel == null)
            {
                Log.Info("lack of effective batteryOpModel");
                return;
            }

            List<BatteryOpModelDetail> batteryOpModelDetails =
                batteryOpModelDetailRepository.QueryListByClause(d => d.ModelId == oid);

            List<BatteryOpModelDetail> opModelDetails = new List<BatteryOpModelDetail>();
            /*List<BatteryOpModelDetail> opModelDetails = batteryOpModelDetails.Where(t =>
            {
                List<int> start = t.StartTime.Split(":").Select(int.Parse).ToList();
                List<int> end = t.EndTime.Split(":").Select(int.Parse).ToList();
                return now.Hour >= start[0] && now.Hour < end[0] && now.Minute >= start[1] && now.Minute < end[1] &&
                       now.Second >= start[2] && now.Second < end[2];
            }).ToList();*/

            foreach (var t in batteryOpModelDetails)
            {
                if (DateTime.Now >= DateTime.Parse(t.StartTime) &&
                    DateTime.Now <= DateTime.Parse(t.EndTime))
                {
                    opModelDetails.Add(t);
                }
            }

            if (opModelDetails.Count == 0)
            {
                Log.Info("lack of effective batteryOpModelDetails");
                return;
            }

            int needBatteryCount = opModelDetails[0].BatteryCount ?? 8;
            List<BinInfo> canSwapList = allBinInfos.Where(it =>
                    it.Soc != null && Convert.ToSingle(it.Soc) >= StaticStationInfo.SwapSoc && it.CanSwapFlag == 1 && it.ChargeStatus!=1)
                .ToList();

            List<BinInfo> chargingList = binInfos.Where(it => it.ChargeStatus == 1).ToList();
            Log.Info(
                $"chargingList={JsonConvert.SerializeObject(chargingList.Select(info => info.Code + "," + info.ChargerNo).ToList())}");

            #region 现有电池满足需求 停止正在充电的电池

            /*if (canSwapList.Count >= needBatteryCount)
            {
                Log.Info($"can swap count {canSwapList.Count} > {needBatteryCount}, all stop={JsonConvert.SerializeObject(chargingList.Select(info => info.Code + "," + info.ChargerNo).ToList())}");
                foreach (var t in chargingList)
                {
                    if (t.Soc >= StaticStationInfo.ChargeSoc)
                    {
                        Log.Info($"auto stop charge {t.No}");
                        ClientMgr.GetBySn(t.ChargerNo)?.SendRemoteStopCharging();
                    }

                }
            }*/

            #endregion
            
            int stopCount = chargingList.Count + canSwapList.Count - needBatteryCount;
            Log.Info($"chargingList.Count={chargingList.Count}, canSwapList.Count={canSwapList.Count},needBatteryCount={needBatteryCount}");
            Log.Info($"stopCount={stopCount}");
            if (canSwapList.Count < needBatteryCount)
            {
              
                if (stopCount < 0)
                {
                   // Log.Info($"stopCount={stopCount}");
                    List<BinInfo> canChargeList = binInfos.Where(it =>
                            it.Soc != null && Convert.ToSingle(it.Soc) < StaticStationInfo.SwapSoc &&
                            it.CanChargeFlag == 1 && it.Exists == 1 && it.ChargeStatus != 1
                            &&it.BatteryNo!="0"&&it.BatteryNo!="-1"&&it.BatteryNo!=null)
                        .ToList();
                    //启动电量高的
                    canChargeList.Sort((a, b) => (b.Soc ?? 0).CompareTo(a.Soc ?? 0));
                    

                    byte chargeSoc = StaticStationInfo.ChargeSoc;
                    Log.Info(
                        $"need start count={-stopCount}, canChargeList={JsonConvert.SerializeObject(canChargeList.Select(info => info.Code + "," + info.ChargerNo).ToList())}");
                    int number = 0;
                    foreach (var binInfo in canChargeList)
                    {
                        if (binInfo.ChargeStatus != 1)
                        {
                            //没有充电时候在充电
                            Result<bool>? result = V14DClientMgr.GetBySn(binInfo.ChargerNo, binInfo.ChargerGunNo)
                                ?.SendRemoteStartCharge( $"{binInfo.ChargerNo}{binInfo.ChargerGunNo}{DateTime.Now:yyMMddHHmmss}{new Random().Next(10, 99)}", binInfo.ChargerNo, Convert.ToByte(binInfo.ChargerGunNo),
                                    null, null);
                            if (result is { IsSuccess: true })
                            {
                                Log.Info($"auto start charge {binInfo.ChargerNo}");
                                number++;
                            }

                            if (-stopCount == number)
                            {
                                Log.Info($"auto start charge count {-stopCount}");
                                break;
                            }
                        }
                    }
                }
            }
          
            if (stopCount > 0)
            {
                if (chargingList.Count > 0)
                {
                    chargingList = chargingList.OrderByDescending(i => i.Soc).ToList();
                    int count = 0;
                    foreach (var ch in chargingList)
                    {
                        if (ch.Soc < StaticStationInfo.ChargeSoc)
                        {
                            Log.Info($"auto stop charge by more charging bin {ch.No} soc:{ch.Soc}");
                            V14DClientMgr.GetBySn(ch.ChargerNo, ch.ChargerGunNo)
                                ?.SendRemoteStopCharge(ch.ChargerNo, Convert.ToByte(ch.ChargerGunNo));
                            count++;
                            if (count == stopCount)
                            {
                                break;
                            }
                        }
                    }
                }
            }


            #region 达到充电SOC自动停

            List<BinInfo> stopList = chargingList.Where(it => it.Soc > StaticStationInfo.ChargeSoc).ToList();
            Log.Info(
                $"need stop count={stopList.Count}, stopList={JsonConvert.SerializeObject(stopList.Select(info => info.Code + "," + info.ChargerNo).ToList())}");
            foreach (var binInfo in stopList)
            {
                Log.Info($"auto stop charge {binInfo.No} soc:{binInfo.Soc}");
                V14DClientMgr.GetBySn(binInfo.ChargerNo, binInfo.ChargerGunNo)
                    ?.SendRemoteStopCharge(binInfo.ChargerNo, Convert.ToByte(binInfo.ChargerGunNo));
            }

            #endregion

            #endregion
        }
        catch (Exception e)
        {
            Log.Error("handle with error", e);
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