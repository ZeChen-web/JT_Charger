using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>10.4 站控发送电池在仓数据 (0x78, 下行)</summary>
public class V14DBatteryInBinSignalCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BatteryInBinSignal;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>电池入仓信号布尔型（1，电池在仓；0，仓内无电池）</summary>
    public byte BatteryInBin { get; set; }
    /// <summary>保留字段，按协议默认置 0。</summary>
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
