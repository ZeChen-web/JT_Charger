using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Handler;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DLockCommandReplyHandler : SimpleChannelInboundHandler<V14DLockCommandReplyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DLockCommandReplyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DLockCommandReplyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Debug($"V14D LockCommandReply from {sn}, result={msg.Result}");
    }
}
