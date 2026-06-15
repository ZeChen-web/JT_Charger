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

            // 同步心跳时间到 gun 2，保证两把枪的连接状态一致
            var client2 = V14DClientMgr.GetBySn(sn, "2");
            if (client2 != null)
            {
                client2.GunStatus = msg.GunStatus;
                client2.LastHeartbeat = DateTime.Now;
                client2.HeartTime = DateTime.Now;
            }

            var resp = new V14DHeartbeatResp(msg.PileCode, msg.GunNo) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(resp);
        }
    }
}
