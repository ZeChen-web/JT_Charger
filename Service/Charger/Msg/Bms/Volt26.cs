using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt26 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV26CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV26No { get; set; }

        /// <summary>
        /// 76电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV76 { get; set; }

        /// <summary>
        /// 76电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV76QUAL { get; set; }

        /// <summary>
        /// 77电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV77 { get; set; }

        /// <summary>
        /// 77电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV77QUAL { get; set; }

        /// <summary>
        /// 78电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV78 { get; set; }

        /// <summary>
        /// 78电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV78QUAL { get; set; }
    }
}