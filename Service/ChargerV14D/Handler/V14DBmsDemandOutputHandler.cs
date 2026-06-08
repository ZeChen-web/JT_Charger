using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBmsDemandOutputHandler : SimpleChannelInboundHandler<V14DBmsDemandOutputReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBmsDemandOutputHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBmsDemandOutputReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            client.V14DBmsDemandOutputReq = msg;
            Log.Info($"V14D BmsDemandOutput from {sn}, msg={msg}");
        }
    }
}
