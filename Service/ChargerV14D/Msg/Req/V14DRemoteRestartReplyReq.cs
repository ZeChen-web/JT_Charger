using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>11.2 远程重启应答 (0x91, 上行)</summary>
public class V14DRemoteRestartReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteRestartReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }
    public V14DRemoteRestartReplyReq() { }
    public V14DRemoteRestartReplyReq(byte[] body)
    {
        if (body.Length < 8) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Result = body[7];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Result;
        return b;
    }
}
