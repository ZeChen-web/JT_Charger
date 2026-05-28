using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt03 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV3CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV3No { get; set; }

        /// <summary>
        /// 07电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV07 { get; set; }

        /// <summary>
        /// 07电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV07QUAL { get; set; }

        /// <summary>
        /// 08电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV08 { get; set; }

        /// <summary>
        /// 08电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV08QUAL { get; set; }

        /// <summary>
        /// 09电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV09 { get; set; }

        /// <summary>
        /// 09电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV09QUAL { get; set; }
    }
}