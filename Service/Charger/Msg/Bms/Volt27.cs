using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt27 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV27CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV27No { get; set; }

        /// <summary>
        /// 79电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV79 { get; set; }

        /// <summary>
        /// 79电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV79QUAL { get; set; }

        /// <summary>
        /// 80电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV80 { get; set; }

        /// <summary>
        /// 80电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV80QUAL { get; set; }

        /// <summary>
        /// 81电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV81 { get; set; }

        /// <summary>
        /// 81电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV81QUAL { get; set; }
    }
}