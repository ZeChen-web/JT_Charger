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
    ///  3.5.12 电池包上报充放电口温度（站内充电模式有电池包时周期性上传）
    /// <code>
    /// 1，保存电池包上报充放电口温度
    /// 2，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class BatteryPackPortTemperatureHandler : SimpleChannelInboundHandler<BatteryPackPortTemperature>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BatteryPackPortTemperatureHandler));
        protected override void ChannelRead0(IChannelHandlerContext ctx, BatteryPackPortTemperature msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");

                client.BatteryPackPortTemperature = msg;
            }
            
        }
    }
}
