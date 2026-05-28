using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 电池包实时遥信上报（站内充电模式有电池包时周期性上传）
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class RemoteSignalingHandler : SimpleChannelInboundHandler<RemoteSignaling>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RemoteSignalingHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, RemoteSignaling msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.RemoteSignaling = msg;
            }
        }
    }
}
