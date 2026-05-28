namespace Service.ChargerV14D.Common;

/// <summary>直流充电桩停止原因代码 (附录13.2.1)</summary>
public enum DcStopReason : byte
{
    // === 充电桩故障产生的停机 ===
    None = 0,
    EmergencyStop = 1,             // 急停按钮动作故障
    DoorSwitch = 2,                // 门禁开关故障
    WaterImmersion = 3,            // 水浸开关故障
    Fuse = 4,                      // 熔断器熔断故障
    GunELock = 5,                  // 充电枪电子锁故障
    GunDisconnect = 6,             // 充电枪连接故障
    Insulation = 7,                // 绝缘检测故障
    InsulationShort = 8,           // 绝缘检测短路故障
    K1Refuse = 9,                  // 接触器K1拒动故障
    K1Adhesion = 10,               // 接触器K1粘连故障
    K2Refuse = 11,                 // 接触器K2拒动故障
    K2Adhesion = 12,               // 接触器K2粘连故障
    GunDcPlusOverTemp = 13,        // 枪DC+过温故障
    GunDcMinusOverTemp = 14,       // 枪DC-过温故障
    DischargeCircuit = 15,         // 泄放回路故障
    AcContactor = 16,              // 交流接触器故障
    BusContactor = 17,             // 母联接触器故障
    ModuleFault = 18,              // 模块故障
    AuxPower = 19,                 // 辅助电源故障
    Arrester = 20,                 // 避雷器故障
    PileOverTemp = 21,             // 充电桩过温故障
    Controller = 22,               // 控制器故障(副CPU通讯故障)
    ModuleComm = 23,               // 模块通信故障
    AcInput = 24,                  // 交流输入故障
    MeterComm = 25,                // 电表通讯故障
    ScreenComm = 26,               // 串口屏通讯故障
    PCUFault = 27,                 // PCU故障

    // === 整流柜故障产生的停机 ===
    RectifierModuleComm = 33,      // 模块通讯故障
    RectifierModuleFault = 34,     // 模块故障
    RectifierAcInput = 35,         // 交流输入故障
    RectifierPCUComm = 36,         // PCU通讯故障
    RectifierNoModule = 37,        // 无可用模块
    RectifierAcContactor = 38,     // 交流接触器故障
    RectifierEmergencyStop = 39,   // 急停故障
    RectifierDoor = 40,            // 门禁故障
    RectifierArrester = 41,        // 避雷器故障
    RectifierWater = 42,           // 水浸故障
    RectifierOverTemp = 43,        // 机柜过温故障

    // === 充电流程中判断BMS故障产生的停机 ===
    BatteryReversed = 65,          // 电池接反
    BmsCommTimeout = 66,           // BMS通信超时
    ChargeVoltageTooHigh = 67,     // 充电机测量电压过高
    VoltageNotReach = 68,          // 电压达不到预期值
    ChargeCurrentTooHigh = 69,     // 充电机测量电流过大
    BmsMismatch1 = 70,             // BMS不匹配1
    BmsMismatch2 = 71,             // BMS不匹配2
    BmsMismatch3 = 72,             // BMS不匹配3
    OutsideVoltage = 73,           // 外侧电压大于50V
    BatteryVoltageError = 74,      // 电池电压误差过大
    BatteryVoltageLow = 75,        // 电池电压过低
    BatteryVoltageHigh = 76,       // 电池电压过高
    BsmBatTempHigh = 77,           // BSM判断电池温度过高
    BsmCellVoltageHigh = 78,       // BSM单体电压过高
    BsmCellVoltageLow = 79,        // BSM单体电压过低
    BsmSocHigh = 80,               // BSM SOC过高
    BsmSocLow = 81,                // BSM SOC过低
    BsmCurrentHigh = 82,           // BSM电流过大
    BsmBatTempHigh2 = 83,          // BSM电池温度过高
    BsmInsulationError = 84,       // BSM绝缘错误
    BsmConnectError = 85,          // BSM连接错误
    BsmChargeForbidden = 86,       // BSM禁止充电超时
    BcsVoltageOver = 87,           // BCS判断电池电压超过配置允许
    BcsCurrentOver = 88,           // BCS判断电池电流超过配置允许
    BcsCellVoltageOver = 89,       // BCS判断单体电压超过配置允许
    VinAuthTimeout = 90,           // VIN鉴权超时
    VinAuthFailed = 91,            // VIN鉴权失败
    TrickleChargeTimeout = 92,     // 涓流充电超时
    MeterDataJump = 93,            // 电表数据异常突变
    MeterMeasureStop = 94,         // 电表计量停止
    MeterErrorLarge = 95,          // 电表计量误差过大
    BrmTimeout = 96,               // BRM报文超时
    BrmTimeout2 = 97,              // BRM报文超时
    BcpTimeout = 98,               // BCP报文超时
    BcpTimeout2 = 99,              // BCP报文超时
    BroTimeout0 = 100,             // BRO(0x00)报文超时
    BroTimeoutAA = 101,            // BRO(0xAA)报文超时
    BclTimeout = 102,              // BCL报文超时
    BclTimeout2 = 103,             // BCL报文超时
    BcsTimeout = 104,              // BCS报文超时
    BsmTimeout = 105,              // BSM报文超时
    BroError = 106,                // BRO报文错误
    PreChargeUnderVoltage = 107,   // 预充电压欠压

    // === BMS主动发送的停机 ===
    BmsNormalStop = 129,           // 正常停机 (BMS主动)
    BmsSocReached = 130,           // 达到目标SOC
    BmsTotalVoltageReached = 131,  // 达到总电压设定值
    BmsCellVoltageReached = 132,   // 达到单体电压设定值
    BmsChargerActiveStop = 133,    // 充电机主动终止
    BmsInsulationFault = 134,      // 绝缘故障
    BmsOutputConnectorOverTemp = 135,// 输出连接器过温故障
    BmsComponentOverTemp = 136,    // BMS元件、输出连接器过温故障
    BmsChargeConnectorFault = 137, // 充电连接器故障
    BmsBatteryTempHigh = 138,      // 电池组温度过高故障
    BmsHighVoltageRelayFault = 139,// 高压继电器故障
    BmsCheckPoint2Fault = 140,     // 检测点2电压检测故障
    BmsOtherFault = 141,           // 其它故障
    BmsCurrentOver = 142,          // 电流过大
    BmsVoltageOver = 143,          // 电压过大

    // === 充电桩正常停机 ===
    NormalLocalStop = 161,         // 本地手动停机
    NormalBalanceInsufficient = 162,// 余额不足
    NormalTimeReached = 163,       // 时间到
    NormalEnergyReached = 164,     // 电量到
    NormalAmountReached = 165,     // 金额到
    NormalSocReached = 166,        // SOC到
    NormalRemoteStop = 167,        // 远程停机
}
