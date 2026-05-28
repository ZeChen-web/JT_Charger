using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Utils;
using log4net;
using Newtonsoft.Json;
using Service.ChargerV14D.Msg;

namespace Service.ChargerV14D.Codec;

/// <summary>V1.4D 协议编码器</summary>
public class V14DEncoder : MessageToByteEncoder<V14DFrame>
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DEncoder));

    protected override void Encode(IChannelHandlerContext context, V14DFrame msg, IByteBuffer output)
    {
        byte[] bytes = msg.ToBytes();
        Log.Debug($"V14D Send: {BitUtls.BytesToHexStr(bytes)} : {JsonConvert.SerializeObject(msg)}");
        output.WriteBytes(bytes);
    }
}
