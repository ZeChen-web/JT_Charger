using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.3.8 充放电机响应远程停止充电
    /// </summary>
    public class RemoteStopChargingRes : ASDU
    {
        /// <summary>
        /// 结果码.0成功 1设备已停机 0xFF 其他
        /// </summary>
        [Property(0, 8)]
        public byte Result { get; set; }
    }
}