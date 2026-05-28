using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Host.Resp.OutCharger;

namespace Service.Charger.Handler.OutCharger;

/// <summary>
/// 3.7.7 充电桩上送停止完成帧
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileChargeCompleteHandler : SimpleChannelInboundHandler<PileChargeCompleteReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileChargeCompleteHandler));

    public ChargeOrderRepository ChargeOrderRepository { get; set; }

    protected override void ChannelRead0(IChannelHandlerContext ctx, PileChargeCompleteReq msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
            
            PileChargeCompleteRes res = new PileChargeCompleteRes(msg.Pn,0);
            ctx.Channel.WriteAndFlushAsync(res);
            
            if (msg.Result == 0)
            {
                ChargeOrder? chargeOrder = ChargeOrderRepository.GetLatestChargeGunOrder(msg.Pn.ToString(), sn);
                if (chargeOrder == null)
                {
                    return;
                }

                chargeOrder.StopReason = 0;
                
                client.GunCharged[msg.Pn] = false;

                ChargeOrderRepository.Update(chargeOrder);
            }

        }
    }
}