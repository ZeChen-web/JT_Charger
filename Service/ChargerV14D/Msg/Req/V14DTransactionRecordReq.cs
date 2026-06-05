using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.7 交易记录报文 (0x3F，上行)。</summary>
public class V14DTransactionRecordReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.TransactionRecord;

    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>充电开始时间，7 字节 CP56Time2a 格式。</summary>
    public byte[] StartTime { get; set; } = new byte[7];
    /// <summary>充电结束时间，7 字节 CP56Time2a 格式。</summary>
    public byte[] EndTime { get; set; } = new byte[7];
    /// <summary>尖费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint PeakRate { get; set; }
    /// <summary>尖时段充电电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint PeakKWH { get; set; }
    /// <summary>尖时段计损电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint PeakLossKWH { get; set; }
    /// <summary>尖时段金额，4 字节 BIN，精确到 4 位小数。</summary>
    public uint PeakAmount { get; set; }
    /// <summary>峰费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ShoulderRate { get; set; }
    /// <summary>峰时段充电电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ShoulderKWH { get; set; }
    /// <summary>峰时段计损电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ShoulderLossKWH { get; set; }
    /// <summary>峰时段金额，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ShoulderAmount { get; set; }
    /// <summary>平费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint FlatRate { get; set; }
    /// <summary>平时段充电电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint FlatKWH { get; set; }
    /// <summary>平时段计损电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint FlatLossKWH { get; set; }
    /// <summary>平时段金额，4 字节 BIN，精确到 4 位小数。</summary>
    public uint FlatAmount { get; set; }
    /// <summary>谷费率，4 字节 BIN，精确到 5 位小数。</summary>
    public uint ValleyRate { get; set; }
    /// <summary>谷时段充电电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ValleyKWH { get; set; }
    /// <summary>谷时段计损电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ValleyLossKWH { get; set; }
    /// <summary>谷时段金额，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ValleyAmount { get; set; }
    /// <summary>开始电表读数，4 字节 BIN，精确到 4 位小数。</summary>
    public uint MeterStart { get; set; }
    /// <summary>结束电表读数，4 字节 BIN，精确到 4 位小数。</summary>
    public uint MeterEnd { get; set; }
    /// <summary>总电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint TotalKWH { get; set; }
    /// <summary>计损总电量，4 字节 BIN，精确到 4 位小数。</summary>
    public uint TotalLossKWH { get; set; }
    /// <summary>金额字段，4 字节 BIN，精确到 4 位小数。</summary>
    public uint TotalAmount { get; set; }
    /// <summary>车辆 VIN，17 字节 ASCII，右补 0。</summary>
    public string VIN { get; set; } = "";
    /// <summary>交易标识，1 字节 BIN；按协议定义表示交易类型/状态。</summary>
    public byte TradeFlag { get; set; }
    /// <summary>交易时间，7 字节 CP56Time2a 格式。</summary>
    public byte[] TradeTime { get; set; } = new byte[7];
    /// <summary>停机原因，1 字节 BIN；按 V1.4D 故障及停机原因优化定义。</summary>
    public byte StopReason { get; set; }
    /// <summary>物理卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string PhysicalCard { get; set; } = "";

    /// <summary>时间字段，按协议格式解析。</summary>
    public DateTime StartDateTime => V14DUtils.CP56Time2aToDateTime(StartTime);
    /// <summary>时间字段，按协议格式解析。</summary>
    public DateTime EndDateTime => V14DUtils.CP56Time2aToDateTime(EndTime);
    /// <summary>时间字段，按协议格式解析。</summary>
    public DateTime TradeDateTime => V14DUtils.CP56Time2aToDateTime(TradeTime);

    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float PeakRateValue => PeakRate * 0.00001f;
    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float PeakKWHValue => PeakKWH * 0.0001f;
    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float PeakAmountValue => PeakAmount * 0.0001f;
    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float TotalKWHValue => TotalKWH * 0.0001f;
    /// <summary>按协议精度或格式换算后的只读值。</summary>
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

