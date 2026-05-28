using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.3.4 监控平台应答充电设备登录签到报文
    /// </summary>
    public class LogSignMessage : ASDU
    {
        [Property(0, 8)] public byte RecordType { get; set; }

        /// <summary>
        /// 结果  
        /// 0:成功/确认
        ///1:失败-平台处理该消息失败
        ///2:消息有误-消息校验错误/消息长度有误
        ///4:该设备编码在系统没有找到
        ///5:该设备编码在系统中异常，可能存在冲突
        ///6:充电控制器数目不对
        ///255:其它错误
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        [Property(16, 16)]
        public ushort TimeYear { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Property(32, 8)]
        public byte TimeMonth { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [Property(40, 8)]
        public byte TimeDay { get; set; }

        /// <summary>
        /// 时
        /// </summary>
        [Property(48, 8)]
        public byte TimeHour { get; set; }

        /// <summary>
        /// 分
        /// </summary>
        [Property(56, 8)]
        public byte TimeMinute { get; set; }

        /// <summary>
        /// 秒
        /// </summary>
        [Property(64, 8)]
        public byte TimeSecond { get; set; }

        /// <summary>
        /// 保留:1位, 默认0xFF
        /// </summary>
        [Property(72, 8)]
        public byte Time { get; set; }

        public LogSignMessage(byte result)
        {
            Result = result;

            CtlArea = 0;
            SrcAddr = 0;
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 4;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };
            RecordType = 12;

            DateTime dtime = DateTime.Now;
            TimeYear = Convert.ToUInt16(dtime.Year.ToString());
            TimeMonth = Convert.ToByte(dtime.Month);
            TimeDay = Convert.ToByte(dtime.Day);
            TimeHour = Convert.ToByte(dtime.Hour);
            TimeMinute = Convert.ToByte(dtime.Minute);
            TimeSecond = Convert.ToByte(dtime.Second);
        }
    }
}