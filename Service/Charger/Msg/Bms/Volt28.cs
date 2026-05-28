using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt28 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV28CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV28No { get; set; }

        /// <summary>
        /// 82电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV82 { get; set; }

        /// <summary>
        /// 82电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV82QUAL { get; set; }

        /// <summary>
        /// 83电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV83 { get; set; }

        /// <summary>
        /// 83电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV83QUAL { get; set; }

        /// <summary>
        /// 84电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV84 { get; set; }

        /// <summary>
        /// 84电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV84QUAL { get; set; }
    }
}