using System.Collections.Concurrent;
using AutoMapper;
using Common.Const;
using Entity.DbModel.Station;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Req;
using Service.Init;
using SqlSugar;

namespace Service.Charger;

/// <summary>
/// 充电机服务
/// </summary>
[Scope]
public class ChargerService
{
    public BinInfoRepository BinInfoRepository { get; set; }

    public EquipInfoRepository EquipInfoRepository { get; set; }
    public ElecPriceModelVersionDetailRepository ElecPriceModelVersionDetailRepository { get; set; }

    /// <summary>
    /// 启动充电
    /// </summary>
    /// <param name="binNo"></param>
    /// <returns></returns>
    public Result<bool> StartChargeByBinNo(string binNo)
    {
        BinInfo? binInfo = BinInfoRepository.QueryByBinNo(binNo);
        if (binInfo == null)
        {
            return Result<bool>.Fail(@"充电仓不存在");
        }

        if (string.IsNullOrWhiteSpace(binInfo.ChargerNo))
        {
            return Result<bool>.Fail(@"充电仓未配置充电机编号");
        }

        ChargerClient? chargerClient = ClientMgr.GetBySn(binInfo.ChargerNo);
        if (chargerClient == null || !chargerClient.Connected)
        {
            return Result<bool>.Fail(@"充电机未连接");
        }

        //自动充电策略下不支持手动充电
       EquipInfo? charge =
            EquipInfoRepository.QueryByClause(it => it.TypeCode == (int)EquipmentType.Charger && it.AutoCharge==0
            && it.Code==binInfo.ChargerNo);

       if (charge == null)
       {
           return Result<bool>.Fail(@"自动充电模式下不支持手动充电");
       }

       byte chargeSoc = StaticStationInfo.ChargeSoc;

        float? chargePower = EquipInfoRepository.QueryPowerByCode(binInfo.ChargerNo);
        float? power = chargePower == null ? StaticStationInfo.ChargePower : chargePower;
        return chargerClient.StartCharge(chargeSoc,(float)power, 1);
    }

    /// <summary>
    /// 停止充电
    /// </summary>
    /// <param name="binNo"></param>
    /// <returns></returns>
    public Result<bool> StopChargeByBinNo(string binNo)
    {
        BinInfo? binInfo = BinInfoRepository.QueryByBinNo(binNo);
        if (binInfo == null)
        {
            return Result<bool>.Fail(@"充电仓不存在");
        }

        if (string.IsNullOrWhiteSpace(binInfo.ChargerNo))
        {
            return Result<bool>.Fail(@"充电仓未配置充电机编号");
        }

        ChargerClient? chargerClient = ClientMgr.GetBySn(binInfo.ChargerNo);
        if (chargerClient == null || !chargerClient.Connected)
        {
            return Result<bool>.Fail(@"充电机未连接");
        }

        chargerClient.SendRemoteStopCharging();

        return Result<bool>.Success(true, "发送停止命令成功");
    }

    /// <summary>
    /// 下发尖峰平谷
    /// </summary>
    /// <param name="binNo"></param>
    /// <returns></returns>
    public Result<bool> DistributeElecPriceForCharge(int version)
    {
        ConcurrentDictionary<string, ChargerClient> chargerClients = ClientMgr.Dictionary;
        if (chargerClients.Values.Count <= 0)
        {
            return Result<bool>.Fail();
        }

        foreach (var chargerClientsValue in chargerClients.Values)
        {
            if (chargerClientsValue.Connected)
            {
                chargerClientsValue.SendSetPeakValleyTime(chargerClientsValue.BulidSetPeakValleyTimeObj(version));
            }
        }

        return Result<bool>.Success();
    }

