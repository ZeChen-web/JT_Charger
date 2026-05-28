using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 系统总放电
    /// </summary>
    public class DisChrgaccuInfo : ASDU
    {
        /// <summary>
        /// 系统总放电电量(累计)
        /// </summary>
        [Property(0, 32)]
        public float SysAccuDischrgEnergy { get; set; }

        /// <summary>
        /// 系统总放电容量(累计)
        /// </summary>
        [Property(32, 32)]
        public float SysAccuDischrgCapacity { get; set; }
    }
}