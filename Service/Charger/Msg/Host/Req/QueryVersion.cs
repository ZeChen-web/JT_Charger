using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.11 监控平台下发版本号查询
    /// </summary>
    public class QueryVersion : ASDU
    {
        /// <summary>
        ///版本号查询 01：查询版本号
        /// </summary>
        [Property(0, 8)]
        public byte Query { get; set; }
    }
}