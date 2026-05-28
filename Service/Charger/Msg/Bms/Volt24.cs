using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt24 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV24CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV24No { get; set; }

        /// <summary>
        /// 70电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV70 { get; set; }

        /// <summary>
        /// 70电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV70QUAL { get; set; }

        /// <summary>
        /// 71电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV71 { get; set; }

        /// <summary>
        /// 71电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV71QUAL { get; set; }

        /// <summary>
        /// 72电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV72 { get; set; }

        /// <summary>
        /// 72电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV72QUAL { get; set; }
    }
}