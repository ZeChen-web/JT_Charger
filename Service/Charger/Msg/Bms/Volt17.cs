using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt17 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV17CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV17No { get; set; }

        /// <summary>
        /// 49电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV49 { get; set; }

        /// <summary>
        /// 49电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV49QUAL { get; set; }

        /// <summary>
        /// 50电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV50 { get; set; }

        /// <summary>
        /// 50电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV50QUAL { get; set; }

        /// <summary>
        /// 51电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV51 { get; set; }

        /// <summary>
        /// 51电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV51QUAL { get; set; }
    }
}