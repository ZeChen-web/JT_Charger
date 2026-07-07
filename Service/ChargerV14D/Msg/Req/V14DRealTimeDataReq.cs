using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.1 充电桩实时监测数据报文 (0x13，上行)。</summary>
public class V14DRealTimeDataReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.UploadRealTimeData;

    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }

    /// <summary>
    /// 充电状态，1 字节 BIN；按协议状态枚举定义。
    /// 0x00：离线
    /// 0x01：故障
    /// 0x02：空闲
    /// 0x03：充电
    /// 0x04:已插枪未充电
    /// 0x05:充电完成未拔枪
    /// </summary>
    public byte Status { get; set; }

    /// <summary>充电枪归位状态，1 字节 BIN。0x00 否 0x01 是 0x02 未知（无法检测到枪是否插回枪座即 未知）</summary>
    public byte GunHome { get; set; }

    /// <summary>枪连接状态，1 字节 BIN。0x00 否 0x01 是</summary>
    public byte GunConnected { get; set; }

    /// <summary>
    /// 电压字段，按协议精度换算。
    /// </summary>
    public ushort OutputVoltage { get; set; }

    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort OutputCurrent { get; set; }

    /// <summary>枪温度，1 字节 BIN，1C/位，偏移 -50C。</summary>
    public byte GunTemp { get; set; }

    /// <summary>枪编码，定长 ASCII，右补 0。</summary>
    public string GunCode { get; set; } = "";

    /// <summary>车辆/电池 SOC，1 字节 BIN，1%/位。</summary>
    public byte SOC { get; set; }

    /// <summary>电池最高温度，1 字节 BIN，1C/位，偏移 -50C。</summary>
    public byte MaxBatTemp { get; set; }

    /// <summary>时间字段，按协议格式解析。</summary>
    public ushort ChargeTime { get; set; }

    /// <summary>时间字段，按协议格式解析。</summary>
    public ushort RemainTime { get; set; }

    /// <summary>电量字段，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ChargeKWH { get; set; }

    /// <summary>电量字段，4 字节 BIN，精确到 4 位小数。</summary>
    public uint LossKWH { get; set; }

    /// <summary>金额字段，4 字节 BIN，精确到 4 位小数。</summary>
    public uint ChargeAmount { get; set; }

    /// <summary>硬件故障码，按协议位定义解析。</summary>
    public byte HardwareFault { get; set; }

    /// <summary>硬件告警码，按协议位定义解析。</summary>
    public byte HardwareWarning { get; set; }

    /// <summary>BMS/电池编号，4 字节 BIN，厂商自定义。</summary>
    public uint BMSBatSN { get; set; }

    /// <summary>电压字段，按协议精度换算。</summary>
    public float OutputVoltageValue => OutputVoltage * 0.1f;

    /// <summary>电流字段，按协议精度换算。</summary>
    public float OutputCurrentValue => OutputCurrent * 0.1f;

    /// <summary>温度字段，按协议偏移和精度换算。</summary>
    public int GunTempValue => GunTemp - 50;

    /// <summary>温度字段，按协议偏移和精度换算。</summary>
    public int MaxBatTempValue => MaxBatTemp - 50;

    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float ChargeKWHValue => ChargeKWH * 0.0001f;

    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float LossKWHValue => LossKWH * 0.0001f;

    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float ChargeAmountValue => ChargeAmount * 0.0001f;

    /// <summary>按协议精度或格式换算后的只读值。</summary>
    public float ChargePower => OutputVoltageValue * OutputCurrentValue / 1000f;

    public V14DRealTimeDataReq()
    {
    }

    public V14DRealTimeDataReq(byte[] body)
    {
        if (body.Length < 64) return;
        int offset = 0;

        TransactionSN = V14DUtils.BcdToString(body, offset, 16);
        offset += 16;
        PileCode = V14DUtils.BcdToString(body, offset, 7);
        offset += 7;
        Gun = body[offset++];
        Status = body[offset++];
        GunHome = body[offset++];
        GunConnected = body[offset++];
        OutputVoltage = BitConverter.ToUInt16(body, offset);
        offset += 2;
        OutputCurrent = BitConverter.ToUInt16(body, offset);
        offset += 2;
        GunTemp = body[offset++];
        GunCode = BitConverter.ToString(body, offset, 8);
        offset += 8;
        SOC = body[offset++];
        MaxBatTemp = Convert.ToByte(body[offset++]-50);
        ChargeTime = BitConverter.ToUInt16(body, offset);
        offset += 2;
        RemainTime = BitConverter.ToUInt16(body, offset);
        offset += 2;
        ChargeKWH = BitConverter.ToUInt32(body, offset);
        offset += 4;
        LossKWH = BitConverter.ToUInt32(body, offset);
        offset += 4;
        ChargeAmount = BitConverter.ToUInt32(body, offset);
        offset += 4;
        HardwareFault = body[offset++];
        HardwareWarning = body[offset++];
        BMSBatSN = BitConverter.ToUInt32(body, offset);
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[64];
        int offset = 0;

        var tsnBcd = V14DUtils.StringToBcd(TransactionSN, 16);
        Array.Copy(tsnBcd, 0, body, offset, 16);
        offset += 16;
        var pileBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileBcd, 0, body, offset, 7);
        offset += 7;
        body[offset++] = Gun;
        body[offset++] = Status;
        body[offset++] = GunHome;
        body[offset++] = GunConnected;
        BitConverter.GetBytes(OutputVoltage).CopyTo(body, offset);
        offset += 2;
        BitConverter.GetBytes(OutputCurrent).CopyTo(body, offset);
        offset += 2;
        body[offset++] = GunTemp;
        // GunCode as BIN (raw bytes)
        {
            var gc = string.IsNullOrEmpty(GunCode)
                ? new byte[8]
                : global::System.Text.Encoding.ASCII.GetBytes(GunCode.PadRight(8, '\0').Substring(0, 8));
            Array.Copy(gc, 0, body, offset, Math.Min(8, gc.Length));
            offset += 8;
        }
        body[offset++] = SOC;
        body[offset++] = MaxBatTemp;
        BitConverter.GetBytes(ChargeTime).CopyTo(body, offset);
        offset += 2;
        BitConverter.GetBytes(RemainTime).CopyTo(body, offset);
        offset += 2;
        BitConverter.GetBytes(ChargeKWH).CopyTo(body, offset);
        offset += 4;
        BitConverter.GetBytes(LossKWH).CopyTo(body, offset);
        offset += 4;
        BitConverter.GetBytes(ChargeAmount).CopyTo(body, offset);
        offset += 4;
        body[offset++] = HardwareFault;
        body[offset++] = HardwareWarning;
        BitConverter.GetBytes(BMSBatSN).CopyTo(body, offset);

        return body;
    }
}