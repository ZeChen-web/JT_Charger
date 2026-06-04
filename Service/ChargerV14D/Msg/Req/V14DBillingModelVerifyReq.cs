using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>6.5 计费模型验证请求 (0x05, 上行)</summary>
public class V14DBillingModelVerifyReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BillingModelVerify;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>计费模型编号，2 字节 BIN。</summary>
    public ushort ModelNo { get; set; }

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
