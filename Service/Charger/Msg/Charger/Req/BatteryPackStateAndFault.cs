using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar.DistributedSystem.Snowflake;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.13 电池包内部接触器状态和故障上报（站内充电模式有电池包时周期性上传）
    /// </summary>
    public class BatteryPackStateAndFault : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 充负 1 继电器粘连故障
        /// </summary>
        [Property(8, 8)]
        public byte ChargingNegativeOneAdhesionFault { get; set; }
        /// <summary>
        /// 充正 1 继电器粘连故 障
        /// </summary>
        [Property(16, 8)]
        public byte ChargingCorrectOneAdhesionFault { get; set; }
        /// <summary>
        /// 主负继电器粘连故障
        /// </summary>
        [Property(24, 8)]
        public byte MainNegativeAdhesionFault { get; set; }
        /// <summary>
        /// 主正继电器粘连故障
        /// </summary>
        [Property(32, 8)]
        public byte MainCorrectAdhesionFault { get; set; }
        /// <summary>
        /// 主负继电器状态(如继 电器状态由 BMS检测)
        /// </summary>
        [Property(40, 8)]
        public byte MainNegativeState { get; set; }
        /// <summary>
        /// 主正继电器状态(如继 电器状态由 BMS检测)
        /// </summary>
        [Property(48, 8)]
        public byte MainCorrectState { get; set; }
        /// <summary>
        /// 加热 2 继电器粘连故障
        /// </summary>
        [Property(56, 8)]
        public byte HeatingTwoAdhesionFault { get; set; }
        /// <summary>
        /// 加热 1 继电器粘连故障
        /// </summary>
        [Property(64, 8)]
        public byte HeatingOneAdhesionFault { get; set; }
        /// <summary>
        /// 充负 2 继电器粘连故障
        /// </summary>
        [Property(72, 8)]
        public byte ChargingNegativeTwoAdhesionFault { get; set; }
        /// <summary>
        /// 充正 2 继电器粘连故障
        /// </summary>
        [Property(80, 8)]
        public byte ChargingCorrectTwoAdhesionFault { get; set; }
        /// <summary>
        /// 充正继电器 2状态(如 继电器状态由 BMS检测)
        /// </summary>
        [Property(88, 8)]
        public byte ChargingCorrectTwoState { get; set; }
        /// <summary>
        /// 充负继电器 2状态(如 继电器状态由 BMS检 测)
        /// </summary>
        [Property(96, 8)]
        public byte ChargingNegativeTwoState { get; set; }
        /// <summary>
        /// 充负继电器 1状态(如 继电器状态由 BMS检 测)
        /// </summary>
        [Property(104, 8)]
        public byte ChargingNegativeOneState { get; set; }
        /// <summary>
        /// 充正继电器 1状态(如 继电器状态由 BMS检 测
        /// </summary>
        [Property(112, 8)]
        public byte ChargingCorrectOneState { get; set; }
        /// <summary>
        /// 预充继电器状态(如继电器状态由 BMS 检测)
        /// </summary>
        [Property(120, 8)]
        public byte PreChargingState { get; set; }
        /// <summary>
        /// 主负继电器无法闭合 报警
        /// </summary>
        [Property(128, 8)]
        public byte MainNegativeNotCloseFault { get; set; }
        /// <summary>
        /// 主正继电器无法闭合 报警
        /// </summary>
        [Property(136, 8)]
        public byte MainCorrectNotCloseFault { get; set; }
        /// <summary>
        /// 支路断路故障
        /// </summary>
        [Property(144, 8)]
        public byte BranchBrokenFault { get; set; }
        /// <summary>
        /// 附件继电器粘连故障（保留）
        /// </summary>
        [Property(152, 8)]
        public byte AnnexAdhesionFault { get; set; }
        /// <summary>
        /// BMS 24V 供电异常报 警
        /// </summary>
        [Property(160, 8)]
        public byte BMSPowerSupplyFault { get; set; }
        /// <summary>
        /// BMS 24V 供电异常报 警 
        /// </summary>
        [Property(168, 8)]
        public byte BMSPowerSupplyFault2 { get; set; }//协议里两个字段一样
        /// <summary>
        /// 热管理系统故障
        /// </summary>
        [Property(176, 8)]
        public byte ThermalManagementFault { get; set; }
        /// <summary>
        /// 加热膜或 TMS接触器 无法闭合故障
        /// </summary>
        [Property(184, 8)]
        public byte HeatingFilmNotCloseFault { get; set; }
        /// <summary>
        /// 加热膜或 TMS接触器 无法闭合故障
        /// </summary>
        [Property(192, 8)]
        public byte HeatingFilmNotCloseFault2 { get; set; }////协议里两个字段一样
        /// <summary>
        /// 加热膜或 TMS接触器 无法断开报警
        /// </summary>
        [Property(200, 8)]
        public byte HeatingFilmNotDisconnectFault { get; set; }
        /// <summary>
        /// 直流充电 2 负继电器 无法闭合报警
        /// </summary>
        [Property(208, 8)]
        public byte DcTwoNegativeNotCloseFault { get; set; }

        /// <summary>
        /// 直流充电 2 正继电器 无法闭合报警
        /// </summary>
        [Property(216, 8)]
        public byte DcTwoCorrectNotCloseFault { get; set; }
        /// <summary>
        /// 直流充电 1 负继电器 无法闭合报警
        /// </summary>
        [Property(224, 8)]
        public byte DcOneNegativeNotCloseFault { get; set; }
        /// <summary>
        /// 直流充电 1 正继电器 无法闭合报警
        /// </summary>
        [Property(232, 8)]
        public byte DcOneCorrectNotCloseFault { get; set; }
        /// <summary>
        /// 充电插座过温报警
        /// </summary>
        [Property(240, 8)]
        public byte ChargingSocketOverheatedFault { get; set; }
        /// <summary>
        /// 电池包自保护报警
        /// </summary>
        [Property(248, 8)]
        public byte SelfProtectionFault { get; set; }
        /// <summary>
        /// 电池系统故障码
        /// </summary>
        [Property(256, 8)]
        public byte BatterySystemFaultCode { get; set; }
    }
}