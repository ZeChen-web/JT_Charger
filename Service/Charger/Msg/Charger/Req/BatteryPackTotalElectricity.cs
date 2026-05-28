using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.11 电池包上报累计充放电电量（站内充电模式有电池包时周期性上传
    /// </summary>
    public class BatteryPackTotalElectricity : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 累计充电电量
        /// </summary>
        [Property(8, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float TotalElectricCharge { get; set; }
        /// <summary>
        /// 累计放电电量
        /// </summary>
        [Property(40, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float TotalElectricDischarge { get; set; }
        /// <summary>
        /// 单次充电电量
        /// </summary>
        [Property(72, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float OnceElectricCharge { get; set; }
        /// <summary>
        /// 累计动能回馈充电电量
        /// </summary>
        [Property(88, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float TotalFeedbackElectricCharge { get; set; }
        /// <summary>
        /// 累计换电电量
        /// </summary>
        [Property(120, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float TotalElectricSwap { get; set; }
        /// <summary>
        /// 累计插枪充电电量
        /// </summary>
        [Property(152, 32, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float TotalElectricInsertCharge { get; set; }


    }
}