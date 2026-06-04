/*using Entity.Dto.Resp;
using HybirdFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.Station;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Http.Req;
using Service.Init;
using Service.Station;

namespace WebStarter.Controllers;

/// <summary>
/// 站外充电机管理
/// </summary>
[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class OutChargerController
{
    private ChargeOrderRepository _chargeOrderRepository;
    private BinInfoService _binInfoService;
    private BinGunInfoRepository _binGunInfoRepository;
    public OutChargerController(ChargeOrderRepository chargeOrderRepository,BinInfoService binInfoService,BinGunInfoRepository binGunInfoRepository)
    {
        _chargeOrderRepository = chargeOrderRepository;
        _binInfoService = binInfoService;
        _binGunInfoRepository = binGunInfoRepository;
    }

    /// <summary>
    /// 云平台下发开始充电操作
    /// </summary>
    /// <param name="httpReq"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("SendStartOutCharger")]
    public Result<bool> SendStartOutCharger([FromBody] PileStartChargeHttpReq httpReq)
    {
        
        string chargerCode = ChargerUtils.GetOutChargerCode(httpReq.pn);

        byte chargerGunCode = ChargerUtils.GetTheGun(httpReq.pn);

        ChargerClient? chargerClient = ClientMgr.GetBySn(chargerCode);

        if (chargerClient == null)
        {
            return Result<bool>.Fail("充电机未连接");
        }


        string chargeGunOrder = ChargerUtils.GenChargeOrderSn();
        if (string.IsNullOrWhiteSpace(httpReq.con))
        {
            httpReq.con = chargeGunOrder;
        }

        ChargerPile chargerPile = new()
        {
            ct = httpReq.ct,
            cp = httpReq.cp,
            st = httpReq.st,
            con = httpReq.con,
            cosn = chargeGunOrder,
            pn = httpReq.pn,
        };
        
        if (chargerGunCode==1)
            chargerClient.ChargerPile1 = chargerPile;
        else
            chargerClient.ChargerPile2 = chargerPile;

        byte chargeSoc = StaticStationInfo.ChargeSoc;
        // 下发充电枪充电
        chargerClient.SendStartOutCharger(chargerGunCode, chargeSoc, 360, 1, chargeGunOrder);
        // 初始化订单
        _chargeOrderRepository.SaveChargeGunOrder(chargeGunOrder, httpReq.con, chargerCode, httpReq.pn,
            chargerGunCode.ToString());

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// 云端下发充电枪停止充电
    /// </summary>
    /// <param name="httpReq"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("SendStopOutCharger")]
    public Result<bool> SendStopOutCharger([FromBody] PileStopChargeHttpReq httpReq)
    {
        string chargerCode = ChargerUtils.GetOutChargerCode(httpReq.pn);

        byte chargerGunCode = ChargerUtils.GetTheGun(httpReq.pn);

        ChargerClient? chargerClient = ClientMgr.GetBySn(chargerCode);

        if (chargerClient == null)
        {
            return Result<bool>.Fail("充电机未连接");
        }

        // 下发充电枪停止充电
       return chargerClient.SendStopOutCharger(chargerGunCode, 0);
    }
    
    /// <summary>
    /// 给充电枪发送功率调节指令
    /// </summary>
    /// <param name="code">充电机code</param>
    /// <param name="pn">1枪 or 2枪</param>
    /// <param name="power">功率</param>
    /// <returns></returns>
    [HttpGet]
    [Route("SendPowerRegulation/{code}/{pn}/{power}")]
    public Result<bool> SendPowerRegulation(string code,byte pn, float power)
    {
        if (power <=0  || power > 280)
        {
            return Result<bool>.Fail("功率值范围1到360");
        }
        
        if (pn != 1 && pn != 2)
        {
            return Result<bool>.Fail("请选择1枪或者2枪");
        }

        string _code = _binInfoService.QueryByClause(i => i.Code == code).ChargerNo;
        ChargerClient? chargerClient = ClientMgr.GetBySn(_code);

        if (chargerClient != null)
        {
            chargerClient.SendPileAdjustPower(pn,power);
            return Result<bool>.Success(true);
        }

        return Result<bool>.Fail("充电机未连接");
    }
    
    /// <summary>
    /// 获取充电桩信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetChargerPile")]
    public Result<List<ChargerPileResp>> GetChargerPile()
    {
        var chargerClients = ClientMgr.Dictionary;
        if (chargerClients.IsEmpty)
        {
            return Result<List<ChargerPileResp>>.Fail("没有充电机连接");
        }

        var list = chargerClients.Values.Select(client => new ChargerPileResp
        {
            Sn = client.Sn,
            GunChargedOne = client.GunCharged[1],
            GunChargedTwo = client.GunCharged[2],
            ChargedPileOne = client.ChargedPile[1],
            ChargedPileTwo = client.ChargedPile[2],
            ChargePilePowerOne = client.ChargePilePower[1],
            ChargePilePowerTwo = client.ChargePilePower[2]
        }).ToList();

        return Result<List<ChargerPileResp>>.Success(list);
    }
}*/