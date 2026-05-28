using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.5.17 充电机应答 VIN 鉴权结果
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    internal class AuthVINResHandler : SimpleChannelInboundHandler<AuthVINRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthVINResHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, AuthVINRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");

            }
        }
    }
}
