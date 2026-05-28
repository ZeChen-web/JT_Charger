using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Handler;
using Service.Charger.Msg.Charger.OutCharger.Req;

namespace Service.Charger.Handler.OutCharger;
/// <summary>
/// 3.7.11 充电桩遥信数据上报
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileUploadRemoteSignalHandler: SimpleChannelInboundHandler<PileUploadRemoteSignal>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileUploadRemoteSignalHandler));

    
    protected override void ChannelRead0(IChannelHandlerContext ctx, PileUploadRemoteSignal msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            //存储日志
            Log.Info($"receive {msg} from {sn}");
            
            client.PileUploadRemoteSignal[msg.Pn] = msg;

            client.Workstate = msg.WorkStatus;
            
            client.GunCharged[msg.Pn]  = msg.WorkStatus == 1 ? true : false;
            
            client.ChargedPile[msg.Pn] = !msg.ChargeStationGunHolderStatus;
            client.WorkStatus[msg.Pn] = msg.WorkStatus;



        }
    }
}