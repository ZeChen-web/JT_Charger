using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class VoltageExtremumStatisticsHandler : SimpleChannelInboundHandler<VoltageExtremumStatistics>, IBaseHandler
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(VoltageExtremumStatisticsHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, VoltageExtremumStatistics msg)
        {

            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.VoltageExtremumStatistics = msg;
            }
        }
    }
}
