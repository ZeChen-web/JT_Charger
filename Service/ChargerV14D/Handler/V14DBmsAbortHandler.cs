using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBmsAbortHandler : SimpleChannelInboundHandler<V14DBmsAbortReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBmsAbortHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBmsAbortReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            //TODO::BMS异常中止需要入库
            Log.Info($"V14D BmsAbort from {sn}, tsn={msg.TransactionSN}, reason={msg.BmsStopReason:X2}");
        }
    }
}
