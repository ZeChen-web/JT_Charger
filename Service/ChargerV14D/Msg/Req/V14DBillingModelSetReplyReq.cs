using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>9.6 计费模型设置应答 (0x57, 上行)</summary>
public class V14DBillingModelSetReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BillingModelSetReply;
    public string PileCode { get; set; } = "";
    public byte Result { get; set; } // 0x00失败 0x01成功
    public V14DBillingModelSetReplyReq() { }
    public V14DBillingModelSetReplyReq(byte[] body)
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
