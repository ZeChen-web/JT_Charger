using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Handler;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBillingModelReqHandler : SimpleChannelInboundHandler<V14DBillingModelReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelReqHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D BillingModelReq from {sn}");
            
            //TODO:: 处理计费模型请求
            var resp = new V14DBillingModelResp() { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(resp);
        }
    }
}
