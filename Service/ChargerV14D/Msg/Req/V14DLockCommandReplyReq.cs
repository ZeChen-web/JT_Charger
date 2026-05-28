using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>10.3 充电桩返回数据 (0x63, 上行)</summary>
public class V14DLockCommandReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.LockCommandReply;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Result { get; set; }

    public V14DLockCommandReplyReq() { }
    public V14DLockCommandReplyReq(byte[] body)
    {
        if (body.Length < 9) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Gun = body[7];
        Result = body[8];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[9];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = Result;
        return b;
    }
}
