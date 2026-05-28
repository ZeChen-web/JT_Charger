using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt23 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV23CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV23No { get; set; }

        /// <summary>
        /// 67电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV67 { get; set; }

        /// <summary>
        /// 67电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV67QUAL { get; set; }

        /// <summary>
        /// 68电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV68 { get; set; }

        /// <summary>
        /// 68电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV68QUAL { get; set; }

        /// <summary>
        /// 69电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV69 { get; set; }

        /// <summary>
        /// 69电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV69QUAL { get; set; }
    }
}