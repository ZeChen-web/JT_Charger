using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.3 远程更新 (0x94, 下行)</summary>
public class V14DRemoteUpdateCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteUpdate;
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>桩型号，1 字节 BIN。</summary>
    public byte PileModel { get; set; }
    /// <summary>桩额定功率，2 字节 BIN。</summary>
    public ushort PilePower { get; set; }
    /// <summary>升级服务器地址，16 字节 ASCII，右补 0。</summary>
    public string ServerAddress { get; set; } = "";
    /// <summary>升级服务器端口，2 字节 BIN。</summary>
    public ushort ServerPort { get; set; }
    /// <summary>升级服务器用户名，16 字节 ASCII，右补 0。</summary>
    public string Username { get; set; } = "";
    /// <summary>密码字段，定长 ASCII，右补 0。</summary>
    public string Password { get; set; } = "";
    /// <summary>升级文件路径，32 字节 ASCII，右补 0。</summary>
    public string FilePath { get; set; } = "";
    /// <summary>执行控制，1 字节 BIN；0x01 立即执行，0x02 空闲执行。</summary>
    public byte ExecutionControl { get; set; }
    /// <summary>下载超时时间，1 字节 BIN，单位分钟。</summary>
    public byte DownloadTimeout { get; set; }

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
