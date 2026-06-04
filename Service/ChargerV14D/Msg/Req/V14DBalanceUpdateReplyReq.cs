using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.10 余额更新应答 (0x41, 上行)</summary>
public class V14DBalanceUpdateReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BalanceUpdateReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>物理卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string PhysicalCard { get; set; } = "";
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
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

