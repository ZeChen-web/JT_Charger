using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 电池包状态信息展示
    /// </summary>
    public class Status : ASDU
    {
        /// <summary>
        /// SBMU状态报文的CRC
        /// </summary>
        [Property(0, 8)]
        public byte SBMUCRC { get; set; }

        /// <summary>
        /// SBMU生命信号,0~15循环
        /// </summary>
        [Property(8, 4)]
        public byte SBMUALIV { get; set; }

        /// <summary>
        /// SBMU高压上电状态(软件驱动状态)
        /// </summary>
        [Property(12, 2)]
        public byte HVPwrOnStatus { get; set; }


        /// <summary>
        /// SBMU高压下电请求
        /// </summary>
        [Property(14, 2)]
        public byte HVPwrOffRuquest { get; set; }


        /// <summary>
        /// SBMU低压下电准备状态
        /// </summary>
        [Property(16, 2)]
        public byte LVPwrOffReady { get; set; }


        /// <summary>
        /// SBMU地址
        /// </summary>
        [Property(18, 4)]
        public byte SbmuAddress { get; set; }


        /// <summary>
        /// DC充电控制电流请求模式
        /// </summary>
        [Property(22, 2)]
        public byte ReqDCChrgMode { get; set; }


        /// <summary>
        /// 高精度显示SOC
        /// </summary>
        [Property(24, 16)]
        public float PackDispSOC { get; set; }

        /// <summary>
        /// 真实SOH
        /// </summary>
        [Property(40, 8)]
        public byte PackRealSOH { get; set; }


        /// <summary>
        /// 故障等级
        /// </summary>
        [Property(48, 4)]
        public byte SysFltLvl { get; set; }


        /// <summary>
        /// 主回路允许上高压
        /// </summary>
        [Property(52, 2)]
        public byte MainAllowPwrOn { get; set; }

        /// <summary>
        /// 辅件回路允许上高压
        /// </summary>
        [Property(54, 2)]
        public byte AuxAllowPwrOn { get; set; }

        /// <summary>
        /// 直流充电请求停止标志
        /// </summary>
        [Property(56, 1)]
        public byte ReqDCChrgStop { get; set; }

        /// <summary>
        /// BMS内部唯一识别故障码（故障列表）
        /// </summary>
        [Property(57, 7)]
        public byte StsSysFltID { get; set; }
    }
}