using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt25 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV25CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV25No { get; set; }

        /// <summary>
        /// 73电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV73 { get; set; }

        /// <summary>
        /// 73电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV73QUAL { get; set; }

        /// <summary>
        /// 74电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV74 { get; set; }

        /// <summary>
        /// 74电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV74QUAL { get; set; }

        /// <summary>
        /// 75电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV75 { get; set; }

        /// <summary>
        /// 75电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV75QUAL { get; set; }
    }
}