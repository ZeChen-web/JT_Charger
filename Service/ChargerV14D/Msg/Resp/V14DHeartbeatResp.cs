using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>
/// 6.4 心跳包应答 (帧类型 0x04, 下行)
/// </summary>
public class V14DHeartbeatResp : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.HeartbeatResp;

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN。</summary>
    public byte GunNo { get; set; }

    /// <summary>应答结果，1 字节 BIN；心跳应答默认置 0。</summary>
    public byte Response { get; set; } = 0;

    public V14DHeartbeatResp() { }

    public V14DHeartbeatResp(string pileCode, byte gunNo, byte response = 0)
    {
        PileCode = pileCode;
        GunNo = gunNo;
        Response = response;
    }

    public V14DHeartbeatResp(byte[] body)
    {
        if (body.Length < 9) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        GunNo = body[7];
        Response = body[8];
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[9];
        var pileCodeBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileCodeBcd, 0, body, 0, 7);
        body[7] = GunNo;
        body[8] = Response;
        return body;
    }
}
