using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>11.5 二维码应答 (0x9B, 上行)</summary>
public class V14DQRCodeReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.QRCodeReply;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Result { get; set; } // 0x00失败 0x01成功
    public V14DQRCodeReplyReq() { }
    public V14DQRCodeReplyReq(byte[] body)
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
