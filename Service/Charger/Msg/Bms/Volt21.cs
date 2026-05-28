using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt21 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV21CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV21No { get; set; }

        /// <summary>
        /// 16电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV61 { get; set; }

        /// <summary>
        /// 16电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV61QUAL { get; set; }

        /// <summary>
        /// 62电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV62 { get; set; }

        /// <summary>
        /// 62电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV62QUAL { get; set; }

        /// <summary>
        /// 63电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV63 { get; set; }

        /// <summary>
        /// 63电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV63QUAL { get; set; }
    }
}