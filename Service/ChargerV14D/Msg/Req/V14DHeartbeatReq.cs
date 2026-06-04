using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>
/// 6.3 充电桩心跳包 (帧类型 0x03, 上行, 10秒周期)
/// </summary>
public class V14DHeartbeatReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.Heartbeat;

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN。</summary>
    public byte GunNo { get; set; }

    /// <summary>枪状态，1 字节 BIN；表示当前充电枪工作状态。</summary>
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
