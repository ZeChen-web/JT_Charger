using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBillingModelSetReplyHandler : SimpleChannelInboundHandler<V14DBillingModelSetReplyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelSetReplyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelSetReplyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Info($"V14D BillingModelSetReply from {sn}, result={msg.Result}");
    }
}
