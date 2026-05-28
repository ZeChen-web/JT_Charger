namespace Service.Charger.Msg.Http.Resp;

/// <summary>
/// 9.2.1.2 站控应答开始充电操作
/// </summary>
public class PileStartChargeHttpRes
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public string? rs { get; set; }

    /// <summary>
    /// 充电订单号
    /// </summary>
    public string? con { get; set; }

    /// <summary>
    /// 充电枪编号
    /// </summary>
    public string? pn { get; set; }

    /// <summary>
    /// 故障码
    /// </summary>
    public string? ec { get; set; }
}