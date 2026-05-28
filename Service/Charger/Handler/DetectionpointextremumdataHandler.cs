using DotNetty.Transport.Channels;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 
    /// </summary>
    public class DetectionpointextremumdataHandler : SimpleChannelInboundHandler<Detectionpointextremumdata>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DetectionpointextremumdataHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, Detectionpointextremumdata msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
            }
        }
    }
}
