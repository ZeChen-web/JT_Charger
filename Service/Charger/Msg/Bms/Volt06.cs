using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt06 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV6CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV6No { get; set; }

        /// <summary>
        /// 16电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV16 { get; set; }

        /// <summary>
        /// 16电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV16QUAL { get; set; }

        /// <summary>
        /// 17电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV17 { get; set; }

        /// <summary>
        /// 17电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV17QUAL { get; set; }

        /// <summary>
        /// 18电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV18 { get; set; }

        /// <summary>
        /// 18电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV18QUAL { get; set; }
    }
}