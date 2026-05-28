using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.10 电池包实时单体温度&单体电压数据（站内充电模式有电池包时周期性上传）
    /// </summary>
    public class BatteryPackDataVoltage : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 电芯温度最大值
        /// </summary>
        [Property(8, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 CellTemperatureMax { get; set; }
        /// <summary>
        /// 电芯温度最小值
        /// </summary>
        [Property(16, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 CellTemperatureMin { get; set; }
        /// <summary>
        /// 电芯温度平均值
        /// </summary>
        [Property(24, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 CellTemperatureAverage { get; set; }
        /// <summary>
        /// 电芯温度最大值所在 CSC 编号
        /// </summary>
        [Property(32, 8 )]
        public byte CellTemperatureCSCNumber { get; set; }
        /// <summary>
        /// 电芯温度最大值所在 CSC 内温度探针编号
        /// </summary>
        [Property(40, 8 )]
        public byte CellTemperatureCSCProbeNumber { get; set; }
        /// <summary>
        /// 电芯温度最小值所在 CSC 编号
        /// </summary>
        [Property(48, 8)]
        public byte CellTemperatureMinCSCNumber { get; set; }
        /// <summary>
        /// 电芯温度最小值所在 CSC 内温度探针编号
        /// </summary>
        [Property(56, 8)]
        public byte CellTemperatureMinCSCProbeNumber { get; set; }

        /// <summary>
        /// 电芯电压最大值
        /// </summary>
        [Property(64, 16, PropertyReadConstant.Bit, 0.001, 3)]
        public float CellVoltageMax { get; set; }
        /// <summary>
        /// 电芯电压最大值所在 CSC 编号
        /// </summary>
        [Property(80, 8)]
        public byte CellVoltageCSCNumber { get; set; }
        /// <summary>
        /// 电芯电压最大值所在 CSC 内的单体编号
        /// </summary>
        [Property(88, 8)]
        public byte CellVoltageCSCProbeNumber { get; set; }
        /// <summary>
        /// 电芯电压平均值
        /// </summary>
        [Property(96, 16, PropertyReadConstant.Bit, 0.001, 3)]
        public float CellVoltageAverage { get; set; }
        /// <summary>
        /// 电芯电压最小值
        /// </summary>
        [Property(112, 8, PropertyReadConstant.Bit, 0.001, 3)]
        public float CellVoltageMin { get; set; }
        /// <summary>
        /// 电芯电压最小值所在 CSC 编号
        /// </summary>
        [Property(128, 8)]
        public byte CellVoltageMinCSCNumber { get; set; }
        /// <summary>
        /// 电芯电压最小值所在 CSC 内的单体编号
        /// </summary>
        [Property(136, 8)]
        public byte CellVoltageMinCSCProbeNumber { get; set; }


    }
}