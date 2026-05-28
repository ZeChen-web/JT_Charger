using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt01 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV1CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV1No { get; set; }

        /// <summary>
        /// 01电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV01 { get; set; }

        /// <summary>
        /// 01电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV01QUAL { get; set; }

        /// <summary>
        /// 02电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV02 { get; set; }

        /// <summary>
        /// 02电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV02QUAL { get; set; }

        /// <summary>
        /// 03电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV03 { get; set; }

        /// <summary>
        /// 03电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV03QUAL { get; set; }
    }
}