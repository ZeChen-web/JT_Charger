using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.6 二维码下发命令报文 (0x9C，下行)。</summary>
public class V14DQRCodeDistributeCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.QRCodeDistribute;
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>二维码 URL 长度，2 字节 BIN。</summary>
    public ushort UrlLength { get; set; }
    /// <summary>二维码 URL，ASCII 编码，最长 200 字节。</summary>
    public string Url { get; set; } = "";

    public V14DQRCodeDistributeCmd() { }
    public override byte[] GetBodyBytes()
    {
        var urlBytes = global::System.Text.Encoding.ASCII.GetBytes(Url ?? "");
        var urlLen = (ushort)Math.Min(urlBytes.Length, 200);
        var b = new byte[3 + urlLen];
        b[0] = Gun;
        BitConverter.GetBytes(urlLen).CopyTo(b, 1);
        Array.Copy(urlBytes, 0, b, 3, urlLen);
        return b;
    }
}

