using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>
/// 7.1 上传实时监测数据 (帧类�?0x13, 上行, 待机5分钟/充电15秒周�?
/// Body大小: 64字节
/// </summary>
public class V14DRealTimeDataReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.UploadRealTimeData;

    public string TransactionSN { get; set; } = "";   // 16B BCD
    public string PileCode { get; set; } = "";         // 7B BCD
    public byte Gun { get; set; }                      // 1B
    public byte Status { get; set; }                   // 1B (0x00离线-0x05充电完成)
    public byte GunHome { get; set; }                  // 1B (0x00鍚?0x01鏄?0x02鏈煡)
    public byte GunConnected { get; set; }             // 1B (0x00鍚?0x01鏄?
    public ushort OutputVoltage { get; set; }          // 2B (0.1V/�?
    public ushort OutputCurrent { get; set; }          // 2B (0.1A/�?
    public byte GunTemp { get; set; }                  // 1B (整型, 偏移�?50C)
    public string GunCode { get; set; } = "";          // 8B BIN
    public byte SOC { get; set; }                      // 1B (%)
    public byte MaxBatTemp { get; set; }               // 1B (整型, 偏移�?50C)
    public ushort ChargeTime { get; set; }             // 2B (分钟)
    public ushort RemainTime { get; set; }             // 2B (分钟)
    public uint ChargeKWH { get; set; }                // 4B (4位小�?
    public uint LossKWH { get; set; }                  // 4B (4位小�?
    public uint ChargeAmount { get; set; }             // 4B (4位小�?
    public byte HardwareFault { get; set; }            // 1B (鐩存祦瑙侀檮褰?3.1.1)
    public byte HardwareWarning { get; set; }           // 1B (鐩存祦瑙侀檮褰?3.1.1)
    public uint BMSBatSN { get; set; }                 // 4B (厂商自定�?

    // 计算属�?
    public float OutputVoltageValue => OutputVoltage * 0.1f;
    public float OutputCurrentValue => OutputCurrent * 0.1f;
    public int GunTempValue => GunTemp - 50;
    public int MaxBatTempValue => MaxBatTemp - 50;
    public float ChargeKWHValue => ChargeKWH * 0.0001f;
    public float LossKWHValue => LossKWH * 0.0001f;
    public float ChargeAmountValue => ChargeAmount * 0.0001f;
    public float ChargePower => OutputVoltageValue * OutputCurrentValue / 1000f; // kW

    public V14DRealTimeDataReq() { }

    public V14DRealTimeDataReq(byte[] body)
    {
        if (body.Length < 64) return;
        int offset = 0;

        TransactionSN = V14DUtils.BcdToString(body, offset, 16); offset += 16;
        PileCode = V14DUtils.BcdToString(body, offset, 7); offset += 7;
        Gun = body[offset++];
        Status = body[offset++];
        GunHome = body[offset++];
        GunConnected = body[offset++];
        OutputVoltage = BitConverter.ToUInt16(body, offset); offset += 2;
        OutputCurrent = BitConverter.ToUInt16(body, offset); offset += 2;
        GunTemp = body[offset++];
        GunCode = BitConverter.ToString(body, offset, 8); offset += 8;
        SOC = body[offset++];
        MaxBatTemp = body[offset++];
        ChargeTime = BitConverter.ToUInt16(body, offset); offset += 2;
        RemainTime = BitConverter.ToUInt16(body, offset); offset += 2;
        ChargeKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        LossKWH = BitConverter.ToUInt32(body, offset); offset += 4;
        ChargeAmount = BitConverter.ToUInt32(body, offset); offset += 4;
        HardwareFault = body[offset++];
        HardwareWarning = body[offset++];
        BMSBatSN = BitConverter.ToUInt32(body, offset);
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[64];
        int offset = 0;

        var tsnBcd = V14DUtils.StringToBcd(TransactionSN, 16);
        Array.Copy(tsnBcd, 0, body, offset, 16); offset += 16;
        var pileBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileBcd, 0, body, offset, 7); offset += 7;
        body[offset++] = Gun;
        body[offset++] = Status;
        body[offset++] = GunHome;
        body[offset++] = GunConnected;
        BitConverter.GetBytes(OutputVoltage).CopyTo(body, offset); offset += 2;
        BitConverter.GetBytes(OutputCurrent).CopyTo(body, offset); offset += 2;
        body[offset++] = GunTemp;
        // GunCode as BIN (raw bytes)
        {
            var gc = string.IsNullOrEmpty(GunCode) ? new byte[8] : global::System.Text.Encoding.ASCII.GetBytes(GunCode.PadRight(8, '\0').Substring(0, 8));
            Array.Copy(gc, 0, body, offset, Math.Min(8, gc.Length)); offset += 8;
        }
        body[offset++] = SOC;
        body[offset++] = MaxBatTemp;
        BitConverter.GetBytes(ChargeTime).CopyTo(body, offset); offset += 2;
        BitConverter.GetBytes(RemainTime).CopyTo(body, offset); offset += 2;
        BitConverter.GetBytes(ChargeKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(LossKWH).CopyTo(body, offset); offset += 4;
        BitConverter.GetBytes(ChargeAmount).CopyTo(body, offset); offset += 4;
        body[offset++] = HardwareFault;
        body[offset++] = HardwareWarning;
        BitConverter.GetBytes(BMSBatSN).CopyTo(body, offset);

        return body;
    }
}


