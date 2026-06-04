using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>6.7 充电桩计费模型请求 (0x09, 上行)</summary>
public class V14DBillingModelReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BillingModelReq;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    public V14DBillingModelReq() { }
    public V14DBillingModelReq(byte[] body) { PileCode = V14DUtils.BcdToString(body, 0, 7); }
    public override byte[] GetBodyBytes() => V14DUtils.StringToBcd(PileCode, 7);
}
