using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 充放电电流限制显示
    /// </summary>
    public class PackCurrlmt : ASDU
    {
        /// <summary>
        /// 允许脉冲放电电流
        /// </summary>
        [Property(0, 16)]
        public float AllwPulseDischrgCurr { get; set; }

        /// <summary>
        /// 允许脉冲回充电流
        /// </summary>
        [Property(16, 16)]
        public float AllwPulseRechrgCurr { get; set; }

        /// <summary>
        /// 允许持续放电电流
        /// </summary>
        [Property(32, 16)]
        public float AllwContiDischrgCurr { get; set; }

        /// <summary>
        /// 允许持续回充电流
        /// </summary>
        [Property(48, 16)]
        public float AllwContiRechrgCurr { get; set; }
    }
}