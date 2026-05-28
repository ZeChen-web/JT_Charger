using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt19 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV19CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV19No { get; set; }

        /// <summary>
        /// 55电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV55 { get; set; }

        /// <summary>
        /// 55电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV55QUAL { get; set; }

        /// <summary>
        /// 56电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV56 { get; set; }

        /// <summary>
        /// 56电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV56QUAL { get; set; }

        /// <summary>
        /// 57电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV57 { get; set; }

        /// <summary>
        /// 57电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV57QUAL { get; set; }
    }
}