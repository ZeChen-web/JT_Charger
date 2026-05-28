namespace Service.Charger.Msg.Http.Req;
/// <summary>
/// 9.2.1.5 站控上报充电枪实时数据上报
/// </summary>
public class PileRealtimeReq
{
    /// <summary>
    /// 换电站编码
    /// </summary>
    public string sn { get; set; }


    /// <summary>
    /// Desc:充电枪编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string? pn { get; set; }

    /// <summary>
    /// 充电枪状态
    /// </summary>
    public int ps { get; set; }

    /// <summary>
    /// 插枪状态
    /// </summary>
    public int con { get; set; }


    /// <summary>
    /// 单枪输出电压
    /// </summary>
    public float pov { get; set; }

    /// <summary>
    /// 单枪输出电流
    /// </summary>
    public float poe { get; set; }

    /// <summary>
    /// 单枪故障代码
    /// </summary>
    public int ec { get; set; }
}