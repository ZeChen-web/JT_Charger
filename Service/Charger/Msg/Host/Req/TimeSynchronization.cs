using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.13 监控平台下发同步时间
    /// </summary>
    public class TimeSynchronization : ASDU
    {
        /// <summary>
        /// 年
        /// </summary>
        [Property(0, 16)]
        public ushort timeYear { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Property(16, 8)]
        public byte timeMonth { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [Property(24, 8)]
        public byte timeDay { get; set; }

        /// <summary>
        /// 时
        /// </summary>
        [Property(32, 8)]
        public byte timeHour { get; set; }

        /// <summary>
        /// 分
        /// </summary>
        [Property(40, 8)]
        public byte timeMinute { get; set; }

        /// <summary>
        /// 秒
        /// </summary>
        [Property(48, 8)]
        public byte timeSecond { get; set; }

        /// <summary>
        /// 保留:1位, 默认0xFF
        /// </summary>
        [Property(56, 8)]
        public byte time { get; set; }
    }
}