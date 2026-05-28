using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>10.4 站控发送电池在仓数据 (0x78, 下行)</summary>
public class V14DBatteryInBinSignalCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.BatteryInBinSignal;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte BatteryInBin { get; set; } // 1=电池在仓 0=无电池
    public uint Reserve { get; set; }

    public V14DBatteryInBinSignalCmd() { }
    public V14DBatteryInBinSignalCmd(string pileCode, byte gun, byte inBin)
    { PileCode = pileCode; Gun = gun; BatteryInBin = inBin; }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[9];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = BatteryInBin;
        return b;
    }
}
