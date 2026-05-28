using Autofac;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Redis;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D;

/// <summary>V1.4D 充电服务 (高层业务操作)</summary>
[Scope]
public class V14DChargeService
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DChargeService));

    /// <summary>远程启动充电</summary>
    public Result<string> StartCharge(string chargerSn, byte gun, byte socLimit, uint balance,
        string logicCardNo = "", string physicalCardNo = "", string? chargeOrderNo = null)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
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
                Sn = chargeOrderNo,
                ChargerNo = chargerSn,
                ChargeMode = 2, // V1.4D远程启动
                CmdStatus = 0,
                StartMode = 1
            });
            Log.Info($"StartCharge success: tsn={chargeOrderNo}, charger={chargerSn}, gun={gun}");
        }

        return Result<string>.Success(chargeOrderNo);
    }

    /// <summary>远程停止充电</summary>
    public Result<bool> StopCharge(string chargerSn, byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        if (!client.Connected)
            return Result<bool>.Fail($"充电机 {chargerSn} 连接已断开");

        return client.SendRemoteStopCharge(client.PileCode, gun);
    }

    /// <summary>读取实时数据</summary>
    public Result<bool> ReadRealTimeData(string chargerSn, byte gun)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendReadRealTimeData(client.PileCode, gun);
    }

    /// <summary>设置工作参数</summary>
    public Result<bool> SetParam(string chargerSn, byte gun, byte allowWork, byte maxPower)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendParamSet(client.PileCode, gun, allowWork, maxPower);
    }

    /// <summary>对时</summary>
    public Result<bool> TimeSync(string chargerSn)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendTimeSync(client.PileCode, DateTime.Now);
    }

    /// <summary>下发计费模型</summary>
    public Result<bool> DistributeBillingModel(string chargerSn, V14DBillingModelSetCmd cmd)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        cmd.PileCode = client.PileCode;
        return client.SendBillingModelSet(cmd);
    }

    /// <summary>获取实时数据</summary>
    public V14DRealTimeDataReq? GetRealTimeData(string chargerSn)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        return client?.RealTimeData;
    }

    /// <summary>获取充电功率 (kW)</summary>
    public float GetChargePower(string chargerSn)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        return client?.ChargePower ?? 0;
    }

    /// <summary>发送电池在仓信号</summary>
    public Result<bool> SendBatteryInBinSignal(string chargerSn, byte gun, byte inBin)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendBatteryInBinSignal(client.PileCode, gun, inBin);
    }

    /// <summary>远程重启充电桩</summary>
    public Result<bool> RemoteRestart(string chargerSn, bool immediate = true)
    {
        var client = V14DClientMgr.GetBySn(chargerSn);
        if (client == null)
            return Result<bool>.Fail($"充电机 {chargerSn} 未连接");

        return client.SendRemoteRestart(client.PileCode, immediate ? (byte)0x01 : (byte)0x02);
    }
}
