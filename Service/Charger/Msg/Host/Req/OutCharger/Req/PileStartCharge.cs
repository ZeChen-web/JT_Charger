using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req.OutCharger.Req;

/// <summary>
/// 3.7.1 监控平台远程启动充电桩充电
/// </summary>
public class PileStartCharge : ASDU
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
    /// SOC 限制
    /// 百分比
    /// </summary>
    [Property(16, 8)]
    public byte SocValue { get; set; }

    /// <summary>
    /// 功率调节指令类型
    /// 默认 1 绝对功率值
    /// </summary>
    [Property(24, 8)]
    public byte ChangePowerCmdType { get; set; } = 1;

    /// <summary>
    /// 功率调节参数
    /// 0. 1kwh/位
    /// </summary>
    [Property(32, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public short ChangePower { get; set; }

    /// <summary>
    /// 充电流水号
    /// </summary>
    [Property(48, 256)]
    public string ChargeOrderNo { get; set; }

    public PileStartCharge(byte pn, byte socValue, byte changePowerCmdType, short changePower, string chargeOrderNo)
    {
        RecordType = 1;
        FrameTypeNo = 51;
        MsgBodyCount = 1;
        TransReason = 3;
        PublicAddr = 0;
        MsgBodyAddr = new byte[] { 0, 0, 0 };

        Pn = pn;
        SocValue = socValue;
        ChangePowerCmdType = changePowerCmdType;
        ChangePower = changePower;
        ChargeOrderNo = chargeOrderNo;
    }
}