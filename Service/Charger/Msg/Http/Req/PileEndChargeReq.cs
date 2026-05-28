namespace Service.Charger.Msg.Http.Req;
/// <summary>
/// 9.2.1.3 站控上报充电枪充电结束事件
/// </summary>
public class PileEndChargeReq
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
    /// 充电方式
    /// 0：自动（充满为止）； 1：按电量； 2：按时间； 3：按金额；
    /// </summary>
    public int ct { get; set; }

    /// <summary>
    /// 充电参数
    /// 按充电方式判断，除0外 电量：单位 kWh，精确到 0.01 时间：单位 min，精确到 0.01 金额：单位 元，精确到 0.01
    /// </summary>
    public string cp { get; set; }

    /// <summary>
    /// 启动类型
    /// 0：运营平台启动；1：APP 启动；2: 本地启动
    /// </summary>
    public int st { get; set; }

    /// <summary>
    /// 本次充电订单的开始时间 格式 yyyy-MM-dd HH:mm:ss
    /// </summary>
    public DateTime cst { get; set; }

    /// <summary>
    /// 本次充电订单的开始时间 格式 yyyy-MM-dd HH:mm:ss
    /// </summary>
    public DateTime? cet { get; set; }

    /// <summary>
    /// 充电电量
    /// 至少保留两位有效小数
    /// </summary>
    public float ceq { get; set; }

    /// <summary>
    /// Desc:充电开始soc
    /// Default:
    /// Nullable:True
    /// </summary>
    public int? cssoc { get; set; }

    /// <summary>
    /// Desc:充电结束soc
    /// Default:
    /// Nullable:True
    /// </summary>
    public int? cesoc { get; set; }

    /*/// <summary>
    /// 计费时间段个数
    /// </summary>
    public int? ctn { get; set; }

    /// <summary>
    /// 计费时间段列表
    /// </summary>
    public List<PeriodReq> ctl { get; set; }*/

    /// <summary>
    /// 尖时段电量
    /// </summary>
    public float tp { get; set; }
    /// <summary>
    /// 峰时段电量
    /// </summary>
    public float pp { get; set; }
    /// <summary>
    /// 平时段电量
    /// </summary>
    public float fp { get; set; }
    /// <summary>
    /// 谷时段电量
    /// </summary>
    public float vp { get; set; }
    
    /// <summary>
    /// 充电车辆车架号
    /// </summary>
    public string cvin { get; set; }
}