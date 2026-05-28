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
}