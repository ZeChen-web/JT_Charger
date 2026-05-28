using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Host.Resp.OutCharger;

namespace Service.Charger.Handler.OutCharger;
/// <summary>
/// 3.7.5 充电桩上送充电启动完成帧
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileStartChargeCompleteHandler : SimpleChannelInboundHandler<PileStartChargeCompleteReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileStartChargeCompleteHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, PileStartChargeCompleteReq msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");

            if (client == null)
            {
                return;
            }

            if (msg.Result == 0)
            {
                client.Vin[msg.Pn] = msg.Vin;
                client.GunCharged[msg.Pn] = true;
            }
            // 响应启动完成帧
            PileStartChargeCompleteRes res=new PileStartChargeCompleteRes(msg.Pn,0,0);

            ctx.Channel.WriteAndFlushAsync(res);
        }
    }
}