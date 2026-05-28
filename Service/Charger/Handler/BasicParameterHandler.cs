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
    /// 3.6.2.2 充放电机上传基本参数 2（PGN:0x00F802）
    /// </summary>
    public class BasicParameterHandler : SimpleChannelInboundHandler<BasicParameter>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BasicParameterHandler));

        protected override void ChannelRead0(IChannelHandlerContext ctx, BasicParameter msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.BasicParameter = msg;
            }
        }
    }
}
