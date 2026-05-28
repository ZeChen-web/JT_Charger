using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.8 充放电机应答监控平台掉线停止充电
    /// 帧类型	45	记录类型	44
    /// </summary>
    public class OfflineStopChargingRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 应答结果
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }
    }
}