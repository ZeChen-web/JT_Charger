using AutoMapper;
using Entity.DbModel.Station;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Server;

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
            V14DChargerClient? chargerClient = ServerMgr.GetBySn(binInfoResp.ChargerNo);
            if (chargerClient != null)
            {
                binInfoResp.power = chargerClient.ChargePower;
                binInfoResp.ChargeConnectFlag = chargerClient.Connected;
                binInfoResp.IsAuthed = chargerClient.IsLoggedIn;

                var realtime = chargerClient.RealTimeData;
                if (realtime != null)
                {
                    binInfoResp.ChargingTime = realtime.ChargeTime;
                    binInfoResp.EstimatedRemainingTime = realtime.RemainTime;
                    binInfoResp.CellTemperatureMax = (short)realtime.MaxBatTempValue;
                }
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
            V14DChargerClient? chargerClient = ServerMgr.GetBySn(binInfoResp.ChargerNo);
            if (chargerClient != null)
            {
                binInfoResp.ChargeConnectFlag = chargerClient.Connected;
                binInfoResp.IsAuthed = chargerClient.IsLoggedIn;

                var realtime = chargerClient.RealTimeData;
                if (realtime != null)
                {
                    binInfoResp.ChargingTime = realtime.ChargeTime;
                    binInfoResp.EstimatedRemainingTime = realtime.RemainTime;
                    binInfoResp.Power = realtime.ChargePower;
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
