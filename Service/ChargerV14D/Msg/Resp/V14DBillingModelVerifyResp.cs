using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>6.6 计费模型验证请求应答 (0x06, 下行)</summary>
public class V14DBillingModelVerifyResp : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BillingModelVerifyResp;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>计费模型编号，2 字节 BIN。</summary>
    public ushort ModelNo { get; set; }
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。
    /// 0x00 桩计费模型与平台一致
    /// 0x01 桩计费模型与平台不一致
    /// </summary>
    public byte Result { get; set; }

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
