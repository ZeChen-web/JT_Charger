using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>9.4 对时设置应答 (0x55, 上行)</summary>
public class V14DTimeSyncReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.TimeSyncReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>当前时间，7 字节 CP56Time2a 格式。</summary>
    public byte[] CurrentTime { get; set; } = new byte[7];
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
