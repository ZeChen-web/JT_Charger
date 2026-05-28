using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.3.3 充放电机登陆签到
    /// 监控平台应答充电设备登录签到报文
    /// <code>
    /// 1，保存日志到log
    /// 2，回复签到应答
    /// 3，保存签到应答日志
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class LoginHandler : SimpleChannelInboundHandler<Login>, IBaseHandler
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(LoginHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, Login msg)
        {

            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                msg.ConnProtocolVersion = msg.ConnProtocolVersion0 + "." + msg.ConnProtocolVersion1 + "." + msg.ConnProtocolVersion2;
                msg.ControllerHardwareVersion = msg.ControllerHardwareVersion0 + "." + msg.ControllerHardwareVersion1 + "." + msg.ControllerHardwareVersion2;
                msg.ControllerSoftwareVersion = msg.ControllerSoftwareVersion0 + "." + msg.ControllerSoftwareVersion1 + "." + msg.ControllerSoftwareVersion2;

               
            }
            LogSignMessage logSignMessage = new LogSignMessage(0);
            ctx.Channel.WriteAndFlushAsync(logSignMessage);
        }
    }
}