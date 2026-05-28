using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.9 充电过程BMS需求与充电机输出 (0x23, 上行, 15秒周期)</summary>
public class V14DBmsDemandOutputReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BmsDemandOutput;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public ushort BmsVoltageDemand { get; set; }       // 2B 0.1V/位
    public ushort BmsCurrentDemand { get; set; }        // 2B 0.1A/位, -400A
    public byte BmsChargeMode { get; set; }              // 1B 0x01恒压 0x02恒流
    public ushort BmsVoltageMeasured { get; set; }       // 2B 0.1V/位
    public ushort BmsCurrentMeasured { get; set; }       // 2B 0.1A/位, -400A
    public ushort BmsMaxCellVoltageGroup { get; set; }   // 2B 高12位电压, 低4位组号
    public byte BmsSoc { get; set; }                     // 1B 1%/位
    public ushort BmsRemainTime { get; set; }            // 2B 分钟
    public ushort PileOutputVoltage { get; set; }        // 2B 0.1V/位
    public ushort PileOutputCurrent { get; set; }        // 2B 0.1A/位, -400A
    public ushort AccumulatedChargeTime { get; set; }    // 2B 分钟

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
