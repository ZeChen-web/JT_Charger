using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    public class InquireGatewaySpikesValleys : ASDU
    {
        /// <summary>
        /// 保留
        /// </summary>
        [Property(0, 8)]
        public byte Retain { get; set; }
    }
}