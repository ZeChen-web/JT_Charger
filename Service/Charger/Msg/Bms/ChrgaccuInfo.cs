using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 系统总插枪充电
    /// </summary>
    public class ChrgaccuInfo : ASDU
    {
        /// <summary>
        /// 系统总插枪充电电量(累计) 
        /// </summary>
        [Property(0, 32)]
        public float SysAccuChrgEnergy { get; set; }

        /// <summary>
        /// 系统总插枪充电容量(累计)
        /// </summary>
        [Property(32, 32)]
        public float SysAccuChrgCapacity { get; set; }
    }

    public class INFO
    {
        public ChrgaccuInfo ChrgaccuInfo { get; set; }
        public DisChrgaccuInfo DisChrgaccuInfo { get; set; }
        public REChrgaccuInfo REChrgaccuInfo { get; set; }
        public IsoInfo IsoInfo { get; set; }
    }
}