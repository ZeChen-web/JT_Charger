using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp.OutCharger;
/// <summary>
/// 3.7.8 监控平台应答充电桩停止完成帧
/// </summary>
public class PileChargeCompleteRes : ASDU
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
    
    public PileChargeCompleteRes(byte pn,byte result)
    {
        RecordType = 8;
        FrameTypeNo = 51;
        MsgBodyCount = 1;
        TransReason = 4;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
        Result = result;
    }
}