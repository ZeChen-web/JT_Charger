using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.OutCharger.Resp;

/// <summary>
/// 3.7.2 充电桩响应远程启动充电
/// </summary>
public class PileStartChargeRes : ASDU
{
    /// <summary>
    /// 记录类型
    /// </summary>
    [Property(0, 8)]
    public byte RecordType { get; set; }

    /// <summary>
    /// 充电枪ID
    /// 0x01：充电枪1；0x02：充电枪2；0x03：双枪充电;(0x00&0xFF无效)
    /// </summary>
    [Property(8, 8)]
    public byte Pn { get; set; }

    /// <summary>
    /// 启动结果
    /// 0 成功 1 失败
    /// </summary>
    [Property(16, 8)]
    public byte Result { get; set; }

    /// <summary>
    /// 失败原因
    /// 默认 0
    /// </summary>
    [Property(24, 8)]
    public byte FailReason { get; set; }
}