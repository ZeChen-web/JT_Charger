using Autofac;
using Common.Const;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;
using Service.Swap.Dto;
using HybirdFrameworkCore.Redis;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBatteryStatusReportHandler : SimpleChannelInboundHandler<V14DBatteryStatusReportReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBatteryStatusReportHandler));
    private BinInfoRepository _binInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();
    private RedisHelper RedisHelper { get; set; } = AppInfo.Container.Resolve<RedisHelper>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBatteryStatusReportReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun, out var sn, out var client))
        {
            #region 更新电池信息

            client.BatteryNo = msg.BatteryCode;

            var u1 = _binInfoRepository.Update(
                t => new BinInfo()
                {
                    Soc = msg.SOC,
                    BatteryNo = msg.BatteryCode
                },
                i => i.ChargerNo == msg.PileCode && i.ChargerGunNo == (msg.Gun).ToString());

            #endregion

            #region 报警入库

            
            Dictionary<string, bool> lstAlarm = new();
            if (msg.BatteryFault == 1)
                lstAlarm.Add("1", msg.BatteryFault == 1);

            FaultHandling.SaveAlarmInfo(lstAlarm, EquipmentType.BMS, msg.PileCode + msg.Gun);
            
            #endregion

            #region redis数据上传

            DataInfo dataInfo = new DataInfo();
            dataInfo.en = client.PileCode + msg.Gun;
            dataInfo.sd = client.BinNo;
            dataInfo.hb = client.Exists;
            //dataInfo.el =电接头 
            dataInfo.cno = client.No;
            dataInfo.cs = client.PileStatus;
            dataInfo.fs = client.PileStatus == 3 ? 1 : 0;
            //dataInfo.@as=
            //dataInfo.fc=
            dataInfo.st = client.StartTime;
            dataInfo.ct = Convert.ToInt32(client.RealTimeData.ChargeTime);
            dataInfo.ssoc = (int)client.StartSoc;
            dataInfo.csoc = (int)client.SOC;
            dataInfo.cvot = msg.ChargeVoltage;
            dataInfo.ccur = msg.ChargeCurrent;
            dataInfo.nvot = msg.DemandVoltage;
            dataInfo.ncur = msg.DemandCurrent;
            dataInfo.lsv = msg.MinCellVoltage;
            dataInfo.hsv = msg.MaxCellVoltage;
            dataInfo.lst = msg.MinBatteryTemperature;
            dataInfo.hst = msg.MaxBatteryTemperature;
            dataInfo.bt=DateTime.Now;
            

            RedisHelper.PublishAsync("BatteryInfoUploadTask", JsonConvert.SerializeObject(dataInfo));

            #endregion
            


            client.BatteryStatusReport = msg;
            Log.Info(
                $"V14D BatteryStatusReport from {sn}, pile={msg.PileCode}, gun={msg.Gun}, soc={msg.SOC}%, fault={msg.BatteryFault}");
        }
    }
}