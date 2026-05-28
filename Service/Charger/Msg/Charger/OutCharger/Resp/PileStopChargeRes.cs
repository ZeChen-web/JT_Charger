using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.OutCharger.Resp;

/// <summary>
/// 3.7.4 充电桩响应远程停止充电
/// </summary>
public class PileStopChargeRes : ASDU
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
    public byte pn { get; set; }

    /// <summary>
    /// 启动结果 0 成功 1 设备已停机 0xFF 其他
    /// </summary>
    [Property(16, 8)]
    public byte rs { get; set; }
}