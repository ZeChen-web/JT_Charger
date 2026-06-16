using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

/// <summary>
/// 
/// </summary>
[Scope("SingleInstance")]
public class ElecPriceModelVersionRepository : BaseRepository<ElecPriceModelVersion>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlSugar"></param>
    public ElecPriceModelVersionRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
    
    
    /// <summary>查询当前时间处于有效期内的计费模型版本（左开右闭）</summary>
    public ElecPriceModelVersion? GetActiveVersion()
    {
        var now = DateTime.Now;
        var allVersions = this.Query();
        if (allVersions == null || allVersions.Count == 0)
            return null;

        return allVersions
            .Where(v => (!v.StartTime.HasValue || now > v.StartTime.Value)
                        && (!v.EndTime.HasValue || now <= v.EndTime.Value))
            .OrderByDescending(v => v.Version)
            .FirstOrDefault();
    }
}