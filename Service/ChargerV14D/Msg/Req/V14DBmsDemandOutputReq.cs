using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.9 充电过程BMS需求与充电机输出 (0x23, 上行, 15秒周期)</summary>
public class V14DBmsDemandOutputReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BmsDemandOutput;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsVoltageDemand { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort BmsCurrentDemand { get; set; }
    /// <summary>BMS 充电模式，1 字节 BIN；0x01 恒压，0x02 恒流。</summary>
    public byte BmsChargeMode { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort BmsVoltageMeasured { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort BmsCurrentMeasured { get; set; }
    /// <summary>最高单体动力蓄电池电压及组号，2 字节 BIN；高 12 位电压，低 4 位组号。</summary>
    public ushort BmsMaxCellVoltageGroup { get; set; }
    /// <summary>SOC 字段，按协议精度换算。</summary>
    public byte BmsSoc { get; set; }
    /// <summary>BMS 估算剩余充电时间，2 字节 BIN，单位分钟。</summary>
    public ushort BmsRemainTime { get; set; }
    /// <summary>电压字段，按协议精度换算。</summary>
    public ushort PileOutputVoltage { get; set; }
    /// <summary>电流字段，按协议精度换算。</summary>
    public ushort PileOutputCurrent { get; set; }
    /// <summary>时间字段，按协议格式解析。</summary>
    public ushort AccumulatedChargeTime { get; set; }

    public V14DBmsDemandOutputReq() { }
    public V14DBmsDemandOutputReq(byte[] body)
    {
        if (body.Length < 42) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        BmsVoltageDemand = BitConverter.ToUInt16(body, o); o += 2;
        BmsCurrentDemand = BitConverter.ToUInt16(body, o); o += 2;
        BmsChargeMode = body[o++];
        BmsVoltageMeasured = BitConverter.ToUInt16(body, o); o += 2;
        BmsCurrentMeasured = BitConverter.ToUInt16(body, o); o += 2;
        BmsMaxCellVoltageGroup = BitConverter.ToUInt16(body, o); o += 2;
        BmsSoc = body[o++];
        BmsRemainTime = BitConverter.ToUInt16(body, o); o += 2;
        PileOutputVoltage = BitConverter.ToUInt16(body, o); o += 2;
        PileOutputCurrent = BitConverter.ToUInt16(body, o); o += 2;
        AccumulatedChargeTime = BitConverter.ToUInt16(body, o);
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[42];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        BitConverter.GetBytes(BmsVoltageDemand).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsCurrentDemand).CopyTo(b, o); o += 2;
        b[o++] = BmsChargeMode;
        BitConverter.GetBytes(BmsVoltageMeasured).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsCurrentMeasured).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(BmsMaxCellVoltageGroup).CopyTo(b, o); o += 2;
        b[o++] = BmsSoc;
        BitConverter.GetBytes(BmsRemainTime).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileOutputVoltage).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PileOutputCurrent).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(AccumulatedChargeTime).CopyTo(b, o);
        return b;
    }
}
