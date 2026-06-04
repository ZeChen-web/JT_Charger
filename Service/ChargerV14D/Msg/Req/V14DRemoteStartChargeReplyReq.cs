using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.4 远程启动充电命令回复 (0x33, 上行)</summary>
public class V14DRemoteStartChargeReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteStartChargeReply;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }
    /// <summary>失败原因，1 字节 BIN；成功时通常为 0x00。</summary>
    public byte FailReason { get; set; }

    public V14DRemoteStartChargeReplyReq() { }
    public V14DRemoteStartChargeReplyReq(byte[] body)
    {
        if (body.Length < 26) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        Result = body[o++];
        FailReason = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[26];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = Result; b[o] = FailReason;
        return b;
    }
}
