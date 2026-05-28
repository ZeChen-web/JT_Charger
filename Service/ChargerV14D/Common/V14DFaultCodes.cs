namespace Service.ChargerV14D.Common;

/// <summary>直流充电桩故障代码 (附录13.1.1) - 硬件故障(不能启动充电)</summary>
public enum DcHardwareFault : byte
{
    None = 0,
    EmergencyStop = 1,        // 急停按钮动作故障
    DoorSwitch = 2,           // 门禁开关故障
    WaterImmersion = 3,       // 水浸开关故障
    Fuse = 4,                 // 熔断器熔断故障
    GunELock = 5,             // 充电枪电子锁故障
    GunDisconnect = 6,        // 充电枪连接故障
    Insulation = 7,           // 绝缘检测故障
    InsulationShort = 8,      // 绝缘检测短路故障
    K1Refuse = 9,             // 接触器K1拒动故障
    K1Adhesion = 10,          // 接触器K1粘连故障
    K2Refuse = 11,            // 接触器K2拒动故障
    K2Adhesion = 12,          // 接触器K2粘连故障
    GunDcPlusOverTemp = 13,   // 枪DC+过温故障
    GunDcMinusOverTemp = 14,  // 枪DC-过温故障
    DischargeCircuit = 15,    // 泄放回路故障
    AcContactor = 16,         // 交流接触器故障
    BusContactorAdhesion = 17,// 母联接触器粘连故障
    ModuleFault = 18,         // 模块故障
    AuxPower = 19,            // 辅助电源故障
    Arrester = 20,            // 避雷器故障
    PileOverTemp = 21,        // 充电桩过温故障
    Controller = 22,          // 控制器故障(副CPU通讯故障)
    ModuleComm = 23,          // 模块通信故障
    AcInput = 24,             // 交流输入故障
    MeterComm = 25,           // 电表通讯故障
    ScreenComm = 26,          // 串口屏通讯故障
    PCUFault = 27,            // PCU故障
    ChargerCabinetFault = 28, // 充电柜上报故障
}

/// <summary>直流充电桩告警代码 (附录13.1.1) - 硬件告警(不影响充电)</summary>
public enum DcHardwareWarning : byte
{
    None = 0,
    GunDcPlusOverTemp = 1,      // 枪DC+过温告警
    GunDcMinusOverTemp = 2,     // 枪DC-过温告警
    ChargerOverTemp = 3,        // 充电机过温告警
    InsulationWarning = 4,      // 绝缘检测预警
    ChargeLightOn = 5,          // 上位机点亮告警灯
    GunNotHomed = 6,            // 充电枪未归位
    PartialModuleComm = 7,      // 部分模块通讯故障告警
    PartialModuleFault = 8,     // 部分模块故障告警
    PartialAcInput = 9,         // 部分模块交流输入告警
    BusContactorRefuse = 10,    // 母联接触器拒动告警
    PCUWarning = 11,            // PCU告警
}
