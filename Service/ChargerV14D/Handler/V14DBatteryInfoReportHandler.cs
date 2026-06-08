using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBatteryInfoReportHandler : SimpleChannelInboundHandler<V14DBatteryInfoReportReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBatteryInfoReportHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBatteryInfoReportReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            client.BatteryInfoReport = msg;
            client.BatteryNo= msg.BatteryCode;
            Log.Info($"V14D BatteryInfoReport from {sn}, pile={msg.PileCode}, gun={msg.Gun}, batteryNo={msg.BatteryNo}");
        }
    }
}
