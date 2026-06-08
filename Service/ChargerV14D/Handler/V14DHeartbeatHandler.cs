using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DHeartbeatHandler : SimpleChannelInboundHandler<V14DHeartbeatReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DHeartbeatHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DHeartbeatReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            client.GunStatus = msg.GunStatus;
            client.LastHeartbeat = DateTime.Now;
            client.HeartTime = DateTime.Now;

            var resp = new V14DHeartbeatResp(msg.PileCode, msg.GunNo) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(resp);
        }
    }
}
