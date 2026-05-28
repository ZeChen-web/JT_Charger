using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.5.18 充放电机上报模块状态消息名称 充放电机上
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class ModuleStateHandler : SimpleChannelInboundHandler<ModuleState>, IBaseHandler
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(ModuleStateHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, ModuleState msg)
        {

            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");

            }
        }
    }
}
