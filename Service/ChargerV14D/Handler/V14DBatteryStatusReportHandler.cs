using Autofac;
using Common.Const;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBatteryStatusReportHandler : SimpleChannelInboundHandler<V14DBatteryStatusReportReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBatteryStatusReportHandler));
    private BinInfoRepository _binInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBatteryStatusReportReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun, out var sn, out var client))
        {
            client.BatteryNo = msg.BatteryCode;

            var u1 = _binInfoRepository.Update(
                t => new BinInfo()
                {
                    Soc = msg.SOC,
                    BatteryNo = msg.BatteryCode
                },
                i => i.ChargerNo == msg.PileCode && i.ChargerGunNo == (msg.Gun).ToString());


            Dictionary<string, bool> lstAlarm = new();
            if (msg.BatteryFault == 1)
                lstAlarm.Add("1", msg.BatteryFault == 1);

            FaultHandling.SaveAlarmInfo(lstAlarm, EquipmentType.BMS, msg.PileCode + msg.Gun);


            client.BatteryStatusReport = msg;
            Log.Info(
                $"V14D BatteryStatusReport from {sn}, pile={msg.PileCode}, gun={msg.Gun}, soc={msg.SOC}%, fault={msg.BatteryFault}");
        }
    }
}