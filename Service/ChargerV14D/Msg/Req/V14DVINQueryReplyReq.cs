using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>12.2 VIN查询应答 (0xAE, 上行)</summary>
public class V14DVINQueryReplyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.VINQueryReply;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }
    /// <summary>车辆 VIN，17 字节 ASCII，右补 0。</summary>
    public string VinCode { get; set; } = "";

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
