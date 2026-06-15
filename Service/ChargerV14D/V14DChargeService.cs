using Autofac;
using AutoMapper;
using Entity.DbModel.Station;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Redis;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Server;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.Init;

namespace Service.ChargerV14D;

/// <summary>V1.4D 充电服务 (高层业务操作)</summary>
[Scope]
public class V14DChargeService
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DChargeService));

    public BinInfoRepository BinInfoRepository { get; set; }
    
    /// <summary>远程启动充电</summary>
    public Result<string> StartCharge(string chargerSn, byte gun, uint balance,
        string logicCardNo = "", string physicalCardNo = "", string? chargeOrderNo = null)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<string>.Fail($"充电机 {chargerSn} 未连接");

        if (!client.Connected)
            return Result<string>.Fail($"充电机 {chargerSn} 连接已断开");

        if (string.IsNullOrWhiteSpace(chargeOrderNo))
        {
            chargeOrderNo = $"{client.PileCode}{gun}{DateTime.Now:yyMMddHHmmss}{new Random().Next(10, 99)}";
        }

        var result = client.SendRemoteStartCharge(chargeOrderNo, client.PileCode, gun,
            logicCardNo, physicalCardNo, balance);

        if (result.IsSuccess)
        {
            // 写入充电订单
            var chargeOrderRepo = AppInfo.Container.Resolve<ChargeOrderRepository>();
            chargeOrderRepo.Insert(new ChargeOrder
            {
                StartSoc = client.SOC,
                Sn = chargeOrderNo,
                ChargerNo = chargerSn,
                ChargeMode = 2, // V1.4D远程启动
                CmdStatus = 0,
                StartMode = 1,
                StartType = 1,
                BatteryNo = client.BatteryNo,
            });
            Log.Info($"StartCharge success: tsn={chargeOrderNo}, charger={chargerSn}, gun={gun}");
        }

        return Result<string>.Success(chargeOrderNo);
    }

    /// <summary>远程停止充电</summary>
    public Result<bool> StopCharge(string chargerSn, byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        if (!client.Connected)
            return Result<bool>.Fail($"充电机 {chargerSn} 连接已断开");

        return client.SendRemoteStopCharge(client.PileCode, gun);
    }

    /// <summary>读取实时数据</summary>
    public Result<bool> ReadRealTimeData(string chargerSn, byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendReadRealTimeData(client.PileCode, gun);
    }

    /// <summary>设置工作参数</summary>
    public Result<bool> SetParam(string chargerSn, byte gun, byte maxPower)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendParamSet(client.PileCode, gun, maxPower);
    }

    /// <summary>对时</summary>
    public Result<bool> TimeSync(string chargerSn)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,"1");
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendTimeSync(client.PileCode, DateTime.Now);
    }

    /// <summary>下发计费模型（传入已构建好的命令）</summary>
    public Result<bool> DistributeBillingModel(string chargerSn, V14DBillingModelSetCmd cmd)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,"1");
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        cmd.PileCode = client.PileCode;
        return client.SendBillingModelSet(cmd);
    }

    /// <summary>按计费模型版本号下发到指定充电桩</summary>
    public Result<bool> DistributeBillingModel(string chargerSn, int version)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,"1");
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        var versionRepo = AppInfo.Container.Resolve<ElecPriceModelVersionRepository>();
        var detailRepo = AppInfo.Container.Resolve<ElecPriceModelVersionDetailRepository>();

        var ver = versionRepo.QueryByClause(v => v.Version == version);
        if (ver == null || ver.Version == 0)
            return Result<bool>.Fail($"计费模型版本 {version} 不存在");

        var details = detailRepo.QueryByClauseToList(d => d.Version == version);
        if (details == null || details.Count == 0)
            return Result<bool>.Fail($"计费模型版本 {version} 无明细数据");

        var cmd = new V14DBillingModelSetCmd(client.PileCode, (ushort)version);
        cmd.PopulateFromDetails(details);

        Log.Info($"DistributeBillingModel charger={chargerSn}, version={version}");
        return client.SendBillingModelSet(cmd);
    }

    /// <summary>获取实时数据</summary>
    public V14DRealTimeDataReq? GetRealTimeData(string chargerSn,byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        return client?.RealTimeData;
    }

    /// <summary>获取充电功率 (kW)</summary>
    public float GetChargePower(string chargerSn,byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        return client?.ChargePower ?? 0;
    }

    /// <summary>发送电池在仓信号</summary>
    public Result<bool> SendBatteryInBinSignal(string chargerSn, byte gun, byte inBin)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendBatteryInBinSignal(client.PileCode, gun, inBin);
    }

    /// <summary>远程重启充电桩</summary>
    public Result<bool> RemoteRestart(string chargerSn,byte gun ,bool immediate = true)
    {
        var client = V14DClientMgr.GetBySn(chargerSn,gun.ToString());
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendRemoteRestart(client.PileCode, immediate ? (byte)0x01 : (byte)0x02);
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
            i.Exists == 1 && i.Status == 1 && i.Soc >= Convert.ToDecimal(StaticStationInfo.SwapSoc));
        if (canSwapCounts.Count > 0)
            batteryStatusInfoResp.canSwapCount = canSwapCounts.Count();
        List<BinInfo> chargingCounts =
            BinInfoRepository.QueryListByClause(i => i.Exists == 1 && i.Status == 1 && i.ChargeStatus == 1);
        if (chargingCounts.Count > 0)
            batteryStatusInfoResp.chargingCount = chargingCounts.Count();
        return Result<BatteryStatusInfoResp>.Success(batteryStatusInfoResp);
    }
    
    //TODO::映射数据给前端
    public Result<BatteryInfoResp> BatteryInfo(string chargeSn,byte no)
    {
        int gun = ((no & 1) == 1) ? 1 : 2;
        var chargerClient = V14DClientMgr.GetBySn(chargeSn,gun.ToString());
        BatteryInfoResp batteryInfoResp = new();
        if (chargerClient != null)
        {
            /*var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UploadTelemetryDataResp, UploadTelemetryData>().ReverseMap();
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
            batteryInfoResp.UploadTelemetryDataResp =
                mapper.Map<UploadTelemetryDataResp>(chargerClient.UploadTelemetryData);
            batteryInfoResp.BatteryBaseInfoResp = mapper.Map<BatteryBaseInfoResp>(chargerClient.BatteryBaseInfo);
            batteryInfoResp.BasicParameterResp = mapper.Map<BasicParameterResp>(chargerClient.BasicParameter);
            batteryInfoResp.UpBmsResp = mapper.Map<UpBmsResp>(chargerClient.UpBms);
            batteryInfoResp.UpAlarmResp = mapper.Map<UpAlarmResp>(chargerClient.UpAlarm);
            batteryInfoResp.VoltageCurrentSocResp = mapper.Map<VoltageCurrentSocResp>(chargerClient.VoltageCurrentSoc);
            batteryInfoResp.VoltageExtremumStatisticsResp =
                mapper.Map<VoltageExtremumStatisticsResp>(chargerClient.VoltageExtremumStatistics);*/
        }


        return Result<BatteryInfoResp>.Success(batteryInfoResp);
    }
}
