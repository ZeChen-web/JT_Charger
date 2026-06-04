using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.1 远程重启 (0x92, 下行)</summary>
public class V14DRemoteRestartCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteRestart;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>执行控制，1 字节 BIN；0x01 立即执行，0x02 空闲执行。</summary>
    public byte ExecutionControl { get; set; }
    public V14DRemoteRestartCmd() { }
    public V14DRemoteRestartCmd(string pileCode, byte exec) { PileCode = pileCode; ExecutionControl = exec; }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = ExecutionControl;
        return b;
    }
}
