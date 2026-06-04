using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>10.2 遥控地锁升锁与降锁命令 (0x62, 下行)</summary>
public class V14DLockControlCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.LockControl;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>控制命令，1 字节 BIN；0x55 升锁，0xFF 降锁。</summary>
    public byte Command { get; set; }
    /// <summary>保留字段，按协议默认置 0。</summary>
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
