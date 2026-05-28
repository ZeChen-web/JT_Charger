using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BrachSumVolt3 : ASDU
    {
        /// <summary>
        /// 支路1平均单体电压
        /// </summary>
        [Property(0, 16)]
        public ushort Bran1CellAvgVolt { get; set; }

        /// <summary>
        /// 支路2平均单体电压
        /// </summary>
        [Property(16, 16)]
        public ushort Bran2CellAvgVolt { get; set; }

        /// <summary>
        /// 支路3平均单体电压
        /// </summary>
        [Property(32, 16)]
        public ushort Bran3CellAvgVolt { get; set; }

        /// <summary>
        /// 支路4平均单体电压
        /// </summary>
        [Property(48, 16)]
        public ushort Bran4CellAvgVolt { get; set; }
    }
}