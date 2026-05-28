using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Handler;
using Service.Charger.Msg.Charger.Resp;

namespace HybirdFrameworkServices.Charger.Handler
{
    /// <summary>
    /// 3.3.8 充放电机响应远程停止充电
    /// <code>
    /// 1，保存日志到log
    /// 2，保存是否充电结果到ChargerManager
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class RemoteStopChargingResHandler : SimpleChannelInboundHandler<RemoteStopChargingRes>, IBaseHandler
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(RemoteStopChargingResHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, RemoteStopChargingRes msg)
        {
            
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                if (msg.Result != 0xff)
                {
                  client.IsCanSendStopCmd = false;
                  client.IsStopped = true;
                  client.ChargingStatus = (int)ChargingStatus.StopChargingSuccess;
                  client.IsCharged = false;
                  client.ChargingStopTime = DateTime.Now;
                }
                else
                {
                    client.IsStopped = false;
                    client.ChargingStatus = (int)ChargingStatus.StopChargingFailed;
                    client.ChargingStopTime = Convert.ToDateTime("2000-1-1");
                }
            }
        }
    }
}
