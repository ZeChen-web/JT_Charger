using DotNetty.Buffers;
using HybirdFrameworkCore.Utils;
using Service.Charger.Common;

namespace Service.Charger.Msg;

public class ASDU : APCI
{
    /// <summary>
    ///     帧类型号
    /// </summary>
    public byte FrameTypeNo { get; set; }

    /// <summary>
    ///     信息体个数
    /// </summary>
    public byte MsgBodyCount { get; set; }

    /// <summary>
    ///     传送原因-3.1.3.2
    /// </summary>
    public ushort TransReason { get; set; }

    /// <summary>
    ///     公共地址
    /// </summary>
    public ushort PublicAddr { get; set; }

    /// <summary>
    ///     信息体地址-3个字节
    /// </summary>
    public byte[]? MsgBodyAddr { get; set; }


    public override byte[] GetBytes()
    {
        var list = new List<byte>();
        list.Add(FrameTypeNo);
        list.Add(MsgBodyCount);
        list.AddRange(BitConverter.GetBytes(TransReason));
        list.AddRange(BitConverter.GetBytes(PublicAddr));
        list.AddRange(MsgBodyAddr);

        list.AddRange(ModelConvert.Encode(this));
        return list.ToArray();
    }


    public static void ParseHeader(byte[] data, ASDU asdu)
    {
        IByteBuffer byteBuffer = Unpooled.WrappedBuffer(data);
        try
        {
            var start = ChargerConst.StartChar.Length - 1;

            asdu.PackLen = byteBuffer.GetUnsignedShortLE(start + 1);
            asdu.CtlArea = byteBuffer.GetUnsignedInt(start + 3);
            asdu.DestAddr = new[]
            {
                byteBuffer.GetByte(start + 7),
                byteBuffer.GetByte(start + 8),
                byteBuffer.GetByte(start + 9),
                byteBuffer.GetByte(start + 10)
            };
            asdu.SrcAddr = byteBuffer.GetUnsignedInt(start + 11);
            asdu.FrameTypeNo = byteBuffer.GetByte(start + 15);
            asdu.MsgBodyCount = byteBuffer.GetByte(start + 16);
            asdu.TransReason = byteBuffer.GetUnsignedShortLE(start + 17);
            asdu.PublicAddr = byteBuffer.GetUnsignedShortLE(start + 19);
            asdu.MsgBodyAddr = new[]
            {
                byteBuffer.GetByte(start + 21),
                byteBuffer.GetByte(start + 22),
                byteBuffer.GetByte(start + 23)
            };
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            byteBuffer.Release();
        }
    }
}
