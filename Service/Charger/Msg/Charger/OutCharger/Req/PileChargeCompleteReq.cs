using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.OutCharger.Req;

/// <summary>
/// 3.7.7 充电桩上送停止完成帧
/// </summary>
public class PileChargeCompleteReq : ASDU
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
    /// 成功标识
    /// </summary>
    [Property(16, 8)]
    public byte Result { get; set; }


    /// <summary>
    /// 失败原因
    /// </summary>
    [Property(24, 8)]
    public byte FailReason { get; set; }

    /// <summary>
    ///BMS 中止充电原因
    /// </summary>
    [Property(32, 8)]
    public byte SuspendingChargingReason { get; set; }

    /// <summary>
    ///BMS 充电故障原因
    /// </summary>
    [Property(40, 16)]
    public ushort CauseOfChargingFault { get; set; }

    /// <summary>
    ///BMS 中止错误原因
    /// </summary>
    [Property(56, 8)]
    public byte AbortErrorReason { get; set; }

    /// <summary>
    ///中止荷电状态 SOC(%)
    /// </summary>
    [Property(64, 8, PropertyReadConstant.Bit, 1, 0)]
    public float SuspendTheStateOfCharge { get; set; }

    /// <summary>
    ///动力蓄电池单体最低电压(V)
    /// </summary>
    [Property(72, 16, PropertyReadConstant.Bit, 0.01, 2)]
    public float MinimumVoltageOfTractionBattery { get; set; }

    /// <summary>
    ///动力蓄电池单体最高电压(V)
    /// </summary>
    [Property(88, 16, PropertyReadConstant.Bit, 0.01, 2)]
    public float MaximumVoltageOfTractionBattery { get; set; }

    /// <summary>
    ///动力蓄电池最低温度(ºC)
    /// </summary>
    [Property(104, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public float MinimumTemperatureOfTractionBattery { get; set; }

    /// <summary>
    ///动力蓄电池最高温度(ºC)
    /// </summary>
    [Property(112, 8, PropertyReadConstant.Bit, 1, 0, 50)]
    public float MaximumTemperatureOfTractionBattery { get; set; }

    /// <summary>
    ///接收SPN2560=0x00 的充电机辨识报文超时
    /// </summary>
    [Property(120, 2)]
    public byte XOOIdentificationMessageTimeout { get; set; }

    /// <summary>
    ///接收SPN2560=0xAA 的充电机辨识报文超时
    /// </summary>
    [Property(122, 2)]
    public byte XAAIdentificationMessageTimeout { get; set; }

    /// <summary>
    ///接收充电机的时间同步和充电机最大输出能力报文超时
    /// </summary>
    [Property(124, 2)]
    public byte TimeSyncAndMaxOutCapTimeout { get; set; }

    /// <summary>
    ///接收充电机完成充电准备报文超时
    /// </summary>
    [Property(126, 2)]
    public byte ReceiveFinishPrepareChargeTimeout { get; set; }

    /// <summary>
    ///接收充电机充电状态报文超时
    /// </summary>
    [Property(128, 2)]
    public byte TimeReceiveChargerStatusMessage { get; set; }

    /// <summary>
    ///接收充电机中止充电报文超时
    /// </summary>
    [Property(130, 2)]
    public byte TimeReceiveChargingSuspensionMessage { get; set; }

    /// <summary>
    ///接收充电机充电统计报文超时
    /// </summary>
    [Property(132, 2)]
    public byte TimeReceiveChargingStatisticsMessageOfCharger { get; set; }

    /// <summary>
    ///BMS 检测到的其他错误
    /// </summary>
    [Property(134, 6)]
    public byte OtherErrorsDetectedByBms6 { get; set; }
    
    /// <summary>
    ///接收BMS和车辆的辨识报文超时
    /// </summary>
    [Property(140, 2)]
    public byte TimeoutReceivingIdentificationMessageBms { get; set; }

    /// <summary>
    ///接收电池充电参数报文超时
    /// </summary>
    [Property(142, 2)]
    public byte TimeoutReceivingBatteryChargingParameterMessage { get; set; }

    /// <summary>
    ///接收 BMS完成充电准备报文超时
    /// </summary>
    [Property(144, 2)]
    public byte TimeoutReceivingBmsChargingPreparationMessage { get; set; }

    /// <summary>
    ///接收电池充电要求报文超时
    /// </summary>
    [Property(146, 2)]
    public byte TimeoutReceivingBatteryChargingMessage { get; set; }

    /// <summary>
    ///接收电池充电总状态报文超时
    /// </summary>
    [Property(148, 2)]
    public byte TimeoutReceivingBatteryChargingTotalStatusMessage { get; set; }

    /// <summary>
    ///接收BMS中止充电报文超时
    /// </summary>
    [Property(150, 2)]
    public byte TimeoutReceivingBmsChargingSuspensionMessage { get; set; }

    /// <summary>
    ///接收BMS充电统计报文超时
    /// </summary>
    [Property(152, 2)]
    public byte TimeoutReceivingBmsChargingStatisticsMessage { get; set; }

    /// <summary>
    ///充电机检测到的其他错误
    /// </summary>
    [Property(154, 6)]
    public byte OtherErrorsDetectedByTheCharger { get; set; }
}