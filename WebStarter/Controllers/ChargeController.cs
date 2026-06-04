using AutoMapper;
using Entity.DbModel.Station;
using Entity.Dto.Req;
using Entity.Dto.Resp;
using HybirdFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Station;
using Service.ChargerV14D;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Server;
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
    private V14DChargeService _chargerService;
    private BinInfoService _binInfoService;
    private EquipInfoRepository _equipInfoRepository;

    public ChargeController(V14DChargeService chargerService, BinInfoService binInfoService,EquipInfoRepository equipInfoRepository)
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
        List<string> keysList = new List<string>(ServerMgr.Dictionary.Keys);
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
        V14DChargerClient? chargerClient = ServerMgr.GetBySn(code);

        if (chargerClient != null)
        {
            // TODO: V14D鉴权需通过计费模型验证流程实现
            return Result<bool>.Fail("V14D协议暂不支持独立鉴权指令，请使用计费模型验证");
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
        V14DChargerClient? chargerClient = ServerMgr.GetBySn(_code);

        if (chargerClient != null)
        {
            chargerClient.SendParamSet(chargerClient.PileCode, 1, 1, (byte)power);
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
        float[] results = ServerMgr.Dictionary.Values
            .Select(chargerClient => chargerClient.ChargePower)
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
        var binInfo = _binInfoService.QueryByClause(i => i.Code == binNo);
        if (binInfo == null)
            return Result<bool>.Fail("仓位不存在");
        /*return */_chargerService.StartCharge(binInfo.ChargerNo, 1, 100, 0);
        return Result<bool>.Success(true);
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
        var binInfo = _binInfoService.QueryByClause(i => i.Code == binNo);
        if (binInfo == null)
            return Result<bool>.Fail("仓位不存在");
        return _chargerService.StopCharge(binInfo.ChargerNo, 1);
    }

    /// <summary>
    /// 下发电价配置
    /// </summary>
    /// <param name="version"></param>
    [HttpGet]
    [Route("DistributeElecPriceForCharge/{Version}")]
    public Result<bool> DistributeElecPriceForCharge(int Version)
    {
        // TODO: V14D协议需使用DistributeBillingModel接口
        return Result<bool>.Fail("V14D协议请使用计费模型下发接口");
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
        // TODO: V14D协议需重新实现
        return Result<BatteryStatusInfoResp>.Fail("V14D协议暂未实现");
    }

    /// <summary>
    /// 电池数据显示
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("BatteryInfo/{chargeNo}")]
    public Result<BatteryInfoResp> BatteryInfo(string chargeNo)
    {
        // TODO: V14D协议需重新实现
        return Result<BatteryInfoResp>.Fail("V14D协议暂未实现");
    }
}