using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.OutCharger.Req;

/// <summary>
/// 3.7.5 充电桩上送充电启动完成帧
/// </summary>
public class PileStartChargeCompleteReq : ASDU
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
    /// 成功标识  0:成功；1:失败
    /// </summary>
    [Property(16, 8)]
    public byte Result { get; set; }

    /// <summary>
    /// 失败原因
    /// 默认 0
    /// </summary>
    [Property(24, 8)]
    public byte FailReason { get; set; }

    /// <summary>
    /// BMS 与充电桩通信协议版本号
    /// </summary>
    [Property(32, 24)]
    public string? ConnProtocolVersion0 { get; set; }

    /// <summary>
    /// 充电桩与BMS 握手结果
    /// </summary>
    [Property(56, 8)]
    public byte HandshakeResult { get; set; }

    /// <summary>
    /// 电池类型
    /// </summary>
    [Property(64, 8)]
    public byte BatteryType { get; set; }

    /// <summary>
    /// 最高允许温度
    /// </summary>
    [Property(72, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public Int16 MaxAllowTemp { get; set; }

    /// <summary>
    /// BMS最高允许充电电压
    /// </summary>
    [Property(80, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float BmsMaxAllowVoltage { get; set; }

    /// <summary>
    /// 单体最高允许充电电压
    /// </summary>
    [Property(96, 16, PropertyReadConstant.Bit, 0.01, 2, 0)]
    public float SingleMaxAllowVoltage { get; set; }

    /// <summary>
    /// 最高允许充电电流
    /// </summary>
    [Property(112, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
    public float MaxAllowCurrent { get; set; }

    /// <summary>
    /// 整车动力蓄电池额定总电压
    /// </summary>
    [Property(128, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float VehiclePowerBatteryTotalVoltage { get; set; }

    /// <summary>
    /// 整车动力蓄电池当前电压
    /// </summary>
    [Property(144, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float VehiclePowerBatteryCurrentVoltage { get; set; }

    /// <summary>
    /// 整车动力蓄电池额定容量
    /// </summary>
    [Property(160, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float VehiclePowerBatteryRatedCapacity { get; set; }

    /// <summary>
    ///整车动力蓄电池标称容量
    /// </summary>
    [Property(176, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float VehiclePowerBatteryNormalCapacity { get; set; }

    /// <summary>
    ///充电机最高输出电压
    /// </summary>
    [Property(192, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float ChargerMaxOutputVoltage { get; set; }

    /// <summary>
    ///充电机最低输出电压
    /// </summary>
    [Property(208, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
    public float ChargerMinOutputVoltage { get; set; }

    /// <summary>
    ///充电机最大输出电流
    /// </summary>
    [Property(224, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
    public float ChargerMaxOutputCurrent { get; set; }

    /// <summary>
    ///充电机最小输出电流
    /// </summary>
    [Property(240, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
    public float ChargerMinOutputCurrent { get; set; }

    /// <summary>
    /// VIN
    /// </summary>
    [Property(256, 136)]
    public string Vin { get; set; }

    /// <summary>
    /// 整车动力蓄电 池荷电状态
    /// </summary>
    [Property(392, 8, PropertyReadConstant.Bit, 0.01, 2, 0)]
    public byte ChargeState { get; set; }
}