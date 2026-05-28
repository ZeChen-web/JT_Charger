using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.11 离线卡数据同�?(0x44, 下行)</summary>
public class V14DCardSyncCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.CardSync;
    public string PileCode { get; set; } = "";
    public byte CardCount { get; set; }
    // 每张�? 8B逻辑卡号 + 8B物理卡号 = 16B/�?
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


