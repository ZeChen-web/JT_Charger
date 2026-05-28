using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.OutCharger.Req;

/// <summary>
/// 3.7.12 充电桩遥测数据上报
/// </summary>
public class PileUploadTelemetry : ASDU
{
    /// <summary>
    ///  记录类型
    /// </summary>
    [Property(0, 8)]
    public byte RecordType { get; set; }

    /// <summary>
    /// 充电枪ID号
    /// 0x01：充电枪1；0x02：充电枪2；0x03：双枪充电;(0x00&0xFF无效)
    /// </summary>
    [Property(8, 8)]
    public byte Pn { get; set; }

    /// <summary>
    /// 当前 SOC
    /// </summary>
    [Property(16, 8)]
    public byte CurrentSoc { get; set; }

    /// <summary>
    /// 最高蓄电池温度
    /// </summary>
    [Property(24, 16, offset: 50)]
    public Int16 MaxBatteryTemp { get; set; }

    /// <summary>
    /// 最高温度检测点编号
    /// </summary>
    [Property(40, 16)]
    public ushort MaxTempDetectionPointNo { get; set; }

    /// <summary>
    /// 最低蓄电池温度数据分辨率：1ºC/位，-50 ºC 偏移量；数据范围：-50ºC ~+200ºC；
    /// </summary>
    [Property(56, 16, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 MinBatteryTemp { get; set; }

    /// <summary>
    /// 最低温度检测点编号
    /// </summary>
    [Property(72, 16)]
    public ushort MinTempDetectionPointNo { get; set; }

    /// <summary>
    /// 单体电池最高电压
    /// </summary>
    [Property(88, 16, PropertyReadConstant.Bit, 0.01, 2, 0)]
    public float SingleBatteryMaxVoltage { get; set; }

    /// <summary>
    /// 单体电池最低电压
    /// </summary>
    [Property(104, 16, PropertyReadConstant.Bit, 0.01, 2, 0)]
    public float SingleBatteryMinVoltage { get; set; }

    /// <summary>
    /// 充电机环境温度
    /// </summary>
    [Property(120, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 ChargerEnvTemp { get; set; }

    /// <summary>
    /// 充电导引电压
    /// </summary>
    [Property(128, 16, PropertyReadConstant.Bit, 0.01, 2, 0)]
    public float ChargingPilotVoltage { get; set; }

    /// <summary>
    /// BMS 需求电压
    /// </summary>
    [Property(144, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float BmsNeedVoltage { get; set; }

    /// <summary>
    /// BMS 需求电流
    /// </summary>
    [Property(160, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
    public float BmsNeedCurrent { get; set; }

    /// <summary>
    /// 充电模式 01H:恒压充电、02H恒流充电
    /// </summary>
    [Property(176, 8)]
    public byte ChargeMode { get; set; }


    /// <summary>
    /// BMS 充电电压测量值
    /// </summary>
    [Property(184, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float BmsChargingVoltage { get; set; }


    /// <summary>
    /// BMS 充电电流测量值
    /// </summary>
    [Property(200, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
    public float BmsChargingCurrent { get; set; }

    /// <summary>
    /// 估算剩余充电时间
    /// </summary>
    [Property(216, 16, PropertyReadConstant.Bit, 1, 0, 0)]
    public ushort EstimatedRemainingTime { get; set; }

    /// <summary>
    /// 充电接口温度探头 1
    /// </summary>
    [Property(232, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 ChargingInterfaceDetectionOneTemp { get; set; }

    /// <summary>
    /// 充电接口温度探头 2
    /// </summary>
    [Property(240, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 ChargingInterfaceDetectionTwoTemp { get; set; }

    /// <summary>
    /// 充电接口温度探头 3
    /// </summary>
    [Property(248, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 ChargingInterfaceDetectionTheTemp { get; set; }
    /// <summary>
    /// 充电接口温度探头 4
    /// </summary>
    [Property(248+8, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 ChargingInterfaceDetectionFourTemp { get; set; }

    /// <summary>
    /// 直流电表当前电量
    /// </summary>
    [Property(256+8, 32, PropertyReadConstant.Bit, 0.01, 2)]
    public float DcMeterCurrentPower { get; set; }

    /// <summary>
    /// 充电电压（直流电表电压）
    /// </summary>
    [Property(288+8, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public float DcMeterVoltage { get; set; }

    /// <summary>
    /// 充电电流（直流电表电流）
    /// </summary>
    [Property(304+8, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public float DcMeterCurrent { get; set; }

    /// <summary>
    /// 高压采集电压
    /// </summary>
    [Property(320+8, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public float HighVoltageAcquisitionVoltage { get; set; }

    /// <summary>
    /// 高压采集电流
    /// </summary>
    [Property(336+8, 16, PropertyReadConstant.Bit, 0.1, 1)]
    public float HighVoltageAcquisitionCurrent { get; set; }

    /// <summary>
    /// 桩内部温度
    /// </summary>
    [Property(352+8, 8, PropertyReadConstant.Bit, 1, 0)]
    public byte ChargerInsideTemp { get; set; }

    /// <summary>
    /// 本次充电时间
    /// </summary>
    [Property(360+8, 16)]
    public ushort ChargingTime { get; set; }

    /// <summary>
    /// 模块进风口温度
    /// </summary>
    [Property(376+8, 8)]
    public byte ModuleOneAirInletTemp { get; set; }

    /// <summary>
    /// 模块出风口温度
    /// </summary>
    [Property(384+8, 8)]
    public byte ModuleTwoAirInletTemp { get; set; }

    /// <summary>
    /// 充电模式 0：站内充电 1：站外充电
    /// </summary>
    [Property(392+8, 8)]
    public byte ChargeModel { get; set; }

    /// <summary>
    /// 充电启动方式 1：站控启动 2：本地充电
    /// </summary>
    [Property(400+8, 8)]
    public byte ChargingStartMethod { get; set; }


    /// <summary>
    /// 交流电表当前电量
    /// </summary>
    [Property(408+8, 32, PropertyReadConstant.Bit, 0.01, 2)]
    public float ACMeterCurrentBatteryValue { get; set; }
}