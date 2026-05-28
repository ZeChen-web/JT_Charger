using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 单体温度状态展示
    /// </summary>
    public class SumTemp : ASDU
    {
        /// <summary>
        /// 最高单体温度
        /// </summary>
        [Property(0, 8)]
        public byte MaxCellTemp { get; set; }

        /// <summary>
        /// 最低单体温度
        /// </summary>
        [Property(8, 8)]
        public byte MinCellTemp { get; set; }

        /// <summary>
        /// 平均单体温度
        /// </summary>
        [Property(16, 8)]
        public byte AvgCellTemp { get; set; }

        /// <summary>
        /// 最高温度所在CSC号(内CAN编号从0开始计)
        /// </summary>
        [Property(24, 6)]
        public byte MaxCellTempCSCNum { get; set; }

        /// <summary>
        /// 最高温度所在CSC中的位置(指对应CSC中的电芯位置）(内CAN编号从0开始计)
        /// </summary>
        [Property(30, 6)]
        public byte MaxCellTempCellNum { get; set; }

        /// <summary>
        /// 最低温度所在CSC号(内CAN编号从0开始计)
        /// </summary>
        [Property(36, 6)]
        public byte MinCellTempCSCNum { get; set; }

        /// <summary>
        ///最低温度所在CSC中的位置(指对应CSC中的电芯位置）(内CAN编号从0开始计)
        /// </summary>
        [Property(42, 6)]
        public byte MinCellTempCellNum { get; set; }

        /// <summary>
        /// 平均单体电压
        /// </summary>
        [Property(48, 16)]
        public ushort AvgCellVolt { get; set; }
    }
}