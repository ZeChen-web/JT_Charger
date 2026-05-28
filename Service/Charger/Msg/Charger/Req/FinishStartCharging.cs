using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.3.9 充放电机上送充电启动完成帧
    /// </summary>
    public class FinishStartCharging : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 成功标识
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [Property(16, 8)]
        public byte FailReason { get; set; }

        /// <summary>
        /// BMS 与充电桩通信协议版本号
        /// </summary>
        [Property(24, 8)]
        public byte ConnProtocolVersion0 { get; set; }

        [Property(32, 8)] public byte ConnProtocolVersion1 { get; set; }
        [Property(40, 8)] public byte ConnProtocolVersion2 { get; set; }

        /// <summary>
        /// 充电桩与BMS 握手结果
        /// </summary>
        [Property(48, 8)]
        public byte HandshakeResult { get; set; }

        /// <summary>
        /// 电池类型
        /// </summary>
        [Property(56, 8)]
        public byte BatteryType { get; set; }

        /// <summary>
        /// 最高允许温度
        /// </summary>
        [Property(64, 8, PropertyReadConstant.Bit, 1, 0, 50)]
        public Int16 MaxAllowTemp { get; set; }

        /// <summary>
        /// BMS最高允许充电电压
        /// </summary>
        [Property(72, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float BmsMaxAllowVoltage { get; set; }

        /// <summary>
        /// 单体最高允许充电电压
        /// </summary>
        [Property(88, 16, PropertyReadConstant.Bit, 0.01, 2, 0)]
        public float SingleMaxAllowVoltage { get; set; }

        /// <summary>
        /// 最高允许充电电流
        /// </summary>
        [Property(104, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
        public float MaxAllowCurrent { get; set; }

        /// <summary>
        /// 整车动力蓄电池额定总电压
        /// </summary>
        [Property(120, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float VehiclePowerBatteryTotalVoltage { get; set; }

        /// <summary>
        /// 整车动力蓄电池当前电压
        /// </summary>
        [Property(136, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float VehiclePowerBatteryCurrentVoltage { get; set; }

        /// <summary>
        /// 整车动力蓄电池额定容量
        /// </summary>
        [Property(152, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float VehiclePowerBatteryRatedCapacity { get; set; }

        /// <summary>
        ///整车动力蓄电池标称容量
        /// </summary>
        [Property(168, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float VehiclePowerBatteryNormalCapacity { get; set; }

        /// <summary>
        ///充电机最高输出电压
        /// </summary>
        [Property(184, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float ChargerMaxOutputVoltage { get; set; }

        /// <summary>
        ///充电机最低输出电压
        /// </summary>
        [Property(200, 16, PropertyReadConstant.Bit, 0.1, 1, 0)]
        public float ChargerMinOutputVoltage { get; set; }

        /// <summary>
        ///充电机最大输出电流
        /// </summary>
        [Property(216, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
        public float ChargerMaxOutputCurrent { get; set; }

        /// <summary>
        ///充电机最小输出电流
        /// </summary>
        [Property(232, 16, PropertyReadConstant.Bit, 0.1, 1, 400)]
        public float ChargerMinOutputCurrent { get; set; }

        /// <summary>
        /// 车辆识别码
        /// </summary>
        [Property(31, 17, PropertyReadConstant.Byte)]
        public byte[] Vin { get; set; }

        /// <summary>
        /// 车辆识别码
        /// </summary>
        public string VehIdeNum { get; set; }

        /// <summary>
        /// 整车动力蓄电 池荷电状态
        /// </summary>
        [Property(384, 8, PropertyReadConstant.Bit, 0.01, 2, 0)]
        public byte ChargeState { get; set; }

    }
}