    public SetPeakValleyTime BulidSetPeakValleyTimeObj(int version)
    {
        List<ElecPriceModelVersionDetail> elecPriceModelVersionDetails =
            ElecPriceModelVersionDetailRepository.QueryListByClause(u => u.Version == version, u => u.StartHour,
                OrderByType.Asc);
        SetPeakValleyTime setPeakValleyTime = new SetPeakValleyTime()
        {
            NumberTime = Convert.ToByte(elecPriceModelVersionDetails.Count),
            StartHH1 = (byte)(elecPriceModelVersionDetails.Count > 0?Convert.ToByte(elecPriceModelVersionDetails[0].StartHour) :0),
            StartHH2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].StartHour) : 0),
            StartHH3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].StartHour):0),
            StartHH4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].StartHour):0),
            StartHH5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].StartHour):0),
            StartHH6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].StartHour):0),
            StartHH7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].StartHour):0),
            StartHH8 = (byte)(elecPriceModelVersionDetails.Count > 7?Convert.ToByte(elecPriceModelVersionDetails[7].StartHour) : 0),
            StartHH9 = (byte)(elecPriceModelVersionDetails.Count > 8?Convert.ToByte(elecPriceModelVersionDetails[9].StartHour) : 0),
            StartHH10 = (byte)(elecPriceModelVersionDetails.Count > 9?Convert.ToByte(elecPriceModelVersionDetails[9].StartHour) : 0),
            StartHH11 = (byte)(elecPriceModelVersionDetails.Count > 10?Convert.ToByte(elecPriceModelVersionDetails[10].StartHour) : 0),
            StartHH12 = (byte)(elecPriceModelVersionDetails.Count > 11?Convert.ToByte(elecPriceModelVersionDetails[11].StartHour) : 0),
            StartHH13 = (byte)(elecPriceModelVersionDetails.Count > 12?Convert.ToByte(elecPriceModelVersionDetails[12].StartHour) : 0),
            StartHH14 = (byte)(elecPriceModelVersionDetails.Count > 13?Convert.ToByte(elecPriceModelVersionDetails[13].StartHour) : 0),
            
            StartMM1 = (byte)(elecPriceModelVersionDetails.Count > 0?Convert.ToByte(elecPriceModelVersionDetails[0].StartMinute):0),
            StartMM2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].StartMinute):0),
            StartMM3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].StartMinute):0),
            StartMM4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].StartMinute):0),
            StartMM5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].StartMinute):0),
            StartMM6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].StartMinute):0),
            StartMM7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].StartMinute) : 0),
            StartMM8 = (byte)(elecPriceModelVersionDetails.Count > 7 ? Convert.ToByte(elecPriceModelVersionDetails[7].StartMinute) : 0),
            StartMM9 = (byte)(elecPriceModelVersionDetails.Count > 8 ? Convert.ToByte(elecPriceModelVersionDetails[8].StartMinute) : 0),
            StartMM10 = (byte)(elecPriceModelVersionDetails.Count > 9 ? Convert.ToByte(elecPriceModelVersionDetails[9].StartMinute) : 0),
            StartMM11 = (byte)(elecPriceModelVersionDetails.Count > 10 ? Convert.ToByte(elecPriceModelVersionDetails[10].StartMinute) : 0),
            StartMM12 = (byte)(elecPriceModelVersionDetails.Count > 11 ? Convert.ToByte(elecPriceModelVersionDetails[11].StartMinute) : 0),
            StartMM13 = (byte)(elecPriceModelVersionDetails.Count > 12 ? Convert.ToByte(elecPriceModelVersionDetails[12].StartMinute) : 0),
            StartMM14 = (byte)(elecPriceModelVersionDetails.Count > 13 ? Convert.ToByte(elecPriceModelVersionDetails[13].StartMinute) : 0),
            
            TimePeak1 = (byte)(elecPriceModelVersionDetails.Count > 0 ? Convert.ToByte(elecPriceModelVersionDetails[0].Type):0),
            TimePeak2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].Type):0),
            TimePeak3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].Type):0),
            TimePeak4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].Type):0),
            TimePeak5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].Type):0),
            TimePeak6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].Type):0),
            TimePeak7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].Type):0),
            TimePeak8 = (byte)(elecPriceModelVersionDetails.Count > 7?Convert.ToByte(elecPriceModelVersionDetails[7].Type):0),
            TimePeak9 = (byte)(elecPriceModelVersionDetails.Count > 8?Convert.ToByte(elecPriceModelVersionDetails[8].Type):0),
            TimePeak10 = (byte)(elecPriceModelVersionDetails.Count > 9?Convert.ToByte(elecPriceModelVersionDetails[9].Type):0),
            TimePeak11 = (byte)(elecPriceModelVersionDetails.Count > 10?Convert.ToByte(elecPriceModelVersionDetails[10].Type):0),
            TimePeak12 = (byte)(elecPriceModelVersionDetails.Count > 11?Convert.ToByte(elecPriceModelVersionDetails[11].Type):0),
            TimePeak13 = (byte)(elecPriceModelVersionDetails.Count > 12?Convert.ToByte(elecPriceModelVersionDetails[12].Type):0),
            TimePeak14 = (byte)(elecPriceModelVersionDetails.Count > 13?Convert.ToByte(elecPriceModelVersionDetails[13].Type):0),
        };
        return setPeakValleyTime;
    }

    /// <summary>
    /// 电池状态信息：电池总数 满电数量、充电中、故障电池、维护中电池
    /// </summary>
    /// <returns></returns>
    public Result<BatteryStatusInfoResp> BatteryStatusInfo()
    {
        BatteryStatusInfoResp batteryStatusInfoResp = new BatteryStatusInfoResp();
        List<BinInfo> binInfos = BinInfoRepository.QueryListByClause(i => i.Exists == 1 && i.Status == 1);
        if (binInfos.Count > 0)
            batteryStatusInfoResp.btyTotalCount = binInfos.Count();
        List<BinInfo> canSwapCounts = BinInfoRepository.QueryListByClause(i =>
            i.Exists == 1 && i.Status == 1 && i.Soc >= Convert.ToDecimal(StaticStationInfo.SwapSoc) && i.ChargeStatus!=1);
        if (canSwapCounts.Count > 0)
            batteryStatusInfoResp.canSwapCount = canSwapCounts.Count();
        List<BinInfo> chargingCounts =
            BinInfoRepository.QueryListByClause(i => i.Exists == 1 && i.Status == 1 && i.ChargeStatus == 1);
        if (chargingCounts.Count > 0)
            batteryStatusInfoResp.chargingCount = chargingCounts.Count();
        return Result<BatteryStatusInfoResp>.Success(batteryStatusInfoResp);
    }

    public Result<BatteryInfoResp> BatteryInfo(string chargeSn)
    {

        var chargerClient = ClientMgr.GetBySn(chargeSn);
        BatteryInfoResp batteryInfoResp = new();
        if (chargerClient != null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UploadTelemetryDataResp , UploadTelemetryData>().ReverseMap();
                cfg.CreateMap<BatteryBaseInfoResp, BatteryBaseInfo>().ReverseMap();
                cfg.CreateMap<BasicParameterResp, BasicParameter>().ReverseMap();
                cfg.CreateMap<UpBmsResp, UpBms>().ReverseMap();
                cfg.CreateMap<UpAlarmResp, UpAlarm>().ReverseMap();
                cfg.CreateMap<VoltageCurrentSocResp, VoltageCurrentSoc>().ReverseMap();
                cfg.CreateMap<VoltageExtremumStatisticsResp, VoltageExtremumStatistics>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            
          
            batteryInfoResp.BinNo = chargerClient.BinNo;
            batteryInfoResp.ChargerNo = chargerClient.Sn;
            batteryInfoResp.BatteryNo = chargerClient.BatteryNo;
            batteryInfoResp.UploadTelemetryDataResp=mapper.Map<UploadTelemetryDataResp>(chargerClient.UploadTelemetryData);
            batteryInfoResp.BatteryBaseInfoResp=mapper.Map<BatteryBaseInfoResp>(chargerClient.BatteryBaseInfo);
            batteryInfoResp.BasicParameterResp=mapper.Map<BasicParameterResp>(chargerClient.BasicParameter);
            batteryInfoResp.UpBmsResp=mapper.Map<UpBmsResp>(chargerClient.UpBms);
            batteryInfoResp.UpAlarmResp=mapper.Map<UpAlarmResp>(chargerClient.UpAlarm);
            batteryInfoResp.VoltageCurrentSocResp=mapper.Map<VoltageCurrentSocResp>(chargerClient.VoltageCurrentSoc);
          
        }

      
        
        return Result<BatteryInfoResp>.Success(batteryInfoResp);
    }
}
