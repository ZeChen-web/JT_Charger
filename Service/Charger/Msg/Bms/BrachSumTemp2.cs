using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BrachSumTemp2 : ASDU
    {
        /// <summary>
        /// 支路1单体平均温度
        /// </summary>
        [Property(0, 8)]
        public byte Bran1CellAvgTemp { get; set; }

        /// <summary>
        /// 支路2单体平均温度
        /// </summary>
        [Property(8, 8)]
        public byte Bran2CellAvgTemp { get; set; }

        /// <summary>
        /// 支路3单体平均温度
        /// </summary>
        [Property(16, 8)]
        public byte Bran3CellAvgTemp { get; set; }

        /// <summary>
        /// 支路4单体平均温度
        /// </summary>
        [Property(24, 8)]
        public byte Bran4CellAvgTemp { get; set; }

        /// <summary>
        /// Pack真实最小SOC(高精度)
        /// </summary>
        [Property(32, 16)]
        public double PackMinSOC { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(48, 16)]
        public ushort BranTempReserved1 { get; set; }
    }
}