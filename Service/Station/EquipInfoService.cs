using System.ComponentModel;
using Entity.Base;
using Entity.DbModel.Station;
using Entity.Dto.Req;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using Mapster;
using Repository.Station;
using SqlSugar;

namespace Service.Station;

[Scope("SingleInstance")]
public class EquipInfoService : BaseServices<EquipInfo>
{
    private readonly EquipInfoRepository _equipInfoRepository;
    
    public EquipInfoService(EquipInfoRepository equipInfoRepository)
    {
        _equipInfoRepository = equipInfoRepository;
    }

    /// <summary>
    /// 获取分页列表 🔖
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页列表</returns>
    public async Task<PageResult<EquipInfo>> Page(PageEquipInfoReq input)
    {
        RefAsync<int> total = 0;
        var items = await _equipInfoRepository.EquipInfoQueryPageAsync(
            !string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name),
            !string.IsNullOrEmpty(input.Code), u => u.Code.Contains(input.Code),
            input.Status != null, (u => input.Status != null && u.Status.Equals(input.Status.Value)),
            input.PageNum, input.PageSize, total, input);
        return new PageResult<EquipInfo>()
        {
            PageNum = input.PageNum,
            PageSize = input.PageSize,
            ToTal = total,
            Rows = items,
        };
    }

    /// <summary>
    /// 修改充电模式配置 🔖
    /// </summary>
    /// <param name="input">修改参数</param>
    /// <returns>修改结果</returns>
    [DisplayName("更新充电模式配置")]
    public async Task<Result<object>> UpdateEquipInfoReq(UpdateEquipInfoReq input)
    {
        var isExist = await _equipInfoRepository.QueryByClauseAsync
            (u => (u.Name == input.Name || u.Code == input.Code) && u.Id != input.Id);
        if (isExist != null)
        {
            return Result<object>.Fail(false, "已存在同名或同编码参数配置");
        }

        var config = input.Adapt<EquipInfo>();
        int affectedRows = await _equipInfoRepository.UpdateAsync(config, true);
        // 判断是否更新成功
        if (affectedRows > 0)
        {
            return Result<object>.Success(true, "修改成功");
        }

        return Result<object>.Fail(false, "修改失败");
    }

    /// <summary>
    /// 删除充电模式配置 🔖
    /// </summary>
    /// <param name="input">删除参数</param>
    /// <returns>删除结果</returns>
    [DisplayName("删除充电模式配置")]
    public async Task<Result<object>> DeleteEquipInfoReq(DeleteEquipInfoReq input)
    {
        var config = await _equipInfoRepository.QueryByClauseAsync(u => u.Id == input.Id);
        bool deleteResult = await _equipInfoRepository.DeleteAsync(config);
        if (deleteResult)
            return Result<object>.Success(true, "修改成功");
        return Result<object>.Fail(true, "删除成功");
    }
}