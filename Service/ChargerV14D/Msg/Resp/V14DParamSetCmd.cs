using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>9.1 充电桩工作参数设置 (0x52, 下行)</summary>
public class V14DParamSetCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.ParamSet;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte AllowWork { get; set; }       // 0x00允许 0x01停用
    public byte MaxOutputPower { get; set; }  // 1BIN=1%, 最大100%最小30%

    public V14DParamSetCmd() { }
    public V14DParamSetCmd(string pileCode, byte gun, byte allowWork, byte maxPower)
    { PileCode = pileCode; Gun = gun; AllowWork = allowWork; MaxOutputPower = maxPower; }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[10];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = AllowWork; b[9] = MaxOutputPower;
        return b;
    }
}
