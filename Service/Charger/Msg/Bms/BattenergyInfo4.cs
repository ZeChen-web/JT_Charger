using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo4 : ASDU
    {
        /// <summary>
        /// 系统站内放电电能量（累计）
        /// </summary>
        [Property(0, 32)]
        public float SysAccuDisChrgEngyInStat { get; set; }

        /// <summary>
        /// 系统站内充电能量（累计）
        /// </summary>
        [Property(0, 32)]
        public float SysAccuChrgEngyInStat { get; set; }
    }
}