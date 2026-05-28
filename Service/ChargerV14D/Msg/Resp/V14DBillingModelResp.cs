using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>6.8 计费模型请求应答 (0x0A, 下行) 48个半小时费率段</summary>
public class V14DBillingModelResp : V14DFrame
{
    public override byte FrameType => V14DFrameType.BillingModelResp;
    public string PileCode { get; set; } = "";
    public ushort ModelNo { get; set; }
    public uint PeakElecRate { get; set; }
    public uint PeakServiceRate { get; set; }
    public uint ShoulderElecRate { get; set; }
    public uint ShoulderServiceRate { get; set; }
    public uint FlatElecRate { get; set; }
    public uint FlatServiceRate { get; set; }
    public uint ValleyElecRate { get; set; }
    public uint ValleyServiceRate { get; set; }
    public byte LossRatio { get; set; }
    public byte[] RateSegments { get; set; } = new byte[48]; // 每半小时一个费率号

    public V14DBillingModelResp() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[94];
        int o = 0;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        BitConverter.GetBytes(ModelNo).CopyTo(b, o); o += 2;
        BitConverter.GetBytes(PeakElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(PeakServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ShoulderElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ShoulderServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(FlatElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(FlatServiceRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ValleyElecRate).CopyTo(b, o); o += 4;
        BitConverter.GetBytes(ValleyServiceRate).CopyTo(b, o); o += 4;
        b[o++] = LossRatio;
        Array.Copy(RateSegments, 0, b, o, 48);
        return b;
    }
}
