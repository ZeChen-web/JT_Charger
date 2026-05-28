using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.5.2 监控平台心跳应答
    /// </summary>
    public class HeartBeatRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 结果 0 正常 1 未注册
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        public HeartBeatRes(byte result)
        {
            Result = result;

            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 0;
            TransReason = 4;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 14;
        }
    }
}