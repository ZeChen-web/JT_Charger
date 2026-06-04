using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.3 充电握手 (0x15, 上行) GBT-27930充电桩与BMS充电握手阶段报文</summary>
public class V14DChargeHandshakeReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ChargeHandshake;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>BMS 通信协议版本号，3 字节 BIN。</summary>
    public byte[] BmsProtocolVer { get; set; } = new byte[3];
    /// <summary>电池类型，1 字节 BIN。</summary>
    public byte BmsBatType { get; set; }
    /// <summary>电池额定容量，2 字节 BIN，0.1Ah/位。</summary>
    public ushort BmsRatedCapacity { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsRatedVoltage { get; set; }
    /// <summary>电池生产厂商名称，4 字节 ASCII。</summary>
    public byte[] BmsManufacturer { get; set; } = new byte[4];
    /// <summary>BMS/电池编号，4 字节 BIN，厂商自定义。</summary>
    public byte[] BmsBatSN { get; set; } = new byte[4];
    /// <summary>电池组生产年份，1 字节 BIN，偏移 1985。</summary>
    public byte BmsYear { get; set; }
    /// <summary>电池组生产月份，1 字节 BIN。</summary>
    public byte BmsMonth { get; set; }
    /// <summary>电池组生产日期，1 字节 BIN。</summary>
    public byte BmsDay { get; set; }
    /// <summary>电池组充电次数，3 字节 BIN。</summary>
    public byte[] BmsChargeCount { get; set; } = new byte[3];
    /// <summary>电池组产权标识，1 字节 BIN。</summary>
    public byte BmsOwnership { get; set; }
    /// <summary>保留字段，按协议默认置 0。</summary>
    public byte Reserve { get; set; }
    /// <summary>车辆 VIN，17 字节 ASCII，右补 0。</summary>
    public byte[] VinCode { get; set; } = new byte[17];
    /// <summary>BMS 软件版本号，8 字节 ASCII，右补 0。</summary>
    public byte[] BmsSoftwareVer { get; set; } = new byte[8];

    public V14DChargeHandshakeReq() { }
    public V14DChargeHandshakeReq(byte[] body)
    {
        if (body.Length < 55) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        Array.Copy(body, o, BmsProtocolVer, 0, 3); o += 3;
        BmsBatType = body[o++];
        BmsRatedCapacity = BitConverter.ToUInt16(body, o); o += 2;
        BmsRatedVoltage = BitConverter.ToUInt16(body, o); o += 2;
        Array.Copy(body, o, BmsManufacturer, 0, 4); o += 4;
        Array.Copy(body, o, BmsBatSN, 0, 4); o += 4;
        BmsYear = body[o++]; BmsMonth = body[o++]; BmsDay = body[o++];
        Array.Copy(body, o, BmsChargeCount, 0, 3); o += 3;
        BmsOwnership = body[o++];
        Reserve = body[o++];
        Array.Copy(body, o, VinCode, 0, 17); o += 17;
        Array.Copy(body, o, BmsSoftwareVer, 0, 8);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[75];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        FixArr(BmsProtocolVer, 3).CopyTo(b, o); o += 3;
        b[o++] = BmsBatType;
        BitConverter.GetBytes(BmsRatedCapacity).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsRatedVoltage).CopyTo(b, o); o += 2;
        FixArr(BmsManufacturer, 4).CopyTo(b, o); o += 4;
        FixArr(BmsBatSN, 4).CopyTo(b, o); o += 4;
        b[o++] = BmsYear; b[o++] = BmsMonth; b[o++] = BmsDay;
        FixArr(BmsChargeCount, 3).CopyTo(b, o); o += 3;
        b[o++] = BmsOwnership; b[o++] = Reserve;
        FixArr(VinCode, 17).CopyTo(b, o); o += 17;
        FixArr(BmsSoftwareVer, 8).CopyTo(b, o);
        return b;
    }
    private static byte[] FixArr(byte[] arr, int len) => arr.Length >= len ? arr[..len] : arr.Concat(new byte[len - arr.Length]).ToArray();
}
