using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Utils;
using HybirdFrameworkDriver.Session;
using log4net;
using Newtonsoft.Json;
using Service.Charger.Common;
using Service.Charger.Msg;

namespace Service.Charger.Codec;

/// <summary>
///
/// </summary>
public class Encoder : MessageToByteEncoder<APCI>
{
    private ILog? Log(string? chargerSn)
    {
        if (ObjUtils.IsNotNullOrWhiteSpace(chargerSn))
        {
            return LogManager.GetLogger("Charger" + chargerSn);
        }
        return LogManager.GetLogger(typeof(Encoder));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <param name="obj"></param>
    /// <param name="output"></param>
    protected override void Encode(IChannelHandlerContext context, APCI obj, IByteBuffer output)
    {
        string? s = ChannelUtils.GetAttr(context.Channel, ChargerConst.DestAddr);
        string? sn = ChannelUtils.GetAttr(context.Channel, ChargerConst.ChargerSn);
        if (s != null)
        {
            byte[] destAddr = s.Split(",").Select(b => Convert.ToByte(b)).ToArray();
            obj.DestAddr = destAddr;
        }

        byte[] bytes = obj.ToBytes();
        Log(sn)?.Info($"send {BitUtls.BytesToHexStr(bytes)}:{JsonConvert.SerializeObject(obj)} to {ChannelUtils.GetAttr(context.Channel, ChargerConst.ChargerSn)}");

        output.WriteBytes(bytes);
    }
}
