using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt09 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV9CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV9No { get; set; }

        /// <summary>
        /// 25电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV25 { get; set; }

        /// <summary>
        /// 25电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV25QUAL { get; set; }

        /// <summary>
        /// 26电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV26 { get; set; }

        /// <summary>
        /// 26电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV26QUAL { get; set; }

        /// <summary>
        /// 27电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV27 { get; set; }

        /// <summary>
        /// 27电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV27QUAL { get; set; }
    }
}