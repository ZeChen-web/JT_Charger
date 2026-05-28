using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.3.7 监控平台远程停止充电
    /// </summary>
    public class RemoteStopCharging: ASDU
    {
        /// <summary>
        ///停止原因  0 正常停机 1 服务器发现桩异常,强制停机
        /// </summary>
        [Property(0, 8)]
        public byte StopReason { get; set; }

        public RemoteStopCharging(byte stopReason)
        {
            FrameTypeNo = 48;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };
            StopReason = stopReason;
        }
    }
}