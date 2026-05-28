using DotNetty.Codecs.Mqtt.Packets;
using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.7 远程升级-站级监控响应升级完成确认帧
    /// </summary>
    public class UplinkUpgradeRes : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 应答结果 默认 0
        /// </summary>
        public byte ResponseResult { get; set; }

        public UplinkUpgradeRes()
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 36;

            RecordType = 0;
        }
    }
}
