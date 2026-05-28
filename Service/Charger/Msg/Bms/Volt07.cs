using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt07 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV7CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV7No { get; set; }

        /// <summary>
        /// 19电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV19 { get; set; }

        /// <summary>
        /// 19电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV19QUAL { get; set; }

        /// <summary>
        /// 20电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV20 { get; set; }

        /// <summary>
        /// 20电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV20QUAL { get; set; }

        /// <summary>
        /// 21电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV21 { get; set; }

        /// <summary>
        /// 21电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV21QUAL { get; set; }
    }
}