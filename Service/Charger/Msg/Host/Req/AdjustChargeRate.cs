using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.5 监控平台下发充电速率设置
    /// </summary>
    public class AdjustChargeRate : ASDU
    {
        /// <summary>
        /// 倍率 例如，0.单5C位该0值.1C为 5 ,1C 时该值为 10
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///期望运行 功率
        /// </summary>
        [Property(8, 16, scale: 0.1)]
        public float ChargeRage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chargeRage"></param>
        public AdjustChargeRate(float chargeRage)
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 20;
            ChargeRage = chargeRage;
        }
    }
}