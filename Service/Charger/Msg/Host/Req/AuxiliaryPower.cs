using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.3 监控平台下发辅源控制指令
    /// </summary>
    public class AuxiliaryPower : ASDU
    {
        /// <summary>
        ///     记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        ///打开辅助电源标志 1：电池包辅助电源导通 0：电池包辅助电源断开
        /// </summary>
        [Property(8, 8)]
        public byte OpenFlag { get; set; }

        public AuxiliaryPower(byte openFlag) 
        {
            PackLen = 0;
            CtlArea = 0;
            SrcAddr = 0;

            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 9;

            OpenFlag = openFlag;
        }
    }
}