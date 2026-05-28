using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt11 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV11CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV11No { get; set; }

        /// <summary>
        /// 31电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV31 { get; set; }

        /// <summary>
        /// 31电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV31QUAL { get; set; }

        /// <summary>
        /// 32电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV32 { get; set; }

        /// <summary>
        /// 32电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV32QUAL { get; set; }

        /// <summary>
        /// 33电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV33 { get; set; }

        /// <summary>
        /// 33电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV33QUAL { get; set; }
    }
}