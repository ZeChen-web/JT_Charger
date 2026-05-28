using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 接收到鉴权帧
    /// <code>
    /// 1，保存日志到log
    /// 2，从SessionMgr中取目的地址，解析后写入ChargerManager
    /// 3，保存鉴权状态和充电状态
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class AuthResHandler : SimpleChannelInboundHandler<AuthRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthResHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, AuthRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                if (msg.ConnSeq == client.AuthTimes)
                {
                    if (msg.AuthResult == 0)
                    {
                        client.IsAuthed = true;
                        client.ChargingStatus = (int)ChargingStatus.Authed;
                    }
                    else
                    {
                        client.IsAuthed = false;
                        client.ChargingStatus = (int)ChargingStatus.AuthFailed;
                    }
                }
            }
        }
    }
}