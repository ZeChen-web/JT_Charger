using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>10.1 地锁数据上送 (0x61, 上行)</summary>
public class V14DLockDataUploadReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.LockDataUpload;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte LockStatus { get; set; }        // 0x00未入位 0x55已升锁 0xFF已降锁
    public byte ParkingStatus { get; set; }     // 0x00无车 0xFF有车
    public byte BatteryStatus { get; set; }     // 0~100 %
    public byte AlarmStatus { get; set; }       //
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
