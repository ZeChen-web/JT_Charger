using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>电池状态接口 (0x75, 上行)</summary>
public class V14DBatteryStatusReportReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BatteryStatusReport;

    /// <summary>桩编号，7 字节 BCD</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN</summary>
    public byte Gun { get; set; }

    /// <summary>预留字段，4 字节 BIN，厂商自定义</summary>
    public byte[] Reserved { get; set; } = new byte[4];

    /// <summary>电池编号，2 字节 BIN</summary>
    public ushort BatteryNo { get; set; }

    /// <summary>SOC，2 字节 BIN，精度0.1%，0~100%</summary>
    public ushort SOC { get; set; }

    /// <summary>电池箱状态，1 字节 BIN；0=不正常，1=正常</summary>
    public byte BatteryBoxStatus { get; set; }

    /// <summary>充电电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort ChargeVoltage { get; set; }

    /// <summary>充电电流，2 字节 BIN，0.1A/位，0A偏移</summary>
    public ushort ChargeCurrent { get; set; }

    /// <summary>需求电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort DemandVoltage { get; set; }

    /// <summary>需求电流，2 字节 BIN，0.1A/位，0A偏移</summary>
    public ushort DemandCurrent { get; set; }

    /// <summary>充电模式，1 字节 BIN；0x01=恒压，0x02=恒流</summary>
    public byte ChargeMode { get; set; }

    /// <summary>交易订单，16 字节 BCD</summary>
    public string TransactionOrder { get; set; } = "";

    /// <summary>最高单体电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort MaxCellVoltage { get; set; }

    /// <summary>最高单体电压编号，1 字节 BIN</summary>
    public byte MaxCellVoltageNo { get; set; }

    /// <summary>最低单体电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort MinCellVoltage { get; set; }

    /// <summary>最低单体电压编号，1 字节 BIN</summary>
    public byte MinCellVoltageNo { get; set; }

    /// <summary>最高电池温度，2 字节 BIN，-50°C偏移</summary>
    public ushort MaxBatteryTemperature { get; set; }

    /// <summary>最高温度检测编号，1 字节 BIN</summary>
    public byte MaxTempDetectNo { get; set; }

    /// <summary>最低电池温度，2 字节 BIN，-50°C偏移</summary>
    public ushort MinBatteryTemperature { get; set; }

    /// <summary>最低温度检测编号，1 字节 BIN</summary>
    public byte MinTempDetectNo { get; set; }

    /// <summary>电池编码，27 字节 ASCII，协议仅用24位</summary>
    public string BatteryCode { get; set; } = "";

    /// <summary>BMS软件版本，8 字节 ASCII</summary>
    public string BmsSoftwareVersion { get; set; } = "";

    /// <summary>动力电池故障，1 字节 BIN；0=无故障,1=一级告警,2=二级严重,3=三级紧急</summary>
    public byte BatteryFault { get; set; }

    /// <summary>SOH健康状态，1 字节 BIN</summary>
    public byte SOH { get; set; }

    /// <summary>单体电池总数，2 字节 BIN</summary>
    public ushort CellCount { get; set; }

    /// <summary>SOC换算值（%）</summary>
    public float SOCValue => SOC * 0.1f;

    /// <summary>最高温度换算值</summary>
    public float MaxTemperatureValue => (short)MaxBatteryTemperature - 50f;

    /// <summary>最低温度换算值</summary>
    public float MinTemperatureValue => (short)MinBatteryTemperature - 50f;

    public V14DBatteryStatusReportReq() { }

    public V14DBatteryStatusReportReq(byte[] body)
    {
        if (body.Length < 93) return;
        int o = 0;

        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        Array.Copy(body, o, Reserved, 0, 4); o += 4;
        BatteryNo = BitConverter.ToUInt16(body, o); o += 2;
        SOC = BitConverter.ToUInt16(body, o); o += 2;
        BatteryBoxStatus = body[o++];
        ChargeVoltage = BitConverter.ToUInt16(body, o); o += 2;
        ChargeCurrent = BitConverter.ToUInt16(body, o); o += 2;
        DemandVoltage = BitConverter.ToUInt16(body, o); o += 2;
        DemandCurrent = BitConverter.ToUInt16(body, o); o += 2;
        ChargeMode = body[o++];
        TransactionOrder = V14DUtils.BcdToString(body, o, 16); o += 16;
        MaxCellVoltage = BitConverter.ToUInt16(body, o); o += 2;
        MaxCellVoltageNo = body[o++];
        MinCellVoltage = BitConverter.ToUInt16(body, o); o += 2;
        MinCellVoltageNo = body[o++];
        MaxBatteryTemperature = BitConverter.ToUInt16(body, o); o += 2;
        MaxTempDetectNo = body[o++];
        MinBatteryTemperature = BitConverter.ToUInt16(body, o); o += 2;
        MinTempDetectNo = body[o++];
        BatteryCode = global::System.Text.Encoding.ASCII.GetString(body, o, 27).TrimEnd('\0'); o += 27;
        BmsSoftwareVersion = global::System.Text.Encoding.ASCII.GetString(body, o, 8).TrimEnd('\0'); o += 8;
        BatteryFault = body[o++];
        SOH = body[o++];
        CellCount = BitConverter.ToUInt16(body, o);
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[93];
        int o = 0;

        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        Array.Copy(Reserved, 0, b, o, 4); o += 4;
        BitConverter.GetBytes(BatteryNo).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(SOC).CopyTo(b, o); o += 2;
        b[o++] = BatteryBoxStatus;
        BitConverter.GetBytes(ChargeVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(ChargeCurrent).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(DemandVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(DemandCurrent).CopyTo(b, o); o += 2;
        b[o++] = ChargeMode;
        V14DUtils.StringToBcd(TransactionOrder, 16).CopyTo(b, o); o += 16;
        BitConverter.GetBytes(MaxCellVoltage).CopyTo(b, o); o += 2;
        b[o++] = MaxCellVoltageNo;
        BitConverter.GetBytes(MinCellVoltage).CopyTo(b, o); o += 2;
        b[o++] = MinCellVoltageNo;
        BitConverter.GetBytes(MaxBatteryTemperature).CopyTo(b, o); o += 2;
        b[o++] = MaxTempDetectNo;
        BitConverter.GetBytes(MinBatteryTemperature).CopyTo(b, o); o += 2;
        b[o++] = MinTempDetectNo;
        WriteAscii(b, ref o, BatteryCode, 27);
        WriteAscii(b, ref o, BmsSoftwareVersion, 8);
        b[o++] = BatteryFault;
        b[o++] = SOH;
        BitConverter.GetBytes(CellCount).CopyTo(b, o);

        return b;
    }

    private static void WriteAscii(byte[] buf, ref int offset, string s, int length)
    {
        var bytes =global::System.Text.Encoding.ASCII.GetBytes((s ?? "").PadRight(length, '\0').Substring(0, length));
        Array.Copy(bytes, 0, buf, offset, length);
        offset += length;
    }
}
