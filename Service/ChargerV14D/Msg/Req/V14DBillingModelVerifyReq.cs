using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>6.5 计费模型验证请求 (0x05, 上行)</summary>
public class V14DBillingModelVerifyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BillingModelVerify;
    public string PileCode { get; set; } = "";     // 7B BCD
    public ushort ModelNo { get; set; }             // 2B BIN

    public V14DBillingModelVerifyReq() { }
    public V14DBillingModelVerifyReq(byte[] body)
    {
        if (body.Length < 9) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        ModelNo = BitConverter.ToUInt16(body, 7);
    }
    public override byte[] GetBodyBytes()
    {
        var body = new byte[9];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(body, 0);
        BitConverter.GetBytes(ModelNo).CopyTo(body, 7);
        return body;
    }
}
