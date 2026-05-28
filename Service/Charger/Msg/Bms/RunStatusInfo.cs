using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 运行状态，满充标识
    /// </summary>
    public class RunStatusInfo : ASDU
    {
        /// <summary>
        /// Pack真实SOC（高精度）
        /// </summary>
        [Property(0, 16)]
        public float RealSoc { get; set; }

        /// <summary>
        /// 直流充电请求充电充电电流
        /// </summary>
        [Property(16, 16)]
        public float ReqDCChrgCurr { get; set; }

        /// <summary>
        /// 满充标志
        /// </summary>
        [Property(32, 1)]
        public byte FullChrgFlg { get; set; }

        /// <summary>
        /// 电芯电压过低标志
        /// </summary>
        [Property(33, 1)]
        public byte CellVoltTooLowFlg { get; set; }

        /// <summary>
        /// 电芯温度完全收齐
        /// </summary>
        [Property(34, 2)]
        public byte SysCellTempRxFlg { get; set; }

        /// <summary>
        /// 电芯电压完全收齐
        /// </summary>
        [Property(36, 2)]
        public byte SysCellVoltRxFlg { get; set; }

        /// <summary>
        /// 充电使能标志
        /// </summary>
        [Property(38, 2)]
        public byte ChrgEn { get; set; }

        /// <summary>
        /// 充电故障标志
        /// </summary>
        [Property(40, 2)]
        public byte ChrgFault { get; set; }

        /// <summary>
        /// 充电停止原因
        /// </summary>
        [Property(42, 3)]
        public byte ChrgStopReason { get; set; }

        /// <summary>
        /// 低温加热状态
        /// </summary>
        [Property(45, 3)]
        public byte ChrgHeatState { get; set; }

        /// <summary>
        /// 直流充电请求充电机充电电压
        /// </summary>
        [Property(48, 16)]
        public float ReqDCChrgVolt { get; set; }
    }
}