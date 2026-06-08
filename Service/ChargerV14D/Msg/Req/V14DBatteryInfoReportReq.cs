using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>电池基本信息应答 (0x73, 上行)</summary>
public class V14DBatteryInfoReportReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BatteryInfoReport;

    /// <summary>桩编号，7 字节 BCD</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN</summary>
    public byte Gun { get; set; }

    /// <summary>当前时间，7 字节 CP56Time2a 格式</summary>
    public byte[] CurrentTime { get; set; } = new byte[7];

    /// <summary>预留字段，4 字节 BIN，厂商自定义</summary>
    public byte[] Reserved { get; set; } = new byte[4];

    /// <summary>电池编号，4 字节 BIN</summary>
    public uint BatteryNo { get; set; }

    /// <summary>协议版本，3 字节 BIN；如 V1.1: byte2=0x00,byte1=0x01,byte0=0x01</summary>
    public byte[] ProtocolVersion { get; set; } = new byte[3];

    /// <summary>电池类型，1 字节 BIN</summary>
    public byte BatteryType { get; set; }

    /// <summary>VIN，17 字节 ASCII</summary>
    public string VIN { get; set; } = "";

    /// <summary>单体允许最高电压，2 字节 BIN，0.01V/位，0V偏移</summary>
    public ushort CellMaxVoltage { get; set; }

    /// <summary>额定容量，2 字节 BIN，0.1Ah/位，0Ah偏移</summary>
    public ushort RatedCapacity { get; set; }

    /// <summary>最高允许电流，2 字节 BIN，0.1A/位，0A偏移</summary>
    public ushort MaxAllowCurrent { get; set; }

    /// <summary>额定总电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort RatedTotalVoltage { get; set; }

    /// <summary>标称总容量，2 字节 BIN，0.1kWh/位，0kWh偏移</summary>
    public ushort NominalCapacity { get; set; }

    /// <summary>电池厂商，4 字节 ASCII</summary>
    public string Manufacturer { get; set; } = "";

    /// <summary>最高充电电压，2 字节 BIN，0.1V/位，0V偏移</summary>
    public ushort MaxChargeVoltage { get; set; }

    /// <summary>生产日期-年，2 字节 BIN，1985年偏移</summary>
    public ushort ProductionYear { get; set; }

    /// <summary>生产日期-月，1 字节 BIN，0月偏移</summary>
    public byte ProductionMonth { get; set; }

    /// <summary>生产日期-日，1 字节 BIN，0日偏移</summary>
    public byte ProductionDay { get; set; }

    /// <summary>最高允许温度，1 字节 BIN，1°C/位，-50°C偏移</summary>
    public byte MaxAllowTemperature { get; set; }

    /// <summary>充电次数，4 字节 BIN</summary>
    public uint ChargeCycles { get; set; }

    /// <summary>初始SOC，2 字节 BIN，精度0.1%，0~100%</summary>
    public ushort InitialSOC { get; set; }

    /// <summary>实时SOC，2 字节 BIN，精度0.1%，0~100%</summary>
    public ushort RealTimeSOC { get; set; }

    /// <summary>电池产权，1 字节 BIN；0=租赁，1=车自有</summary>
    public byte BatteryOwnership { get; set; }

    /// <summary>初始电池电压，2 字节 BIN，精度0.1V，0~750V</summary>
    public ushort InitialVoltage { get; set; }

    /// <summary>电池编码，27 字节 ASCII</summary>
    public string BatteryCode { get; set; } = "";

    /// <summary>解析后的时间</summary>
    public DateTime CurrentTimeValue => V14DUtils.CP56Time2aToDateTime(CurrentTime);

    public V14DBatteryInfoReportReq() { }

    public V14DBatteryInfoReportReq(byte[] body)
    {
        if (body.Length < 103) return;
        int o = 0;

        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        Array.Copy(body, o, CurrentTime, 0, 7); o += 7;
        Array.Copy(body, o, Reserved, 0, 4); o += 4;
        BatteryNo = BitConverter.ToUInt32(body, o); o += 4;
        Array.Copy(body, o, ProtocolVersion, 0, 3); o += 3;
        BatteryType = body[o++];
        VIN = global::System.Text.Encoding.ASCII.GetString(body, o, 17).TrimEnd('\0'); o += 17;
        CellMaxVoltage = BitConverter.ToUInt16(body, o); o += 2;
        RatedCapacity = BitConverter.ToUInt16(body, o); o += 2;
        MaxAllowCurrent = BitConverter.ToUInt16(body, o); o += 2;
        RatedTotalVoltage = BitConverter.ToUInt16(body, o); o += 2;
        NominalCapacity = BitConverter.ToUInt16(body, o); o += 2;
        Manufacturer = global::System.Text.Encoding.ASCII.GetString(body, o, 4).TrimEnd('\0'); o += 4;
        MaxChargeVoltage = BitConverter.ToUInt16(body, o); o += 2;
        ProductionYear = BitConverter.ToUInt16(body, o); o += 2;
        ProductionMonth = body[o++];
        ProductionDay = body[o++];
        MaxAllowTemperature = body[o++];
        ChargeCycles = BitConverter.ToUInt32(body, o); o += 4;
        InitialSOC = BitConverter.ToUInt16(body, o); o += 2;
        RealTimeSOC = BitConverter.ToUInt16(body, o); o += 2;
        BatteryOwnership = body[o++];
        InitialVoltage = BitConverter.ToUInt16(body, o); o += 2;
        BatteryCode = global::System.Text.Encoding.ASCII.GetString(body, o, 27).TrimEnd('\0');
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[103];
        int o = 0;

        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        Array.Copy(CurrentTime, 0, b, o, 7); o += 7;
        Array.Copy(Reserved, 0, b, o, 4); o += 4;
        BitConverter.GetBytes(BatteryNo).CopyTo(b, o); o += 4;
        Array.Copy(ProtocolVersion, 0, b, o, 3); o += 3;
        b[o++] = BatteryType;
        WriteAscii(b, ref o, VIN, 17);
        BitConverter.GetBytes(CellMaxVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(RatedCapacity).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(MaxAllowCurrent).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(RatedTotalVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(NominalCapacity).CopyTo(b, o); o += 2;
        WriteAscii(b, ref o, Manufacturer, 4);
        BitConverter.GetBytes(MaxChargeVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(ProductionYear).CopyTo(b, o); o += 2;
        b[o++] = ProductionMonth;
        b[o++] = ProductionDay;
        b[o++] = MaxAllowTemperature;
        BitConverter.GetBytes(ChargeCycles).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(InitialSOC).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(RealTimeSOC).CopyTo(b, o); o += 2;
        b[o++] = BatteryOwnership;
        BitConverter.GetBytes(InitialVoltage).CopyTo(b, o); o += 2;
        WriteAscii(b, ref o, BatteryCode, 27);

        return b;
    }

    private static void WriteAscii(byte[] buf, ref int offset, string s, int length)
    {
        var bytes = global::System.Text.Encoding.ASCII.GetBytes((s ?? "").PadRight(length, '\0').Substring(0, length));
        Array.Copy(bytes, 0, buf, offset, length);
        offset += length;
    }
}
