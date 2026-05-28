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
public class V14DTransactionRecordHandler : SimpleChannelInboundHandler<V14DTransactionRecordReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DTransactionRecordHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DTransactionRecordReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D TransactionRecord from {sn}, tsn={msg.TransactionSN}, totalKWH={msg.TotalKWHValue:F4}");

            var confirm = new V14DTransactionRecordConfirm(msg.TransactionSN) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(confirm);
        }
    }
}
