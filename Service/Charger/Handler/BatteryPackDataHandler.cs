using System.Text;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Handler;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;

namespace HybirdFrameworkServices.Charger.Handler
{
    /// <summary>
    /// 3.5.9 电池包实时数据上报（站内充电模式有电池包时周期性上传）
    /// <code>
    /// 1，保存电池包实时数据
    /// 2，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class BatteryPackDataHandler : SimpleChannelInboundHandler<BatteryPackData>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BatteryPackDataHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, BatteryPackData msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");

                client.BatteryPackData = msg;
            }
            
        }
    }
}
