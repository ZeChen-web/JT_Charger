namespace Service.Charger.Msg.Http.Req;

/// <summary>
/// 9.2.1.7 站控上报充电枪充电遥测数据
/// </summary>
public class PileChargeRealtimeReq
{
    /// <summary>
    /// 换电站编码
    /// </summary>
    public string sn { get; set; }

    /// <summary>
    /// 充电订单号
    /// 云平台下发的充电订单编号，；当启动模式为本地主动启动（即插即充）时，该 值以 0 填充
    /// </summary>
    public string con { get; set; }

    /// <summary>
    /// 充电流水号
    /// 充电记录唯一编码
    /// </summary>
    public string cosn { get; set; }

    /// <summary>
    /// Desc:充电枪编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string? pn { get; set; }

    /// <summary>
    /// 需求电压
    /// </summary>
    public float rv { get; set; }

    /// <summary>
    /// 需求电流
    /// </summary>
    public float re { get; set; }


    /// <summary>
    /// 充电模式 01H:恒压充电、02H恒流充电
    /// </summary>
    public int cm { get; set; }

    /// <summary>
    /// 充电电压测量值
    /// </summary>
    public float cdv { get; set; }

    /// <summary>
    /// 充电电流测量值
    /// </summary>
    public float cde { get; set; }

    /// <summary>
    /// 当前荷电状态 S0C （%）
    /// </summary>
    public float soc { get; set; }

    /// <summary>
    /// 估算剩余充电时间
    /// </summary>
    public int tr { get; set; }

    /// <summary>
    /// 电桩电压输岀值
    /// </summary>
    public float pov { get; set; }

    /// <summary>
    /// 电桩电流输岀值
    /// </summary>
    public float poe { get; set; }

    /// <summary>
    /// 累计充电时间
    /// </summary>
    public int tct { get; set; }

    /// <summary>
    /// 最高单体动力 蓄电池电压所 在编号
    /// </summary>
    public int? hbvn { get; set; }

    /// <summary>
    /// 最高单体动力 蓄电池电压
    /// </summary>
    public float? hbv { get; set; }

    /// <summary>
    /// 最高温度检测 点编号
    /// </summary>
    public int hbtn { get; set; }

    /// <summary>
    /// 最高动力蓄电池温度
    /// </summary>
    public int hbt { get; set; }

    /// <summary>
    /// 最低单体动力 蓄电池电压所在编号
    /// </summary>
    public int lbvn { get; set; }

    /// <summary>
    /// 最低单体动力蓄电池电压
    /// </summary>
    public float lbv { get; set; }

    /// <summary>
    /// 最低动力蓄电池温度检测点 编号
    /// </summary>
    public int lbtn { get; set; }

    /// <summary>
    /// 最低动力蓄电 池温度
    /// </summary>
    public int lbt { get; set; }

    /// <summary>
    /// 单体动力蓄电 池电压过高 / 过低告警
    /// </summary>
    public int hlbva { get; set; }

    /// <summary>
    /// 整车动力蓄电 池荷电状态 S0C 过高/过低 告警
    /// </summary>
    public int hlbsa { get; set; }

    /// <summary>
    /// 动力蓄电池充电过流告警
    /// </summary>
    public int baa { get; set; }

    /// <summary>
    /// 动力蓄电池温度过高告警
    /// </summary>
    public int bta { get; set; }

    /// <summary>
    /// 动力蓄电池绝缘状态告警
    /// </summary>
    public int bia { get; set; }

    /// <summary>
    /// 动力蓄电池组 输岀连接器连 接状态告警
    /// </summary>
    public int bca { get; set; }
}