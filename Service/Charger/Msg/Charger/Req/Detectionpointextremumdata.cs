using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.6.1.5 充放电机上传温度检测点极值数据（PGN:0x00F823）
    /// </summary>
    public class Detectionpointextremumdata : ASDU
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
        /// 最高温度
        /// 分辨率：1℃/位，偏移量：-50℃，数值范围：-50℃~200℃
        /// </summary>
        [Property(32, 8, offset: 50)]
        public short MaximumTemp { get; set; }
        /// <summary>
        /// 最高温度检测点编号
        /// 分辨率：1/位，偏移量：0，数值范围1~250
        /// </summary>
        [Property(40, 8)]
        public byte MaximumTempNum { get; set; }
        /// <summary>
        /// 最低温度
        /// 分辨率：1℃/位，偏移量：-50℃，数值范围：-50℃~200℃
        /// </summary>
        [Property(48, 8, offset: 50)]
        public short MinimumTemp { get; set; }
        /// <summary>
        /// 最低温度检测点编号
        /// 分辨率：1℃/位，偏移量：0，数值范围：1~250
        /// </summary>
        [Property(56, 8, offset: 50)]
        public short MinimumTempNum { get; set; }
        /// <summary>
        /// 连接器总正极柱温度
        /// 分辨率：1℃/位，偏移量：-50℃，数值范围：-50℃~200℃
        /// </summary>
        [Property(64, 8, offset: 50)]
        public short PositiveTemp { get; set; }
        /// <summary>
        /// 连接器总负极柱温度
        /// 分辨率：1℃/位，偏移量：-50℃，数值范围：-50℃~200℃
        /// </summary>
        [Property(72, 8, offset: 50)]
        public short NegativeTemp { get; set; }
        /// <summary>
        /// 单体平均温度
        /// 分辨率：1℃/位，偏移量：-50℃，数值范围：-50℃~200℃
        /// </summary>
        [Property(80, 8, offset: 50)]
        public short MonomerMeantemp { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        [Property(88, 8)]
        public byte Reserve { get; set; }
    }
}
