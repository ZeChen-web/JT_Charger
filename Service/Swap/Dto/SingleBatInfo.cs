namespace Service.Swap.Dto;

public class SingleBatInfo
{
    /// <summary>
    /// 电池序列号
    /// </summary>
    public string bn { get; set; }

    /// <summary>
    /// 充电架 ID	按电池架的编号 A1，A2…
    /// </summary>
    public string sd { get; set; }

    /// <summary>
    /// 所在充电机序号	从 1 开始递增
    /// </summary>
    public int cno { get; set; }

    /// <summary>
    /// 是否在充电	0：未知  1：正在充电 2：未电池
    /// </summary>
    public int hc { get; set; }

    /// <summary>
    /// 电接头连接状态	0：未知  1：已经连接 2：未连接
    /// </summary>
    public int el { get; set; }

    /// <summary>
    /// 剩余能量	单位 0.1 kwh
    /// </summary>
    public float soe { get; set; }

    /// <summary>
    /// 当前 SOC	0-100  单位 0.1 ，没有充电填 0
    /// </summary>
    public float soc { get; set; }

    /// <summary>
    /// 当前 SOH	0-100  单位 0.1 ，没有充电填 0
    /// </summary>
    public float soh { get; set; }

    /// <summary>
    /// 最低单体电压	单位 0.01V
    /// </summary>
    public float lsv { get; set; }

    /// <summary>
    /// 最高单体电压	单位 0.01V
    /// </summary>
    public float hsv { get; set; }

    /// <summary>
    /// 最低单体温度	单位 0.1℃
    /// </summary>
    public float lst { get; set; }

    /// <summary>
    /// 最高单体温度	单位 0.1℃
    /// </summary>
    public float hst { get; set; }

    /// <summary>
    /// 单体电池号	从 1 开始递增
    /// </summary>
    public int sl { get; set; }

    /// <summary>
    /// 单体电压	每一节电芯的单体电压 单位 0.1V ，如果没有该节电芯的数据，填65535.0 无效值
    /// </summary>
    public float sv { get; set; }

    /// <summary>
    /// 单体温度	每一节电芯的单体温度 单位 0.1℃ ,如果没有该节电芯的数据，填65535.0 无效值
    /// </summary>
    public float st { get; set; }

    /// <summary>
    /// 更新时间	格式 ”yyyy-MM-dd HH:mm:ss ”
    /// </summary>
    public DateTime bt { get; set; }
}