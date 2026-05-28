using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.12 电池包上报充放电口温度（站内充电模式有电池包时周期性上传）
    /// </summary>
    public class BatteryPackPortTemperature : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// A 枪温度 1/充电连接器1 温度 1
        /// </summary>
        [Property(8, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 AGunTemperature1 { get; set; }
        /// <summary>
        /// A 枪温度 2/充电连接器1 温度2
        /// </summary>
        [Property(16, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 AGunTemperature2 { get; set; }
        /// <summary>
        /// B 枪温度 1/充电连接器2 温度 1
        /// </summary>
        [Property(24, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 BGunTemperature1 { get; set; }
        /// <summary>
        /// B 枪温度 2/充电连接器2 温度2
        /// </summary>
        [Property(32, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 BGunTemperature2 { get; set; }


    }
}