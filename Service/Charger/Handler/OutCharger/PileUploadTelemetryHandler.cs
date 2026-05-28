using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Req;

namespace Service.Charger.Handler.OutCharger;

/// <summary>
/// 3.7.12 充电桩遥测数据上报
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileUploadTelemetryHandler : SimpleChannelInboundHandler<PileUploadTelemetry>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileUploadTelemetryHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, PileUploadTelemetry msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
            
            client.PileUploadTelemetry[msg.Pn] = msg;
            
            client.ChargePilePower[msg.Pn] = msg.HighVoltageAcquisitionCurrent * msg.HighVoltageAcquisitionVoltage;

        }
    }
}