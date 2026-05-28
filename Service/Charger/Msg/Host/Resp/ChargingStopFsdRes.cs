using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.3.12 监控平台应答停止完成帧
    /// </summary>
    public class ChargingStopFsdRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///成功标识  0:成功；1:失败
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        public ChargingStopFsdRes(byte result)
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 4;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 0x04;
            Result = result;
        }
    }
}