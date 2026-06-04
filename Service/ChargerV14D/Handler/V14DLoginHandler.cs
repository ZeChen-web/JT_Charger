using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Handler;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DLoginHandler : SimpleChannelInboundHandler<V14DLoginReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DLoginHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DLoginReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D Login from {sn}, pileCode={msg.PileCode}, protocolVer={msg.ProtocolVersion}");
            client.PileCode = msg.PileCode;
            client.IsLoggedIn = true;

            var resp = new V14DLoginResp(msg.PileCode, 0x01) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(resp);
        }
    }
}
