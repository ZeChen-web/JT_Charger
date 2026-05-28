using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.4.2 充放电设备应答站功率调节指令
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class PowerRegulationHandler : SimpleChannelInboundHandler<PowerRegulationRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PowerRegulationHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, PowerRegulationRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.PowerRegulationRes = msg;
            }
        }
    }
}
