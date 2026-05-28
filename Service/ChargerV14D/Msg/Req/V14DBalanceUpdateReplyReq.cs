using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.10 余额更新应答 (0x41, 上行)</summary>
public class V14DBalanceUpdateReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BalanceUpdateReply;
    public string PileCode { get; set; } = "";
    public string PhysicalCard { get; set; } = ""; // 8B
    public byte Result { get; set; }

    public V14DBalanceUpdateReplyReq() { }
    public V14DBalanceUpdateReplyReq(byte[] body)
    {
        if (body.Length < 16) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        PhysicalCard = BitConverter.ToString(body, 7, 8).Replace("-", "");
        Result = body[15];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[16];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        var card = global::System.Text.Encoding.ASCII.GetBytes((PhysicalCard ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(card, 0, b, 7, 8);
        b[15] = Result;
        return b;
    }
}

