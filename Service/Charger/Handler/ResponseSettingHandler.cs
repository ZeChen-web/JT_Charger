using System.Text;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Handler;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Resp;

namespace HybirdFrameworkServices.Charger.Handler
{
    /// <summary>
    /// 3.4.10 监控网关响应尖峰平谷设置
    /// <code>
    /// 1，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class ResponseSettingHandler : SimpleChannelInboundHandler<ResponseSetting>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ResponseSettingHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, ResponseSetting msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");

            }
            
        }
    }
}
