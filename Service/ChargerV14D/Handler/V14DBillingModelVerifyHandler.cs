using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Handler;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBillingModelVerifyHandler : SimpleChannelInboundHandler<V14DBillingModelVerifyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelVerifyHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelVerifyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo}");
            // TODO::回复计费模型一致
            var resp = new V14DBillingModelVerifyResp(msg.PileCode, msg.ModelNo, 0x00) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(resp);
        }
    }
}
