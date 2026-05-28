using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.9 远程账户余额更新 (0x42, 下行)</summary>
public class V14DBalanceUpdateCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.BalanceUpdate;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public string PhysicalCard { get; set; } = ""; // 8B
    public uint NewBalance { get; set; }            // 4B

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

