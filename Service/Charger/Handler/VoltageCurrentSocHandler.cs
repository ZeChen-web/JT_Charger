using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 3.6.1.3 充放电机上传电压电流 SOC 数据（PGN:0x00F812）
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class VoltageCurrentSocHandler : SimpleChannelInboundHandler<VoltageCurrentSoc>, IBaseHandler
    {
        private readonly ILog Log = LogManager.GetLogger(typeof(VoltageCurrentSocHandler));
        private BinInfoRepository _binInfoRepository;

        public VoltageCurrentSocHandler(BinInfoRepository binInfoRepository)
        {
            _binInfoRepository = binInfoRepository;
        }
        protected override void ChannelRead0(IChannelHandlerContext ctx, VoltageCurrentSoc msg)
        {

            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                bool update = _binInfoRepository.Update(it => new BinInfo()
                {
                    Soc =  (decimal)msg.SOC,
                    Soh =  (decimal)msg.SOH
                }, t => t.No == client.BinNo);
                Log.Info(update
                    ? $"succeed update battery soc {msg.SOC} for {client.BinNo}"
                    : $"fail update battery soc {msg.SOC} for {client.BinNo}");
                
            }
        }
    }
}
