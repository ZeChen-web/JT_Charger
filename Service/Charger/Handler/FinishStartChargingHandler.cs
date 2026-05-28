using System.Text;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Utils;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Handler;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;

namespace HybirdFrameworkServices.Charger.Handler
{
    /// <summary>
    /// 充放电机上送充电启动完成帧
    /// <code>
    /// 1，保存充电机是否充电
    /// 2，保存日志到log
    /// 3，监控平台应答充电启动完成帧
    /// 4，保存应答日志
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class FinishStartChargingHandler : SimpleChannelInboundHandler<FinishStartCharging>, IBaseHandler
    {
        public BinInfoRepository BinInfoRepository { get; set; }
        public ChargeOrderRepository ChargeOrderRepository { get; set; }
        public SwapOrderBatteryRepository SwapOrderBatteryRepository { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(typeof(FinishStartChargingHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, FinishStartCharging msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                msg.VehIdeNum = Encoding.ASCII.GetString(msg.Vin);
                int chargeStatus = 0;
                if (msg.Result == 0)
                {
                    client.IsCharged = true;
                    client.IsStopped = false;
                    chargeStatus = 1;
                    SwapOrderBattery? swapOrderBattery = SwapOrderBatteryRepository.QueryLatestOrderNoByBatterySn(client.BatteryNo);
                    if (swapOrderBattery != null)
                    {
                        ChargeOrderRepository.Update(it => it.SwapOrderSn == swapOrderBattery.SwapOrderSn,
                            it => it.Sn == client.ChargeOrderNo);

                        List<ChargeOrder> orders = ChargeOrderRepository.QueryBySwapOrderAndBatterySn(swapOrderBattery.SwapOrderSn,
                            client.BatteryNo);
                        if (orders.Count > 0)
                        {
                            string? cloudChargeOrder = null;
                            foreach (ChargeOrder order in orders)
                            {
                                if (!string.IsNullOrWhiteSpace(order.CloudChargeOrder))
                                {
                                    cloudChargeOrder = order.CloudChargeOrder;
                                }

                            }

                            if (string.IsNullOrWhiteSpace(cloudChargeOrder))
                            {
                                cloudChargeOrder = ChargerUtils.GenChargeOrderNo(sn);
                            }

                            HashSet<int> hashSet = orders.Select(it => it.Id).ToHashSet();
                            ChargeOrderRepository.Update(it => it.CloudChargeOrder == cloudChargeOrder,
                                it => hashSet.Contains(it.Id));
                        }


                    }
                    else
                    {
                        Log.Error($"can not find swapOrderBattery by {client.BatteryNo}");
                    }

                }
                else if (msg.Result == 1)
                {
                    client.IsCharged = false;
                }

                if (ObjUtils.IsNotNullOrWhiteSpace( client.ChargeOrderNo))
                {
                    ChargeOrderRepository.Update(it => it.CmdStatus == (msg.Result == 0 ? 1 : 2),
                        it => it.Sn == client.ChargeOrderNo);
                }


                int update = BinInfoRepository.Update(t => t.ChargeStatus == chargeStatus, t => t.No == client.BinNo);
                Log.Info($"update {update} start charge finish status {chargeStatus} for {client.BinNo}");

                StartChargingFinishedRes msgRespond = new StartChargingFinishedRes(0, 0);
                ctx.Channel.WriteAndFlushAsync(msgRespond);
            }
        }
    }
}
