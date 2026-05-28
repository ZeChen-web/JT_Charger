using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Resp
{
    /// <summary>
    /// 3.5.17 充电机应答 VIN 鉴权结果
    /// </summary>
    public class AuthVINRes : ASDU
    {

        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
    }
}
