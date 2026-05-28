using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.9 监控平台下发站外允许充电 SOC
    /// </summary>
    public class OutSideAllowChargingSoc : ASDU
    {
        /// <summary>
        /// SOC 限制值
        /// </summary>
        [Property(0, 8)]
        public byte SocValue { get; set; }

    }
}