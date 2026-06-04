using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.11 离线卡数据同步命令报文 (0x44，下行)。</summary>
public class V14DCardSyncCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.CardSync;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>卡数量，1 字节 BIN。</summary>
    public byte CardCount { get; set; }
    /// <summary>卡数据列表；每张卡包含 8 字节逻辑卡号和 8 字节物理卡号。</summary>
    public List<(string LogicCard, string PhysicalCard)> Cards { get; set; } = new();

    public V14DCardSyncCmd() { }
    public override byte[] GetBodyBytes()
    {
        int totalCards = Math.Min(CardCount, Cards.Count);
        var b = new byte[8 + totalCards * 16];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = CardCount;
        for (int i = 0; i < totalCards; i++)
        {
            int o = 8 + i * 16;
            var lc = global::System.Text.Encoding.ASCII.GetBytes((Cards[i].LogicCard ?? "").PadRight(8, '\0').Substring(0, 8));
            Array.Copy(lc, 0, b, o, 8);
            var pc = global::System.Text.Encoding.ASCII.GetBytes((Cards[i].PhysicalCard ?? "").PadRight(8, '\0').Substring(0, 8));
            Array.Copy(pc, 0, b, o + 8, 8);
        }
        return b;
    }
}


