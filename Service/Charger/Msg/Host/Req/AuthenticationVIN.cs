using DotNetty.Codecs.Mqtt.Packets;
using HybirdFrameworkCore.Autofac.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.5.16 站控下发 VIN 鉴权的结果
    /// </summary>
    public class AuthenticationVIN:ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// VIN 鉴权结果 1:通过 2 不通过
        /// </summary>
        [Property(8, 8)]
        public byte VINResult { get; set; }

        public AuthenticationVIN(byte vinresult) 
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 4;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 51;

            VINResult = vinresult;
        }
    }
}
