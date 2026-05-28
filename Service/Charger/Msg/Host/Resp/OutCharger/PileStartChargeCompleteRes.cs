using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp.OutCharger;

/// <summary>
/// 3.7.6 监控平台应答充电桩启动完成帧
/// </summary>
public class PileStartChargeCompleteRes : ASDU
{
    /// <summary>
    ///  记录类型
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
    /// 成功标识  0:成功；1:失败
    /// </summary>
    [Property(16, 8)]
    public byte Result { get; set; }

    /// <summary>
    /// 失败原因
    /// 0:成功1:交易流水号数据异常2:充电方式数据异常3:其他数据异常4:服务器异常5:服务器繁忙6:枪编号非法7 ：服务器判定启动完成帧超时0xFF:其他错误
    /// </summary>
    [Property(24, 8)]
    public byte FailReason { get; set; }
    
    public PileStartChargeCompleteRes(byte pn,byte result,byte failReason)
    {
        RecordType = 6;
        FrameTypeNo = 51;
        MsgBodyCount = 1;
        TransReason = 4;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
        Result = result;
        FailReason = failReason;
    }
}