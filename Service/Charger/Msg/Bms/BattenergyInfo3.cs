using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo3 : ASDU
    {
        /// <summary>
        /// 系统站外插枪充电能量（累计）
        /// </summary>
        [Property(0, 32)]
        public float SysAccuChrgEngyOutStat { get; set; }

        /// <summary>
        /// 电池包行驶里程（累计）
        /// </summary>
        [Property(32, 32)]
        public float SysAccuBattOdometer { get; set; }
    }
}