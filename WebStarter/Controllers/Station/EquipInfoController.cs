using Entity.Base;
using Entity.DbModel.Station;
using Entity.Dto.Req;
using HybirdFrameworkCore.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Station;

namespace WebStarter.Controllers.Station;

/// <summary>
/// 充电模式配置
/// </summary>
[Produces("application/json")]
[ApiController]
public class EquipInfoController
{
    private EquipInfoService _equipInfoService;

    public EquipInfoController(EquipInfoService equipInfoService)
    {
        _equipInfoService = equipInfoService;
    }

    /// <summary>
    /// 充电模式配置分页
    /// </summary>
    /// <returns>充电模式分页列表</returns>
    [HttpPost]
    [Route("/api/equipInfo/page")]
    public async Task<Result<PageResult<EquipInfo>>> Page(PageEquipInfoReq input)
    {
        return Result<PageResult<EquipInfo>>.Success(await _equipInfoService.Page(input));
    }

    /// <summary>
    /// 充电模式配置修改
    /// </summary>
    /// <returns>修改结果</returns>
    [HttpPost]
    [Route("/api/equipInfo/update")]
    public async Task<Result<object>> Update(UpdateEquipInfoReq input)
    {
        return await _equipInfoService.UpdateEquipInfoReq(input);
    }

    /// <summary>
    /// 充电模式配置删除
    /// </summary>
    /// <returns>删除结果</returns>
    [HttpPost]
    [Route("/api/equipInfo/delete")]
    public async Task<Result<object>> Delete(DeleteEquipInfoReq input)
    {
        return await _equipInfoService.DeleteEquipInfoReq(input);
    }
}