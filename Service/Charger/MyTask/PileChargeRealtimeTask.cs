using System.Collections.Concurrent;
using Common.Util;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Http.Req;
using Service.Init;

namespace Service.Charger.MyTask;
/// <summary>
/// 9.2.1.7 站控上报充电枪充电遥测数据
/// </summary>
[Scope]
public class PileChargeRealtimeTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileChargeRealtimeTask));
    private volatile bool _stop;


    public string Name()
    {
        return "PileChargeRealtimeTask";
    }

    public int Interval()
    {
        return 1000 * 30;
    }

    public void Handle()
    {
        
        ConcurrentDictionary<string, ChargerClient> chargerClients = ClientMgr.Dictionary;

        if (chargerClients.Values.Count <= 0)
        {
            return;
        }

        foreach (var clientPair in chargerClients)
        {
            ChargerClient client = clientPair.Value;

            ProcessClient(client, 1);
            ProcessClient(client, 2);
        }
    }

    private void ProcessClient(ChargerClient client, byte gunNumber)
    {
        if (client.GunCharged[gunNumber])
        {
            ChargerPile chargerPile =gunNumber ==1?client.ChargerPile1:client.ChargerPile2;
            PileUploadTelemetry telemetry = client.PileUploadTelemetry[gunNumber];
            PileUploadRemoteSignal pileUploadRemoteSignal = client.PileUploadRemoteSignal[gunNumber];

            PileChargeRealtimeReq req = new PileChargeRealtimeReq
            {
                sn = StaticStationInfo.StationNo,
                con = chargerPile.con,
                cosn = chargerPile.cosn,
                pn = chargerPile.pn,
                rv = telemetry.BmsNeedVoltage,
                re = telemetry.BmsNeedCurrent,
                cm = telemetry.ChargeMode,
                cdv = telemetry.BmsChargingVoltage,
                cde = telemetry.BmsChargingCurrent,
                soc = telemetry.CurrentSoc,
                tr = telemetry.EstimatedRemainingTime,
                pov = telemetry.DcMeterVoltage,
                poe = telemetry.DcMeterCurrent,
                tct = telemetry.ChargingTime,
                lbtn = telemetry.MinTempDetectionPointNo,
                lbt = telemetry.MinBatteryTemp,
                hbtn = telemetry.MaxTempDetectionPointNo,
                hbt = telemetry.MaxBatteryTemp,
                hlbva = pileUploadRemoteSignal.ChargerInputOverVoltageError ? 1 :
                    pileUploadRemoteSignal.ChargerInputUnderVoltageError ? 2 : 0,
                bia = pileUploadRemoteSignal.InsulationDetectionAlarm ? 1 : 0
            };

            HttpUtil.SendPostRequest(req, "http://127.0.0.1:5034/api/OutCharger/SendPileChargeRealtime");
        }
    }

    public bool Stoped()
    {
        return _stop;
    }

    public void Stop()
    {
        _stop = true;
    }

    public void ResetStop()
    {
        _stop = false;
    }
}