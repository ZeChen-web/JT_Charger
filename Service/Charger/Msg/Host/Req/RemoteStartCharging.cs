using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.3.5 监控平台远程启动充电
    /// </summary>
    public class RemoteStartCharging: ASDU
    {
        public RemoteStartCharging(byte socLimit, byte changePowerCmdType, float changePower,
            string chargeOrderNo)
        {
            FrameTypeNo = 47;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            SocLimit = socLimit;
            ChangePowerCmdType = changePowerCmdType;
            ChangePower = changePower;
            ChargeOrderNo = chargeOrderNo;
        }

        /// <summary>
        /// SOC 限制
        /// </summary>
        [Property(0, 8)]
        public byte SocLimit { get; set; }

        /// <summary>
        /// 功率调节指令类型
        /// </summary>
        [Property(8, 8)]
        public byte ChangePowerCmdType { get; set; } = 1;

        /// <summary>
        /// 功率调节参数
        /// </summary>
        [Property(16, 16, PropertyReadConstant.Bit, 0.1, 1)]
        public float ChangePower { get; set; }

        /// <summary>
        /// 充电流水号
        /// </summary>
        [Property(32, 256)]
        public string ChargeOrderNo { get; set; }
    }
}