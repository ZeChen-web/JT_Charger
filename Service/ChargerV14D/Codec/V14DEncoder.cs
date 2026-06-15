using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Utils;
using HybirdFrameworkDriver.Session;
using log4net;
using Newtonsoft.Json;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Msg;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Codec;

/// <summary>V1.4D 协议编码器</summary>
public class V14DEncoder : MessageToByteEncoder<V14DFrame>
{
    //private static readonly ILog Log = LogManager.GetLogger(typeof(V14DEncoder));

    private ILog? Log(string? chargerSn)
    {
        if (ObjUtils.IsNotNullOrWhiteSpace(chargerSn))
        {
            return LogManager.GetLogger(chargerSn);
        }

        return LogManager.GetLogger(typeof(V14DEncoder));
    }

    protected override void Encode(IChannelHandlerContext context, V14DFrame obj, IByteBuffer output)
    {
        byte[] bytes = obj.ToBytes();

        string? sn = ChannelUtils.GetAttr(context.Channel, V14DConst.PileSn);
        Log(sn)?.Info($"send [{bytes[5]:X2}] {BitUtls.BytesToHexStr(bytes)}:{JsonConvert.SerializeObject(obj)} to {sn}");

        output.WriteBytes(bytes);
    }
}