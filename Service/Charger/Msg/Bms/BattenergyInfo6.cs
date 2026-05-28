using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo6 : ASDU
    {
        /// <summary>
        /// 系统站外回充能量（累计）
        /// </summary>
        [Property(0, 32)]
        public float SysAccuReChrgEngyOutStat { get; set; }

        /// <summary>
        /// 系统站外放电能量（累计）
        /// </summary>
        [Property(32, 32)]
        public float SysAccuDisChrgEngyOutStat { get; set; }
    }
}