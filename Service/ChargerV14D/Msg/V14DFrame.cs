using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg;

/// <summary>V1.4D 协议帧基类</summary>
public abstract class V14DFrame
{
    /// <summary>报文序列号，2 字节 BIN；应答报文需与请求保持一致。</summary>
    public ushort SeqNo { get; set; }

    /// <summary>加密标志，1 字节 BIN；0x00 不加密，0x01 3DES 加密。</summary>
    public byte EncryptFlag { get; set; } = V14DConst.EncryptNone;

    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public abstract byte FrameType { get; }

    /// <summary>获取消息体字节数组</summary>
    public abstract byte[] GetBodyBytes();

    /// <summary>序列化为完整帧字节数组</summary>
    public byte[] ToBytes()
    {
        var body = GetBodyBytes();

        // DataLen = SeqNo(2) + EncryptFlag(1) + FrameType(1) + Body(N)
        byte dataLen = checked((byte)(2 + 1 + 1 + body.Length));

        // 构建CRC覆盖部分: SeqNo + EncryptFlag + FrameType + Body
        var crcData = new byte[dataLen];
        BitConverter.GetBytes(SeqNo).CopyTo(crcData, 0);
        crcData[2] = EncryptFlag;
        crcData[3] = FrameType;
        Array.Copy(body, 0, crcData, 4, body.Length);

        // 计算CRC (从SeqNo到Body结束)
        ushort crc = V14DUtils.Crc16(crcData, 0, crcData.Length);

        // 组装完整帧
        var list = new List<byte>();
        list.Add(V14DConst.StartFlag);                    // 起始标志 0x68
        list.Add(dataLen);                                // 数据长度 (1B)
        list.AddRange(crcData);                           // SeqNo + EncryptFlag + FrameType + Body
        list.AddRange(BitConverter.GetBytes(crc));        // CRC16 (2B LE, 低字节在前)

        return list.ToArray();
    }

    /// <summary>从完整帧字节数组中提取Body部分</summary>
    public static byte[] ExtractBody(byte[] fullFrame)
    {
        // fullFrame: StartFlag(1) + DataLen(1) + SeqNo(2) + EncryptFlag(1) + FrameType(1) + Body(N) + CRC(2)
        int dataLen = fullFrame[1];
        int bodyLen = dataLen - 4; // 减去SeqNo(2) + EncryptFlag(1) + FrameType(1)
        var body = new byte[bodyLen];
        Array.Copy(fullFrame, 6, body, 0, bodyLen); // Body起始于偏移6
        return body;
    }

    /// <summary>从完整帧中解析头部信息</summary>
    public static void ParseHeader(byte[] fullFrame, V14DFrame frame)
    {
        frame.SeqNo = BitConverter.ToUInt16(fullFrame, 2);
        frame.EncryptFlag = fullFrame[4];
    }

    /// <summary>从完整帧中获取帧类型</summary>
    public static byte GetFrameType(byte[] fullFrame)
    {
        return fullFrame[5];
    }
}
