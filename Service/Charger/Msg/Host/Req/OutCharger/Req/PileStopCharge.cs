using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req.OutCharger.Req;

/// <summary>
/// 3.6.2.1 监控平台下发（PGN）查询电池信息(PGN:0x00F82C)
/// </summary>
public class PileStopCharge : ASDU
{
    /// <summary>
    /// 记录类型
    /// </summary>
    [Property(0, 8)]
    public byte RecordType { get; set; }

    /// <summary>
    /// 充电枪ID号
    /// 0x01：充电枪1；0x02：充电枪2；0x03：双枪充电;(0x00&0xFF无效)
    /// </summary>
    [Property(8, 8)]
    public byte Pn { get; set; }

    /// <summary>
    /// 停止原因
    /// </summary>
    [Property(16, 8)]
    public byte StopReason { get; set; }
    
    public PileStopCharge(byte pn,byte stopReason)
    {
        RecordType = 3;
        FrameTypeNo = 51;
        MsgBodyCount = 1;
        TransReason = 3;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
        
        StopReason = stopReason;
    }
}