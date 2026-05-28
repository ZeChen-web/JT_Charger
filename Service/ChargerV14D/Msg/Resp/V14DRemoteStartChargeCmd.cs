using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.3 运营平台远程控制启机 (0x34, 下行)</summary>
public class V14DRemoteStartChargeCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteStartCharge;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public string LogicCardNo { get; set; } = "";   // 8B
    public string PhysicalCardNo { get; set; } = ""; // 8B
    public uint Balance { get; set; }                // 4B
    public string StopPassword { get; set; } = "";   // 最�?字节ASCII

    public V14DRemoteStartChargeCmd() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[42];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        var lc = global::System.Text.Encoding.ASCII.GetBytes((LogicCardNo ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(lc, 0, b, o, 8); o += 8;
        var pc = global::System.Text.Encoding.ASCII.GetBytes((PhysicalCardNo ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(pc, 0, b, o, 8); o += 8;
        BitConverter.GetBytes(Balance).CopyTo(b, o); o += 4;
        V14DUtils.WriteAscii(StopPassword, b, o, 6);
        return b;
    }
}

