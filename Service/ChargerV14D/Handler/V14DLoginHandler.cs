using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkDriver.Session;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DLoginHandler : SimpleChannelInboundHandler<V14DLoginReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DLoginHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DLoginReq msg)
    {
        ServerMgr.SessionAttr(ctx.Channel, msg.PileCode, "0,0,0,0");
        IoSession session = ServerMgr.Server.SessionMgr.GetSession(ctx.Channel.Id.ToString());
        if (session == null)
        {
            session = ServerMgr.Server.SessionMgr.GetSession(msg.PileCode);
        }
        ServerMgr.Server.SessionMgr.ChangeSessionKey(session, msg.PileCode);

        V14DChargerClient client = AppInfo.Container.Resolve<V14DChargerClient>();
        client.Channel = ctx.Channel;
        client.Sn = msg.PileCode;
        client.HeartTime = DateTime.Now;
        client.IsLoggedIn = true;

        client.PileCode = msg.PileCode;
        ServerMgr.AddBySn(msg.PileCode, client);

        var resp = new V14DLoginResp(msg.PileCode, 0x00) { SeqNo = msg.SeqNo };
        ctx.Channel.WriteAndFlushAsync(resp);

        Log.Info($"V14D Login from {msg.PileCode}, pileCode={msg.PileCode}, protocolVer={msg.ProtocolVersion}");
    }
}
