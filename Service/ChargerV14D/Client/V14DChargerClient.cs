using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkDriver.Session;
using HybirdFrameworkDriver.TcpClient;
using log4net;
using Service.ChargerV14D.Codec;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Client;

/// <summary>V1.4D 协议充电桩客户端</summary>
[Scope("InstancePerDependency")]
public class V14DChargerClient
{
    #region 属性

    /// <summary>桩序列号/标识</summary>
    public string Sn { get; set; } = "";
    
    public IChannel Channel { get; set; }

    /// <summary>桩编码 (7位BCD)</summary>
    public string PileCode { get; set; } = "";
    
    /// <summary>
    /// 电池仓编号
    /// </summary>
    public string? BinNo { get; set; }

    /// <summary>设备类型编号</summary>
    public string EqmTypeNo { get; set; } = "";

    /// <summary>设备代码</summary>
    public string EqmCode { get; set; } = "";

    /// <summary>是否已登录</summary>
    public bool IsLoggedIn { get; set; } = false;

    /// <summary>枪状态 0=正常 1=故障</summary>
    public byte GunStatus { get; set; }

    /// <summary>桩状态</summary>
    public byte PileStatus { get; set; }

    /// <summary>最后心跳时间</summary>
    public DateTime? LastHeartbeat { get; set; }

    /// <summary>实时数据缓存</summary>
    public V14DRealTimeDataReq? RealTimeData { get; set; }

    /// <summary>BMS需求与输出缓存</summary>
    public V14DBmsDemandOutputReq? BmsDemandOutput { get; set; }

    /// <summary>BMS信息缓存</summary>
    public V14DBmsInfoReq? BmsInfo { get; set; }

    /// <summary>充电功率 (kW)</summary>
    public float ChargePower => RealTimeData?.ChargePower ?? 0;

    /// <summary>SOC</summary>
    public byte SOC => RealTimeData?.SOC ?? 0;


    public DateTime? HeartTime = DateTime.Now.AddSeconds(-30);
    
    /// <summary>
    /// 充电机是否连接
    /// </summary>
    public bool Connected
    {
        get
        {
            if (HeartTime == null)
                return false;

            if ((DateTime.Now - HeartTime.Value).TotalSeconds >= 30)
            {
                return false;
            }

            return true;
        }
        set
        {
            if (value)
            {
                HeartTime = DateTime.Now; // 更新 HeartTime
            }
            else
            {
                HeartTime = null; // 如果不需要保持连接状态，可以选择清空 HeartTime
            }
        } 
    }
    
    #endregion

    private ILog Log()
    {
        return LogManager.GetLogger("ChargerV14D" + Sn);
    }

    /// <summary>接收消息处理</summary>
    public void ReceiveMsgHandle(V14DRealTimeDataReq msg)
    {
        RealTimeData = msg;
        PileStatus = msg.Status;
    }

    #region 发送指令

    /// <summary>发送远程启动充电 (0x34)</summary>
    public Result<bool> SendRemoteStartCharge(string transactionSN, string pileCode, byte gun,
        string logicCardNo, string physicalCardNo, uint balance, string stopPassword = "")
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DRemoteStartChargeCmd
        {
            SeqNo = V14DUtils.NextSeqNo(),
            TransactionSN = transactionSN,
            PileCode = pileCode,
            Gun = gun,
            LogicCardNo = logicCardNo,
            PhysicalCardNo = physicalCardNo,
            Balance = balance,
            StopPassword = stopPassword
        };
        
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendRemoteStartCharge tsn={transactionSN}, pile={pileCode}, gun={gun}");
        return Result<bool>.Success();
    }

    /// <summary>发送远程停止充电 (0x36)</summary>
    public Result<bool> SendRemoteStopCharge(string pileCode, byte gun)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DRemoteStopChargeCmd(pileCode, gun) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendRemoteStopCharge pile={pileCode}, gun={gun}");
        return Result<bool>.Success();
    }

    /// <summary>发送读取实时数据 (0x12)</summary>
    public Result<bool> SendReadRealTimeData(string pileCode, byte gun)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DReadRealTimeDataCmd(pileCode, gun) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        return Result<bool>.Success();
    }

    /// <summary>发送参数设置 (0x52)</summary>
    public Result<bool> SendParamSet(string pileCode, byte gun, byte allowWork, byte maxPower)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DParamSetCmd(pileCode, gun, allowWork, maxPower) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendParamSet pile={pileCode}, gun={gun}, allowWork={allowWork}, maxPower={maxPower}");
        return Result<bool>.Success();
    }

    /// <summary>发送对时 (0x56)</summary>
    public Result<bool> SendTimeSync(string pileCode, DateTime dt)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DTimeSyncCmd(pileCode, dt) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendTimeSync pile={pileCode}, time={dt}");
        return Result<bool>.Success();
    }

    /// <summary>发送计费模型下发 (0x58)</summary>
    public Result<bool> SendBillingModelSet(V14DBillingModelSetCmd cmd)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        cmd.SeqNo = V14DUtils.NextSeqNo();
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendBillingModelSet pile={cmd.PileCode}, modelNo={cmd.ModelNo}");
        return Result<bool>.Success();
    }

    /// <summary>发送地锁控制 (0x62)</summary>
    public Result<bool> SendLockControl(string pileCode, byte gun, byte command)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DLockControlCmd { PileCode = pileCode, Gun = gun, Command = command, SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        return Result<bool>.Success();
    }

    /// <summary>发送电池在仓信号 (0x78)</summary>
    public Result<bool> SendBatteryInBinSignal(string pileCode, byte gun, byte inBin)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DBatteryInBinSignalCmd(pileCode, gun, inBin) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendBatteryInBinSignal pile={pileCode}, gun={gun}, inBin={inBin}");
        return Result<bool>.Success();
    }

    /// <summary>发送远程重启 (0x92)</summary>
    public Result<bool> SendRemoteRestart(string pileCode, byte executionControl = 0x01)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DRemoteRestartCmd(pileCode, executionControl) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        Log().Info($"SendRemoteRestart pile={pileCode}, exec={executionControl}");
        return Result<bool>.Success();
    }

    /// <summary>发送VIN查询 (0xAD)</summary>
    public Result<bool> SendVINQuery(string pileCode, byte gun)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        var cmd = new V14DVINQueryCmd(pileCode, gun) { SeqNo = V14DUtils.NextSeqNo() };
        Channel.WriteAndFlushAsync(cmd);
        return Result<bool>.Success();
    }

    /// <summary>发送确认启动充电 (0x32)</summary>
    public Result<bool> SendConfirmStartCharge(V14DConfirmStartChargeResp resp)
    {
        if (!Connected)
            return Result<bool>.Fail($"Charger {Sn} disconnect");

        resp.SeqNo = V14DUtils.NextSeqNo();
        Channel.WriteAndFlushAsync(resp);
        Log().Info($"SendConfirmStartCharge tsn={resp.TransactionSN}, result={resp.AuthResult}");
        return Result<bool>.Success();
    }

    #endregion

    #region 连接管理

    /// <summary>建立连接</summary>
    public bool Connect()
    {
        Log().Info($"V14D charger {Sn} connect succeed");
        return Connected;
    }

    /// <summary>设置通道属性</summary>
    public void SessionAttr(IChannel channel,string sn, string destAddr)
    {
        ChannelUtils.AddAttr(channel, V14DConst.ChargerSn, sn);
        //ChannelUtils.AddAttr(channel, V14DConst.EqmTypeNo, sn);
        //ChannelUtils.AddAttr(channel, V14DConst.EqmCode, sn);
    }

    #endregion
}
