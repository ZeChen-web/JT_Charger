using System.Text;
using Common.Util;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Resp;
using Service.Charger.Msg.Http.Resp;

namespace Service.Charger.Handler.OutCharger;

/// <summary>
/// 3.7.2 充电桩响应远程启动充电
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileStartChargeResHandler : SimpleChannelInboundHandler<PileStartChargeRes>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileStartChargeResHandler));
    public ChargeOrderRepository ChargeOrderRepository { get; set; }


    protected override void ChannelRead0(IChannelHandlerContext ctx, PileStartChargeRes msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");

            string chargeNo = sn.Substring(4);
            
            ChargeOrder? chargeOrder = ChargeOrderRepository.GetLatestChargeGunOrder(msg.Pn.ToString(), chargeNo);
            if (chargeOrder == null)
            {
                return;
            }

            PileStartChargeHttpRes chargeRes = new PileStartChargeHttpRes();

            chargeRes.con = chargeOrder.Sn;
            chargeRes.pn = chargeOrder.ChargerGunNo;

            if (msg.Result == 0)
            {
                chargeOrder.StartTime = DateTime.Now;
                chargeOrder.CmdStatus = 1;
                ChargeOrderRepository.Update(chargeOrder);


                chargeRes.rs = "1";
                chargeRes.ec = "0";
                HttpUtil.SendPostRequest(chargeRes, "http://127.0.0.1:5034/api/OutCharger/ResStartOutCharger");
                // 9.2.1.2 站控应答开始充电操作
                
            }
            else
            {
                chargeRes.rs = "2";
                chargeRes.ec = msg.FailReason.ToString();
                HttpUtil.SendPostRequest(chargeRes, "http://127.0.0.1:5034/api/OutCharger/ResStartOutCharger");
            }
        }
    }
}