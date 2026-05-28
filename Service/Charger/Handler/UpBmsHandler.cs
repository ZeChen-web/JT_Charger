using DotNetty.Transport.Channels;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler;

public class UpBmsHandler: SimpleChannelInboundHandler<UpBms>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(UpBmsHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, UpBms msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
        {
            Log.Info($"receive {msg} from {sn}");
            client.UpBms = msg;
        }
    }
}