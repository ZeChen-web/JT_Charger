using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.1 监控平台发送功率调节指令
    /// </summary>
    public class PowerRegulation : ASDU
    {

        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 期望运行功率
        /// </summary>
        [Property(8, 16, scale: 0.1)]
        public float ExpectedOperatingPower { get; set; }

        public PowerRegulation(float expectedOperatingPower)
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 5;

            ExpectedOperatingPower = expectedOperatingPower;
        }
    }
}
