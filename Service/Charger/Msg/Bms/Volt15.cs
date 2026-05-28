using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt15 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV15CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV15No { get; set; }

        /// <summary>
        /// 43电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV43 { get; set; }

        /// <summary>
        /// 43电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV43QUAL { get; set; }

        /// <summary>
        /// 44电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV44 { get; set; }

        /// <summary>
        /// 44电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV44QUAL { get; set; }

        /// <summary>
        /// 45电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV45 { get; set; }

        /// <summary>
        /// 45电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV45QUAL { get; set; }
    }
}