using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 支路单体温度
    /// </summary>
    public class BrachSumTemp1 : ASDU
    {
        /// <summary>
        /// 支路1最高单体温度
        /// </summary>
        [Property(0, 8)]
        public byte Bran1MaxCellTemp { get; set; }

        /// <summary>
        /// 支路1最低单体温度
        /// </summary>
        [Property(8, 8)]
        public byte Bran1MinCellTemp { get; set; }

        /// <summary>
        /// 支路2最高单体温度
        /// </summary>
        [Property(16, 8)]
        public byte Bran2MaxCellTemp { get; set; }

        /// <summary>
        /// 支路2最低单体温度
        /// </summary>
        [Property(24, 8)]
        public byte Bran2MinCellTemp { get; set; }

        /// <summary>
        /// 支路3最高单体温度
        /// </summary>
        [Property(32, 8)]
        public byte Bran3MaxCellTemp { get; set; }

        /// <summary>
        /// 支路3最低单体温度
        /// </summary>
        [Property(40, 8)]
        public byte Bran3MinCellTemp { get; set; }

        /// <summary>
        /// 支路4最高单体温度
        /// </summary>
        [Property(48, 8)]
        public byte Bran4MaxCellTemp { get; set; }

        /// <summary>
        /// 支路4最低单体温度
        /// </summary>
        [Property(56, 8)]
        public byte Bran4MinCellTemp { get; set; }
    }
}