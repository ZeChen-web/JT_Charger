using AutoMapper.Execution;
using HybirdFrameworkCore.Autofac.Attribute;
using Service.Charger.Msg.Host.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.7 远程升级-监控网关上送升级完成确认帧
    /// </summary>
    public class UplinkUpgrade : ASDU
    { /// <summary>
      ///     记录类型
      /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 当前版本号
        /// </summary>
        [Property(8, 8)]
        public byte CurrentVersionNumberByte1 { get; set; }
        [Property(16, 8)]
        public byte CurrentVersionNumberByte2 { get; set; }
        [Property(24, 8)]
        public byte CurrentVersionNumberByte3 { get; set; }
        public string CurrentVersionNumber
        {
            get
            {
                return CurrentVersionNumberByte1 + "." + CurrentVersionNumberByte2 + "." + CurrentVersionNumberByte3;
            }
            set { }
        }
        /// <summary>
        /// 原来版本号
        /// </summary>
        [Property(32, 8)]
        public byte OriginalVersionNumberByte1 { get; set; }
        [Property(40, 8)]
        public byte OriginalVersionNumberByte2 { get; set; }
        [Property(48, 8)]
        public byte OriginalVersionNumberByte3 { get; set; }
        public string OriginalVersionNumber
        {
            get
            {
                return OriginalVersionNumberByte1 + "." + OriginalVersionNumberByte2 + "." + OriginalVersionNumberByte3;
            }
            set { }
        }
        /// <summary>
        /// 升级结果 0: 成功; 1: 失败
        /// </summary>
        [Property(56, 32)]
        public uint UpgradeResultByte { get; set; }
    }
}
