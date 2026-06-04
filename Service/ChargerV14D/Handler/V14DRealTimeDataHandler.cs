using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Handler;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DRealTimeDataHandler : SimpleChannelInboundHandler<V14DRealTimeDataReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DRealTimeDataHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DRealTimeDataReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            client.RealTimeData = msg;
            client.PileStatus = msg.Status;
            Log.Debug($"V14D RealTimeData from {sn}, status={msg.Status}, soc={msg.SOC}%, power={msg.ChargePower:F2}kW");
            V14DReadRealTimeDataCmd readCmd = new V14DReadRealTimeDataCmd(msg.PileCode, msg.Gun);
            ctx.Channel.WriteAndFlushAsync(readCmd);
        }
    }
}
