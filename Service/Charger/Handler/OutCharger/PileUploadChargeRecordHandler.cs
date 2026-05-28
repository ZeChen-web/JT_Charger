using Common.Util;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Host.Resp.OutCharger;
using Service.Charger.Msg.Http.Req;
using Service.Init;

namespace Service.Charger.Handler.OutCharger;

/// <summary>
/// 3.7.13 充电桩上送充电记录
/// </summary>
[Order(8)]
[Scope("InstancePerDependency")]
public class PileUploadChargeRecordHandler : SimpleChannelInboundHandler<PileUploadChargeRecord>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileUploadChargeRecordHandler));

    public ChargeOrderRepository ChargeOrderRepository { get; set; }


    protected override void ChannelRead0(IChannelHandlerContext ctx, PileUploadChargeRecord msg)
    {
        if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            ctx.Channel.WriteAndFlushAsync(new PileUploadChargeRecordRes(msg.Pn));
            if (client == null)
            {
                return;
            }

            float[] powersPeriods = new float[4] { 0, 0, 0, 0 }; //元素索引顺序代表值；1：尖；2：峰；3：平；4：谷
            var acPowersPeriods = PeaksAndValleys(msg, powersPeriods);

            Log.Info($"receive {msg} from {sn}");
            ChargeOrder chargeOrder = ChargeOrderRepository.QueryByClause(it => it.Sn == msg.ChargeOrderNo);

            var startTime = new DateTime(msg.StartYear + 2000, msg.StartMonth, msg.StartDay, msg.StartHour,
                msg.StartMinute, msg.StartSecond);
            var endTime = new DateTime(msg.EndYear + 2000, msg.EndMonth, msg.EndDay, msg.EndHour, msg.EndMinute,
                msg.EndSecond);


            float chargingPower = msg.ChargingPower;
            var socBefore = msg.SocBefore;
            byte socAfter = msg.SocAfter;

            void UpdateChargeOrder(ChargeOrder order)
            {
                order.StartTime = startTime;
                order.EndTime = endTime;
                order.StartSoc = socBefore;
                order.StopSoc = socAfter;
                order.ChargeTimeCount = (int)(endTime - startTime).TotalMinutes;
                order.ElecCount = Convert.ToDecimal(chargingPower);
                order.AcElecCount = Convert.ToDecimal(msg.AcMeterElecCount);
                order.StartAcElec = Convert.ToDecimal(msg.AcMeterDataBefore);
                order.StopAcElec = Convert.ToDecimal(msg.AcMeterDataAfter);
                order.StartDcElec = Convert.ToDecimal(msg.DcMeterDataBefore);
                order.StopDcElec = Convert.ToDecimal(msg.DcMeterDataAfter);
                order.SharpElecCount = Convert.ToDecimal(powersPeriods[0]);
                order.PeakElecCount = Convert.ToDecimal(powersPeriods[1]);
                order.FlatElecCount = Convert.ToDecimal(powersPeriods[2]);
                order.ValleyElecCount = Convert.ToDecimal(powersPeriods[3]);
                order.AcSharpElecCount = Convert.ToDecimal(acPowersPeriods[0]);
                order.AcPeakElecCount = Convert.ToDecimal(acPowersPeriods[1]);
                order.AcFlatElecCount = Convert.ToDecimal(acPowersPeriods[2]);
                order.AcValleyElecCount = Convert.ToDecimal(acPowersPeriods[3]);
                order.ChargeMode = msg.ChargeMode;
                order.StartMode = msg.StartMode;
            }

            if (chargeOrder == null)
            {
                ChargeOrder newOrder = new ChargeOrder()
                {
                    Sn = msg.ChargeOrderNo
                };
                UpdateChargeOrder(newOrder);
                ChargeOrderRepository.Insert(newOrder);

                PileEndChargeReq req = new PileEndChargeReq();


                req.sn = StaticStationInfo.StationNo;
                req.con = msg.ChargeOrderNo;
                req.cosn = msg.ChargeOrderNo;

                req.tp = powersPeriods[0];
                req.pp = powersPeriods[1];
                req.fp = powersPeriods[2];
                req.vp = powersPeriods[3];

                UploadChargeRecord(msg, client, req, sn, startTime, endTime, chargingPower, socBefore, socAfter);
            }
            else
            {
                UpdateChargeOrder(chargeOrder);
                // 充电完成入库
                ChargeOrderRepository.Update(chargeOrder);

                PileEndChargeReq req = new PileEndChargeReq();
                
                req.sn = StaticStationInfo.StationNo;
                req.con = chargeOrder.CloudChargeOrder==null?"0":chargeOrder.CloudChargeOrder;
                req.cosn = chargeOrder.Sn;
                
                req.tp = powersPeriods[0];
                req.pp = powersPeriods[1];
                req.fp = powersPeriods[2];
                req.vp = powersPeriods[3];
                // 充电完成上报云平台
                UploadChargeRecord(msg, client, req, sn, startTime, endTime, chargingPower, socBefore, socAfter);
            }
        }

        
    }

    private static void UploadChargeRecord(PileUploadChargeRecord msg, ChargerClient client, PileEndChargeReq req,
        string sn, DateTime startTime, DateTime endTime, float chargingPower, byte socBefore, byte socAfter)
    {
        ChargerPile chargerPile = msg.Pn==1?client.ChargerPile1:client.ChargerPile2;
        req.pn = msg.Pn.ToString();
        req.ct = chargerPile.ct;
        req.cp = chargerPile.cp;
        req.st = chargerPile.st;
        req.cosn = chargerPile.cosn != null ? chargerPile.cosn : req.cosn;
        req.cst = startTime;
        req.cet = endTime;
        req.ceq = chargingPower;
        req.cssoc = socBefore;
        req.cesoc = socAfter;
        
        /*List<PeriodReq> periodList = new List<PeriodReq>();
        // 利用反射组装充电时间段数据
        for (int i = 1; i <= msg.ChargingTimeCount; i++)
        {
            PeriodReq periodReq = new PeriodReq();
            // 获取属性
            var startTimeHourProperty = typeof(PileUploadChargeRecord).GetProperty($"StartTime{i}");
            var startTimeMinuteProperty = typeof(PileUploadChargeRecord).GetProperty($"StartTimeMinute{i}");

            if (startTimeHourProperty != null && startTimeMinuteProperty != null)
            {
                // 获取值
                byte startTimeHour = (byte)startTimeHourProperty.GetValue(msg);
                byte startTimeMinute = (byte)startTimeMinuteProperty.GetValue(msg);
                periodReq.StartTimeMinute = DateUtils.GetFormattedTime(startTimeHour, startTimeMinute);
            }

            var chargingPowerProperty = typeof(PileUploadChargeRecord).GetProperty($"ChargingPowerOfTime{i}");
            var flagOfTimeProperty = typeof(PileUploadChargeRecord).GetProperty($"FlagOfTime{i}");
            if (chargingPowerProperty != null && flagOfTimeProperty != null)
            {
                periodReq.ChargingPowerOfTime = (float)chargingPowerProperty.GetValue(msg);
                periodReq.FlagOfTime = (byte)flagOfTimeProperty.GetValue(msg);
            }

            periodList.Add(periodReq);
        }*/

        req.cvin = client.Vin[msg.Pn];

        HttpUtil.SendPostRequest(req, "http://127.0.0.1:5034/api/OutCharger/SendPileEndCharge");
    }

    private static float[] PeaksAndValleys(PileUploadChargeRecord msg, float[] powersPeriods)
    {
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

        return acPowersPeriods;
    }
}