using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.15 充放电机应答程序升级指令
    /// </summary>
    public class UpgradeRes : ASDU
    {
        /// <summary>
        /// 设备类型 1：充放电机；2：BMS
        /// </summary>
        [Property(0, 8)]
        public byte DeviceType { get; set; }

        /// <summary>
        /// 响应码 1-立即执行 2-稍后执行 3-下载地址无法解析 4-版本一致，无需升级
        /// </summary>
        [Property(8, 8)]
        public byte ResponseCode { get; set; }
    }
}