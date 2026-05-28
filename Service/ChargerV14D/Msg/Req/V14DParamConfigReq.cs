using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.4 参数配置 (0x17, 上行) GBT-27930</summary>
public class V14DParamConfigReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ParamConfig;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public ushort BmsMaxCellVoltage { get; set; }      // 2B 0.01V/位
    public ushort BmsMaxChargeCurrent { get; set; }     // 2B 0.1A/位, -400A偏移
    public ushort BmsNominalEnergy { get; set; }         // 2B 0.1kWh/位
    public ushort BmsMaxChargeVoltage { get; set; }      // 2B 0.1V/位
    public byte BmsMaxTemp { get; set; }                 // 1B 1C/位, -50C偏移
    public ushort BmsSoc { get; set; }                   // 2B 0.1%/位
    public ushort BmsCurrentVoltage { get; set; }        // 2B 0.1V/位
    public ushort PileMaxOutputVoltage { get; set; }     // 2B 0.1V/位
    public ushort PileMinOutputVoltage { get; set; }     // 2B 0.1V/位
    public ushort PileMaxOutputCurrent { get; set; }     // 2B 0.1A/位, -400A
    public ushort PileMinOutputCurrent { get; set; }     // 2B 0.1A/位, -400A

    public V14DParamConfigReq() { }
    public V14DParamConfigReq(byte[] body)
    {
        if (body.Length < 44) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        BmsMaxCellVoltage = BitConverter.ToUInt16(body, o); o += 2;
        BmsMaxChargeCurrent = BitConverter.ToUInt16(body, o); o += 2;
        BmsNominalEnergy = BitConverter.ToUInt16(body, o); o += 2;
        BmsMaxChargeVoltage = BitConverter.ToUInt16(body, o); o += 2;
        BmsMaxTemp = body[o++];
        BmsSoc = BitConverter.ToUInt16(body, o); o += 2;
        BmsCurrentVoltage = BitConverter.ToUInt16(body, o); o += 2;
        PileMaxOutputVoltage = BitConverter.ToUInt16(body, o); o += 2;
        PileMinOutputVoltage = BitConverter.ToUInt16(body, o); o += 2;
        PileMaxOutputCurrent = BitConverter.ToUInt16(body, o); o += 2;
        PileMinOutputCurrent = BitConverter.ToUInt16(body, o);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[44];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        BitConverter.GetBytes(BmsMaxCellVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsMaxChargeCurrent).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsNominalEnergy).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsMaxChargeVoltage).CopyTo(b, o); o += 2;
        b[o++] = BmsMaxTemp;
        BitConverter.GetBytes(BmsSoc).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsCurrentVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileMaxOutputVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileMinOutputVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileMaxOutputCurrent).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileMinOutputCurrent).CopyTo(b, o);
        return b;
    }
}
