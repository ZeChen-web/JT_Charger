using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 主动上送充电记录
    /// <code>
    /// 1，保存到Redis-7
    /// 2，保存日志到log
    /// 3，回复主动上送充电记录
    /// 4，保存充电记录日志
    /// 5，保存 充电记录保存到云平台记录标识
    /// </code>
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class RecordChargeHandler : SimpleChannelInboundHandler<RecordCharge>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RecordChargeHandler));

        private ChargeOrderRepository _chargeOrderRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="chargeOrderRepository"></param>
        public RecordChargeHandler(ChargeOrderRepository chargeOrderRepository)
        {
            _chargeOrderRepository = chargeOrderRepository;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, RecordCharge msg)
        {

            if(ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {


                float[] powersPeriods = new float[4] { 0, 0, 0, 0 }; //元素索引顺序代表值；1：尖；2：峰；3：平；4：谷
                if (msg.FlagOfTime1 >= 1 && msg.FlagOfTime1 <= 4){powersPeriods[msg.FlagOfTime1 - 1] += msg.ChargingPowerOfTime1;}
                if (msg.FlagOfTime2 >= 1 && msg.FlagOfTime2 <= 4){powersPeriods[msg.FlagOfTime2 - 1] += msg.ChargingPowerOfTime2;}
                if (msg.FlagOfTime3 >= 1 && msg.FlagOfTime3 <= 4){powersPeriods[msg.FlagOfTime3 - 1] += msg.ChargingPowerOfTime3;}
                if (msg.FlagOfTime4 >= 1 && msg.FlagOfTime4 <= 4){powersPeriods[msg.FlagOfTime4 - 1] += msg.ChargingPowerOfTime4;}
                if (msg.FlagOfTime5 >= 1 && msg.FlagOfTime5 <= 4){powersPeriods[msg.FlagOfTime5 - 1] += msg.ChargingPowerOfTime5;}
                if (msg.FlagOfTime6 >= 1 && msg.FlagOfTime6 <= 4){powersPeriods[msg.FlagOfTime6 - 1] += msg.ChargingPowerOfTime6;}
                if (msg.FlagOfTime7 >= 1 && msg.FlagOfTime7 <= 4){powersPeriods[msg.FlagOfTime7 - 1] += msg.ChargingPowerOfTime7;}
                if (msg.FlagOfTime8 >= 1 && msg.FlagOfTime8 <= 4){powersPeriods[msg.FlagOfTime8 - 1] += msg.ChargingPowerOfTime8;}
                if (msg.FlagOfTime9 >= 1 && msg.FlagOfTime9 <= 4){powersPeriods[msg.FlagOfTime9 - 1] += msg.ChargingPowerOfTime9;}
                if (msg.FlagOfTime10 >= 1 && msg.FlagOfTime10 <= 4){powersPeriods[msg.FlagOfTime10 - 1] += msg.ChargingPowerOfTime10;}
                if (msg.FlagOfTime11 >= 1 && msg.FlagOfTime11 <= 4){powersPeriods[msg.FlagOfTime11 - 1] += msg.ChargingPowerOfTime11;}
                if (msg.FlagOfTime12 >= 1 && msg.FlagOfTime12 <= 4){powersPeriods[msg.FlagOfTime12 - 1] += msg.ChargingPowerOfTime12;}
                if (msg.FlagOfTime13 >= 1 && msg.FlagOfTime13 <= 4){powersPeriods[msg.FlagOfTime13 - 1] += msg.ChargingPowerOfTime13;}
                if (msg.FlagOfTime14 >= 1 && msg.FlagOfTime14 <= 4){powersPeriods[msg.FlagOfTime14 - 1] += msg.ChargingPowerOfTime14;}

                float[] acPowersPeriods = new float[4] { 0, 0, 0, 0 }; //元素索引顺序代表值；1：尖；2：峰；3：平；4：谷
                if (msg.AcFlagOfTime1 >= 1 && msg.AcFlagOfTime1 <= 4){acPowersPeriods[msg.AcFlagOfTime1 - 1] += msg.AcChargingPowerOfTime1;}
                if (msg.AcFlagOfTime2 >= 1 && msg.AcFlagOfTime2 <= 4){acPowersPeriods[msg.AcFlagOfTime2 - 1] += msg.AcChargingPowerOfTime2;}
                if (msg.AcFlagOfTime3 >= 1 && msg.AcFlagOfTime3 <= 4){acPowersPeriods[msg.AcFlagOfTime3 - 1] += msg.AcChargingPowerOfTime3;}
                if (msg.AcFlagOfTime4 >= 1 && msg.AcFlagOfTime4 <= 4){acPowersPeriods[msg.AcFlagOfTime4 - 1] += msg.AcChargingPowerOfTime4;}
                if (msg.AcFlagOfTime5 >= 1 && msg.AcFlagOfTime5 <= 4){acPowersPeriods[msg.AcFlagOfTime5 - 1] += msg.AcChargingPowerOfTime5;}
                if (msg.AcFlagOfTime6 >= 1 && msg.AcFlagOfTime6 <= 4){acPowersPeriods[msg.AcFlagOfTime6 - 1] += msg.AcChargingPowerOfTime6;}
                if (msg.AcFlagOfTime7 >= 1 && msg.AcFlagOfTime7 <= 4){acPowersPeriods[msg.AcFlagOfTime7 - 1] += msg.AcChargingPowerOfTime7;}
                if (msg.AcFlagOfTime8 >= 1 && msg.AcFlagOfTime8 <= 4){acPowersPeriods[msg.AcFlagOfTime8 - 1] += msg.AcChargingPowerOfTime8;}
                if (msg.AcFlagOfTime9 >= 1 && msg.AcFlagOfTime9 <= 4){acPowersPeriods[msg.AcFlagOfTime9 - 1] += msg.AcChargingPowerOfTime9;}
                if (msg.AcFlagOfTime10 >= 1 && msg.AcFlagOfTime10 <= 4){acPowersPeriods[msg.AcFlagOfTime10 - 1] += msg.AcChargingPowerOfTime10;}
                if (msg.AcFlagOfTime11 >= 1 && msg.AcFlagOfTime11 <= 4){acPowersPeriods[msg.AcFlagOfTime11 - 1] += msg.AcChargingPowerOfTime11;}
                if (msg.AcFlagOfTime12 >= 1 && msg.AcFlagOfTime12 <= 4){acPowersPeriods[msg.AcFlagOfTime12 - 1] += msg.AcChargingPowerOfTime12;}
                if (msg.AcFlagOfTime13 >= 1 && msg.AcFlagOfTime13 <= 4){acPowersPeriods[msg.AcFlagOfTime13 - 1] += msg.AcChargingPowerOfTime13;}
                if (msg.AcFlagOfTime14 >= 1 && msg.AcFlagOfTime14 <= 4){acPowersPeriods[msg.AcFlagOfTime14 - 1] += msg.AcChargingPowerOfTime14;}

                client.ChargeOrderNo = msg.ChargerOrderNo;

                ChargeOrder db = _chargeOrderRepository.QueryByClause(it => it.Sn == msg.ChargerOrderNo);

                DateTime startTime = new DateTime((msg.StartYear+2000) , msg.StartMonth ,msg.StartDay ,msg.StartHour, msg.StartMinute, msg.StartSecond);
                DateTime endTime = new DateTime(msg.EndYear + 2000, msg.EndMonth, msg.EndDay, msg.EndHour, msg.EndMinute, msg.EndSecond);
                
                if (db == null)
                {
                    TimeSpan timeSpan = endTime - startTime;
                    ChargeOrder chargeOrder = new ChargeOrder()
                    {
                        Sn = client.ChargeOrderNo,
                        BatteryNo = client.BatteryNo,
                        StartTime = startTime,
                        EndTime = endTime,
                        StartSoc = msg.SocBefore,
                        StopSoc = msg.SocAfter,
                        
                        ChargeTimeCount = (int)Math.Round(timeSpan.TotalMinutes),
                        ElecCount = Convert.ToDecimal(msg.ChargingPower),
                        AcElecCount = Convert.ToDecimal(msg.AcMeterElecCount),
                        StartAcElec = Convert.ToDecimal(msg.AcMeterDataBefore),
                        StopAcElec = Convert.ToDecimal(msg.AcMeterDataAfter),
                        StartDcElec = Convert.ToDecimal(msg.DcMeterDataBefore),
                        StopDcElec = Convert.ToDecimal(msg.DcMeterDataAfter),
                        SharpElecCount = Convert.ToDecimal(powersPeriods[0]),
                        PeakElecCount = Convert.ToDecimal(powersPeriods[1]),
                        FlatElecCount = Convert.ToDecimal(powersPeriods[2]),
                        ValleyElecCount = Convert.ToDecimal(powersPeriods[3]),
                        AcSharpElecCount = Convert.ToDecimal(acPowersPeriods[0]),
                        AcPeakElecCount = Convert.ToDecimal(acPowersPeriods[1]),
                        AcFlatElecCount = Convert.ToDecimal(acPowersPeriods[2]),
                        AcValleyElecCount = Convert.ToDecimal(acPowersPeriods[3]),
                        ChargeMode = msg.ChargeMode,
                        StartMode = msg.StartMode,
                        StartType = 2
                    };

                    _chargeOrderRepository.Insert(chargeOrder);
                }
                else
                {
                    db.StartTime = new DateTime((msg.StartYear+2000) , msg.StartMonth ,msg.StartDay ,msg.StartHour, msg.StartMinute, msg.StartSecond);
                    db.EndTime = new DateTime(msg.EndYear + 2000, msg.EndMonth, msg.EndDay, msg.EndHour, msg.EndMinute, msg.EndSecond);
                    db.StartSoc = msg.SocBefore;
                    db.StopSoc = msg.SocAfter;
                    TimeSpan? timeSpan = (db.EndTime - db.StartTime);
                    db.ChargeTimeCount= (int)Math.Round(Convert.ToDecimal(timeSpan?.TotalMinutes));
                    db.ElecCount = Convert.ToDecimal(msg.ChargingPower);
                    db.AcElecCount = Convert.ToDecimal(msg.AcMeterElecCount);
                    db.StartAcElec = Convert.ToDecimal(msg.AcMeterDataBefore);
                    db.StopAcElec = Convert.ToDecimal(msg.AcMeterDataAfter);
                    db.StartDcElec = Convert.ToDecimal(msg.DcMeterDataBefore);
                    db.StopDcElec = Convert.ToDecimal(msg.DcMeterDataAfter);
                    db.SharpElecCount = Convert.ToDecimal(powersPeriods[0]);
                    db.PeakElecCount = Convert.ToDecimal(powersPeriods[1]);
                    db.FlatElecCount = Convert.ToDecimal(powersPeriods[2]);
                    db.ValleyElecCount = Convert.ToDecimal(powersPeriods[3]);
                    db.AcSharpElecCount = Convert.ToDecimal(acPowersPeriods[0]);
                    db.AcPeakElecCount = Convert.ToDecimal(acPowersPeriods[1]);
                    db.AcFlatElecCount = Convert.ToDecimal(acPowersPeriods[2]);
                    db.AcValleyElecCount = Convert.ToDecimal(acPowersPeriods[3]);
                    db.ChargeMode = msg.ChargeMode;
                    db.StartMode = msg.StartMode;
                    _chargeOrderRepository.Update(db);
                }

                ctx.Channel.WriteAndFlushAsync(new RecordChargeRespData());

                Log.Info($"receive {msg} from {sn}");
            }

        }
    }
}
