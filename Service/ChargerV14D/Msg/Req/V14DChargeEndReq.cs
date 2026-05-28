using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.5 充电结束 (0x19, 上行) GBT-27930</summary>
public class V14DChargeEndReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ChargeEnd;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte BmsEndSoc { get; set; }                 // 1B 1%/位
    public ushort BmsMinCellVoltage { get; set; }       // 2B 0.01V/位
    public ushort BmsMaxCellVoltage { get; set; }       // 2B 0.01V/位
    public byte BmsMinTemp { get; set; }                // 1B 1C/位 -50C偏移
    public byte BmsMaxTemp { get; set; }                // 1B 1C/位 -50C偏移
    public ushort ChargeTime { get; set; }              // 2B 分钟
    public ushort OutputEnergy { get; set; }            // 2B 0.1kWh/位
    public uint ChargerNo { get; set; }                 // 4B 充电机编号

    public V14DChargeEndReq() { }
    public V14DChargeEndReq(byte[] body)
    {
        if (body.Length < 37) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        BmsEndSoc = body[o++];
        BmsMinCellVoltage = BitConverter.ToUInt16(body, o); o += 2;
        BmsMaxCellVoltage = BitConverter.ToUInt16(body, o); o += 2;
        BmsMinTemp = body[o++];
        BmsMaxTemp = body[o++];
        ChargeTime = BitConverter.ToUInt16(body, o); o += 2;
        OutputEnergy = BitConverter.ToUInt16(body, o); o += 2;
        ChargerNo = BitConverter.ToUInt32(body, o);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[37];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = BmsEndSoc;
        BitConverter.GetBytes(BmsMinCellVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsMaxCellVoltage).CopyTo(b, o); o += 2;
        b[o++] = BmsMinTemp; b[o++] = BmsMaxTemp;
        BitConverter.GetBytes(ChargeTime).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(OutputEnergy).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(ChargerNo).CopyTo(b, o);
        return b;
    }
}
