using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.1 充电桩主动申请启动充电报文 (0x31，上行)。</summary>
public class V14DApplyStartChargeReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ApplyStartCharge;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>启动方式，1 字节 BIN；0x01 卡，0x02 账号，0x03 VIN。</summary>
    public byte StartMethod { get; set; }
    /// <summary>是否需要密码，1 字节 BIN；0x00 否，0x01 是。</summary>
    public byte NeedPassword { get; set; }
    /// <summary>账号或物理卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string AccountOrCard { get; set; } = "";
    /// <summary>密码字段，定长 ASCII，右补 0。</summary>
    public string Password { get; set; } = "";
    /// <summary>车辆 VIN，17 字节 ASCII，右补 0。</summary>
    public string VinCode { get; set; } = "";

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


