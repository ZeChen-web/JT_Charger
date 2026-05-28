namespace Entity.Dto.Resp;

public class BatteryStatusInfoResp
{
    /// <summary>
    /// 电池总数
    /// </summary>
    public  int btyTotalCount { get;set; }
    /// <summary>
    /// 正在充电数量
    /// </summary>
    public  int chargingCount  { get;set; }
    /// <summary>
    /// 满足换电数量
    /// </summary>
    public  int canSwapCount  { get;set; }
}