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
    /// <summary>充电机中止充电原因，
    /// 1-2 位——达到充电机设定的条件中止
    /// 3-4 位——人工中止
    /// 5-6 位——异常中止
    /// 7-8 位——BMS 主动中止
    /// </summary>
    public byte ChargerStopReason { get; set; }
    /// <summary>充电机中止充电故障原因，2 字节
    /// 1-2 位——充电机过温故障
    /// 3-4 位——充电连接器故障
    /// 5-6 位——充电机内部过温故障
    /// 7-8 位——所需电量不能传送
    /// 9-10 位——充电机急停故障
    /// 11-12 位——其他故障
    /// 13-16 位——预留位</summary>
    public ushort ChargerStopFaultReason { get; set; }
    /// <summary>充电机中止充电错误原因，1 字节 BIN。
    /// 1-2 位——电流不匹配
    /// 3-4 位——电压异常
    /// 5-8 位——预留位</summary>
    public byte ChargerStopErrorReason { get; set; }
    
    
    // =========================
    // ChargerStopReason (1 byte)
    // =========================

    /// <summary>1-2：达到设定条件中止</summary>
    public bool StopByTargetCondition => Get2Bit(ChargerStopReason, 0);

    /// <summary>3-4：人工中止</summary>
    public bool StopByManual => Get2Bit(ChargerStopReason, 1);

    /// <summary>5-6：异常中止</summary>
    public bool StopByException => Get2Bit(ChargerStopReason, 2);

    /// <summary>7-8：BMS主动中止</summary>
    public bool StopByBms => Get2Bit(ChargerStopReason, 3);

    // =========================
    // FaultReason (2 bytes)
    // =========================

    /// <summary>1-2：充电机过温故障</summary>
    public bool ChargerOverTempFault => Get2Bit(ChargerStopFaultReason, 0);

    /// <summary>3-4：充电连接器故障</summary>
    public bool ChargerConnectorFault => Get2Bit(ChargerStopFaultReason, 1);

    /// <summary>5-6：充电机内部过温故障</summary>
    public bool InternalOverTempFault => Get2Bit(ChargerStopFaultReason, 2);

    /// <summary>7-8：所需电量不能传送</summary>
    public bool PowerTransferFault => Get2Bit(ChargerStopFaultReason, 3);

    /// <summary>9-10：急停故障</summary>
    public bool EmergencyStopFault => Get2Bit(ChargerStopFaultReason, 4);

    /// <summary>11-12：其他故障</summary>
    public bool OtherFault => Get2Bit(ChargerStopFaultReason, 5);

    // =========================
    // ErrorReason (1 byte)
    // =========================

    /// <summary>1-2：电流不匹配</summary>
    public bool CurrentMismatch => Get2Bit(ChargerStopErrorReason, 0);

    /// <summary>3-4：电压异常</summary>
    public bool VoltageAbnormal => Get2Bit(ChargerStopErrorReason, 1);

    // =========================
    // Bit解析
    // =========================

    private bool Get2Bit(byte value, int index)
    {
        int shift = index * 2;
        return ((value >> shift) & 0x03) != 0;
    }

    private bool Get2Bit(ushort value, int index)
    {
        int shift = index * 2;
        return ((value >> shift) & 0x03) != 0;
    }

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
