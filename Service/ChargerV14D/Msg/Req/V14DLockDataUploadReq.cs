using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>10.1 地锁数据上送 (0x61, 上行)</summary>
public class V14DLockDataUploadReq : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.LockDataUpload;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>地锁状态，1 字节 BIN。</summary>
    public byte LockStatus { get; set; }
    /// <summary>车位状态，1 字节 BIN。</summary>
    public byte ParkingStatus { get; set; }
    /// <summary>电池在仓状态，1 字节 BIN。</summary>
    public byte BatteryStatus { get; set; }
    /// <summary>告警状态，1 字节 BIN。</summary>
    public byte AlarmStatus { get; set; }
    /// <summary>保留字段，按协议默认置 0。</summary>
    public uint Reserve { get; set; }

    public V14DLockDataUploadReq() { }
    public V14DLockDataUploadReq(byte[] body)
    {
        if (body.Length < 16) return;
        int o = 0;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        LockStatus = body[o++];
        ParkingStatus = body[o++];
        BatteryStatus = body[o++];
        AlarmStatus = body[o++];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[12];
        int o = 0;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = LockStatus; b[o++] = ParkingStatus;
        b[o++] = BatteryStatus; b[o++] = AlarmStatus;
        return b;
    }
}
