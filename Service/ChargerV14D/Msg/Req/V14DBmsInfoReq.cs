using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>7.10 充电过程BMS信息 (0x25, 上行, 15秒周期)</summary>
public class V14DBmsInfoReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.BmsInfo;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte BmsMaxCellVoltageNo { get; set; }        // 1B 1/位, 1偏移
    public byte BmsMaxTemp { get; set; }                  // 1B 1C/位, -50C
    public byte MaxTempPointNo { get; set; }               // 1B 1/位, 1偏移
    public byte BmsMinTemp { get; set; }                  // 1B 1C/位, -50C
    public byte MinTempPointNo { get; set; }               // 1B 1/位, 1偏移
    public byte BmsCellVoltageAlarm { get; set; }          // 2位
    public byte BmsSocAlarm { get; set; }                  // 2位
    public byte BmsOverCurrent { get; set; }               // 2位
    public byte BmsOverTemp { get; set; }                  // 2位
    public byte BmsInsulation { get; set; }                // 2位
    public byte BmsConnectorStatus { get; set; }           // 2位
    public byte ChargeForbidden { get; set; }              // 2位
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
