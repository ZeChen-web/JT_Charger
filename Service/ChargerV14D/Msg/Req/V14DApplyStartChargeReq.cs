using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.1 充电桩主动申请启动充�?(0x31, 上行)</summary>
public class V14DApplyStartChargeReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ApplyStartCharge;
    public string PileCode { get; set; } = "";       // 7B BCD
    public byte Gun { get; set; }                     // 1B
    public byte StartMethod { get; set; }             // 1B 0x01�?0x02账号/0x03 VIN
    public byte NeedPassword { get; set; }            // 1B 0x00鍚?0x01鏄?
    public string AccountOrCard { get; set; } = "";   // 8B BIN (账号或物理卡�?
    public string Password { get; set; } = "";        // 16B MD5加密密码
    public string VinCode { get; set; } = "";         // 17B ASCII

    public V14DApplyStartChargeReq() { }
    public V14DApplyStartChargeReq(byte[] body)
    {
        if (body.Length < 50) return;
        int o = 0;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        StartMethod = body[o++];
        NeedPassword = body[o++];
        AccountOrCard = BitConverter.ToString(body, o, 8).Replace("-", ""); o += 8;
        Password = V14DUtils.ReadAscii(body, o, 16); o += 16;
        VinCode = V14DUtils.ReadAscii(body, o, 17);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[50];
        int o = 0;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = StartMethod; b[o++] = NeedPassword;
        var acBytes = global::System.Text.Encoding.ASCII.GetBytes((AccountOrCard ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(acBytes, 0, b, o, 8); o += 8;
        V14DUtils.WriteAscii(Password, b, o, 16); o += 16;
        V14DUtils.WriteAscii(VinCode, b, o, 17);
        return b;
    }
}


