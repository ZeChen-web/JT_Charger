using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.7 远程升级-监控网关响应升级请求
    /// </summary>
    public class UpgradeRequestRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 应答结果
        /// </summary>
        [Property(8, 8)]
        public byte ResponseResult { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [Property(16, 8)]
        public byte CauseFailure { get; set; }
    }
}
