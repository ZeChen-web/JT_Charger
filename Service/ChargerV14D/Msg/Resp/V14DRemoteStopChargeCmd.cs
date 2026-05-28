using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.5 运营平台远程停机 (0x36, 下行)</summary>
public class V14DRemoteStopChargeCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteStopCharge;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }

    public V14DRemoteStopChargeCmd() { }
    public V14DRemoteStopChargeCmd(string pileCode, byte gun) { PileCode = pileCode; Gun = gun; }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        return b;
    }
}
