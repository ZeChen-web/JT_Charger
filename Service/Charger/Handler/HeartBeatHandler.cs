using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;

namespace Service.Charger.Handler
{
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class HeartBeatHandler : SimpleChannelInboundHandler<HeartBeat>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HeartBeatHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, HeartBeat msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                //心跳存储日志
                Log.Info($"receive {msg} from {sn}");
                //心跳桩状态
                client.PileState = msg.Status;

                //监控平台心跳应答：0-正常、1-未注册
                HeartBeatRes heartBeatRes = new HeartBeatRes(0);
                ctx.Channel.WriteAndFlushAsync(heartBeatRes);
            }
        }
    }
}
