using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>12.3 平台VIN启动充电 (0xAF, 下行)</summary>
public class V14DPlatformVINStartChargeCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.PlatformVINStartCharge;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public V14DPlatformVINStartChargeCmd() { }
    public V14DPlatformVINStartChargeCmd(string pileCode, byte gun) { PileCode = pileCode; Gun = gun; }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        return b;
    }
}
