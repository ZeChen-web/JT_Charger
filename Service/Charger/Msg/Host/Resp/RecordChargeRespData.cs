using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Resp
{
    /// <summary>
    /// 3.5.7 主动上送充电记录响应
    /// </summary>
    public class RecordChargeRespData : ASDU
    {
        /// <summary>
        /// 
        /// </summary>
        [Property(0, 8)]
        public byte Reserve1 { get; set; }

        /// <summary>
        /// 充电流水号
        /// </summary>
        [Property(8, 8)]
        public byte Reserve2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RecordChargeRespData()
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 43;
            MsgBodyCount = 1;
            TransReason = 4;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            Reserve1 = 0;
            Reserve2 = 0;
        }
    }
}