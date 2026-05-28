using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.4.4 充放电设备应答辅助控制
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class AuxiliaryPowerResHandler : SimpleChannelInboundHandler<AuxiliaryPowerRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuxiliaryPowerResHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, AuxiliaryPowerRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.AuxiliaryPowerRes = msg;
            }
        }
    }
}
