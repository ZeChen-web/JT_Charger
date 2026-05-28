namespace Entity.Dto.Resp;

/// <summary>
/// 电池包编码和SOC数据
/// </summary>
public class VoltageCurrentSocResp
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
    /// 电压测量值  分辨率：0.1V/位，偏移量：0V，数值范围：0V ~750V
    /// </summary>
    public float Voltage { get; set; }
    /// <summary>
    /// 电流测量值 分辨率：0.05A/位，偏移量：-1600A，数值范围：-1600A~1612.75A
    /// </summary>
    public float Current { get; set; }
    /// <summary>
    /// 当前SOC 分辨率：0.1%/位，偏移量：0%，数值范围 0%~100%
    /// </summary>
    public float SOC { get; set; }
    /// <summary>
    /// 当前SOH  分辨率：1%/位，偏移量：0%，数值范围 0%~100%
    /// </summary>
    public float SOH { get; set; }
}