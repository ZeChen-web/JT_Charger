using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Temp4 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT4CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT4No { get; set; }

        /// <summary>
        /// 电芯温度16数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT16QUAL { get; set; }

        /// <summary>
        /// 电芯温度17数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT17QUAL { get; set; }

        /// <summary>
        /// 电芯温度18数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT18QUAL { get; set; }

        /// <summary>
        /// 电芯温度19数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT19QUAL { get; set; }

        /// <summary>
        /// 电芯温度20数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT20QUAL { get; set; }

        /// <summary>
        /// 温度16
        /// </summary>
        [Property(24, 8)]
        public byte CSCT16 { get; set; }

        /// <summary>
        /// 温度17
        /// </summary>
        [Property(32, 8)]
        public byte CSCT17 { get; set; }

        /// <summary>
        /// 温度18
        /// </summary>
        [Property(40, 8)]
        public byte CSCT18 { get; set; }

        /// <summary>
        /// 温度19
        /// </summary>
        [Property(48, 8)]
        public byte CSCT19 { get; set; }

        /// <summary>
        /// 温度20
        /// </summary>
        [Property(56, 8)]
        public byte CSCT20 { get; set; }
    }
}