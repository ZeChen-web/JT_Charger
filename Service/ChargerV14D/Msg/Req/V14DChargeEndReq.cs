using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.5 充电结束 (0x19, 上行) GBT-27930</summary>
public class V14DChargeEndReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.ChargeEnd;

    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";

    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";

    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }

    /// <summary>BMS 中止荷电状态 SOC，1 字节 BIN，1%/位。</summary>
    public byte BmsEndSoc { get; set; }

    /// <summary>BMS 动力蓄电池单体最低电压。</summary>
    public ushort BmsMinCellVoltage { get; set; }

    /// <summary>BMS 动力蓄电池单体最高电压。</summary>
    public ushort BmsMaxCellVoltage { get; set; }

    /// <summary>BMS 动力蓄电池最低温度。</summary>
    public byte BmsMinTemp { get; set; }

    /// <summary>BMS 动力蓄电池最高温度。</summary>
    public byte BmsMaxTemp { get; set; }

    /// <summary>电桩累计充电时间。</summary>
    public ushort ChargeTime { get; set; }

    /// <summary>电桩输出能量</summary>
    public ushort OutputEnergy { get; set; }

    /// <summary>充电机编号，4 字节 BIN。</summary>
    public uint ChargerNo { get; set; }

    public V14DChargeEndReq()
    {
    }

    public V14DChargeEndReq(byte[] body)
    {
        if (body.Length < 37) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16);
        o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7);
        o += 7;
        Gun = body[o++];
        BmsEndSoc = body[o++];
        BmsMinCellVoltage = BitConverter.ToUInt16(body, o);
        o += 2;
        BmsMaxCellVoltage = BitConverter.ToUInt16(body, o);
        o += 2;
        BmsMinTemp = body[o++];
        BmsMaxTemp = body[o++];
        ChargeTime = BitConverter.ToUInt16(body, o);
        o += 2;
        OutputEnergy = BitConverter.ToUInt16(body, o);
        o += 2;
        ChargerNo = BitConverter.ToUInt32(body, o);
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[37];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o);
        o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o);
        o += 7;
        b[o++] = Gun;
        b[o++] = BmsEndSoc;
        BitConverter.GetBytes(BmsMinCellVoltage).CopyTo(b, o);
        o += 2;
        BitConverter.GetBytes(BmsMaxCellVoltage).CopyTo(b, o);
        o += 2;
        b[o++] = BmsMinTemp;
        b[o++] = BmsMaxTemp;
        BitConverter.GetBytes(ChargeTime).CopyTo(b, o);
        o += 2;
        BitConverter.GetBytes(OutputEnergy).CopyTo(b, o);
        o += 2;
        BitConverter.GetBytes(ChargerNo).CopyTo(b, o);
        return b;
    }
}