using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Req;
using Service.Charger.Msg.Host.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class UpgradeRequestHandler : SimpleChannelInboundHandler<UpgradeRequestRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpgradeRequestHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, UpgradeRequestRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
            }
        }
    }
}
