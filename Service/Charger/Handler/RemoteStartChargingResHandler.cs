using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 充放电机响应远程启动充电
    /// <code>
    /// 1，保存日志到log
    /// 2，保存启动结果，状态等到ChargerManager
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class RemoteStartChargingResHandler : SimpleChannelInboundHandler<RemoteStartChargingRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RemoteStartChargingResHandler));

        public ChargeOrderRepository ChargeOrderRepository { get; set; }

        protected override void ChannelRead0(IChannelHandlerContext ctx, RemoteStartChargingRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                if (msg.Result == 0)
                {
                    client.IsStopped = false;
                    client.ChargingStatus = (int)ChargingStatus.StartChargingSuccess;
                    client.ChargingStartTime = DateTime.Now;
                    //查找最新的充电订单
                    List<ChargeOrder> chargeOrders = ChargeOrderRepository
                        .QueryListByClause(i => i.BatteryNo == client.BatteryNo, "created_time desc")
                        .Take(1).ToList();

                    if (chargeOrders.Count > 0)
                    {
                        ChargeOrder one = chargeOrders[0];
                        ChargeOrderRepository.Update(
                            i => new ChargeOrder()
                            {
                                CmdStatus = 1
                            },
                            it => it.Id == one.Id);
                    }
                }
                else
                {
                    client.ChargingStatus = (int)ChargingStatus.StartChargingFailed;
                    client.ChargingStartTime = Convert.ToDateTime("2000-1-1");
                }
            }
            else
            {
                client.ChargingStatus = (int)ChargingStatus.StartChargingFailed;
            }
        }
    }
}
