using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>9.1 充电桩工作参数设置 (0x52, 下行)</summary>
public class V14DParamSetCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ParamSet;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>是否允许工作，1 字节 BIN；0x00 允许，0x01 停用。</summary>
    public byte AllowWork { get; set; }
    /// <summary>最大输出功率百分比，1 字节 BIN；最大 100%，最小 30%。</summary>
    public byte MaxOutputPower { get; set; }

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
