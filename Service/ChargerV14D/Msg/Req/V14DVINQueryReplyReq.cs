using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>12.2 VIN查询应答 (0xAE, 上行)</summary>
public class V14DVINQueryReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.VINQueryReply;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Result { get; set; }    // 0=失败 1=成功 0=获取 2=启动充电时无效
    public string VinCode { get; set; } = ""; // 17B ASCII

    public V14DVINQueryReplyReq() { }
    public V14DVINQueryReplyReq(byte[] body)
    {
        if (body.Length < 26) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Gun = body[7];
        Result = body[8];
        VinCode = V14DUtils.ReadAscii(body, 9, 17);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[26];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = Result;
        V14DUtils.WriteAscii(VinCode, b, 9, 17);
        return b;
    }
}
