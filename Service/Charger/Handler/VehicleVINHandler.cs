using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.5.14 充电机上报车辆 VIN
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class VehicleVINHandler : SimpleChannelInboundHandler<VehicleVIN>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VehicleVINHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, VehicleVIN msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.VehicleVIN = msg;
            }
        }
    }
}
