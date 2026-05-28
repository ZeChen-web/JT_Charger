namespace Service.Charger.Msg.Http.Req;

/// <summary>
/// 9.2.17  云端下发充电枪停止充电
/// </summary>
public class PileStopChargeHttpReq
{
    /// <summary>
    /// 换电站编码
    /// 换电站唯一码
    /// </summary>
    public string sn { get; set; }


    /// <summary>
    /// 充电枪编号
    /// 充电枪的唯一标识码
    /// </summary>
    public string pn { get; set; }
}