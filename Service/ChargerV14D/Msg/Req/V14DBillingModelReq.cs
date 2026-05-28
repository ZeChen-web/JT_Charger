using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>6.7 充电桩计费模型请求 (0x09, 上行)</summary>
public class V14DBillingModelReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BillingModelReq;
    public string PileCode { get; set; } = ""; // 7B BCD

    public V14DBillingModelReq() { }
    public V14DBillingModelReq(byte[] body) { PileCode = V14DUtils.BcdToString(body, 0, 7); }
    public override byte[] GetBodyBytes() => V14DUtils.StringToBcd(PileCode, 7);
}
