using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.3 远程更新 (0x94, 下行)</summary>
public class V14DRemoteUpdateCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteUpdate;
    public string PileCode { get; set; } = "";      // 7B BCD
    public byte PileModel { get; set; }              // 1B
    public ushort PilePower { get; set; }             // 2B
    public string ServerAddress { get; set; } = "";  // 16B ASCII
    public ushort ServerPort { get; set; }            // 2B
    public string Username { get; set; } = "";       // 16B ASCII
    public string Password { get; set; } = "";       // 16B ASCII
    public string FilePath { get; set; } = "";       // 32B ASCII
    public byte ExecutionControl { get; set; }       // 1B
    public byte DownloadTimeout { get; set; }        // 1B 分钟

    public V14DRemoteUpdateCmd() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[94];
        int o = 0;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = PileModel;
        BitConverter.GetBytes(PilePower).CopyTo(b, o); o += 2;
        V14DUtils.WriteAscii(ServerAddress, b, o, 16); o += 16;
        BitConverter.GetBytes(ServerPort).CopyTo(b, o); o += 2;
        V14DUtils.WriteAscii(Username, b, o, 16); o += 16;
        V14DUtils.WriteAscii(Password, b, o, 16); o += 16;
        V14DUtils.WriteAscii(FilePath, b, o, 32); o += 32;
        b[o++] = ExecutionControl; b[o] = DownloadTimeout;
        return b;
    }
}
