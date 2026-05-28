using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.4.2 充放电设备应答站功率调节指令
    /// </summary>
    public class PowerRegulationRes : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 应答结果 0: 成功 1：失败
        /// </summary>
        [Property(8, 8)]
        public byte ResponseResult { get; set; }
        /// <summary>
        /// 失败原因 
        /// <para>0：正常</para>
        /// <para>1：参数非法</para>
        /// <para>2：双向充电设备放电中</para>
        /// <para>0xFF:其他原因</para>
        /// </summary>
        [Property(16, 8)]
        public ushort CauseFailure { get; set; }

    }
}
