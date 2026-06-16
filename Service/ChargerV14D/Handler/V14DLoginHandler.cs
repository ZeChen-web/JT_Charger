using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkDriver.Session;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Common;
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
        V14DClientMgr.SessionAttr(ctx.Channel, msg.PileCode, "0,0,0,0");
        IoSession session = V14DClientMgr.Server.SessionMgr.GetSession(ctx.Channel.Id.ToString());
        if (session == null)
        {
            session = V14DClientMgr.Server.SessionMgr.GetSession(msg.PileCode);
        }
        V14DClientMgr.Server.SessionMgr.ChangeSessionKey(session, msg.PileCode);

        // gun 1 独立实例
        V14DChargerClient client1 = AppInfo.Container.Resolve<V14DChargerClient>();
        client1.Channel = ctx.Channel;
        client1.Sn = msg.PileCode;
        client1.HeartTime = DateTime.Now;
        client1.IsLoggedIn = true;
        client1.PileCode = msg.PileCode;
        client1.BinNo = ChargerConst.No(msg.PileCode, "1");
        V14DClientMgr.AddBySn(msg.PileCode, "1", client1);

        // gun 2 独立实例，避免两把枪共用同一个对象导致数据覆盖
        V14DChargerClient client2 = AppInfo.Container.Resolve<V14DChargerClient>();
        client2.Channel = ctx.Channel;
        client2.Sn = msg.PileCode;
        client2.HeartTime = DateTime.Now;
        client2.IsLoggedIn = true;
        client2.PileCode = msg.PileCode;
        client2.BinNo = ChargerConst.No(msg.PileCode, "2");
        V14DClientMgr.AddBySn(msg.PileCode, "2", client2);

        //V14DClientMgr.AddBySn(ctx.Channel.Id.ToString(), msg.PileCode, client1);

        var resp = new V14DLoginResp(msg.PileCode, 0x00) { SeqNo = msg.SeqNo };
        ctx.Channel.WriteAndFlushAsync(resp);

        Log.Info($"V14D Login from {msg.PileCode}, pileCode={msg.PileCode}, protocolVer={msg.ProtocolVersion}");
    }
}
