using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 充放电机应答监控平台掉线停止充电
    /// <code>
    /// 1，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class OfflineStopChargingResHandler : SimpleChannelInboundHandler<OfflineStopChargingRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OfflineStopChargingResHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, OfflineStopChargingRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
            }
        }
    }
}