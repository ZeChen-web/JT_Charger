using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BrachSumVolt1 : ASDU
    {
        /// <summary>
        /// 支路1最高单体电压
        /// </summary>
        [Property(0, 16)]
        public ushort Bran1MaxCellVolt { get; set; }

        /// <summary>
        /// 支路1最低单体电压
        /// </summary>
        [Property(16, 16)]
        public ushort Bran1MinCellVolt { get; set; }

        /// <summary>
        /// 支路2最高单体电压
        /// </summary>
        [Property(32, 16)]
        public ushort Bran2MaxCellVolt { get; set; }

        /// <summary>
        /// 支路2最低单体电压
        /// </summary>
        [Property(48, 16)]
        public ushort Bran2MinCellVolt { get; set; }
    }
}