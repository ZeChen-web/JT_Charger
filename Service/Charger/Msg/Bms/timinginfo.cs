using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 对时（时分秒年月日）
    /// </summary>
    public class TimingInfo : ASDU
    {
        /// <summary>
        /// 对时：秒
        /// </summary>
        [Property(0, 8)]
        public double TimingSecond { get; set; }

        /// <summary>
        /// 对时：分
        /// </summary>
        [Property(8, 8)]
        public double TimingMinute { get; set; }

        /// <summary>
        /// 对时：时
        /// </summary>
        [Property(16, 8)]
        public double TimingHour { get; set; }

        /// <summary>
        /// 对时：月
        /// </summary>
        [Property(24, 8)]
        public double TimingMonth { get; set; }

        /// <summary>
        /// 对时：日
        /// </summary>
        [Property(32, 8)]
        public double TimingDay { get; set; }

        /// <summary>
        /// 对时：年
        /// </summary>
        [Property(40, 8)]
        public double TimingYear { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(48, 8)]
        public double Reserved1 { get; set; }

        /// <summary>
        /// 预留位
        /// </summary>
        [Property(56, 8)]
        public double Reserved2 { get; set; }
    }
}