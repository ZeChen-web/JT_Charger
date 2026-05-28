using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 电芯温度
    /// </summary>
    public class Temp1 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT1CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT1No { get; set; }

        /// <summary>
        /// 电芯温度1数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT01QUAL { get; set; }

        /// <summary>
        /// 电芯温度2数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT02QUAL { get; set; }

        /// <summary>
        /// 电芯温度3数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT03QUAL { get; set; }

        /// <summary>
        /// 电芯温度4数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT04QUAL { get; set; }

        /// <summary>
        /// 电芯温度5数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT05QUAL { get; set; }

        /// <summary>
        /// 温度1
        /// </summary>
        [Property(24, 8)]
        public byte CSCT01 { get; set; }

        /// <summary>
        /// 温度2
        /// </summary>
        [Property(32, 8)]
        public byte CSCT02 { get; set; }

        /// <summary>
        /// 温度3
        /// </summary>
        [Property(40, 8)]
        public byte CSCT03 { get; set; }

        /// <summary>
        /// 温度4
        /// </summary>
        [Property(48, 8)]
        public byte CSCT04 { get; set; }

        /// <summary>
        /// 温度5
        /// </summary>
        [Property(56, 8)]
        public byte CSCT05 { get; set; }
    }
}