using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.12 卡数据同步应答 (0x43, 上行)</summary>
public class V14DCardSyncReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.CardSyncReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }
    /// <summary>失败原因，1 字节 BIN；成功时通常为 0x00。</summary>
    public byte FailReason { get; set; }

    public V14DCardSyncReplyReq() { }
    public V14DCardSyncReplyReq(byte[] body)
    {
        if (body.Length < 9) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Result = body[7];
        FailReason = body[8];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[9];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Result; b[8] = FailReason;
        return b;
    }
}
