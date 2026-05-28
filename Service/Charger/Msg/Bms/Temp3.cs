using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class Temp3 : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCT3CRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCT3No { get; set; }

        /// <summary>
        /// 电芯温度11数据有效性
        /// </summary>
        [Property(14, 2)]
        public byte CSCT11QUAL { get; set; }

        /// <summary>
        /// 电芯温度12数据有效性
        /// </summary>
        [Property(16, 2)]
        public byte CSCT12QUAL { get; set; }

        /// <summary>
        /// 电芯温度13数据有效性
        /// </summary>
        [Property(18, 2)]
        public byte CSCT13QUAL { get; set; }

        /// <summary>
        /// 电芯温度14数据有效性
        /// </summary>
        [Property(20, 2)]
        public byte CSCT14QUAL { get; set; }

        /// <summary>
        /// 电芯温度15数据有效性
        /// </summary>
        [Property(22, 2)]
        public byte CSCT15QUAL { get; set; }

        /// <summary>
        /// 温度11
        /// </summary>
        [Property(24, 8)]
        public byte CSCT11 { get; set; }

        /// <summary>
        /// 温度12
        /// </summary>
        [Property(32, 8)]
        public byte CSCT12 { get; set; }

        /// <summary>
        /// 温度13
        /// </summary>
        [Property(40, 8)]
        public byte CSCT13 { get; set; }

        /// <summary>
        /// 温度14
        /// </summary>
        [Property(48, 8)]
        public byte CSCT14 { get; set; }

        /// <summary>
        /// 温度15
        /// </summary>
        [Property(56, 8)]
        public byte CSCT15 { get; set; }
    }
}