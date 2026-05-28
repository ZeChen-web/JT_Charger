using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>
/// 6.3 充电桩心跳包 (帧类型 0x03, 上行, 10秒周期)
/// </summary>
public class V14DHeartbeatReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.Heartbeat;

    /// <summary>桩编码 (7字节BCD)</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号</summary>
    public byte GunNo { get; set; }

    /// <summary>枪状态 0x00=正常 0x01=故障</summary>
    public byte GunStatus { get; set; }

    public V14DHeartbeatReq() { }

    public V14DHeartbeatReq(byte[] body)
    {
        if (body.Length < 9) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        GunNo = body[7];
        GunStatus = body[8];
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[9];
        var pileCodeBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileCodeBcd, 0, body, 0, 7);
        body[7] = GunNo;
        body[8] = GunStatus;
        return body;
    }
}
