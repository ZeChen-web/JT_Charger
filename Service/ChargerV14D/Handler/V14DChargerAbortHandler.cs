using Common.Const;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DChargerAbortHandler : SimpleChannelInboundHandler<V14DChargerAbortReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DChargerAbortHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DChargerAbortReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            Dictionary<string, bool> lstAlarm = new();
            if(msg.ChargerOverTempFault) lstAlarm.Add("14",true);
            if(msg.ChargerConnectorFault ) lstAlarm.Add("15",true);
            if(msg.InternalOverTempFault) lstAlarm.Add("16",true);
            if(msg.PowerTransferFault) lstAlarm.Add("17",true);
            if(msg.EmergencyStopFault) lstAlarm.Add("18",true);
            if(msg.OtherFault) lstAlarm.Add("19",true);
            if(msg.CurrentMismatch) lstAlarm.Add("20",true);
            if(msg.VoltageAbnormal) lstAlarm.Add("21",true);
            
            FaultHandling.SaveAlarmInfoToProcessRecord(lstAlarm,EquipmentType.Charger,msg.PileCode+msg.Gun);
            
            Log.Info($"V14D ChargerAbort from {sn}, tsn={msg.TransactionSN}, reason={msg.ChargerStopReason:X2}");
        }
    }
}
