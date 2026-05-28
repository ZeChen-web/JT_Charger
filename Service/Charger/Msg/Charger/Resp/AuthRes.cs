using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.3.2 充放电机应答鉴权认证
    /// </summary>
    public class AuthRes : ASDU

    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 连接序号
        /// </summary>
        [Property(8, 16)]
        public ushort ConnSeq { get; set; }

        /// <summary>
        /// 鉴权结果
        /// </summary>
        [Property(24, 8)]
        public byte AuthResult { get; set; }

        /// <summary>
        ///  失败原因
        /// </summary>
        [Property(32, 8)]
        public byte FailReason { get; set; }
    }
}