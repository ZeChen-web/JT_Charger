using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.13 响应站控设备切换站内/站外充电
    /// 帧类型	45	记录类型	50
    /// </summary>
    public class ChangeChargeModeRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        ///应答结果 0: 成功 1：失败
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        /// <summary>
        ///失败原因 0：正常1：电池包/或者充电枪未连接 0xFF：其他
        /// </summary>
        [Property(16, 16)]
        public ushort FailReason { get; set; }
    }
}