using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.7 充电阶段BMS中止 (0x1D, 上行)</summary>
public class V14DBmsAbortReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BmsAbort;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>BMS 中止充电原因，1 字节 BIN。</summary>
    public byte BmsStopReason { get; set; }
    /// <summary>BMS 中止充电故障原因，2 字节 BIN。</summary>
    public ushort BmsStopFaultReason { get; set; }
    /// <summary>BMS 中止充电错误原因，1 字节 BIN。</summary>
    public byte BmsStopErrorReason { get; set; }

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
