using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DQRCodeReplyHandler : SimpleChannelInboundHandler<V14DQRCodeReplyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DQRCodeReplyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DQRCodeReplyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun,out var sn, out var client))
            Log.Info($"V14D QRCodeReply from {sn}, result={msg.Result}");
    }
}
