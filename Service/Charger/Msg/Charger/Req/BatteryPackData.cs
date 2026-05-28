using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.9 电池包实时数据上报（站内充电模式有电池包时周期性上传）
    /// </summary>
    public class BatteryPackData : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 电池包 SOC
        /// </summary>
        [Property(8, 8, PropertyReadConstant.Bit, 0.4, 1)]
        public float SOC { get; set; }
        /// <summary>
        /// 电池包 SOH
        /// </summary>
        [Property(16, 8, PropertyReadConstant.Bit, 0.4, 1)]
        public float SOH { get; set; }
        /// <summary>
        /// 电池包总电流，充电为负值，放电为正
        /// </summary>
        [Property(24, 16, PropertyReadConstant.Bit, 0.1, 1, 1000)]
        public float TotalCurrent { get; set; }
        /// <summary>
        /// 电池包允许最大回充电电流值(脉冲)
        /// </summary>
        [Property(40, 16, PropertyReadConstant.Bit, 0.1, 1)]
        public float AllowableMaxBackCurrent { get; set; }

        /// <summary>
        /// 电池包允许最大放电电流值(脉冲)
        /// </summary>
        [Property(56, 16, PropertyReadConstant.Bit, 0.1, 1)]
        public float AllowableMaxPutCurrent { get; set; }
        /// <summary>
        /// 电池包正极绝缘值
        /// </summary>
        [Property(72, 16)]
        public UInt16 PositiveInsulationValue { get; set; }
        /// <summary>
        /// 电池包负极绝缘值
        /// </summary>
        [Property(88, 16)]
        public UInt16 NegativeInsulationValue { get; set; }
        /// <summary>
        /// 电池端高压(主继电器内侧)
        /// </summary>
        [Property(104, 16)]
        public UInt16 BatteryHighVoltage { get; set; }
        /// <summary>
        /// 母线端高压(主继电器外侧) Busbar 
        /// </summary>
        [Property(120, 16)]
        public UInt16 BusbarHighVoltage { get; set; }

    }
}