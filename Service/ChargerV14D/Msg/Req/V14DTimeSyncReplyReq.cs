using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>9.4 对时设置应答 (0x55, 上行)</summary>
public class V14DTimeSyncReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.TimeSyncReply;
    public string PileCode { get; set; } = "";
    public byte[] CurrentTime { get; set; } = new byte[7]; // 7B CP56Time2a
    public V14DTimeSyncReplyReq() { }
    public V14DTimeSyncReplyReq(byte[] body)
    {
        if (body.Length < 14) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Array.Copy(body, 7, CurrentTime, 0, 7);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[14];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        CurrentTime.CopyTo(b, 7);
        return b;
    }
}
