namespace Service.Charger.Msg.Http.Req;
/// <summary>
/// 9.2.1.1 云平台下发开始充电操作
/// </summary>
public class PileStartChargeHttpReq
{
    /// <summary>
    /// 换电站编码
    /// 换电站唯一码
    /// </summary>
    public string sn { get; set; }

    /// <summary>
    /// 充电订单号
    /// 云平台下发的充电订单编号，；当启动模式为本地主动启动（即插即充）时，该 值以 0 填充
    /// </summary>
    public string? con { get; set; }

    /// <summary>
    /// 充电枪编号
    /// 充电枪的唯一标识码
    /// </summary>
    public string pn { get; set; }

    /// <summary>
    /// 充电方式
    /// 0：自动（充满为止）；1：按电量；
    /// </summary>
    public int ct { get; set; }

    /// <summary>
    /// 充电参数
    /// 按充电方式判断，除0外 电量：单位 kWh，精确到 0.01 时间：单位 min，精确到 0.01 金额：单位 元，精确到 0.01
    /// </summary>
    public string? cp { get; set; }

    /// <summary>
    /// 启动类型
    /// 0：运营平台启动；1：APP 启动；2: 本地启动
    /// </summary>
    public int st { get; set; }
    
    
}