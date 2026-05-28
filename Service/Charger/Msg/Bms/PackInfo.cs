using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class PackInfo : ASDU
    {
        /// <summary>
        /// SBMU状态报文的CRC
        /// </summary>
        [Property(0, 8)]
        public byte PackInfoCRC { get; set; }

        /// <summary>
        /// SBMU生命信号,0~15循环
        /// </summary>
        [Property(8, 4)]
        public byte PackInfoALIV { get; set; }

        /// <summary>
        /// 继电器内侧高压_(正值为正向电压，负值为反向电压)
        /// </summary>
        [Property(12, 16)]
        public float BattInVolt { get; set; }

        /// <summary>
        /// 箱体累加电压
        /// </summary>
        [Property(28, 16)]
        public float PackCumulVolt { get; set; }

        /// <summary>
        /// 箱体电流
        /// </summary>
        [Property(44, 16)]
        public float PackCurr { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(60, 4)]
        public byte PackInfoReserved1 { get; set; }
    }
}