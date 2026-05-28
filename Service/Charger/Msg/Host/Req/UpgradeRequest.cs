using HslCommunication.BasicFramework;
using HybirdFrameworkCore.Autofac.Attribute;
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
    public class UpgradeRequest : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 执行控制 0x01：立即执行 0x02：空闲执行
        /// </summary>
        [Property(8, 8)]
        public byte ExecutionControl { get; set; }
        /// <summary>
        /// 下载超时时间
        /// </summary>
        [Property(16, 16)]
        public byte DownloadTimeout { get; set; }
        /// <summary>
        /// 版本号 ASCII 码
        /// </summary>
        [Property(32, 24)]
        public string VersionNumber { get; set; }
        /// <summary>
        /// 文件名称 HEX
        /// </summary>
        [Property(56, 160)]
        public byte[] FileNameByte
        {
            get
            {
                return Enumerable.Range(0, FileName.Length / 2)
                     .Select(i => Convert.ToByte(FileName.Substring(i * 2, 2), 16))
                     .ToArray();
            }
            set { }
        }
        /// <summary>
        /// 文件名称 HEX
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小 HEX
        /// </summary>
        [Property(216, 32)]
        public uint FileSize { get; set; }
        /// <summary>
        /// MD5校验值 HEX
        /// </summary>
        [Property(248, 128)]
        public byte[] MD5VerificationByte
        {
            get
            {
                return Enumerable.Range(0, MD5Verification.Length / 2)
                     .Select(i => Convert.ToByte(MD5Verification.Substring(i * 2, 2), 16))
                     .ToArray();
            }
            set { }
        }
        public string MD5Verification { get; set; }
        /// <summary>
        /// URL（文件路径）ASCII码
        /// </summary>
        [Property(376, 1600)]
        public string URL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionControl">执行控制 0x01：立即执行 0x02：空闲执行</param>
        /// <param name="downloadTimeout">下载超时时间</param>
        /// <param name="versionNumber">版本号</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="mD5Verification">MD5校验值</param>
        /// <param name="url">URL（文件路径）</param>
        public UpgradeRequest(byte executionControl, byte downloadTimeout, string versionNumber,
            string fileName, uint fileSize, string mD5Verification, string url)
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 33;

            ExecutionControl = executionControl;
            DownloadTimeout = downloadTimeout;
            VersionNumber = versionNumber;
            FileName = fileName;
            FileSize = fileSize;
            MD5Verification = mD5Verification;
            URL = url;
        }
    }
}
