using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>10.2 遥控地锁升锁与降锁命令 (0x62, 下行)</summary>
public class V14DLockControlCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.LockControl;
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Command { get; set; } // 0x55升锁 0xFF降锁
    public uint Reserve { get; set; }

    public V14DLockControlCmd() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[12];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = Gun; b[8] = Command;
        return b;
    }
}
