using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt14 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV14CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV14No { get; set; }

        /// <summary>
        /// 40电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV40 { get; set; }

        /// <summary>
        /// 40电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV40QUAL { get; set; }

        /// <summary>
        /// 41电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV41 { get; set; }

        /// <summary>
        /// 41电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV41QUAL { get; set; }

        /// <summary>
        /// 42电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV42 { get; set; }

        /// <summary>
        /// 42电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV42QUAL { get; set; }
    }
}