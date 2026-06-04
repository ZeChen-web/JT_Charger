using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.4 参数配置 (0x17, 上行) GBT-27930</summary>
public class V14DParamConfigReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ParamConfig;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsMaxCellVoltage { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort BmsMaxChargeCurrent { get; set; }
    /// <summary>动力蓄电池标称总能量，2 字节 BIN，0.1kWh/位。</summary>
    public ushort BmsNominalEnergy { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsMaxChargeVoltage { get; set; }
    /// <summary>温度字段，按协议偏移和精度换算。</summary>
    public byte BmsMaxTemp { get; set; }
    /// <summary>SOC 字段，按协议精度换算。</summary>
    public ushort BmsSoc { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsCurrentVoltage { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort PileMaxOutputVoltage { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort PileMinOutputVoltage { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort PileMaxOutputCurrent { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort PileMinOutputCurrent { get; set; }

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
