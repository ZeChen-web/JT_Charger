using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt18 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV18CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV18No { get; set; }

        /// <summary>
        /// 52电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV52 { get; set; }

        /// <summary>
        /// 52电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV52QUAL { get; set; }

        /// <summary>
        /// 53电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV53 { get; set; }

        /// <summary>
        /// 53电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV53QUAL { get; set; }

        /// <summary>
        /// 54电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV54 { get; set; }

        /// <summary>
        /// 54电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV54QUAL { get; set; }
    }
}