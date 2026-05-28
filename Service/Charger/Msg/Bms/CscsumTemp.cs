using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// CSC单体温度
    /// </summary>
    public class CscSumTemp : ASDU
    {
        /// <summary>
        /// 温度数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCSumTCRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCSumTNo { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(14, 2)]
        public byte CSCSumTReserved1 { get; set; }

        /// <summary>
        /// CSC最高单体温度
        /// </summary>
        [Property(16, 8)]
        public byte CSCMaxCellTemp { get; set; }

        /// <summary>
        /// CSC最低单体温度
        /// </summary>
        [Property(24, 8)]
        public byte CSCMinCellTemp { get; set; }

        /// <summary>
        /// CSC单体平均温度
        /// </summary>
        [Property(32, 8)]
        public byte CSCAvgCellTemp { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(48, 16)]
        public int CSCSumTReserved2 { get; set; }
    }
}