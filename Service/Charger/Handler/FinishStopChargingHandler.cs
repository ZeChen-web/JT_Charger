using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Utils;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;
using Service.Init;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.3.11 充放电机上送充电停止帧
    /// <code>
    /// 1，保存日志到log
    /// 2，监控平台应答停止完成帧
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class FinishStopChargingHandler : SimpleChannelInboundHandler<FinishStopCharging>, IBaseHandler
    {
        
        public BinInfoRepository BinInfoRepository { get; set; }
        
        public ChargeOrderRepository ChargeOrderRepository { get; set; }
        
        private static readonly ILog Log = LogManager.GetLogger(typeof(FinishStopChargingHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, FinishStopCharging msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.IsCanSendStopCmd = false;
                int chargeStatus = 0;
                if (msg.Result == 0)
                {
                    client.IsStopped = true;
                    client.IsCharged = false;
                    chargeStatus = 4;
                    if (msg.SuspendTheStateOfCharge >= StaticStationInfo.SwapSoc)
                    {
                        ChargeOrder? chargeOrder = ChargeOrderRepository.QueryLatestByBatterySn(client.BatteryNo);
                        if (chargeOrder != null)
                        {
                            List<ChargeOrder> orders = ChargeOrderRepository.QueryBySwapOrderAndBatterySn(chargeOrder.SwapOrderSn,
                                chargeOrder.BatteryNo);
                            if (ObjUtils.IsNotEmpty(orders))
                            {
                                List<int> list = orders.Select(it => it.Id).ToList();

                                ChargeOrderRepository.Update(it => new ChargeOrder() 
                                        {CanUpload = 1 ,ReportingTimes=0 },
                                    it => list.Contains(it.Id));
                            }
                        }
                    }
                }
                else
                {
                    client.IsStopped = false;
                }
                
                

                BinInfo? binInfo = BinInfoRepository.QueryByBinNo(client.BinNo);
                if (binInfo!=null)
                {
                    binInfo.ChargeStatus = chargeStatus;
                    binInfo.LastChargeFinishTime = DateTime.Now;
                    bool update = BinInfoRepository.Update(binInfo);
                    Log.Info($"update {update} start charge finish status {chargeStatus} for {client.BinNo}");
                }

                
                ChargingStopFsdRes stopFsdRes = new ChargingStopFsdRes(0);
                ctx.Channel.WriteAndFlushAsync(stopFsdRes);
            }

        }
    }
}