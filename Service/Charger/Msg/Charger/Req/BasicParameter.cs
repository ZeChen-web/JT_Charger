using AutoMapper.Execution;
using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static COSXML.Model.Tag.ListBucketVersions;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.6.2.2 充放电机上传基本参数 2（PGN:0x00F802）
    /// </summary>
    public class BasicParameter : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// PGN 码
        /// </summary>
        [Property(8, 8)]
        public byte Pgn1 { get; set; }
        [Property(16, 8)]
        public byte Pgn2 { get; set; }
        [Property(24, 8)]
        public byte Pgn3 { get; set; }
        /// <summary>
        /// 电池编码
        /// </summary>
        [Property(32, 24, PropertyReadConstant.Byte)]
        public byte[] BatteryCode { get; set; }
        /// <summary>
        /// 产权标识
        /// </summary>
        [Property(224, 8)]
        public byte TitleMarke { get; set; }
        /// <summary>
        /// 电池成组厂商
        /// </summary>
        [Property(232, 32)]
        public string Batterymanufacturers { get; set; }
        /// <summary>
        /// 电池成组生产日期：年
        /// </summary>
        [Property(264, 8)]
        public byte GroupYear { get; set; }
        /// <summary>
        /// 电池成组生产日期：月
        /// </summary>
        [Property(272, 8)]
        public byte GroupMonth { get; set; }
        /// <summary>
        /// 电池成组生产日期：日
        /// </summary>
        [Property(280, 8)]
        public byte GroupDay { get; set; }
        /// <summary>
        /// 电池电芯生产厂商
        /// </summary>
        [Property(288, 32)]
        public byte[] BatteryManufacturer { get; set; }
        /// <summary>
        /// 电池电芯生产日期：年
        /// </summary>
        [Property(320, 8)]
        public byte CellYear { get; set; }
        /// <summary>
        /// 电池电芯生产日期：月
        /// </summary>
        [Property(328, 8)]
        public byte CellMonth { get; set; }
        /// <summary>
        /// 电池电芯生产日期：日
        /// </summary>
        [Property(336, 8)]
        public byte CellDay { get; set; }
        /// <summary>
        /// 电池箱电子控制单元生产厂商
        /// </summary>
        [Property(344, 32)]
        public byte[] Manufacturer { get; set; }
        /// <summary>
        /// 电池箱电子控制单元硬件版本
        /// </summary>
        [Property(376, 8)]
        public byte HardwareVersion { get; set; }
        /// <summary>
        /// 电池箱电子控制单元软件版本
        /// </summary>
        [Property(384, 8)]
        public byte SoftwareVersion { get; set; }
        /// <summary>
        /// 电池包序列号
        /// </summary>
        [Property(392, 32)]
        public byte[] SerialNumber { get; set; }
        /// <summary>
        /// 电池包型号
        /// </summary>
        [Property(424, 64)]
        public byte[] ModelNumber { get; set; }


    }
}
