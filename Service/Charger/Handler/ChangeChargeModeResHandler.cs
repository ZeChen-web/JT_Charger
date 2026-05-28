using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.4.4 充放电机应答辅助控制
    /// <code>
    /// 1，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class ChangeChargeModeResHandler : SimpleChannelInboundHandler<ChangeChargeModeRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ChangeChargeModeResHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, ChangeChargeModeRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
            }
        }
    }
}