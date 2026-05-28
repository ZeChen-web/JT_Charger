using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 单体电压状态展示
    /// </summary>
    public class SumVolt : ASDU
    {
        /// <summary>
        /// 最高单体电压
        /// </summary>
        [Property(0, 16)]
        public ushort MaxCellVolt { get; set; }

        /// <summary>
        /// 最低单体电压
        /// </summary>
        [Property(16, 16)]
        public ushort MinCellVolt { get; set; }

        /// <summary>
        /// 最高单体电压所在CSC号(内CAN编号从0开始计)
        /// </summary>
        [Property(32, 8)]
        public byte MaxCellVoltCSCNum { get; set; }

        /// <summary>
        /// 最高单体电压所在电芯位置（指对应CSC中的电芯位置）(内CAN编号从0开始计)
        /// </summary>
        [Property(40, 8)]
        public byte MaxCellVoltCellNum { get; set; }

        /// <summary>
        /// 最低单体电压所在CSC号(内CAN编号从0开始计)
        /// </summary>
        [Property(48, 8)]
        public byte MinCellVoltCSCNum { get; set; }

        /// <summary>
        /// 最低单体电压所在电芯位置（指对应CSC中的电芯位置）(内CAN编号从0开始计)
        /// </summary>
        [Property(56, 8)]
        public byte MinCellVoltCellNum { get; set; }
    }
}