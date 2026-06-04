using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.6 错误报文 (0x1B, 上行)</summary>
public class V14DErrorMsgReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ErrorMsg;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>RecvBro00Timeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBro00Timeout { get; set; }
    /// <summary>RecvBroAATimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBroAATimeout { get; set; }
    /// <summary>保留位 1，按协议默认置 0。</summary>
    public byte Reserve1 { get; set; }
    /// <summary>RecvTimeSyncTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvTimeSyncTimeout { get; set; }
    /// <summary>RecvChargeReadyTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvChargeReadyTimeout { get; set; }
    /// <summary>保留位 2，按协议默认置 0。</summary>
    public byte Reserve2 { get; set; }
    /// <summary>RecvChargeStatusTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvChargeStatusTimeout { get; set; }
    /// <summary>RecvChargerAbortTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvChargerAbortTimeout { get; set; }
    /// <summary>保留位 3，按协议默认置 0。</summary>
    public byte Reserve3 { get; set; }
    /// <summary>RecvChargeStatsTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvChargeStatsTimeout { get; set; }
    /// <summary>BMS 其他故障标志，2 bit。</summary>
    public byte BmsOther { get; set; }
    /// <summary>RecvBmsIdentifyTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBmsIdentifyTimeout { get; set; }
    /// <summary>保留位 4，按协议默认置 0。</summary>
    public byte Reserve4 { get; set; }
    /// <summary>RecvBatParamTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBatParamTimeout { get; set; }
    /// <summary>RecvBmsReadyTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBmsReadyTimeout { get; set; }
    /// <summary>保留位 5，按协议默认置 0。</summary>
    public byte Reserve5 { get; set; }
    /// <summary>RecvBatStatusTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBatStatusTimeout { get; set; }
    /// <summary>RecvBatRequireTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBatRequireTimeout { get; set; }
    /// <summary>RecvBmsAbortTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBmsAbortTimeout { get; set; }
    /// <summary>保留位 6，按协议默认置 0。</summary>
    public byte Reserve6 { get; set; }
    /// <summary>RecvBmsStatsTimeout 超时标志，2 bit，按协议错误报文字段定义。</summary>
    public byte RecvBmsStatsTimeout { get; set; }
    /// <summary>充电机其他故障标志，2 bit。</summary>
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
