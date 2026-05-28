using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.10 监控网关响应尖峰平谷设置
    /// </summary>
    public class ResponseSetting : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 0成功1失败
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }
    }
}