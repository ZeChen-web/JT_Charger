using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.7 监控平台下发掉线停止充电  0：不使能 1：使能
    /// </summary>
    public class OfflineStopCharging : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///应答结果 0：不使能 1：使能
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        [Property(16, 16)]
        public ushort Remark { get; set; }

        public OfflineStopCharging(byte enabled)
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 43;
            Result = enabled;
        }
    }
}