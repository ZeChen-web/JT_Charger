using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo2 : ASDU
    {
        /// <summary>
        /// 小计站外放电能量
        /// </summary>
        [Property(0, 24)]
        public float SubTotalDischgEngyOutStat { get; set; }

        /// <summary>
        /// 小计站内放电能量
        /// </summary>
        [Property(24, 24)]
        public float SubTotalDischgEngyInStat { get; set; }

        /// <summary>
        /// 小计电池包行驶里程
        /// </summary>
        [Property(48, 16)]
        public float SubTotalBattOdometer { get; set; }
    }
}