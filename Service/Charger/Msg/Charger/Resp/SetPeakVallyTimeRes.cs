using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.11 监控网关响应尖峰平谷设置
    /// 帧类型	45	记录类型	48
    /// </summary>
    public class SetPeakVallyTimeRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 应答结果
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }
    }
}