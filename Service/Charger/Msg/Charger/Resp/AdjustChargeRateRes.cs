using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.4 充放电机应答辅助控制
    /// </summary>
    public class AdjustChargeRateRes : ASDU
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
    }
}