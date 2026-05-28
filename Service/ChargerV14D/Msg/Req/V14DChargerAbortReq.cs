using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.8 充电阶段充电机中止 (0x21, 上行)</summary>
public class V14DChargerAbortReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ChargerAbort;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte ChargerStopReason { get; set; }       // 1B 充电机中止充电原因
    public ushort ChargerStopFaultReason { get; set; } // 2B 充电机中止充电故障原因
    public byte ChargerStopErrorReason { get; set; }   // 1B 充电机中止充电错误原因

    public V14DChargerAbortReq() { }
    public V14DChargerAbortReq(byte[] body)
    {
        if (body.Length < 28) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        ChargerStopReason = body[o++];
        ChargerStopFaultReason = BitConverter.ToUInt16(body, o); o += 2;
        ChargerStopErrorReason = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[28];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = ChargerStopReason;
        BitConverter.GetBytes(ChargerStopFaultReason).CopyTo(b, o); o += 2;
        b[o] = ChargerStopErrorReason;
        return b;
    }
}
