using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DRemoteRestartReplyHandler : SimpleChannelInboundHandler<V14DRemoteRestartReplyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DRemoteRestartReplyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DRemoteRestartReplyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Info($"V14D RemoteRestartReply from {sn}, result={msg.Result}");
    }
}
