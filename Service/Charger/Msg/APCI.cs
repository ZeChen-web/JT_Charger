using HybirdFrameworkDriver.Common;
using Service.Charger.Common;

namespace Service.Charger.Msg;

public abstract class APCI : IToBytes
{
    /// <summary>
    ///     报文长度
    /// </summary>
    public ushort PackLen { get; set; }

    /// <summary>
    ///     控制域
    /// </summary>
    public uint CtlArea { get; set; }

    /// <summary>
    ///     目标地址
    /// </summary>
    public byte[]? DestAddr { get; set; }

    /// <summary>
    ///     源地址
    /// </summary>
    public uint SrcAddr { get; set; }


    public byte[] ToBytes()
    {
        var bodyBytes = GetBytes();
        var list = new List<byte>();
        list.AddRange(ChargerConst.StartChar);
        list.AddRange(BitConverter.GetBytes((ushort)(bodyBytes.Length + 12)));
        list.AddRange(BitConverter.GetBytes(CtlArea));
        if (DestAddr != null)
        {
            list.AddRange(DestAddr);
        }
        else
        {
            DestAddr=new byte[4];
            list.AddRange(DestAddr);
        }
        list.AddRange(BitConverter.GetBytes(SrcAddr));

        list.AddRange(bodyBytes);

        return list.ToArray();
    }

    public abstract byte[] GetBytes();
}