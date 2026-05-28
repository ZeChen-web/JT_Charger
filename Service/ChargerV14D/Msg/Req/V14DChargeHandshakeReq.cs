using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.3 充电握手 (0x15, 上行) GBT-27930充电桩与BMS充电握手阶段报文</summary>
public class V14DChargeHandshakeReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ChargeHandshake;
    public string TransactionSN { get; set; } = "";     // 16B BCD
    public string PileCode { get; set; } = "";           // 7B BCD
    public byte Gun { get; set; }                         // 1B
    public byte[] BmsProtocolVer { get; set; } = new byte[3]; // 3B BMS协议版本
    public byte BmsBatType { get; set; }                  // 1B 电池类型
    public ushort BmsRatedCapacity { get; set; }          // 2B 0.1Ah/位
    public ushort BmsRatedVoltage { get; set; }           // 2B 0.1V/位
    public byte[] BmsManufacturer { get; set; } = new byte[4]; // 4B ASCII
    public byte[] BmsBatSN { get; set; } = new byte[4];   // 4B BIN
    public byte BmsYear { get; set; }                      // 1B (1985偏移)
    public byte BmsMonth { get; set; }                     // 1B
    public byte BmsDay { get; set; }                       // 1B
    public byte[] BmsChargeCount { get; set; } = new byte[3]; // 3B
    public byte BmsOwnership { get; set; }                 // 1B
    public byte Reserve { get; set; }                      // 1B
    public byte[] VinCode { get; set; } = new byte[17];   // 17B VIN
    public byte[] BmsSoftwareVer { get; set; } = new byte[8]; // 8B

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
