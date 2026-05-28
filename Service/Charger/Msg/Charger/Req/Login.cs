using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.3.3 充放电机登陆签到
    /// </summary>
    public class Login : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 监控网关编号
        /// </summary>
        [Property(8, 16)]
        public UInt16 GatewayNo { get; set; }

        /// <summary>
        /// 设备属性
        /// </summary>
        [Property(24, 8)]
        public byte EquipType { get; set; }

        /// <summary>
        /// 通讯协议版本
        /// </summary>
        [Property(32, 8)] public byte ConnProtocolVersion0 { get; set; }
        [Property(40, 8)] public byte ConnProtocolVersion1 { get; set; }
        [Property(48, 8)] public byte ConnProtocolVersion2 { get; set; }
        public string ConnProtocolVersion { get; set; }

        /// <summary>
        /// 充电控制器硬件版本号
        /// </summary>
        [Property(56, 8)] public byte ControllerHardwareVersion0 { get; set; }
        [Property(64, 8)] public byte ControllerHardwareVersion1 { get; set; }
        [Property(72, 8)] public byte ControllerHardwareVersion2 { get; set; }
        public string ControllerHardwareVersion { get; set; }

        /// <summary>
        /// 充电控制器软件版本
        /// </summary>
        [Property(80, 8)] public byte ControllerSoftwareVersion0 { get; set; }
        [Property(88, 8)] public byte ControllerSoftwareVersion1 { get; set; }
        [Property(96, 8)] public byte ControllerSoftwareVersion2 { get; set; }
        public string ControllerSoftwareVersion { get; set; }

        /// <summary>
        /// 充电枪口数目
        /// </summary>
        [Property(104, 8)]
        public byte GunNum { get; set; }

        /// <summary>
        /// 充电模块数目
        /// </summary>
        [Property(112, 8)]
        public byte GunModuleNum { get; set; }

        /// <summary>
        /// 额定功率
        /// </summary>
        [Property(128, 16)]
        public ushort RatedPower { get; set; }

        /// <summary>
        /// 当前功率
        /// </summary>
        [Property(136, 8)]
        public byte CurrentPower { get; set; }

        /// <summary>
        /// 当前速率
        /// </summary>
        [Property(144, 8)]
        public byte CurrentSpeed { get; set; }

        /// <summary>
        /// 分流器量程
        /// </summary>
        [Property(152, 16)]
        public ushort DiverterRange { get; set; }
    }
}