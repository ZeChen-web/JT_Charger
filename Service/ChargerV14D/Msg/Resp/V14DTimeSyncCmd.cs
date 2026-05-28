using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>9.3 对时设置 (0x56, 下行)</summary>
public class V14DTimeSyncCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.TimeSync;
    public string PileCode { get; set; } = "";
    public byte[] CurrentTime { get; set; } = new byte[7]; // 7B CP56Time2a

    public V14DTimeSyncCmd() { }
    public V14DTimeSyncCmd(string pileCode, DateTime dt)
    {
        PileCode = pileCode;
        CurrentTime = V14DUtils.DateTimeToCP56Time2a(dt);
    }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[14];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        CurrentTime.CopyTo(b, 7);
        return b;
    }
}
