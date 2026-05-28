using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.12 卡数据同步应答 (0x43, 上行)</summary>
public class V14DCardSyncReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.CardSyncReply;
    public string PileCode { get; set; } = "";
    public byte Result { get; set; }
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
