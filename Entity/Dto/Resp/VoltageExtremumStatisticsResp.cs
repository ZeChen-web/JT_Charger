namespace Entity.Dto.Resp;

/// <summary>
/// 电压温度极值统计
/// </summary>
public class VoltageExtremumStatisticsResp
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public byte RecordType { get; set; }

    /// <summary>
    /// PGN 码
    /// </summary>
    public byte Pgn1 { get; set; }
    public byte Pgn2 { get; set; }
    public byte Pgn3 { get; set; }

    /// <summary>
    /// 单体蓄电池或蓄电池模块最高电压
    /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
    /// </summary>
    public float MaximumVoltage { get; set; }

    /// <summary>
    /// 最高电压单体蓄电池或蓄电池模块的编号
    /// 分辨率：1/位，偏移量：0，数值范围：
    /// </summary>
    public float MaximumVoltageNum { get; set; }

    /// <summary>
    /// 单体蓄电池或蓄电池模块最低电压
    /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
    /// </summary>
    public float MinimumVoltage { get; set; }

    /// <summary>
    /// 最低电压单体蓄电池或蓄电池模块的编号
    /// 分辨率：1/位，偏移量：0，数值范围：1~250
    /// </summary>
    public float MinimumVoltageNum { get; set; }

    /// <summary>
    /// 单体平均电压
    /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
    /// </summary>
    public float CellAverageVoltage { get; set; }
}