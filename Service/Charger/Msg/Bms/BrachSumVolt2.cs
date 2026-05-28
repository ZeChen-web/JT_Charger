using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 支路单体电压
    /// </summary>
    public class BrachSumVolt2 : ASDU
    {
        /// <summary>
        /// 支路3最高单体电压
        /// </summary>
        [Property(0, 16)]
        public ushort Bran3MaxCellVolt { get; set; }

        /// <summary>
        /// 支路3最低单体电压
        /// </summary>
        [Property(16, 16)]
        public ushort Bran3MinCellVolt { get; set; }

        /// <summary>
        /// 支路4最高单体电压
        /// </summary>
        [Property(32, 16)]
        public ushort Bran4MaxCellVolt { get; set; }

        /// <summary>
        /// 支路4最低单体电压
        /// </summary>
        [Property(48, 16)]
        public ushort Bran4MinCellVolt { get; set; }
    }
}