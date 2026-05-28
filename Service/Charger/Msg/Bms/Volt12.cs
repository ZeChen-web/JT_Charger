using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt12 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV12CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV12No { get; set; }

        /// <summary>
        /// 34电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV34 { get; set; }

        /// <summary>
        /// 34电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV34QUAL { get; set; }

        /// <summary>
        /// 35电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV35 { get; set; }

        /// <summary>
        /// 35电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV35QUAL { get; set; }

        /// <summary>
        /// 36电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV36 { get; set; }

        /// <summary>
        /// 36电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV36QUAL { get; set; }
    }
}