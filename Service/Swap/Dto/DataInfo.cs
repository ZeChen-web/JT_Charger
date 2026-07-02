namespace Service.Swap.Dto;

public class DataInfo
{
    public string bn { get; set; }
    /// <summary>
    /// 设备编号	设备编号，唯一的
    /// </summary>
    public string en { get; set; }

    /// <summary>
    /// 充电架 ID	按电池架的编号 A1，A2…
    /// </summary>
    public string sd { get; set; }

    /// <summary>
    /// 充电机最大允许 输出功率	单位 0.1kw
    /// </summary>
    public float mtp { get; set; } = 150;

    /// <summary>
    /// 充电机最大允 许充电速率	单位 0.1C
    /// </summary>
    public float mcr { get; set; }

    /// <summary>
    /// 电池架上是否有 电池	0：未知  1：有电池 2：无电池
    /// </summary>
    public int hb { get; set; }

    /// <summary>
    /// 电接头连接状态	0：未知  1：已经连接 2：未连接
    /// </summary>
    public int el { get; set; }

    /// <summary>
    /// 充电机序号	从 1 开始递增
    /// </summary>
    public int cno { get; set; }

    /// <summary>
    /// 充电机的工作状态	00H：待机 01H：工作02H：工作完成03H：充电暂停
    /// </summary>
    public int cs { get; set; }

    /// <summary>
    /// 故障状态	00H：无故障  01H：有故障
    /// </summary>
    public int fs { get; set; }

    ///告警状态	00H：无告警.01H：有告警
    public int @as { get; set; }

    /// <summary>
    /// 故障码	参考充电机的故障码定义
    /// </summary>
    public int fc { get; set; }

    /// <summary>
    /// 单体温度	每一节电芯的单体温度 单位 0.1℃ ,如果没有该节电芯的数据，填65535.0 无效值
    /// </summary>
    public float st { get; set; }

    /// <summary>
    /// 已经充电时间	单位 分钟
    /// </summary>
    public int ct { get; set; }

    /// <summary>
    /// 充电开始 SOC	0-100  单位 0.1 ，没有充电填 0
    /// </summary>
    public int ssoc { get; set; }

    /// <summary>
    /// 当前 SOC	单位 0.1
    /// </summary>
    public int csoc { get; set; }

    /// <summary>
    /// 充电开始 SOE	单位 0.1kwh，没有充电填 0
    /// </summary>
    public float ssoe { get; set; }

    /// <summary>
    /// 当前 SOE	单位 0.1kwh
    /// </summary>
    public float csoe { get; set; }

    /// <summary>
    /// 当前充电电压	单位 0.1V，没有充电填 0
    /// </summary>
    public float cvot { get; set; }

    /// <summary>
    /// 当前充电电流	单位 0.1A，没有充电填 0
    /// </summary>
    public float ccur { get; set; }

    /// <summary>
    /// BMS 需求电压	单位 0.1V，没有充电填 0
    /// </summary>
    public float nvot { get; set; }

    /// <summary>
    /// BMS 需求电流	单位 0.1A，没有充电填 0
    /// </summary>
    public float ncur { get; set; }

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
    /// 水冷状态	0：未知 1：开启 2：关闭 0xFF： 无水冷设备
    /// </summary>
    public byte ws { get; set; } = 0xff;

    /// <summary>
    /// 进水口温度	单位 0.1℃ , 没有水冷设备填 0xFF
    /// </summary>
    public float it { get; set; } = 0xff;

    /// <summary>
    /// 出水口温度	单位 0.1℃ , 没有水冷设备填 0xFF
    /// </summary>
    public float ot { get; set; } = 0xff;

    /// <summary>
    /// 电池度数
    /// </summary>
    public decimal bc { get; set; } = 0xff;

    /// <summary>
    /// 更新时间	格式 ”yyyy-MM-dd HH:mm:ss ”
    /// </summary>
    public DateTime bt { get; set; }
}