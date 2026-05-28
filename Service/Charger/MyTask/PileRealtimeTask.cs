using System.Collections.Concurrent;
using Common.Util;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Http.Req;
using Service.Init;

namespace Service.Charger.MyTask;
/// <summary>
/// 9.2.1.5 站控上报充电枪实时数据上报
/// </summary>
[Scope]
public class PileRealtimeTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(PileRealtimeTask));
    private volatile bool _stop;

    public string Name()
    {
        return "PileRealtimeTask";
    }

    public int Interval()
    {
        return 1000 * 5;
    }

    public void Handle()
    {
        ConcurrentDictionary<string, ChargerClient> chargerClients = ClientMgr.Dictionary;

        if (chargerClients.Values.Count <= 0)
        {
            return;
        }

        foreach (var kvp in chargerClients)
        {
            ChargerClient client = kvp.Value;

            HandleChargerPile(client, 1);
            HandleChargerPile(client, 2);
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
    
    private void HandleChargerPile(ChargerClient client, byte pileIndex)
    {
        if (client.ChargedPile[pileIndex])
        {
            ChargerPile chargerPile = pileIndex == 1 ? client.ChargerPile1 : client.ChargerPile2;
            PileUploadTelemetry pileUploadTelemetry = client.PileUploadTelemetry[pileIndex];

            PileRealtimeReq req = new PileRealtimeReq
            {
                sn = StaticStationInfo.StationNo,
                pn = chargerPile.pn,
                ps = GetPileStatus(client.WorkStatus[pileIndex]),
                pov = pileUploadTelemetry.BmsChargingVoltage,
                poe = pileUploadTelemetry.BmsChargingCurrent,
                ec = 0
            };

            HttpUtil.SendPostRequest(req, "http://127.0.0.1:5034/api/OutCharger/SendPileRealtime");
        }
    }

    private int GetPileStatus(int workStatus)
    {
        return workStatus switch
        {
            0 => 1,
            1 => 3,
            2 => 4,
            3 => 5,
            _ => 1
        };
    }
    
    
}