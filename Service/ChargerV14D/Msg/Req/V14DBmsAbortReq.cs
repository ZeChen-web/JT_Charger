using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.7 充电阶段BMS中止 (0x1D, 上行)</summary>
public class V14DBmsAbortReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BmsAbort;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte BmsStopReason { get; set; }       // 1B BMS中止充电原因
    public ushort BmsStopFaultReason { get; set; } // 2B BMS中止充电故障原因
    public byte BmsStopErrorReason { get; set; }   // 1B BMS中止充电错误原因

    public V14DBmsAbortReq() { }
    public V14DBmsAbortReq(byte[] body)
    {
        if (body.Length < 28) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        BmsStopReason = body[o++];
        BmsStopFaultReason = BitConverter.ToUInt16(body, o); o += 2;
        BmsStopErrorReason = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[28];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = BmsStopReason;
        BitConverter.GetBytes(BmsStopFaultReason).CopyTo(b, o); o += 2;
        b[o] = BmsStopErrorReason;
        return b;
    }
}
