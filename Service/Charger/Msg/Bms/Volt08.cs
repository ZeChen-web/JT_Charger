using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt08 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV8CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV8No { get; set; }

        /// <summary>
        /// 22电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV22 { get; set; }

        /// <summary>
        /// 22电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV22QUAL { get; set; }

        /// <summary>
        /// 23电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV23 { get; set; }

        /// <summary>
        /// 23电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV23QUAL { get; set; }

        /// <summary>
        /// 24电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV24 { get; set; }

        /// <summary>
        /// 24电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV24QUAL { get; set; }
    }
}