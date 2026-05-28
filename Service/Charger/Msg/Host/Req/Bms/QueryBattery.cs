using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req.Bms
{
    /// <summary>
    /// 3.4.1 监控平台发送功率调节指令
    /// </summary>
    public class QueryBattery : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// PGN 码
        /// </summary>
        [Property(8, 3, PropertyReadConstant.Byte)]
        public byte[] Pgn { get; set; } = { 0x00, 0xf8, 0x2c };

        /// <summary>
        /// 要查询的PGN
        /// </summary>
        [Property(32, 3, PropertyReadConstant.Byte)]
        public byte[] QueryPgn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryPgn"></param>
        public QueryBattery(byte[] queryPgn)
        {
            FrameTypeNo = 46;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 80;
            QueryPgn = queryPgn;
        }
    }
}