using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>
/// 6.4 心跳包应答 (帧类型 0x04, 下行)
/// </summary>
public class V14DHeartbeatResp : V14DFrame
{
    public override byte FrameType => V14DFrameType.HeartbeatResp;

    /// <summary>桩编码 (7字节BCD)</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号</summary>
    public byte GunNo { get; set; }

    /// <summary>心跳应答 (置0)</summary>
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
