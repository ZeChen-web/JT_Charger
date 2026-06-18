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

    
    /// <summary>
    /// ===== FaultReason（16bit，2字节故障位）=====
    /// 每2bit表示一种故障状态：
    /// 00=正常，01=预警，10=故障，11=预留
    /// </summary>

    /// <summary>1-2位：绝缘故障</summary>
    public bool InsulationFault => Get2Bit(BmsStopFaultReason, 0);

    /// <summary>3-4位：输出连接器过温故障</summary>
    public bool OutputConnectorOverTempFault => Get2Bit(BmsStopFaultReason, 1);

    /// <summary>5-6位：BMS元件 / 输出连接器过温</summary>
    public bool BmsComponentOverTempFault => Get2Bit(BmsStopFaultReason, 2);

    /// <summary>7-8位：充电连接器故障</summary>
    public bool ChargeConnectorFault => Get2Bit(BmsStopFaultReason, 3);

    /// <summary>9-10位：电池组温度过高故障</summary>
    public bool BatteryOverTempFault => Get2Bit(BmsStopFaultReason, 4);

    /// <summary>11-12位：高压继电器故障</summary>
    public bool HighVoltageRelayFault => Get2Bit(BmsStopFaultReason, 5);

    /// <summary>13-14位：检测点2电压检测故障</summary>
    public bool DetectPoint2VoltageFault => Get2Bit(BmsStopFaultReason, 6);

    /// <summary>15-16位：其他故障</summary>
    public bool OtherFault => Get2Bit(BmsStopFaultReason, 7);


    /// <summary>
    /// ===== ErrorReason（8bit，1字节错误位）=====
    /// bit0-1：电流过大
    /// bit2-3：电压异常
    /// bit4-7：预留
    /// </summary>

    /// <summary>1-2位：电流过大</summary>
    public bool OverCurrentFault => Get2Bit(BmsStopErrorReason, 0);

    /// <summary>3-4位：电压异常</summary>
    public bool VoltageAbnormalFault => Get2Bit(BmsStopErrorReason, 1);
    
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
    
    private bool Get2Bit(ushort value, int index)
    {
        int shift = index * 2;
        return ((value >> shift) & 0x03) != 0;
    }
}
