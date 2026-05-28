using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.5.10 站控响应充放电机上报模块状态
    /// </summary>
    public class UpModelStationRespData : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; } = 76;

        //结果  0正常
        [Property(8, 8)] public byte RespResult { get; set; }

        public UpModelStationRespData(byte respresult)
        {
            RespResult = respresult;

            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };
        }
    }
}