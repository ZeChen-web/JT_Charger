using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.18 充放电机上报模块状态
    /// </summary>
    public class ModuleState : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 模块总数量
        /// </summary>
        [Property(8, 8)]
        public byte ModelCount { get; set; }
        /// <summary>
        /// 模块状态
        /// </summary>
        [RelativeProperty(16, 8, "ModelCount")]
        public List<ModelCount> LstModelCount { get; set; }
    }

    public class ModelCount
    {
        /// <summary>
        /// 当前 CSC 下的第 N 个模块状态
        /// </summary>
        [Property(0, 8)]
        public byte ModelState { get; set; }
    }
}
