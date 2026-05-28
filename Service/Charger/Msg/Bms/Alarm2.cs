using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 实时报警信息
    /// </summary>
    public class Alarm2 : ASDU
    {
        /// <summary>
        /// SBMU状态报文的CRC
        /// </summary>
        [Property(0, 8)]
        public byte AL2CRC { get; set; }

        /// <summary>
        /// SBMU生命信号,0~15循环C
        /// </summary>
        [Property(8, 4)]
        public byte AL2ALIV { get; set; }

        /// <summary>
        /// 采样线开路报警
        /// </summary>
        [Property(12, 1)]
        public byte AL2SampLineOpnFlg { get; set; }

        /// <summary>
        /// 电压超范围报警
        /// </summary>
        [Property(13, 1)]
        public byte AL2VoltOutOfRangeFlg { get; set; }

        /// <summary>
        /// 温度采样NTC轻微报警
        /// </summary>
        [Property(14, 1)]
        public byte AL2SysNTCMinorFlt { get; set; }

        /// <summary>
        /// 温度采样NTC严重报警
        /// </summary>
        [Property(15, 1)]
        public byte AL2SysNTCSeriousFlt { get; set; }

        /// <summary>
        /// 菊花链通讯丢失故障
        /// </summary>
        [Property(16, 1)]
        public byte AL2DaisyChainComLost { get; set; }

        /// <summary>
        /// Pack SOC跳变故障
        /// </summary>
        [Property(17, 1)]
        public byte AL2PackSOCJumpFlt { get; set; }

        /// <summary>
        /// 加热回路异常故障
        /// </summary>
        [Property(18, 1)]
        public byte AL2HeatFlmFlt { get; set; }

        /// <summary>
        /// 支路断路故障
        /// </summary>
        [Property(19, 2)]
        public byte AL2BranchMsdOffWarn { get; set; }

        /// <summary>
        /// 可充电储能系统不匹配故障
        /// </summary>
        [Property(21, 2)]
        public byte AL2SysParaNotMatchFlt { get; set; }

        /// <summary>
        /// 充电时放电电流过大
        /// </summary>
        [Property(23, 2)]
        public byte AL2ChrgOvrDisCurrFlt { get; set; }

        /// <summary>
        /// 总回路充电过流(低温CC状态)
        /// </summary>
        [Property(25, 2)]
        public byte AL2ChrgOvrCurrLowTFlt { get; set; }

        /// <summary>
        /// 总回路充电过流(非低温CC状态)
        /// </summary>
        [Property(27, 2)]
        public byte AL2ChrgOvrCurrNormTFlt { get; set; }

        /// <summary>
        /// 火警探测器内部故障
        /// </summary>
        [Property(29, 1)]
        public byte AL2FireProbeFlt { get; set; }

        /// <summary>
        /// 火警探测器通信故障
        /// </summary>
        [Property(30, 1)]
        public byte AL2FireProbeComFlt { get; set; }

        /// <summary>
        /// 电流传感器内部故障
        /// </summary>
        [Property(31, 1)]
        public byte AL2CurrSensorFlt { get; set; }

        /// <summary>
        /// ACAN通讯故障
        /// </summary>
        [Property(32, 1)]
        public byte AL2ACANComFlt { get; set; }

        /// <summary>
        /// 电流传感器通讯丢失
        /// </summary>
        [Property(33, 1)]
        public byte AL2CSUCommLostFlg { get; set; }

        /// <summary>
        /// HVB通信故障
        /// </summary>
        [Property(34, 1)]
        public byte AL2HVBMsgTimeOut { get; set; }

        /// <summary>
        /// SCAN通信故障
        /// </summary>
        [Property(35, 1)]
        public byte AL2SCANLostFlg { get; set; }

        /// <summary>
        /// 电池自保护极限过温故障
        /// </summary>
        [Property(36, 1)]
        public byte AL2BSPEOverTempFlt { get; set; }

        /// <summary>
        /// 电池自保护极限过压故障
        /// </summary>
        [Property(37, 1)]
        public byte AL2BSPEOverVoltFlt { get; set; }

        /// <summary>
        /// 电池自保护极限欠压故障
        /// </summary>
        [Property(38, 1)]
        public byte AL2BSPEUnderVoltFlt { get; set; }

        /// <summary>
        /// 火灾报警故障
        /// </summary>
        [Property(39, 1)]
        public byte AL2FireAlarmFlt { get; set; }

        /// <summary>
        /// SBMU供电电压故障
        /// </summary>
        [Property(40, 1)]
        public byte AL2BMUPwrFlt { get; set; }

        /// <summary>
        /// 高压互锁故障
        /// </summary>
        [Property(41, 1)]
        public byte AL2HvilFlt { get; set; }

        /// <summary>
        /// 电池自保护极限故障
        /// </summary>
        [Property(42, 1)]
        public byte AL2BSPEAllFlt { get; set; }

        /// <summary>
        /// 内侧高压回路断路故障
        /// </summary>
        [Property(43, 1)]
        public byte AL2InnerHVOpnCircuitFlt { get; set; }

        /// <summary>
        /// 电芯过放故障
        /// </summary>
        [Property(44, 2)]
        public byte AL2CellOverDisChrgWarn { get; set; }

        /// <summary>
        /// 宇通断继电器标志
        /// </summary>
        [Property(46, 1)]
        public byte AL2ZZTCutOffRelayFlg { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(47, 17)]
        public int AL2Reserved1 { get; set; }
    }
}