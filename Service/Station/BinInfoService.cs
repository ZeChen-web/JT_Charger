using AutoMapper;
using Entity.DbModel.Station;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using Repository.Station;
using Service.Charger.Client;

namespace Service.Station;

[Scope("SingleInstance")]
public class BinInfoService : BaseServices<BinInfo>
{
    private readonly BinInfoRepository _binInfoRepository;
    private readonly BinGunInfoRepository _binGunInfoRepository;

    public BinInfoService(BinInfoRepository binInfoRepository,BinGunInfoRepository binGunInfoRepository)
    {
        _binInfoRepository = binInfoRepository;
        _binGunInfoRepository = binGunInfoRepository;
        this.BaseDal = binInfoRepository;
    }
    /// <summary>
    /// 获取仓位数据
    /// </summary>
    /// <returns>仓位数据列表</returns>
    public List<BinInfoResp> GetChargMonitorChargBinData()
    {
        List<BinInfo> binInfos = _binInfoRepository.Query();
        var configuration = new MapperConfiguration(cfg => cfg.CreateMap<BinInfo, BinInfoResp>());
        var mapper = configuration.CreateMapper();

        // 转换为 BinInfoResp 列表
        List<BinInfoResp> binInfoList = mapper.Map<List<BinInfoResp>>(binInfos);
        // 功率赋值
        foreach (var binInfoResp in binInfoList)
        {
            ChargerClient? chargerClient = ClientMgr.GetBySn(binInfoResp.ChargerNo);
            if (chargerClient != null)
            {
                binInfoResp.power = chargerClient.RealTimeChargePower;
                binInfoResp.ChargeConnectFlag = chargerClient.Connected;
                binInfoResp.ChargingTime = chargerClient.UploadTelemetryData.ChargingTime;
                binInfoResp.EstimatedRemainingTime = chargerClient.UploadTelemetryData.EstimatedRemainingTime;
                if (chargerClient.BatteryPackTotalElectricity != null)
                    binInfoResp.OnceElectricCharge = chargerClient.BatteryPackTotalElectricity.OnceElectricCharge;
                binInfoResp.BmsNeedVoltage = chargerClient.UploadTelemetryData.BmsNeedVoltage;
                binInfoResp.BmsNeedCurrent = chargerClient.UploadTelemetryData.BmsNeedCurrent;
                if (chargerClient.BatteryPackData != null)
                    binInfoResp.TotalCurrent = chargerClient.BatteryPackData.TotalCurrent;
                if (chargerClient.BatteryPackDataVoltage != null)
                    binInfoResp.CellTemperatureMax = chargerClient.BatteryPackDataVoltage.CellTemperatureMax;
                if (chargerClient.BatteryPackDataVoltage != null)
                    binInfoResp.CellTemperatureMin = chargerClient.BatteryPackDataVoltage.CellTemperatureMin;
                binInfoResp.ChargingStartTime = chargerClient.ChargingStartTime;
                binInfoResp.ChargingStopTime = chargerClient.ChargingStopTime;
                binInfoResp.IsAuthed = chargerClient.IsAuthed;

                binInfoResp.ChargingInterfaceDetectionOneTemp = chargerClient.UploadTelemetryData.ChargingInterfaceDetectionOneTemp;
                binInfoResp.ChargingInterfaceDetectionTwoTemp = chargerClient.UploadTelemetryData.ChargingInterfaceDetectionTwoTemp;
                binInfoResp.ChargingInterfaceDetectionTheTemp = chargerClient.UploadTelemetryData.ChargingInterfaceDetectionTheTemp;
                binInfoResp.ChargingInterfaceDetectionFourTemp = chargerClient.UploadTelemetryData.ChargingInterfaceDetectionFourTemp;

            }
        }

        return binInfoList;
    }

    /// <summary>
    /// 获取仓位数据
    /// </summary>
    /// <returns>仓位数据列表</returns>
    public List<BinGunInfoResp> GetChargGunMonitorChargBinData()
    {
        List<BinGunInfo> binInfos = _binGunInfoRepository.Query();
        var configuration = new MapperConfiguration(cfg => cfg.CreateMap<BinGunInfo, BinGunInfoResp>());
        var mapper = configuration.CreateMapper();

        // 转换为 BinInfoResp 列表
        List<BinGunInfoResp> binInfoList = mapper.Map<List<BinGunInfoResp>>(binInfos);
        // 功率赋值
        foreach (var binInfoResp in binInfoList)
        {
            ChargerClient? chargerClient = ClientMgr.GetBySn(binInfoResp.ChargerNo);
            if (chargerClient != null)
            {
                binInfoResp.ChargeConnectFlag = chargerClient.Connected;
                binInfoResp.IsAuthed = chargerClient.IsAuthed;
                binInfoResp.Vin = chargerClient.Vin[(byte)binInfoResp.GunNo];
                var pileUploadTelemetry = chargerClient.PileUploadTelemetry[(byte)binInfoResp.GunNo];
                if (pileUploadTelemetry != null)
                {
                    binInfoResp.ChargingTime = pileUploadTelemetry.ChargingTime;
                    binInfoResp.EstimatedRemainingTime = pileUploadTelemetry.EstimatedRemainingTime;
                    
                    binInfoResp.BmsNeedVoltage = pileUploadTelemetry.BmsNeedVoltage;
                    binInfoResp.BmsNeedCurrent = pileUploadTelemetry.BmsNeedCurrent;
                    binInfoResp.ChargingInterfaceDetectionOneTemp = pileUploadTelemetry.ChargingInterfaceDetectionOneTemp;
                    binInfoResp.ChargingInterfaceDetectionTwoTemp = pileUploadTelemetry.ChargingInterfaceDetectionTwoTemp;
                    binInfoResp.ChargingInterfaceDetectionTheTemp = pileUploadTelemetry.ChargingInterfaceDetectionTheTemp;
                    binInfoResp.ChargingInterfaceDetectionFourTemp = pileUploadTelemetry.ChargingInterfaceDetectionFourTemp;
                    binInfoResp.Power = pileUploadTelemetry.DcMeterCurrentPower;
                    
                }

                var pileUploadRemoteSignal = chargerClient.PileUploadRemoteSignal[(byte)binInfoResp.GunNo];
                if (pileUploadRemoteSignal!=null)
                {
                    binInfoResp.ChargeStationGunHolderStatus = pileUploadRemoteSignal.ChargeStationGunHolderStatus;
                    
                }
                
            }
        }

        return binInfoList;
    }
    
    /// <summary>
    /// 禁用仓位
    /// </summary>
    /// <param name="id"></param>
    /// <returns>修改结果</returns>
    public bool UpdateStatus(int id)
    {
        return _binInfoRepository.UpdateStatus(id);
    }
}
