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
    /// 3.5.19 充放电上报交流电表数据（交流电表接到充电机上的情况）
    /// </summary>
    public class AcMeterHandler: SimpleChannelInboundHandler<AcMeter>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AcMeterHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, AcMeter msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.AcMeter = msg;
            }
        }
    }
}
