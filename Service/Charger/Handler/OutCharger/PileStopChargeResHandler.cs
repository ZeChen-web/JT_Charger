using Common.Util;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Resp;
using Service.Charger.Msg.Http.Resp;

namespace Service.Charger.Handler.OutCharger;
/// <summary>
/// 3.7.4 充电桩响应远程停止充电
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileStopChargeResHandler : SimpleChannelInboundHandler<PileStopChargeRes>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileStopChargeResHandler));

    public ChargeOrderRepository ChargeOrderRepository { get; set; }


    protected override void ChannelRead0(IChannelHandlerContext ctx, PileStopChargeRes msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
            string chargeNo = sn.Substring(4);
            ChargeOrder? chargeOrder = ChargeOrderRepository.GetLatestChargeGunOrder(msg.pn.ToString(), chargeNo);

            if (chargeOrder == null)
            {
                return;
            }

            if (msg.rs == 0 || msg.rs == 1)
            { 
                PileStopChargeHttpRes res = new PileStopChargeHttpRes();
                res.pn = chargeOrder.ChargerGunNo;
                res.rs = msg.rs.ToString();
                HttpUtil.SendPostRequest(res, "http://127.0.0.1:5034/api/OutCharger/ResStopOutCharger");
            }
        }
    }
}