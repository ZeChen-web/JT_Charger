using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.6.1.3 充放电机上传电压电流 SOC 数据（PGN:0x00F812）
    /// </summary>
    public class VoltageCurrentSoc : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// PGN 码
        /// </summary>
        [Property(8, 8)]
        public byte Pgn1 { get; set; }
        [Property(16, 8)]
        public byte Pgn2 { get; set; }
        [Property(24, 8)]
        public byte Pgn3 { get; set; }
        /// <summary>
        /// 电压测量值  分辨率：0.1V/位，偏移量：0V，数值范围：0V ~750V
        /// </summary>
        [Property(32, 16, scale: 0.1)]
        public float Voltage { get; set; }
        /// <summary>
        /// 电流测量值 分辨率：0.05A/位，偏移量：-1600A，数值范围：-1600A~1612.75A
        /// </summary>
        [Property(48, 16, scale: 0.05, offset: 1600)]
        public float Current { get; set; }
        /// <summary>
        /// 当前SOC 分辨率：0.1%/位，偏移量：0%，数值范围 0%~100%
        /// </summary>
        [Property(64, 16, scale: 0.1)]
        public float SOC { get; set; }
        /// <summary>
        /// 当前SOH  分辨率：1%/位，偏移量：0%，数值范围 0%~100%
        /// </summary>
        [Property(80, 8, scale: 0.01)]
        public float SOH { get; set; }
        ///// <summary>
        ///// 保留
        ///// </summary>
        //[Property(88, 8)]
        //public byte Reserve { get; set; }

    }
}
