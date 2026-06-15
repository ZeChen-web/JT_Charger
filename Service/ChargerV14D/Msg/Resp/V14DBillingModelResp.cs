using Entity.DbModel.Station;
using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>
/// 6.8 计费模型请求应答（0x0A，下行）。
/// 平台应答充电桩计费模型请求；每半小时一个费率段，共 48 段。
/// Body 长度 90 字节；完整帧 DataLen = 94 字节（SeqNo + EncryptFlag + FrameType + Body）。
/// </summary>
public class V14DBillingModelResp : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BillingModelResp;

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>计费模型编号，2 字节 BIN。</summary>
    public ushort ModelNo { get; set; }
    
    

    /// <summary>尖电费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint PeakElecRate { get; set; }

    /// <summary>尖服务费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint PeakServiceRate { get; set; }

    /// <summary>峰电费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ShoulderElecRate { get; set; }

    /// <summary>峰服务费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ShoulderServiceRate { get; set; }

    /// <summary>平电费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint FlatElecRate { get; set; }

    /// <summary>平服务费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint FlatServiceRate { get; set; }

    /// <summary>谷电费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ValleyElecRate { get; set; }

    /// <summary>谷服务费费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ValleyServiceRate { get; set; }
    
    

    /// <summary>计损比例，1 字节 BIN；非 0 时按协议对上送充电量计损。</summary>
    public byte LossRatio { get; set; }

    /// <summary>48 个半小时时段费率号；0x00 尖，0x01 峰，0x02 平，0x03 谷。</summary>
    public byte[] RateSegments { get; set; } = new byte[48];

    /// <summary>DB type 到协议费率号的映射: 1=尖→0x00, 2=峰→0x01, 3=平→0x02, 4=谷→0x03</summary>
    protected static readonly Dictionary<int, byte> TypeToRateSegment = new()
    {
        { 1, 0x00 }, { 2, 0x01 }, { 3, 0x02 }, { 4, 0x03 }
    };

    public V14DBillingModelResp() { }

    /// <summary>从 DB 明细数据填充费率与时段信息</summary>
    public void PopulateFromDetails(List<ElecPriceModelVersionDetail> details)
    {
        if (details.Count < 48)
            return;

        // 按尖峰平谷类型取费率: DB price 单位为分，协议需精确到5位小数 (元 * 100000)
        // 分 → 5位小数的元: price * 1000
        var detailsByType = details.GroupBy(d => d.Type).ToDictionary(g => g.Key, g => g.First());
        ModelNo = (ushort)detailsByType[0].Version;
        PeakElecRate     = (uint)(detailsByType.GetValueOrDefault(1)?.Price * 1000 ?? 0);
        PeakServiceRate  = (uint)(detailsByType.GetValueOrDefault(1)?.PriceSerice * 1000 ?? 0);
        ShoulderElecRate = (uint)(detailsByType.GetValueOrDefault(2)?.Price * 1000 ?? 0);
        ShoulderServiceRate = (uint)(detailsByType.GetValueOrDefault(2)?.PriceSerice * 1000 ?? 0);
        FlatElecRate     = (uint)(detailsByType.GetValueOrDefault(3)?.Price * 1000 ?? 0);
        FlatServiceRate  = (uint)(detailsByType.GetValueOrDefault(3)?.PriceSerice * 1000 ?? 0);
        ValleyElecRate   = (uint)(detailsByType.GetValueOrDefault(4)?.Price * 1000 ?? 0);
        ValleyServiceRate = (uint)(detailsByType.GetValueOrDefault(4)?.PriceSerice * 1000 ?? 0);

        LossRatio = 0;

        // 构建48个半小时时段费率号
        for (int slot = 0; slot < 48; slot++)
        {
            int slotMinutes = (slot / 2) * 60 + (slot % 2) * 30;

            var matched = details[slot];

            RateSegments[slot] = TypeToRateSegment.TryGetValue(matched.Type, out var seg) ? seg : (byte)0x02; // 默认为平
        }
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[90];
        int o = 0;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        BitConverter.GetBytes(ModelNo).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PeakElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(PeakServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ShoulderElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ShoulderServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(FlatElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(FlatServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ValleyElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ValleyServiceRate).CopyTo(b, o); o += 4;
        b[o++] = LossRatio;
        Array.Copy(RateSegments, 0, b, o, 48);
        return b;
    }
}
