using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt29 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV29CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV29No { get; set; }

        /// <summary>
        /// 85电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV85 { get; set; }

        /// <summary>
        /// 85电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV85QUAL { get; set; }

        /// <summary>
        /// 86电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV86 { get; set; }

        /// <summary>
        /// 86电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV86QUAL { get; set; }

        /// <summary>
        /// 87电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV87 { get; set; }

        /// <summary>
        ///87电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV87QUAL { get; set; }
    }
}