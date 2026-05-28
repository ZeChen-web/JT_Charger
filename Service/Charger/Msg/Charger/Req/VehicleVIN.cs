using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.14 充电机上报车辆 VIN
    /// </summary>
    public class VehicleVIN : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// VIN
        /// </summary>
        [Property(8, 17, type: PropertyReadConstant.Byte)]
        public string Vin { get; set; }
    }
}
