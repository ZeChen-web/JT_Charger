using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt05 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV5CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV5No { get; set; }

        /// <summary>
        /// 13电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV13 { get; set; }

        /// <summary>
        /// 13电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV13QUAL { get; set; }

        /// <summary>
        /// 14电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV14 { get; set; }

        /// <summary>
        /// 14电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV14QUAL { get; set; }

        /// <summary>
        /// 15电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV15 { get; set; }

        /// <summary>
        /// 15电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV15QUAL { get; set; }
    }
}