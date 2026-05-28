using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req.OutCharger.Req;
/// <summary>
/// 3.7.9 监控平台发送充电桩功率调节指令
/// </summary>
public class PileAdjustPower: ASDU
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
    ///期望运行 功率
    /// </summary>
    [Property(16, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public float ExpectPower { get; set; }
    
    
    public PileAdjustPower(byte pn,float expectPower)
    {
        RecordType = 9;
        FrameTypeNo = 51;
        MsgBodyCount = 1;
        TransReason = 3;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
        ExpectPower = expectPower;
    }
}