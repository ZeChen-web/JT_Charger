using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.19 充放电上报交流电表数据（交流电表接到充电机上的情况）
    /// </summary>
    public class AcMeter : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 交流进线 A 相电压
        /// </summary>
        [Property(8, 16, scale: 0.1)]
        public float PhaseVoltageA { get; set; }
        /// <summary>
        /// 交流进线 B 相电压
        /// </summary>
        [Property(24, 16, scale: 0.1)]
        public float PhaseVoltageB { get; set; }
        /// <summary>
        /// 交流进线 C 相电压
        /// </summary>
        [Property(40, 16, scale: 0.1)]
        public float PhaseVoltageC { get; set; }
        /// <summary>
        /// 交流进线 A 相电流
        /// </summary>
        [Property(56, 16, scale: 0.1)]
        public float PhaseCurrentA { get; set; }
        /// <summary>
        /// 交流进线 B 相电流
        /// </summary>
        [Property(72, 16, scale: 0.1)]
        public float PhaseCurrentB { get; set; }
        /// <summary>
        /// 交流进线 C 相电流
        /// </summary>
        [Property(88, 16, scale: 0.1)]
        public float PhaseCurrentC { get; set; }
        /// <summary>
        /// 交流表总电量
        /// </summary>
        [Property(104, 32, scale: 0.1)]
        public float AcMeterTotalPower { get; set; }
    }
}
