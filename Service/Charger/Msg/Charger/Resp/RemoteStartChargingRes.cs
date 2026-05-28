using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.3.6 充放电机响应远程启动充电
    /// </summary>
    public class RemoteStartChargingRes : ASDU
    {
        /// <summary>
        /// 启动结果.0成功 1失败
        /// </summary>
        [Property(0, 8)]
        public byte Result { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [Property(8, 8)]
        public byte FailReason { get; set; }
    }
}