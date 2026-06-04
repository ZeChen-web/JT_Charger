using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DRemoteStopChargeReplyHandler : SimpleChannelInboundHandler<V14DRemoteStopChargeReplyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DRemoteStopChargeReplyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DRemoteStopChargeReplyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Info($"V14D RemoteStopChargeReply from {sn}, result={msg.Result}");
    }
}
