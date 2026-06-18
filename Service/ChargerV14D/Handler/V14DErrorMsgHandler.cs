using Common.Const;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DErrorMsgHandler : SimpleChannelInboundHandler<V14DErrorMsgReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DErrorMsgHandler));

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DErrorMsgReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun, out var sn, out var client))
        {
            Dictionary<string, bool> lstAlarm = new();
            if (client.V14DErrorMsgReq.RecvBro00Timeout != msg.RecvBro00Timeout)
                lstAlarm.Add("22", msg.RecvBro00Timeout == 1);

            if (client.V14DErrorMsgReq.RecvBroAATimeout != msg.RecvBroAATimeout)
                lstAlarm.Add("23", msg.RecvBroAATimeout == 1);

            if (client.V14DErrorMsgReq.RecvTimeSyncTimeout != msg.RecvTimeSyncTimeout)
                lstAlarm.Add("24", msg.RecvTimeSyncTimeout == 1);

            if (client.V14DErrorMsgReq.RecvChargeReadyTimeout != msg.RecvChargeReadyTimeout)
                lstAlarm.Add("25", msg.RecvChargeReadyTimeout == 1);

            if (client.V14DErrorMsgReq.RecvChargeStatusTimeout != msg.RecvChargeStatusTimeout)
                lstAlarm.Add("26", msg.RecvChargeStatusTimeout == 1);

            if (client.V14DErrorMsgReq.RecvChargerAbortTimeout != msg.RecvChargerAbortTimeout)
                lstAlarm.Add("27", msg.RecvChargerAbortTimeout == 1);

            if (client.V14DErrorMsgReq.RecvChargeStatsTimeout != msg.RecvChargeStatsTimeout)
                lstAlarm.Add("28", msg.RecvChargeStatsTimeout == 1);

            if (client.V14DErrorMsgReq.BmsOther != msg.BmsOther)
                lstAlarm.Add("29", msg.BmsOther == 1);

            if (client.V14DErrorMsgReq.RecvBmsIdentifyTimeout != msg.RecvBmsIdentifyTimeout)
                lstAlarm.Add("30", msg.RecvBmsIdentifyTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBatParamTimeout != msg.RecvBatParamTimeout)
                lstAlarm.Add("31", msg.RecvBatParamTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBmsReadyTimeout != msg.RecvBmsReadyTimeout)
                lstAlarm.Add("32", msg.RecvBmsReadyTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBatStatusTimeout != msg.RecvBatStatusTimeout)
                lstAlarm.Add("33", msg.RecvBatStatusTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBatRequireTimeout != msg.RecvBatRequireTimeout)
                lstAlarm.Add("34", msg.RecvBatRequireTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBmsAbortTimeout != msg.RecvBmsAbortTimeout)
                lstAlarm.Add("35", msg.RecvBmsAbortTimeout == 1);

            if (client.V14DErrorMsgReq.RecvBmsStatsTimeout != msg.RecvBmsStatsTimeout)
                lstAlarm.Add("36", msg.RecvBmsStatsTimeout == 1);

            if (client.V14DErrorMsgReq.ChargerOther != msg.ChargerOther)
                lstAlarm.Add("37", msg.ChargerOther == 1);

            client.V14DErrorMsgReq = msg;

            FaultHandling.SaveAlarmInfo(lstAlarm, EquipmentType.Charger, msg.PileCode + msg.Gun);

            Log.Info($"V14D ErrorMsg from {sn}, tsn={msg.TransactionSN}");
        }
    }
}