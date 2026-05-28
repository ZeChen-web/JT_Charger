using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.3.10 监控平台应答充电启动完成帧
    /// </summary>
    public class StartChargingFinishedRes : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 成功标识 0:成功；1:失败
        /// </summary>
        [Property(8, 8)]
        public byte Result { get; set; }

        /// <summary>
        /// 失败原因
        /// 0:成功
        ///1:交易流水号数据异常
        ///2:充电方式数据异常
        ///3:其他数据异常
        ///4:服务器异常
        ///5:服务器繁忙
        ///6:枪编号非法
        ///7:服务器判定启动完成帧超时
        ///0xFF:其他错误
        /// </summary>
        [Property(16, 8)]
        public byte FailReason { get; set; }

        public StartChargingFinishedRes(byte result, byte failreason)
        {
            Result = result;
            FailReason = failreason;

            RecordType = 0x02;
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 4;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };
        }
    }
}