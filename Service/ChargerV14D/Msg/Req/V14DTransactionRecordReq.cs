using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>
/// 8.7 交易记录 (帧类�?0x3F, 上行)
/// Body大小: ~160字节
/// </summary>
public class V14DTransactionRecordReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.TransactionRecord;

    public string TransactionSN { get; set; } = "";       // 16B BCD
    public string PileCode { get; set; } = "";             // 7B BCD
    public byte Gun { get; set; }                          // 1B
    public byte[] StartTime { get; set; } = new byte[7];   // 7B CP56Time2a
    public byte[] EndTime { get; set; } = new byte[7];     // 7B CP56Time2a
    public uint PeakRate { get; set; }                     // 4B (5位小�?
    public uint PeakKWH { get; set; }                      // 4B (4位小�?
    public uint PeakLossKWH { get; set; }                  // 4B (4位小�?
    public uint PeakAmount { get; set; }                   // 4B (4位小�?
    public uint ShoulderRate { get; set; }                 // 4B
    public uint ShoulderKWH { get; set; }                  // 4B
    public uint ShoulderLossKWH { get; set; }              // 4B
    public uint ShoulderAmount { get; set; }               // 4B
    public uint FlatRate { get; set; }                     // 4B
    public uint FlatKWH { get; set; }                      // 4B
    public uint FlatLossKWH { get; set; }                  // 4B
    public uint FlatAmount { get; set; }                   // 4B
    public uint ValleyRate { get; set; }                   // 4B
    public uint ValleyKWH { get; set; }                    // 4B
    public uint ValleyLossKWH { get; set; }                // 4B
    public uint ValleyAmount { get; set; }                 // 4B
    public uint MeterStart { get; set; }                   // 4B (4位小�?
    public uint MeterEnd { get; set; }                     // 4B (4位小�?
    public uint TotalKWH { get; set; }                     // 4B (4位小�?
    public uint TotalLossKWH { get; set; }                 // 4B (4位小�?
    public uint TotalAmount { get; set; }                  // 4B (4位小�?
    public string VIN { get; set; } = "";                  // 17B ASCII
    public byte TradeFlag { get; set; }                    // 1B
    public byte[] TradeTime { get; set; } = new byte[7];   // 7B CP56Time2a
    public byte StopReason { get; set; }                   // 1B
    public string PhysicalCard { get; set; } = "";         // 8B BIN

    // 计算属�? 时间
    public DateTime StartDateTime => V14DUtils.CP56Time2aToDateTime(StartTime);
    public DateTime EndDateTime => V14DUtils.CP56Time2aToDateTime(EndTime);
    public DateTime TradeDateTime => V14DUtils.CP56Time2aToDateTime(TradeTime);

    // 计算属�? 金额/电量 (带小数位)
    public float PeakRateValue => PeakRate * 0.00001f;
    public float PeakKWHValue => PeakKWH * 0.0001f;
    public float PeakAmountValue => PeakAmount * 0.0001f;
    public float TotalKWHValue => TotalKWH * 0.0001f;
    public float TotalAmountValue => TotalAmount * 0.0001f;

    public V14DTransactionRecordReq() { }

    public V14DTransactionRecordReq(byte[] body)
    {
        if (body.Length < 152) return;
        int offset = 0;

        TransactionSN = V14DUtils.BcdToString(body, offset, 16); offset += 16;
        PileCode = V14DUtils.BcdToString(body, offset, 7); offset += 7;
        Gun = body[offset++];

        StartTime = new byte[7]; Array.Copy(body, offset, StartTime, 0, 7); offset += 7;
        EndTime = new byte[7]; Array.Copy(body, offset, EndTime, 0, 7); offset += 7;

        PeakRate = BitConverter.ToUInt32(body, offset); offset += 4;
        PeakKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        PeakLossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        PeakAmount = BitConverter.ToUInt32(body, offset); offset += 4;

        ShoulderRate = BitConverter.ToUInt32(body, offset); offset += 4;
        ShoulderKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        ShoulderLossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        ShoulderAmount = BitConverter.ToUInt32(body, offset); offset += 4;

        FlatRate = BitConverter.ToUInt32(body, offset); offset += 4;
        FlatKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        FlatLossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        FlatAmount = BitConverter.ToUInt32(body, offset); offset += 4;

        ValleyRate = BitConverter.ToUInt32(body, offset); offset += 4;
        ValleyKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        ValleyLossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        ValleyAmount = BitConverter.ToUInt32(body, offset); offset += 4;

        MeterStart = BitConverter.ToUInt32(body, offset); offset += 4;
        MeterEnd = BitConverter.ToUInt32(body, offset); offset += 4;
        TotalKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        TotalLossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        TotalAmount = BitConverter.ToUInt32(body, offset); offset += 4;

        VIN = V14DUtils.ReadAscii(body, offset, 17); offset += 17;
        TradeFlag = body[offset++];
        TradeTime = new byte[7]; Array.Copy(body, offset, TradeTime, 0, 7); offset += 7;
        StopReason = body[offset++];
        PhysicalCard = BitConverter.ToString(body, offset, 8).Replace("-", ""); offset += 8;
    }

    public override byte[] GetBodyBytes()
    {
        // 计算body长度: 16+7+1+7+7 + 16*4 + 4+4+4+4+4 + 17+1+7+1+8 = 160
        var body = new byte[160];
        int offset = 0;

        var tsnBcd = V14DUtils.StringToBcd(TransactionSN, 16);
        Array.Copy(tsnBcd, 0, body, offset, 16); offset += 16;
        var pileBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileBcd, 0, body, offset, 7); offset += 7;
        body[offset++] = Gun;

        FixLength(StartTime, 7).CopyTo(body, offset); offset += 7;
        FixLength(EndTime, 7).CopyTo(body, offset); offset += 7;

        BitConverter.GetBytes(PeakRate).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(PeakKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(PeakLossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(PeakAmount).CopyTo(body, offset); offset += 4;

        BitConverter.GetBytes(ShoulderRate).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ShoulderKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ShoulderLossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ShoulderAmount).CopyTo(body, offset); offset += 4;

        BitConverter.GetBytes(FlatRate).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(FlatKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(FlatLossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(FlatAmount).CopyTo(body, offset); offset += 4;

        BitConverter.GetBytes(ValleyRate).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ValleyKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ValleyLossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ValleyAmount).CopyTo(body, offset); offset += 4;

        BitConverter.GetBytes(MeterStart).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(MeterEnd).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(TotalKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(TotalLossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(TotalAmount).CopyTo(body, offset); offset += 4;

        V14DUtils.WriteAscii(VIN, body, offset, 17); offset += 17;
        body[offset++] = TradeFlag;
        FixLength(TradeTime, 7).CopyTo(body, offset); offset += 7;
        body[offset++] = StopReason;

        var cardBytes = string.IsNullOrEmpty(PhysicalCard) ? new byte[8]
            : global::System.Text.Encoding.ASCII.GetBytes(PhysicalCard.PadRight(8, '\0').Substring(0, 8));
        Array.Copy(cardBytes, 0, body, offset, Math.Min(8, cardBytes.Length));

        return body;
    }

    private static byte[] FixLength(byte[] arr, int length)
    {
        if (arr.Length >= length) return arr[..length];
        var result = new byte[length];
        Array.Copy(arr, result, arr.Length);
        return result;
    }
}

