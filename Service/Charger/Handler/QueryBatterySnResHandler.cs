using System.Text;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.4.4 充放电机应答辅助控制
    /// <code>
    /// 1，保存日志到log
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class QueryBatterySnResHandler : SimpleChannelInboundHandler<QueryBatterySnRes>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(QueryBatterySnResHandler));

        private BinInfoRepository _binInfoRepository;

        public QueryBatterySnResHandler(BinInfoRepository binInfoRepository)
        {
            _binInfoRepository = binInfoRepository;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, QueryBatterySnRes msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                StringBuilder sb = new StringBuilder(msg.BatterSnLength);
                for (int i = 0; i < msg.BatterSnLength; i++)
                {
                    sb.Append(Convert.ToChar(msg.BatterSnBytes[i]));
                }

                if (!sb.ToString().Contains("ÿ"))
                {
                    client.BatteryNo = sb.ToString();
                }
                client.BatteryFactory = msg.BatterFactory;

                if (_binInfoRepository.Update(t => t.BatteryNo == client.BatteryNo, t => t.No == client.BinNo) > 0)
                {
                    Log.Info($"succeed update battery no {client.BatteryNo} for {client.BinNo}");
                }
                else
                {
                    Log.Info($"fail update battery no {client.BatteryNo} for {client.BinNo}");
                }

                Log.Info($"receive {msg} from {sn}");
            }
        }
    }
}