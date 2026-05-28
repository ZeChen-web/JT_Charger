using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.1 远程重启 (0x92, 下行)</summary>
public class V14DRemoteRestartCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteRestart;
    public string PileCode { get; set; } = "";
    public byte ExecutionControl { get; set; } // 0x01立即 0x02空闲
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
