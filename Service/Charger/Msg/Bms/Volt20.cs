using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Volt20 : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCV20CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 8)]
        public byte CSCV20No { get; set; }

        /// <summary>
        /// 58电池单体电压
        /// </summary>
        [Property(16, 14)]
        public ushort CSCV58 { get; set; }

        /// <summary>
        /// 58电池单体电压QUAL
        /// </summary>
        [Property(30, 2)]
        public byte CSCV58QUAL { get; set; }

        /// <summary>
        /// 59电池单体电压
        /// </summary>
        [Property(32, 14)]
        public ushort CSCV59 { get; set; }

        /// <summary>
        /// 59电池单体电压QUAL
        /// </summary>
        [Property(46, 2)]
        public byte CSCV59QUAL { get; set; }

        /// <summary>
        /// 60电池单体电压
        /// </summary>
        [Property(48, 14)]
        public ushort CSCV60 { get; set; }

        /// <summary>
        /// 60电池单体电压QUAL
        /// </summary>
        [Property(62, 2)]
        public byte CSCV60QUAL { get; set; }
    }
}