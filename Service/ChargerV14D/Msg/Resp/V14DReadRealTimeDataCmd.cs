using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>7.2 读取实时监测数据 (0x12, 下行)</summary>
public class V14DReadRealTimeDataCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.ReadRealTimeData;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }

    public V14DReadRealTimeDataCmd() { }
    public V14DReadRealTimeDataCmd(string pileCode, byte gun) { PileCode = pileCode; Gun = gun; }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        return b;
    }
}
