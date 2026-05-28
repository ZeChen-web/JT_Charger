using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp.OutCharger;
/// <summary>
/// 3.7.14 主动上送充电记录响应
/// </summary>
public class PileUploadChargeRecordRes : ASDU
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
    
    public PileUploadChargeRecordRes(byte pn)
    {
        FrameTypeNo = 51;
        RecordType = 14;
        MsgBodyCount = 1;
        TransReason = 4;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
    }
}