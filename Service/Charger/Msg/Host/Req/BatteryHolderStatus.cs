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
    /// 3.5.5 监控平台下发电池仓的状态
    /// </summary>
    public class BatteryHolderStatus:ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// 是否有电池 0:无电池 1：有电池
        /// </summary>
        [Property(8, 8)]
        public byte Battery { get; set; }
        /// <summary>
        /// 电接头连接状态 0:未连接 1: 已连接
        /// </summary>
        [Property(16, 8)]
        public byte ConnectionState { get; set; }
        /// <summary>
        /// 水接头状态 0:未连接 1: 已连接
        /// </summary>
        [Property(24, 8)]
        public byte WaterCondition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Battery"></param>
        /// <param name="ConnectionState"></param>
        /// <param name="WaterCondition"></param>
        public BatteryHolderStatus(byte battery, byte connectionState, byte waterCondition)
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 15;

            Battery = battery;
            ConnectionState = connectionState;
            WaterCondition = waterCondition;
        }
    }
}
