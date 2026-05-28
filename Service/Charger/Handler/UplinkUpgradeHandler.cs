using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.7 远程升级-监控网关上送升级完成确认帧
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class UplinkUpgradeHandler : SimpleChannelInboundHandler<UplinkUpgrade>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UplinkUpgradeHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, UplinkUpgrade msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.UplinkUpgrade = msg;

                UplinkUpgradeRes uplinkUpgrade=new UplinkUpgradeRes();
                ctx.Channel.WriteAsync(uplinkUpgrade);
            }
        }
    }
}
