using AutoMapper;
using Entity.DbModel.Station;
using Entity.Dto.Req;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Station;
using Service.Charger;
using Service.Charger.Client;
using Service.Station;

namespace WebStarter.Controllers;

/// <summary>
/// 充电机管理
/// </summary>
[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ChargeController : ControllerBase
{
    private ChargerService _chargerService;
    private BinInfoService _binInfoService;
    private EquipInfoRepository _equipInfoRepository;

    public ChargeController(ChargerService chargerService, BinInfoService binInfoService,EquipInfoRepository equipInfoRepository)
    {
        _chargerService = chargerService;
        _binInfoService = binInfoService;
        _equipInfoRepository = equipInfoRepository;
    }
    
    /// <summary>
    /// 获取充电机code列表
    /// </summary>
    /// <returns>充电机code列表返回</returns>
    [HttpGet]
    [Route("GetChargerCodeList")]
    public async Task<Result<List<string>>> GetChargerCodeList()
    {
        List<string> keysList = new List<string>(ClientMgr.Dictionary.Keys);
        return Result<List<string>>.Success(keysList);
    }

    /// <summary>
    /// 给充电机发鉴权
    /// </summary>
    /// <param name="code">充电机编码</param>
    /// <returns>发送结果</returns>
    [HttpGet]
    [Route("ChargerSendAuth/{code}")]
    public Result<bool> ChargerSendAuth(string code)
    {
        ChargerClient? chargerClient = ClientMgr.GetBySn(code);

        if (chargerClient != null)
        {
            chargerClient.SendAuth();
            return Result<bool>.Success(true);
        }

        return Result<bool>.Fail("充电机未连接");
        
    }
    
    /// <summary>
    /// 给充电机发送功率调节指令
    /// </summary>
    /// <param name="code">充电机编码</param>
    /// <returns>发送结果</returns>
    [HttpGet]
    [Route("SendPowerRegulation/{code}/{power}")]
    public Result<bool> SendPowerRegulation(string code, float power)
    {
        if (power <=0  || power > 280)
        {
            return Result<bool>.Fail("功率值范围1到360");
        }

        string _code = _binInfoService.QueryByClause(i => i.Code == code).ChargerNo;
        ChargerClient? chargerClient = ClientMgr.GetBySn(_code);

        if (chargerClient != null)
        {
            chargerClient.SendPowerRegulation(power);
          
            _equipInfoRepository.Update(i => i.ChargePower == power, it => it.Code == _code);
            return Result<bool>.Success(true);
        }

        return Result<bool>.Fail("充电机未连接");
    }

    /// <summary>
    /// 仓位信息
    /// </summary>
    /// <returns>仓位信息列表</returns>
    [HttpGet]
    [Route("GetChargMonitorChargBinData")]
    public Result<List<BinInfoResp>> GetChargMonitorChargBinData()
    {
        return Result<List<BinInfoResp>>.Success(_binInfoService.GetChargMonitorChargBinData());
    }
    
    /// <summary>
    /// 充电枪信息
    /// </summary>
    /// <returns>充电枪信息列表</returns>
    [HttpGet]
    [Route("GetChargGunMonitorChargBinData")]
    public Result<List<BinGunInfoResp>> GetChargGunMonitorChargBinData()
    {
        return Result<List<BinGunInfoResp>>.Success(_binInfoService.GetChargGunMonitorChargBinData());
    }

    /// <summary>
    /// 仓位禁用
    /// </summary>
    /// <param name="data">需要禁用仓id</param>
    /// <returns>仓位信息列表</returns>
    [HttpGet]
    [Route("ChargingBinDisable/{data}")]
    public Result<bool> ChargingBinDisable(int data)
    {
        return Result<bool>.Success(_binInfoService.UpdateStatus(data));
    }

    /// <summary>
    /// 获取仓位实时功率
    /// </summary>
    /// <returns>仓位实时功率列表</returns>
    [HttpPost]
    [Route("GetBinPowers")]
    public Result<float[]> GetBinPowers()
    {
        float[] results = ClientMgr.Dictionary.Values
            .Select(chargerClient => chargerClient.RealTimeChargePower)
            .ToArray();
        return Result<float[]>.Success(results);
    }

    /// <summary>
    /// 通过仓号启动充电
    /// </summary>
    /// <param name="binNo">仓号</param>
    /// <returns></returns>
    [HttpGet]
    [Route("StartChargeByBinNo/{binNo}")]
    public Result<bool> StartChargeByBinNo(string binNo)
    {
        return _chargerService.StartChargeByBinNo(binNo);
    }

    /// <summary>
    /// 通过仓号停止充电
    /// </summary>
    /// <param name="binNo">仓号</param>
    /// <returns></returns>
    [HttpGet]
    [Route("StopChargeByBinNo/{binNo}")]
    public Result<bool> StopChargeByBinNo(string binNo)
    {
        return _chargerService.StopChargeByBinNo(binNo);
    }
    
    /// <summary>
    /// 下发电价配置
    /// </summary>
    /// <param name="version"></param>
    [HttpGet]
    [Route("DistributeElecPriceForCharge/{Version}")]
    public Result<bool> DistributeElecPriceForCharge(int Version)
    {
        return _chargerService.DistributeElecPriceForCharge(Version);
    }
    //BatteryStatusInfo

    /// <summary>
    /// 电池状态信息：电池总数 满电数量、充电中、故障电池、维护中电池
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("BatteryStatusInfo")]
    public Result<BatteryStatusInfoResp> BatteryStatusInfo()
    {
        return _chargerService.BatteryStatusInfo();
    }

    /// <summary>
    /// 电池数据显示
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("BatteryInfo/{chargeNo}")]
    public Result<BatteryInfoResp> BatteryInfo(string chargeNo)
    {
        return _chargerService.BatteryInfo(chargeNo);
    } 
}