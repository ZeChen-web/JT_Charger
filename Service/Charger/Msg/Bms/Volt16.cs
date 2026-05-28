using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt16 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV16CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV16No { get; set; }

        /// <summary>
        /// 46电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV46 { get; set; }

        /// <summary>
        /// 46电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV46QUAL { get; set; }

        /// <summary>
        ///47电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV47 { get; set; }

        /// <summary>
        /// 47电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV47QUAL { get; set; }

        /// <summary>
        /// 48电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV48 { get; set; }

        /// <summary>
        /// 48电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV48QUAL { get; set; }
    }
}