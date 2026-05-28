using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>6.6 计费模型验证请求应答 (0x06, 下行)</summary>
public class V14DBillingModelVerifyResp : V14DFrame
{
    public override byte FrameType => V14DFrameType.BillingModelVerifyResp;
    public string PileCode { get; set; } = "";
    public ushort ModelNo { get; set; }
    public byte Result { get; set; } // 0x00一致 0x01不一致

    public V14DBillingModelVerifyResp() { }
    public V14DBillingModelVerifyResp(string pileCode, ushort modelNo, byte result)
    { PileCode = pileCode; ModelNo = modelNo; Result = result; }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[10];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        BitConverter.GetBytes(ModelNo).CopyTo(b, 7);
        b[9] = Result;
        return b;
    }
}
