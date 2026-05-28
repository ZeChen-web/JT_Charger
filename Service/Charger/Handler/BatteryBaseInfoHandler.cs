using DotNetty.Transport.Channels;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler;

/// <summary>
/// 3.6.2.4 充放电机上传电池包基本信息（PGN:0x00F882）
/// </summary>
public class BatteryBaseInfoHandler : SimpleChannelInboundHandler<BatteryBaseInfo>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BatteryBaseInfoHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, BatteryBaseInfo msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
            client.BatteryBaseInfo = msg;
        }
    }
}