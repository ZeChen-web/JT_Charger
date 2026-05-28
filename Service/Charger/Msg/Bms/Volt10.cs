using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 电压数据
    /// </summary>
    public class Volt10 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV10CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV10No { get; set; }

        /// <summary>
        /// 28电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV28 { get; set; }

        /// <summary>
        /// 28电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV28QUAL { get; set; }

        /// <summary>
        /// 29电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV29 { get; set; }

        /// <summary>
        /// 29电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV29QUAL { get; set; }

        /// <summary>
        /// 30电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV30 { get; set; }

        /// <summary>
        /// 30电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV30QUAL { get; set; }
    }
}