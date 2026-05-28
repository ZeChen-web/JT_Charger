using DotNetty.Transport.Channels;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Resp;

namespace Service.Charger.Handler.OutCharger;
/// <summary>
/// 3.7.10 充电桩应答功率调节指令
/// </summary>
public class PileAdjustPowerHandler : SimpleChannelInboundHandler<PileAdjustPowerRes>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileAdjustPowerHandler));


    protected override void ChannelRead0(IChannelHandlerContext ctx, PileAdjustPowerRes msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
        }
    }
}