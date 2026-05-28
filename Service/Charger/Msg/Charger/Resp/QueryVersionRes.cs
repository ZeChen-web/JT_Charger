using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.12 充放电机应答版本号
    /// </summary>
    public class QueryVersionRes : ASDU
    {
        /// <summary>
        ///软件版本号
        /// </summary>
        [Property(0, 8)]
        public byte SoftwareVersion0 { get; set; }

        [Property(8, 8)] public byte SoftwareVersion1 { get; set; }
        [Property(16, 8)] public byte SoftwareVersion2 { get; set; }

        /// <summary>
        ///硬件版本号
        /// </summary>
        [Property(24, 8)]
        public byte HardwareVersion0 { get; set; }

        [Property(32, 8)] public byte HardwareVersion1 { get; set; }
        [Property(40, 8)] public byte HardwareVersion2 { get; set; }

        /// <summary>
        ///预留
        /// </summary>
        [Property(48, 8)]
        public byte Remark0 { get; set; }

        [Property(56, 8)] public byte Remark1 { get; set; }
        [Property(64, 8)] public byte Remark2 { get; set; }
    }
}