using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.9 远程账户余额更新 (0x42, 下行)</summary>
public class V14DBalanceUpdateCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BalanceUpdate;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>物理卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string PhysicalCard { get; set; } = "";
    /// <summary>更新后的账户余额，4 字节 BIN；金额按协议精度换算。</summary>
    public uint NewBalance { get; set; }

    public V14DBalanceUpdateCmd() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[20];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        var card = global::System.Text.Encoding.ASCII.GetBytes((PhysicalCard ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(card, 0, b, 8, 8);
        BitConverter.GetBytes(NewBalance).CopyTo(b, 16);
        return b;
    }
}

