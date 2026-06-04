using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.10 充电过程BMS信息 (0x25, 上行, 15秒周期)</summary>
public class V14DBmsInfoReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.BmsInfo;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>最高单体动力蓄电池电压所在编号，1 字节 BIN，1 偏移。</summary>
    public byte BmsMaxCellVoltageNo { get; set; }
    /// <summary>温度字段，按协议偏移和精度换算。</summary>
    public byte BmsMaxTemp { get; set; }
    /// <summary>最高温度检测点编号，1 字节 BIN，1 偏移。</summary>
    public byte MaxTempPointNo { get; set; }
    /// <summary>温度字段，按协议偏移和精度换算。</summary>
    public byte BmsMinTemp { get; set; }
    /// <summary>最低温度检测点编号，1 字节 BIN，1 偏移。</summary>
    public byte MinTempPointNo { get; set; }
    /// <summary>单体动力蓄电池电压过高/过低告警，2 bit。</summary>
    public byte BmsCellVoltageAlarm { get; set; }
    /// <summary>整车动力蓄电池 SOC 过高/过低告警，2 bit。</summary>
    public byte BmsSocAlarm { get; set; }
    /// <summary>动力蓄电池充电过电流告警，2 bit。</summary>
    public byte BmsOverCurrent { get; set; }
    /// <summary>动力蓄电池温度过高告警，2 bit。</summary>
    public byte BmsOverTemp { get; set; }
    /// <summary>动力蓄电池绝缘状态，2 bit。</summary>
    public byte BmsInsulation { get; set; }
    /// <summary>动力蓄电池组输出连接器连接状态，2 bit。</summary>
    public byte BmsConnectorStatus { get; set; }
    /// <summary>BMS 禁止充电状态，2 bit。</summary>
    public byte ChargeForbidden { get; set; }
    /// <summary>保留字段，按协议默认置 0。</summary>
    public byte Reserve { get; set; }

    public V14DBmsInfoReq() { }
    public V14DBmsInfoReq(byte[] body)
    {
        if (body.Length < 33) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        BmsMaxCellVoltageNo = body[o++];
        BmsMaxTemp = body[o++];
        MaxTempPointNo = body[o++];
        BmsMinTemp = body[o++];
        MinTempPointNo = body[o++];
        var flags = body[o++];
        BmsCellVoltageAlarm = (byte)(flags & 0x03);
        BmsSocAlarm = (byte)((flags >> 2) & 0x03);
        BmsOverCurrent = (byte)((flags >> 4) & 0x03);
        BmsOverTemp = (byte)((flags >> 6) & 0x03);
        flags = body[o++];
        BmsInsulation = (byte)(flags & 0x03);
        BmsConnectorStatus = (byte)((flags >> 2) & 0x03);
        ChargeForbidden = (byte)((flags >> 4) & 0x03);
        Reserve = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[33];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        b[o++] = BmsMaxCellVoltageNo; b[o++] = BmsMaxTemp; b[o++] = MaxTempPointNo;
        b[o++] = BmsMinTemp; b[o++] = MinTempPointNo;
        b[o++] = (byte)(BmsCellVoltageAlarm | (BmsSocAlarm << 2) | (BmsOverCurrent << 4) | (BmsOverTemp << 6));
        b[o++] = (byte)(BmsInsulation | (BmsConnectorStatus << 2) | (ChargeForbidden << 4));
        b[o] = Reserve;
        return b;
    }
}
