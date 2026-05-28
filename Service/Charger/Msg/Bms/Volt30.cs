using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt30 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV30CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV30No { get; set; }

        /// <summary>
        /// 88电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV88 { get; set; }

        /// <summary>
        /// 88电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV88QUAL { get; set; }

        /// <summary>
        /// 89电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV89 { get; set; }

        /// <summary>
        /// 89电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV89QUAL { get; set; }

        /// <summary>
        /// 90电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV90 { get; set; }

        /// <summary>
        /// 90电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV90QUAL { get; set; }
    }
}