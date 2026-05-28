using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.6.1.4 充放电机上传单体动力蓄电池电压极值统计（PGN:0x00F822）
    /// </summary>
    public class VoltageExtremumStatistics : ASDU
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
        /// 单体蓄电池或蓄电池模块最高电压
        /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
        /// </summary>
        [Property(32, 16, scale: 0.01)]
        public float MaximumVoltage { get; set; }
        /// <summary>
        /// 最高电压单体蓄电池或蓄电池模块的编号
        /// 分辨率：1/位，偏移量：0，数值范围：
        /// </summary>
        [Property(48, 8)]
        public float MaximumVoltageNum { get; set; }
        /// <summary>
        /// 单体蓄电池或蓄电池模块最低电压
        /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
        /// </summary>
        [Property(56, 16, scale: 0.01)]
        public float MinimumVoltage { get; set; }
        /// <summary>
        /// 最低电压单体蓄电池或蓄电池模块的编号
        /// 分辨率：1/位，偏移量：0，数值范围：1~250
        /// </summary>
        [Property(72, 8)]
        public float MinimumVoltageNum { get; set; }
        /// <summary>
        /// 单体平均电压
        /// 分辨率：0.01V/位，偏移量：0V，数值范围：0V ~24V
        /// </summary>
        [Property(80, 16)]
        public float CellAverageVoltage { get; set; }
    }
}
