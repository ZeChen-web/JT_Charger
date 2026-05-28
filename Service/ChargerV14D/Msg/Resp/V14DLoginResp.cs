using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>
/// 6.2 登录认证应答 (帧类型 0x02, 下行)
/// </summary>
public class V14DLoginResp : V14DFrame
{
    public override byte FrameType => V14DFrameType.LoginResp;

    /// <summary>桩编码 (7字节BCD)</summary>
    public string PileCode { get; set; } = "";

    /// <summary>登陆结果 0x00=成功 0x01=失败</summary>
    public byte Result { get; set; }

    public V14DLoginResp() { }

    public V14DLoginResp(string pileCode, byte result)
    {
        PileCode = pileCode;
        Result = result;
    }

    public V14DLoginResp(byte[] body)
    {
        if (body.Length < 8) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        Result = body[7];
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[8];
        var pileCodeBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileCodeBcd, 0, body, 0, 7);
        body[7] = Result;
        return body;
    }
}
