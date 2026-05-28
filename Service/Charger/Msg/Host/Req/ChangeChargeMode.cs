using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.12 站控设备切换站内/站外充电切换
    /// </summary>
    public class ChangeChargeMode : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 00:无效 01:站内 02:站外
        /// </summary>
        [Property(8, 16)]
        public short ChargeMode { get; set; }

        public ChangeChargeMode(byte chargeMode)
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 49;
            ChargeMode = chargeMode;
        }
    }
}