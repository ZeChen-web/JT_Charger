using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DParamConfigHandler : SimpleChannelInboundHandler<V14DParamConfigReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DParamConfigHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DParamConfigReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Info($"V14D ParamConfig from {sn}, tsn={msg.TransactionSN}");
    }
}
