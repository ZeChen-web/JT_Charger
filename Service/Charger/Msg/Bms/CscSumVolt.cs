using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// CSC单体电压
    /// </summary>
    public class CscSumVolt : ASDU
    {
        /// <summary>
        /// 电压数据CRC
        /// </summary>
        [Property(0, 8)]
        public byte CSCSumVCRC { get; set; }

        /// <summary>
        /// CSC号
        /// </summary>
        [Property(8, 6)]
        public byte CSCSumVNo { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(14, 2)]
        public byte CSCSumVReserved1 { get; set; }

        /// <summary>
        /// CSC最高单体电压
        /// </summary>
        [Property(16, 16)]
        public ushort CSCMaxCellVolt { get; set; }

        /// <summary>
        /// CSC最低单体电压
        /// </summary>
        [Property(32, 16)]
        public ushort CSCMinCellVolt { get; set; }

        /// <summary>
        /// CSC单体平均电压
        /// </summary>
        [Property(48, 16)]
        public ushort CSCCellAvgVolt { get; set; }
    }
}