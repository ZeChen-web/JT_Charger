using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.3.1 监控平台鉴权认证
    /// </summary>
    public class Auth : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 客户端类型.1站控 2本地控制器 3测试客户端 4TCU模式 0未知设备
        /// </summary>
        [Property(8, 8)]
        public byte ClientType { get; set; }

        /// <summary>
        /// 连接序号
        /// </summary>
        [Property(16, 16)]
        public ushort ConnSeq { get; set; }

        /// <summary>
        /// 鉴权码字节数组
        /// </summary>
        [Property(32, 64)]
        public byte[] AuthCodes { get; set; }

        /// <summary>
        /// 鉴码KEY
        /// </summary>
        [Property(96, 8)]
        public byte AuthCodeKey { get; set; }

        public Auth(ushort connseq, byte[] authcodes, byte authcodekey)
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 24;
            ClientType = 1;
            ConnSeq = connseq;
            AuthCodes = authcodes;
            AuthCodeKey = authcodekey;
        }
    }
}