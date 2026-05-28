using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.1 监控平台发送功率调节指令
    /// </summary>
    public class AdjustPower : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///期望运行 功率
        /// </summary>
        [Property(8, 16, PropertyReadConstant.Bit, 0.1, 1)]
        public ushort ExpectPower { get; set; }

        public AdjustPower(ushort expectPower)
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 5;
            ExpectPower = expectPower;
        }
    }
}