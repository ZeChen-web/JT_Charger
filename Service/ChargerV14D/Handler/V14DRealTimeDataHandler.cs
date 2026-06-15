using System.Net.Http.Json;
using Autofac;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DRealTimeDataHandler : SimpleChannelInboundHandler<V14DRealTimeDataReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DRealTimeDataHandler));

    private BinInfoRepository _binInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DRealTimeDataReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun, out var sn, out var client))
        {
            client.RealTimeData = msg;
            client.PileStatus = msg.Status;

            var u1 = _binInfoRepository.Update(
                t => new BinInfo()
                {
                    ChargeStatus = status(msg.Status),
                    Soc = msg.SOC
                },
                i => i.ChargerNo == msg.PileCode && i.ChargerGunNo == (msg.Gun).ToString());

            Log.Info(
                $"V14D RealTimeData from {sn}, status={msg.Status},gun={msg.Gun}, soc={msg.SOC}%, power={msg.ChargePower:F2}kW");
            //V14DReadRealTimeDataCmd readCmd = new V14DReadRealTimeDataCmd(msg.PileCode, msg.Gun);
            //ctx.Channel.WriteAndFlushAsync(readCmd);,
            //TODO::硬件故障，不能充电
            //TODO::硬件告警，不影响充电
        }
    }

    int status(int chargeStatus)
    {
        switch (chargeStatus)
        {
            case 0: return 0; // 离线
            case 1: return 3; // 故障
            case 2: return 0; // 空闲 
            case 3: return 1; // 充电中
            case 4: return 0; // 已插枪未充电
            case 5: return 0; // 充电完成未拔枪
            default: return 0;
        }
    }
}