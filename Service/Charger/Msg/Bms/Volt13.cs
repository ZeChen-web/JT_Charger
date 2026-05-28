using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt13 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV13CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV13No { get; set; }

        /// <summary>
        /// 37电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV37 { get; set; }

        /// <summary>
        /// 37电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV37QUAL { get; set; }

        /// <summary>
        /// 38电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV38 { get; set; }

        /// <summary>
        /// 38电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV38QUAL { get; set; }

        /// <summary>
        /// 39电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV39 { get; set; }

        /// <summary>
        /// 39电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV39QUAL { get; set; }
    }
}