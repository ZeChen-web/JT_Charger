using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.8 充电阶段充电机中止 (0x21, 上行)</summary>
public class V14DChargerAbortReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ChargerAbort;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>充电机中止充电原因，1 字节 BIN。</summary>
    public byte ChargerStopReason { get; set; }
    /// <summary>充电机中止充电故障原因，2 字节 BIN。</summary>
    public ushort ChargerStopFaultReason { get; set; }
    /// <summary>充电机中止充电错误原因，1 字节 BIN。</summary>
    public byte ChargerStopErrorReason { get; set; }

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
