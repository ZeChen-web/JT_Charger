namespace Service.Charger.Msg.Http.Req;
/// <summary>
/// 上报充电桩结束的计费时间段列表
/// </summary>
public class PeriodReq
{
    /// <summary>
    /// 时段 开始时间
    /// </summary>
    public string? StartTimeMinute { get; set; }
    
    /// <summary>
    /// 时段 电量
    /// </summary>
    public float ChargingPowerOfTime { get; set; }
    
    /// <summary>
    /// 时段 标识
    /// </summary>
    public byte FlagOfTime { get; set; }
}