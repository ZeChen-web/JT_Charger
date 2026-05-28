using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Temp2 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT2CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT2No { get; set; }

        /// <summary>
        /// 电芯温度6数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT06QUAL { get; set; }

        /// <summary>
        /// 电芯温度7数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT07QUAL { get; set; }

        /// <summary>
        /// 电芯温度8数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT08QUAL { get; set; }

        /// <summary>
        /// 电芯温度9数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT09QUAL { get; set; }

        /// <summary>
        /// 电芯温度10数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT10QUAL { get; set; }

        /// <summary>
        /// 温度6
        /// </summary>
        [Property(24, 8)]
        public byte CSCT06 { get; set; }

        /// <summary>
        /// 温度7
        /// </summary>
        [Property(32, 8)]
        public byte CSCT07 { get; set; }

        /// <summary>
        /// 温度8
        /// </summary>
        [Property(40, 8)]
        public byte CSCT08 { get; set; }

        /// <summary>
        /// 温度9
        /// </summary>
        [Property(48, 8)]
        public byte CSCT09 { get; set; }

        /// <summary>
        /// 温度10
        /// </summary>
        [Property(56, 8)]
        public byte CSCT10 { get; set; }
    }
}