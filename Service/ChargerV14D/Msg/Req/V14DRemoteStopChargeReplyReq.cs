using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.6 远程停机命令回复 (0x35, 上行)</summary>
public class V14DRemoteStopChargeReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteStopChargeReply;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Result { get; set; }
    public byte FailReason { get; set; }

    public V14DRemoteStopChargeReplyReq() { }
    public V14DRemoteStopChargeReplyReq(byte[] body)
    {
        if (body.Length < 10) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Gun = body[7];
        Result = body[8];
        FailReason = body[9];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[10];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = Result; b[9] = FailReason;
        return b;
    }
}
