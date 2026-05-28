using HslCommunication.BasicFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.7 远程升级-站级监控升级请求下发
    /// </summary>
    public class Upgrade_request : ASDU
    {
        /// <summary>
        /// 执行控制 0x01：立即执行 0x02：空闲执行
        /// </summary>
        public byte ExecutionControl { get; set; }
        /// <summary>
        /// 下 载 超 时时间
        /// </summary>
        public byte DownloadTimeout { get; set; }
        /// <summary>
        /// 版本号 ASCII 码
        /// </summary>
        public string VersionNumber { get; set; }
        /// <summary>
        /// 文件名称 HEX
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小 HEX
        /// </summary>
        public string fileSize { get; set; }
        /// <summary>
        /// MD5 校验值 HEX
        /// </summary>
        public string MD5Verification { get; set; }
        /// <summary>
        /// URL（文件路径）ASCII码
        /// </summary>
        public string URL { get; set; }
    }
}
