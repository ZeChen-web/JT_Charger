using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Temp5 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT5CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT5No { get; set; }

        /// <summary>
        /// 电芯温度21数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT21QUAL { get; set; }

        /// <summary>
        /// 电芯温度22数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT22QUAL { get; set; }

        /// <summary>
        /// 电芯温度23数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT23QUAL { get; set; }

        /// <summary>
        /// 电芯温度24数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT24QUAL { get; set; }

        /// <summary>
        /// 电芯温度25数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT25QUAL { get; set; }

        /// <summary>
        /// 温度21
        /// </summary>
        [Property(24, 8)]
        public byte CSCT21 { get; set; }

        /// <summary>
        /// 温度22
        /// </summary>
        [Property(32, 8)]
        public byte CSCT22 { get; set; }

        /// <summary>
        /// 温度23
        /// </summary>
        [Property(40, 8)]
        public byte CSCT23 { get; set; }

        /// <summary>
        /// 温度24
        /// </summary>
        [Property(48, 8)]
        public byte CSCT24 { get; set; }

        /// <summary>
        /// 温度25
        /// </summary>
        [Property(56, 8)]
        public byte CSCT25 { get; set; }
    }
}