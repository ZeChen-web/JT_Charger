using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Temp6 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT6CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT6No { get; set; }

        /// <summary>
        /// 电芯温度26数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT26QUAL { get; set; }

        /// <summary>
        /// 电芯温度27数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT27QUAL { get; set; }

        /// <summary>
        /// 电芯温度28数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT28QUAL { get; set; }

        /// <summary>
        /// 电芯温度29数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT29QUAL { get; set; }

        /// <summary>
        /// 电芯温度30数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT30QUAL { get; set; }

        /// <summary>
        /// 温度26
        /// </summary>
        [Property(24, 8)]
        public byte CSCT26 { get; set; }

        /// <summary>
        /// 温度27
        /// </summary>
        [Property(32, 8)]
        public byte CSCT27 { get; set; }

        /// <summary>
        /// 温度28
        /// </summary>
        [Property(40, 8)]
        public byte CSCT28 { get; set; }

        /// <summary>
        /// 温度29
        /// </summary>
        [Property(48, 8)]
        public byte CSCT29 { get; set; }

        /// <summary>
        /// 温度30
        /// </summary>
        [Property(56, 8)]
        public byte CSCT30 { get; set; }
    }
}