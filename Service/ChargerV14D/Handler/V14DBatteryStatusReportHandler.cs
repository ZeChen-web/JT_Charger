using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBatteryStatusReportHandler : SimpleChannelInboundHandler<V14DBatteryStatusReportReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBatteryStatusReportHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBatteryStatusReportReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            client.BatteryStatusReport = msg;
            Log.Info($"V14D BatteryStatusReport from {sn}, pile={msg.PileCode}, gun={msg.Gun}, soc={msg.SOCValue}%, fault={msg.BatteryFault}");
        }
    }
}
