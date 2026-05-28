using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.1 充电设备心跳上报
    /// </summary>
    public class HeartBeat : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///心跳计数 范围:1-0xFF
        /// </summary>
        [Property(8, 8)]
        public byte BeatCount { get; set; }

        /// <summary>
        ///桩状态 0:待机状态; 1:服务状态; 2:故障状态; 3:停止服务状态
        /// </summary>
        [Property(16, 8)]
        public byte Status { get; set; }
    }
}