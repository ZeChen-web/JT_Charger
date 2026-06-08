using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>6.6 计费模型验证请求应答 (0x06, 下行)。
/// 平台判断当前接收的计费模型是否为桩最新的计费模型，不一致时需要向平台请求新计费模型。
/// Body 长度 10 字节。</summary>
public class V14DBillingModelVerifyResp : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；固定 0x06。</summary>
    public override byte FrameType => V14DFrameType.BillingModelVerifyResp;

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>计费模型编号，2 字节 BIN。</summary>
    public ushort ModelNo { get; set; }

    /// <summary>验证结果，1 字节 BIN；0x00=一致，0x01=不一致。</summary>
    public byte Result { get; set; }

    public V14DBillingModelVerifyResp() { }

    public V14DBillingModelVerifyResp(string pileCode, ushort modelNo, byte result)
    {
        PileCode = pileCode;
        ModelNo = modelNo;
        Result = result;
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[10];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        BitConverter.GetBytes(ModelNo).CopyTo(b, 7);
        b[9] = Result;
        return b;
    }
}
