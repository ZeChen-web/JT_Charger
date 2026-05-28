using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt22 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV22CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV22No { get; set; }

        /// <summary>
        /// 64电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV64 { get; set; }

        /// <summary>
        /// 64电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV64QUAL { get; set; }

        /// <summary>
        /// 65电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV65 { get; set; }

        /// <summary>
        /// 65电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV65QUAL { get; set; }

        /// <summary>
        /// 66电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV66 { get; set; }

        /// <summary>
        /// 66电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV66QUAL { get; set; }
    }
}