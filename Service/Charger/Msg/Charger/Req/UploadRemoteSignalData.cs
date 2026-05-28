using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.3 遥信数据上报
    /// </summary>
    public class UploadRemoteSignalData : ASDU
    {
        /// <summary>
        /// 工作状态 00H:待机、01H:工作、02H:工作完成、03H:充/放电暂停
        /// </summary>
        [Property(0, 2)]
        public byte WorkStatus { get; set; }

        /// <summary>
        /// 总故障：0-正常、1-故障
        /// </summary>
        [Property(2, 1)]
        public bool TotalError { get; set; }

        /// <summary>
        /// 总告警：0-正常、1-告警
        /// </summary>
        [Property(3, 1)]
        public bool TotalWarning { get; set; }

        /// <summary>
        /// 急停按钮动作故障：0-正常、1-故障
        /// </summary>
        [Property(4, 1)]
        public bool EmergencyStop { get; set; }

        /// <summary>
        /// 烟感故障：0-正常、1-故障
        /// </summary>
        [Property(5, 1)]
        public bool SmokeFault { get; set; }

        /// <summary>
        /// 充电桩交流输入断路器故障(系统供电断路器)：0-正常、1-故障
        /// </summary>
        [Property(6, 1)]
        public bool ChargeACInputCircuitBreakerFault { get; set; }

        /// <summary>
        /// 直流母线正极输出 接触器拒动/误 动故障：0-正常、1-故障
        /// </summary>
        [Property(7, 1)]
        public bool DcBusPositElecContactorRefuFault { get; set; }

        /// <summary>
        /// 直流母线负极输出 接触器拒动/误 动故障：：0-正常、1-故障
        /// </summary>
        [Property(8, 1)]
        public bool DcBusNegatElecContactorRefuFault { get; set; }

        /// <summary>
        /// 直流母线正级输出 熔断器故障
        /// </summary>
        [Property(9, 1)]
        public bool DcBusPositElecFusesFault { get; set; }

        /// <summary>
        /// 直流母线负级输出 熔断器故障
        /// </summary>
        [Property(10, 1)]
        public bool DDcBusNegatElecFusesFault { get; set; }

        /// <summary>
        /// 充电接口电磁锁故障
        /// </summary>
        [Property(11, 1)]
        public bool ChargingInterfaceLockError { get; set; }

        /// <summary>
        /// 充电桩风扇故障
        /// </summary>
        [Property(12, 1)]
        public bool ChargerFanError { get; set; }

        /// <summary>
        /// 避雷器故障
        /// </summary>
        [Property(13, 1)]
        public bool ArresterError { get; set; }

        /// <summary>
        /// 绝缘监测告警
        /// </summary> 
        [Property(14, 1)]
        public bool InsulationDetectionAlarm { get; set; }

        /// <summary>
        /// 绝缘监测故障
        /// </summary>
        [Property(15, 1)]
        public bool InsulationDetectionError { get; set; }

        /// <summary>
        /// 电池极性反接故障
        /// </summary>
        [Property(16, 1)]
        public bool BatteryPolarityReverseError { get; set; }

        /// <summary>
        /// 充电中车辆控制导引故障
        /// </summary>
        [Property(17, 1)]
        public bool VeConGuidanceFailure { get; set; }

        /// <summary>
        /// 充电桩过温故障
        /// </summary>
        [Property(18, 1)]
        public bool ChargingOverTempError { get; set; }

        /// <summary>
        /// 充电接口过温故障
        /// </summary>
        [Property(19, 1)]
        public bool InterfaceOverFaulty { get; set; }

        /// <summary>
        /// 充电枪未归位告警
        /// </summary>
        [Property(20, 1)]
        public bool ChargingGunNotHomingError { get; set; }

        /// <summary>
        /// BMS通信故障 
        /// </summary>
        [Property(21, 1)]
        public bool BmsConnError { get; set; }

        /// <summary>
        /// 充电桩输入电压过压故障
        /// </summary>
        [Property(22, 1)]
        public bool ChargerInputOverVoltageError { get; set; }

        /// <summary>
        /// 充电桩输入电压欠压故障
        /// </summary>
        [Property(23, 1)]
        public bool ChargerInputUnderVoltageError { get; set; }

        /// <summary>
        /// 直流母线输出过压故障
        /// </summary>
        [Property(24, 1)]
        public bool DcBusOutputOverVoltageError { get; set; }

        /// <summary>
        /// 直流母线输出欠压故障
        /// </summary>
        [Property(25, 1)]
        public bool DcBusOutputUnderVoltageError { get; set; }

        /// <summary>
        /// 直流母线输出过流故障
        /// </summary>
        [Property(26, 1)]
        public bool DcBusOutputOverCurrentError { get; set; }

        /// <summary>
        /// 车辆连接状态 0-未连接、1-已连接
        /// </summary>
        [Property(27, 1)]
        public bool VehicleConnStatus { get; set; }

        /// <summary>
        /// 充电桩充电枪座状态 0-已连接、1-未连接
        /// </summary>
        [Property(28, 1)]
        public bool ChargeStationGunHolderStatus { get; set; }

        /// <summary>
        /// 充电接口电子锁状态 0-解锁、1-锁止
        /// </summary>
        [Property(29, 1)]
        public bool ChargingInterfaceLockStatus { get; set; }

        /// <summary>
        /// 正极直流输出接触器状态 0-分断、1-闭合
        /// </summary>
        [Property(30, 1)]
        public bool PositiveDcTransmissionContactorStatus { get; set; }

        /// <summary>
        /// 负极直流输出接触器状态 0-分断 1-闭合
        /// </summary>
        [Property(31, 1)]
        public bool NegativeDcTransmissionContactorStatus { get; set; }

        /// <summary>
        /// 门禁故障 0-正常 1-故障
        /// </summary>
        [Property(32, 1)]
        public bool EntranceGuardError { get; set; }

        /// <summary>
        /// 正极直流输出接触器粘连故障
        /// </summary>
        [Property(33, 1)]
        public bool PConA3dhesionFailure { get; set; }

        /// <summary>
        /// 负极直流输出接触器粘连故故障
        /// </summary>
        [Property(34, 1)]
        public bool NConadhesionFailure { get; set; }

        /// <summary>
        /// 泄放回路故障
        /// </summary>
        [Property(35, 1)]
        public bool ReliefCircuitError { get; set; }

        /// <summary>
        /// 充电桩交流输入接触器据动/误动故(预留位置供其他适用)
        /// </summary>
        [Property(36, 1)]
        public bool ConActivated { get; set; }

        /// <summary>
        /// 充电桩交流输入接触器粘连故障(预留位置供其他适用)
        /// </summary>
        [Property(37, 1)]
        public bool ConAdhesionFailure { get; set; }

        /// <summary>
        /// 辅助电源故障
        /// </summary>
        [Property(38, 1)]
        public bool AuxiliaryPowerError { get; set; }

        /// <summary>
        /// 模块输出反接
        /// </summary>
        [Property(39, 1)]
        public bool ModuleOutputReverseError { get; set; }

        /// <summary>
        /// 充电桩交流接触器状态 0-分断 1-吸合
        /// </summary>
        [Property(40, 1)]
        public bool AcContactorStatus { get; set; }

        /// <summary>
        /// 充电枪过温告警 0-正常 1-故障
        /// </summary>
        [Property(41, 1)]
        public bool ChargingGunOverTempWarning { get; set; }

        /// <summary>
        /// 充电桩过温告警 0-正常 1-故障
        /// </summary>
        [Property(42, 1)]
        public bool ChargerOverTempWarning { get; set; }

        /// <summary>
        /// 电表通信异常 0-正常 01H-故障
        /// </summary>
        [Property(43, 1)]
        public bool MeterConnError { get; set; }

        /// <summary>
        /// 电表电度异常 0-正常 02H-故障
        /// </summary>
        [Property(44, 1)]
        public bool MeterDataError { get; set; }

        /// <summary>
        /// 水浸告警  0-正常 03H-故障
        /// </summary>
        [Property(45, 1)]
        public bool WaterloggingWarning { get; set; }


        /// <summary>
        /// 逆功率报警 00H：正常 01H：故障
        /// 发生该故障后一直保持，只到重合闸完成后，可以重合闸信号变为可用状态时才清零。
        /// </summary>
        [Property(47, 1)]
        public bool ReversePowerWarning { get; set; }


        /// <summary>
        /// 电池包辅助电源状态 0：辅助电源未给电池包供电、1：辅助电源正在给电池包供电
        /// </summary>
        [Property(46, 1)]
        public bool BatteryPackAuxiliaryPowerStatus { get; set; }
    }
}