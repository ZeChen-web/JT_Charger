using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.6 远程停机命令回复 (0x35, 上行)</summary>
public class V14DRemoteStopChargeReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteStopChargeReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }
    /// <summary>失败原因，1 字节 BIN；成功时通常为 0x00。</summary>
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
