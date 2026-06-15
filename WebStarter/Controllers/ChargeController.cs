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
        List<string> keysList = new List<string>(V14DClientMgr.Dictionary.Keys.Select(k => k.Item1).Distinct());
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
        V14DChargerClient? chargerClient = V14DClientMgr.GetBySn(code,"1");

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
        if (power <=0  || power > 100)
        {
            return Result<bool>.Fail("功率值范围1到100");
        }

        var binInfo = _binInfoService.QueryByClause(i => i.Code == code);
        
        V14DChargerClient? chargerClient = V14DClientMgr.GetBySn(binInfo.ChargerNo,binInfo.ChargerGunNo);

        if (chargerClient != null)
        {
            chargerClient.SendParamSet(chargerClient.PileCode,Convert.ToByte(binInfo.ChargerGunNo) ,  (byte)power);
            _equipInfoRepository.Update(i => i.ChargePower == power, it => it.Code == binInfo.ChargerNo);
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
        float[] results = V14DClientMgr.Dictionary.Values
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
        /*return */_chargerService.StartCharge(binInfo.ChargerNo, Convert.ToByte(binInfo.ChargerGunNo),  10);
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
        return _chargerService.StopCharge(binInfo.ChargerNo, Convert.ToByte(binInfo.ChargerGunNo));
    }

    /// <summary>
    /// 下发电价配置到所有已连接充电桩
    /// </summary>
    /// <param name="version">计费模型版本号</param>
    [HttpGet]
    [Route("DistributeElecPriceForCharge/{Version}")]
    public Result<bool> DistributeElecPriceForCharge(int Version)
    {
        var chargerSns = new List<string>(V14DClientMgr.Dictionary.Keys.Select(k => k.Item1).Distinct());
        if (chargerSns.Count == 0)
            return Result<bool>.Fail("没有已连接的充电桩");

        var failedSns = new List<string>();
        foreach (var sn in chargerSns)
        {
            var result = _chargerService.DistributeBillingModel(sn, Version);
            if (!result.IsSuccess)
                failedSns.Add(sn);
        }

        if (failedSns.Count > 0)
            return Result<bool>.Fail($"以下充电桩下发失败: {string.Join(", ", failedSns)}");

        return Result<bool>.Success();
    }

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
    [Route("BatteryInfo/{chargeNo}/{no}")]
    public Result<BatteryInfoResp> BatteryInfo(string chargeNo,byte no)
    {
        if (chargeNo.Length <= 14)
            return Result<BatteryInfoResp>.Fail("充电编码不匹配");
        if (no<0||no>=13)
        {
            return Result<BatteryInfoResp>.Fail("仓位编码不匹配");
        }
        return _chargerService.BatteryInfo(chargeNo,no);
    }
}