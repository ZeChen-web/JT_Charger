using Common.Const;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBmsAbortHandler : SimpleChannelInboundHandler<V14DBmsAbortReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBmsAbortHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBmsAbortReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel,msg.Gun, out var sn, out var client))
        {
            Dictionary<string, bool> lstAlarm = new();
            if(msg.InsulationFault) lstAlarm.Add("4",true);
            if(msg.OutputConnectorOverTempFault ) lstAlarm.Add("5",true);
            if(msg.BmsComponentOverTempFault) lstAlarm.Add("6",true);
            if(msg.ChargeConnectorFault) lstAlarm.Add("7",true);
            if(msg.BatteryOverTempFault) lstAlarm.Add("8",true);
            if(msg.HighVoltageRelayFault) lstAlarm.Add("9",true);
            if(msg.DetectPoint2VoltageFault) lstAlarm.Add("10",true);
            if(msg.OtherFault) lstAlarm.Add("11",true);
            if(msg.OverCurrentFault) lstAlarm.Add("12",true);
            if(msg.VoltageAbnormalFault) lstAlarm.Add("13",true);
            
            FaultHandling.SaveAlarmInfoToProcessRecord(lstAlarm,EquipmentType.BMS,msg.PileCode+msg.Gun);
            
            
            Log.Info($"V14D BmsAbort from {sn}, tsn={msg.TransactionSN}, reason={msg.BmsStopReason:X2}");
        }
    }
}
