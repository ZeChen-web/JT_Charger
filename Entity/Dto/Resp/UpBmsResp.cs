namespace Entity.Dto.Resp;

/// <summary>
/// 电池包基本状态
/// </summary>
public class UpBmsResp
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public byte RecordType { get; set; }

    /// <summary>
    ///PGN码
    /// </summary>
    public string Pgn { get; set; }

    /// <summary>
    /// 报警级别
    /// 0：正常
    /// 1:1 级报警 3:3 级报警 5:5 级报警 其余保留
    /// </summary>
    public short AlarmLevel { get; set; }

    /// <summary>
    /// 电池箱所在位置编号   分辨率：1/位，偏移量：0，数值范围：1~250
    /// </summary>
    public short BatteryBoxLocationNumber { get; set; }

    /// <summary>
    /// 电池箱能输出的最大电流值    分辨率：0.05A/位，偏移量：-1600A，数值范围：-1600A ~ 1612.75A
    /// </summary>
    public short BatteryBoxMaximumCurrentOutput { get; set; }

    /// <summary>
    /// 电池箱能承受最大反馈电流值   分辨率：0.05A/位，偏移量：-1600A，数值范围：-1600A~1612.75A
    /// </summary>
    public short BatteryMaximumFeedback { get; set; }

    /// <summary>
    /// 电池箱风扇状态 0：关闭1：开启 2：不可用 3：不可用
    /// </summary>
    public byte BatteryBoxFanStatus { get; set; }

    /// <summary>
    /// 加热装置状态  0：关闭1：开启 2：不可用 3：不可用
    /// </summary>
    public byte HeaterCondition { get; set; }

    /// <summary>
    /// 均衡状态    0：关闭1：开启 2：不可用 3：不可用
    /// </summary>
    public byte EquilibriumState { get; set; }

    /// <summary>
    /// 高压互锁状态  0 断开 1 连接
    /// </summary>
    public byte HighVoltageInterlockState { get; set; }

    /// <summary>
    /// 保留
    /// </summary>
    public short Reserve { get; set; }
}