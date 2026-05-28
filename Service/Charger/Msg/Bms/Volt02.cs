using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt02 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV2CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV2No { get; set; }

        /// <summary>
        /// 04电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV04 { get; set; }

        /// <summary>
        /// 04电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV04QUAL { get; set; }

        /// <summary>
        /// 05电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV05 { get; set; }

        /// <summary>
        /// 05电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV05QUAL { get; set; }

        /// <summary>
        /// 06电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV06 { get; set; }

        /// <summary>
        /// 06电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV06QUAL { get; set; }
    }
}