using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo1 : ASDU
    {
        /// <summary>
        /// 小计站外插枪充电能量
        /// </summary>
        [Property(0, 24)]
        public float SubTotalChgEngyOutStat { get; set; }

        /// <summary>
        /// 小计站外回充能量(动能回馈充电）
        /// </summary>
        [Property(24, 24)]
        public float SubTotalReChgeEngyOutStat { get; set; }

        /// <summary>
        /// 小计站内充电能量
        /// </summary>
        [Property(48, 16)]
        public float SubTotalchgEngyInStat { get; set; }
    }
}