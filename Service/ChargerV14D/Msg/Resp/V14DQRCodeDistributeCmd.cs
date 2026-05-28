using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>11.6 下发二维�?(0x9C, 下行)</summary>
public class V14DQRCodeDistributeCmd : V14DFrame
{
    public override byte FrameType => V14DFrameType.QRCodeDistribute;
    public byte Gun { get; set; }
    public ushort UrlLength { get; set; }
    public string Url { get; set; } = ""; // 最�?00字节

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

