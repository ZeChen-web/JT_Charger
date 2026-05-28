using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 系统总回充
    /// </summary>
    public class REChrgaccuInfo : ASDU
    {
        /// <summary>
        /// 系统总回充电量(累计)
        /// </summary>
        [Property(0, 32)]
        public float SysAccuReChrgEnergy { get; set; }

        /// <summary>
        /// 系统总回充容量(累计)
        /// </summary>
        [Property(32, 32)]
        public float SysAccuReChrgCapacity { get; set; }
    }
}