using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.6 错误报文 (0x1B, 上行)</summary>
public class V14DErrorMsgReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.ErrorMsg;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte RecvBro00Timeout { get; set; }
    public byte RecvBroAATimeout { get; set; }
    public byte Reserve1 { get; set; }
    public byte RecvTimeSyncTimeout { get; set; }
    public byte RecvChargeReadyTimeout { get; set; }
    public byte Reserve2 { get; set; }
    public byte RecvChargeStatusTimeout { get; set; }
    public byte RecvChargerAbortTimeout { get; set; }
    public byte Reserve3 { get; set; }
    public byte RecvChargeStatsTimeout { get; set; }
    public byte BmsOther { get; set; }
    public byte RecvBmsIdentifyTimeout { get; set; }
    public byte Reserve4 { get; set; }
    public byte RecvBatParamTimeout { get; set; }
    public byte RecvBmsReadyTimeout { get; set; }
    public byte Reserve5 { get; set; }
    public byte RecvBatStatusTimeout { get; set; }
    public byte RecvBatRequireTimeout { get; set; }
    public byte RecvBmsAbortTimeout { get; set; }
    public byte Reserve6 { get; set; }
    public byte RecvBmsStatsTimeout { get; set; }
    public byte ChargerOther { get; set; }

    public V14DErrorMsgReq() { }
    public V14DErrorMsgReq(byte[] body)
    {
        if (body.Length < 30) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        RecvBro00Timeout = body[o++]; RecvBroAATimeout = body[o++]; Reserve1 = body[o++];
        RecvTimeSyncTimeout = body[o++]; RecvChargeReadyTimeout = body[o++]; Reserve2 = body[o++];
        RecvChargeStatusTimeout = body[o++]; RecvChargerAbortTimeout = body[o++]; Reserve3 = body[o++];
        RecvChargeStatsTimeout = body[o++]; BmsOther = body[o++]; RecvBmsIdentifyTimeout = body[o++];
        Reserve4 = body[o++]; RecvBatParamTimeout = body[o++]; RecvBmsReadyTimeout = body[o++];
        Reserve5 = body[o++]; RecvBatStatusTimeout = body[o++]; RecvBatRequireTimeout = body[o++];
        RecvBmsAbortTimeout = body[o++]; Reserve6 = body[o++]; RecvBmsStatsTimeout = body[o++];
        ChargerOther = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[30];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        b[o++] = RecvBro00Timeout; b[o++] = RecvBroAATimeout; b[o++] = Reserve1;
        b[o++] = RecvTimeSyncTimeout; b[o++] = RecvChargeReadyTimeout; b[o++] = Reserve2;
        b[o++] = RecvChargeStatusTimeout; b[o++] = RecvChargerAbortTimeout; b[o++] = Reserve3;
        b[o++] = RecvChargeStatsTimeout; b[o++] = BmsOther; b[o++] = RecvBmsIdentifyTimeout;
        b[o++] = Reserve4; b[o++] = RecvBatParamTimeout; b[o++] = RecvBmsReadyTimeout;
        b[o++] = Reserve5; b[o++] = RecvBatStatusTimeout; b[o++] = RecvBatRequireTimeout;
        b[o++] = RecvBmsAbortTimeout; b[o++] = Reserve6; b[o++] = RecvBmsStatsTimeout;
        b[o] = ChargerOther;
        return b;
    }
}
