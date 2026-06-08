using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>电池基本信息获取命令 (0x72, 下行)</summary>
public class V14DBatteryInfoQueryCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.BatteryInfoQuery;

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN</summary>
    public byte Gun { get; set; }

    public V14DBatteryInfoQueryCmd() { }

    public V14DBatteryInfoQueryCmd(string pileCode, byte gun)
    {
        PileCode = pileCode;
        Gun = gun;
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        return b;
    }
}
