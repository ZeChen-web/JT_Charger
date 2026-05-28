namespace Entity.Dto.Resp;

/// <summary>
/// 遥测数据
/// </summary>
public class UploadTelemetryDataResp
{
    /// <summary>
    /// 当前 SOC
    /// </summary>
    public byte CurrentSoc { get; set; }

    /// <summary>
    /// 最高蓄电池温度
    /// </summary>
    public Int16 MaxBatteryTemp { get; set; }

    /// <summary>
    /// 最高温度检测点编号
    /// </summary>
    public ushort MaxTempDetectionPointNo { get; set; }

    /// <summary>
    /// 最低蓄电池温度数据分辨率：1ºC/位，-50 ºC 偏移量；数据范围：-50ºC ~+200ºC；
    /// </summary>
    public Int16 MinBatteryTemp { get; set; }

    /// <summary>
    /// 最低温度检测点编号
    /// </summary>
    public ushort MinTempDetectionPointNo { get; set; }

    /// <summary>
    /// 单体电池最高电压
    /// </summary>
    public float SingleBatteryMaxVoltage { get; set; }

    /// <summary>
    /// 单体电池最低压
    /// </summary>
    public float SingleBatteryMinVoltage { get; set; }

    /// <summary>
    /// 充电机环境温度
    /// </summary>
    public Int16 ChargerEnvTemp { get; set; }

    /// <summary>
    /// 充电导引电压
    /// </summary>
    public float ChargingPilotVoltage { get; set; }

    /// <summary>
    /// BMS 需求电压
    /// </summary>
    public float BmsNeedVoltage { get; set; }

    /// <summary>
    /// BMS 需求电流
    /// </summary>
    public float BmsNeedCurrent { get; set; }

    /// <summary>
    /// 充电模式 01H:恒压充电、02H恒流充电
    /// </summary>
    public byte ChargeMode { get; set; }

    /// <summary>
    /// BMS 充电电压测量值
    /// </summary>
    public float BmsChargingVoltage { get; set; }

    /// <summary>
    /// BMS 充电电流测量值
    /// </summary>
    public float BmsChargingCurrent { get; set; }

    /// <summary>
    /// 估算剩余充电时间
    /// </summary>
    public ushort EstimatedRemainingTime { get; set; }

    /// <summary>
    /// 充电接口温度探头 1
    /// </summary>
    public Int16 ChargingInterfaceDetectionOneTemp { get; set; }

    /// <summary>
    /// 充电接口温度探头 2
    /// </summary>
    public Int16 ChargingInterfaceDetectionTwoTemp { get; set; }

    /// <summary>
    /// 充电接口温度探头 3
    /// </summary>
    public Int16 ChargingInterfaceDetectionTheTemp { get; set; }

    /// <summary>
    /// 充电接口温度探头 4
    /// </summary>
    public Int16 ChargingInterfaceDetectionFourTemp { get; set; }

    /// <summary>
    /// 直流电表当前电量
    /// </summary>
    public float DcMeterCurrentPower { get; set; }

    /// <summary>
    /// 充电电压（直流电表电压）
    /// </summary>
    public float DcMeterVoltage { get; set; }

    /// <summary>
    /// 充电电流（直流电表电流）
    /// </summary>
    public float DcMeterCurrent { get; set; }

    /// <summary>
    /// 高压采集电压
    /// </summary>
    public float HighVoltageAcquisitionVoltage { get; set; }

    /// <summary>
    /// 高压采集电流
    /// </summary>
    public float HighVoltageAcquisitionCurrent { get; set; }

    /// <summary>
    /// 桩内部温度
    /// </summary>
    public byte ChargerInsideTemp { get; set; }

    /// <summary>
    /// 本次充电时间
    /// </summary>
    public ushort ChargingTime { get; set; }

    /// <summary>
    /// 模块进风口温度
    /// </summary>
    public byte ModuleOneAirInletTemp { get; set; }

    /// <summary>
    /// 模块出风口温度
    /// </summary>
    public byte ModuleTwoAirInletTemp { get; set; }

    /// <summary>
    /// 充电模式 0：站内充电 1：站外充电
    /// </summary>
    public byte ChargeModel { get; set; }

    /// <summary>
    /// 充电启动方式 1：站控启动 2：本地充电
    /// </summary>

    public byte ChargingStartMethod { get; set; }

    /// <summary>
    /// 交流电表当前电量
    /// </summary>
    public float ACMeterCurrentBatteryValue { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string ChargerNo { get; set; }
}