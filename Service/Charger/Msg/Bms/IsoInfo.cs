using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// HVB绝缘检测
    /// </summary>
    public class IsoInfo : ASDU
    {
        /// <summary>
        /// HVB绝缘检测数据的CRC
        /// </summary>
        [Property(0, 8)]
        public byte ISOCRC { get; set; }

        /// <summary>
        /// HVB绝缘检测数据的生命信号，0~15循环
        /// </summary>
        [Property(8, 4)]
        public byte ISOALIV { get; set; }

        /// <summary>
        /// HVB绝缘采样电压采样有效标志
        /// </summary>
        [Property(12, 1)]
        public byte ISOVoltVld { get; set; }

        /// <summary>
        /// 绝缘采样使能状态0-禁止1-使能
        /// </summary>
        [Property(13, 1)]
        public byte ISOISOSampEnStatus { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(14, 2)]
        public byte ISOReserved1 { get; set; }

        /// <summary>
        /// 主正对机壳的有效绝缘阻值
        /// </summary>
        [Property(16, 16)]
        public ushort ISOIsoResPos { get; set; }

        /// <summary>
        /// 主负对机壳的有效绝缘阻值
        /// </summary>
        [Property(32, 16)]
        public ushort ISOIsoResNeg { get; set; }

        /// <summary>
        /// 发给直流充电机剩余充电时间
        /// </summary>
        [Property(48, 16)]
        public ushort ReminChrgTime { get; set; }
    }
}