using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt04 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV4CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV4No { get; set; }

        /// <summary>
        /// 10电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV10 { get; set; }

        /// <summary>
        /// 10电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV10QUAL { get; set; }

        /// <summary>
        /// 11电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV11 { get; set; }

        /// <summary>
        /// 11电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV11QUAL { get; set; }

        /// <summary>
        /// 12电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV12 { get; set; }

        /// <summary>
        /// 12电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV12QUAL { get; set; }
    }
}