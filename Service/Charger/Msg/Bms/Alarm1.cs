using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 报警部分1实时显示
    /// </summary>
    public class Alarm1 : ASDU
    {
        /// <summary>
        /// SBMU状态报文的CRC
        /// </summary>
        [Property(0, 8)]
        public byte AL1CRC { get; set; }

        /// <summary>
        /// SBMU生命信号,0~15循环
        /// </summary>
        [Property(8, 4)]
        public byte AL1ALIV { get; set; }

        /// <summary>
        /// 单体过压报警
        /// </summary>
        [Property(12, 2)]
        public byte AL1CellOverVolt { get; set; }

        /// <summary>
        /// 单体欠压报警
        /// </summary>
        [Property(14, 2)]
        public byte AL1CellUnderVolt { get; set; }

        /// <summary>
        /// 单体过温报警
        /// </summary>
        [Property(16, 2)]
        public byte AL1CellOverTemp { get; set; }

        /// <summary>
        /// 单体低温报警
        /// </summary>
        [Property(18, 2)]
        public byte AL1CellUnderTemp { get; set; }

        /// <summary>
        /// 箱体过压报警(总电压)
        /// </summary>
        [Property(20, 2)]
        public byte AL1PackOverVolt { get; set; }

        /// <summary>
        /// 箱体欠压报警(总电压）
        /// </summary>
        [Property(22, 2)]
        public byte AL1PackUnderVolt { get; set; }

        /// <summary>
        /// 充电过流报警
        /// </summary>
        [Property(24, 2)]
        public byte AL1ChrgOverCurrFlt { get; set; }

        /// <summary>
        /// 支路放电过流报警
        /// </summary>
        [Property(26, 2)]
        public byte AL1BranDchrgOvrCurFlt { get; set; }

        /// <summary>
        /// Pack放电过流报警
        /// </summary>
        [Property(28, 2)]
        public byte AL1PackDchrgOvrCurFlt { get; set; }

        /// <summary>
        /// 支路回充电流超限
        /// </summary>
        [Property(30, 2)]
        public byte AL1BranRechrgOvrCurFlt { get; set; }

        /// <summary>
        /// Pack回充电流超限
        /// </summary>
        [Property(32, 2)]
        public byte AL1PackRechrgOvrCurFlt { get; set; }

        /// <summary>
        /// 行车持续充电支路回充过流
        /// </summary>
        [Property(34, 2)]
        public byte AL1ContBranReOvrCurFlt { get; set; }

        /// <summary>
        /// 行车持续充电Pack回充过流
        /// </summary>
        [Property(36, 2)]
        public byte AL1ContPackReOvrCurFlt { get; set; }

        /// <summary>
        /// 行车持续放电支路放电过流
        /// </summary>
        [Property(38, 2)]
        public byte AL1ContBranDisOvrCurFlt { get; set; }

        /// <summary>
        /// 行车持续充电Pack放电过流
        /// </summary>
        [Property(40, 2)]
        public byte AL1ContPackDisOvrCurFlt { get; set; }

        /// <summary>
        /// SOC过高报警
        /// </summary>
        [Property(42, 2)]
        public byte AL1PackOverSOC { get; set; }

        /// <summary>
        /// SOC过低报警
        /// </summary>
        [Property(44, 2)]
        public byte AL1PackUnderSOC { get; set; }

        /// <summary>
        /// 单支路累加和压差过大(并联支路之间)
        /// </summary>
        [Property(46, 2)]
        public byte AL1BranSumVoltOvrDiff { get; set; }

        /// <summary>
        /// 绝缘阻抗报警
        /// </summary>
        [Property(48, 2)]
        public byte AL1IsoResLowFlt { get; set; }

        /// <summary>
        /// 单体压差过大
        /// </summary>
        [Property(50, 2)]
        public byte AL1CellVoltOverDiff { get; set; }

        /// <summary>
        /// SOC差异过大(并联支路之间)
        /// </summary>
        [Property(52, 2)]
        public byte AL1BranchSOCOverDiff { get; set; }

        /// <summary>
        /// 单体温差过大
        /// </summary>
        [Property(54, 2)]
        public byte AL1CellTempOverDiff { get; set; }

        /// <summary>
        /// 实时时钟故障
        /// </summary>
        [Property(56, 1)]
        public byte AL1RTCFlt { get; set; }

        /// <summary>
        /// 内部通信故障(电流报文丢失/菊花链通讯丢失/HVB报文丢失)
        /// </summary>
        [Property(57, 1)]
        public byte AL1InnerCommonFlt { get; set; }

        /// <summary>
        /// 均衡电路故障
        /// </summary>
        [Property(58, 1)]
        public byte AL1BalaCircuitFlt { get; set; }

        /// <summary>
        /// 水冷告警
        /// </summary>
        [Property(59, 1)]
        public byte AL1WaterCoolWarn { get; set; }

        /// <summary>
        /// 单体电压采样异常
        /// </summary>
        [Property(60, 1)]
        public byte AL1CellVoltSampErr { get; set; }

        /// <summary>
        /// 电芯温度采样异常
        /// </summary>
        [Property(61, 1)]
        public byte AL1CellTempSampErr { get; set; }


        /// <summary>
        /// 单体SOC差异过大
        /// </summary>
        [Property(62, 2)]
        public byte AL1CellSOCOverDiff { get; set; }
    }
}