using Common.Const;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Utils;
using log4net;
using Repository.Station;
using Service.Charger.Client;
using Service.Init;

namespace Service.Charger.MyTask;

[Scope]
public class EmeterEnergyRecordTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(EmeterEnergyRecordTask));
    private volatile bool _stop;
    
    public EmeterEnergyRepository EmeterEnergyRepository { get; set; }
    public EmeterEnergyChangeRepository EmeterEnergyChangeRepository { get; set; }

    public string Name()
    {
        return "EmeterEnergyRecordTask";
    }

    public int Interval()
    {
        return 1000 * 15;
    }

    public void Handle()
    {
        try
        {
            List<EmeterEnergy> list = new List<EmeterEnergy>();
            List<EmeterEnergyChange> listEmeterEnergyChanges = new List<EmeterEnergyChange>();
            
            DateTime time = DateTime.Now.AddDays(-7);
            EmeterEnergyRepository.Delete(i=>i.UploadTime<time);
            EmeterEnergyChangeRepository.Delete(i=>i.UploadTime<time);
            
            foreach (var keyValuePair in ClientMgr.Dictionary)
            {
                var chargerClient = keyValuePair.Value;
                if (chargerClient.Connected)
                {
                 
                        string id = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string code = StaticStationInfo.StationNo + "_" + chargerClient.Sn;
                        id = id + "_" + code;
                        EmeterEnergy emeterEnergy = new()
                        {
                            Id = id,
                            UploadFlag = 0,
                            Value = chargerClient.UploadTelemetryData.DcMeterCurrentPower,
                            Code =code ,
                            UploadTime = DateTime.Now
                        };
                        list.Add(emeterEnergy);
                       
                        EmeterEnergyChange emeterEnergyChange = new()
                        {
                            Id = id,
                            UploadFlag = 0,
                            Value = chargerClient.UploadTelemetryData.ACMeterCurrentBatteryValue,
                            Code = code,
                            UploadTime = DateTime.Now
                        };
                        listEmeterEnergyChanges.Add(emeterEnergyChange);
                       
                    
                }
            }

            if (list.Count > 0)
            {
                EmeterEnergyRepository.Insert(list);
            }

            if (listEmeterEnergyChanges.Count > 0)
            {
                EmeterEnergyChangeRepository.Insert(listEmeterEnergyChanges);
            }

            
        }
        catch (Exception e)
        {
            Log.Error("handle with error", e);
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