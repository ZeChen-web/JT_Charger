using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Configuration;
using HybirdFrameworkCore.Redis;
using log4net;
using Newtonsoft.Json;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 遥测数据上报Handler
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class UploadTelemetryDataHandler : SimpleChannelInboundHandler<UploadTelemetryData>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UploadTelemetryDataHandler));

        public RedisHelper RedisHelper { get; set; }
        protected override void ChannelRead0(IChannelHandlerContext ctx, UploadTelemetryData msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                msg.ChargerNo = sn;
                Log.Info($"receive {msg} from {sn}");

                if (!AppSettingsConstVars.DisabledTask.Contains("UploadTelemetryData"))
                {
                    RedisHelper.PublishAsync("UploadTelemetryData", JsonConvert.SerializeObject(msg));
                }

                client.UploadTelemetryData = msg;
                //充电机实时充电功率
                client.RealTimeChargePower = msg.HighVoltageAcquisitionCurrent * msg.HighVoltageAcquisitionVoltage;
            }
        }
    }
}
