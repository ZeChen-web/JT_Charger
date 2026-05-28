using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>12.1 VIN查询 (0xAD, 下行)</summary>
public class V14DVINQueryCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.VINQuery;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public V14DVINQueryCmd() { }
    public V14DVINQueryCmd(string pileCode, byte gun) { PileCode = pileCode; Gun = gun; }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun;
        return b;
    }
}
