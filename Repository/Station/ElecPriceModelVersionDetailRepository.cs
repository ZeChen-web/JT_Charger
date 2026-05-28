using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

/// <summary>
/// 
/// </summary>
[Scope("SingleInstance")]
public class ElecPriceModelVersionDetailRepository : BaseRepository<ElecPriceModelVersionDetail>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlSugar"></param>
    public ElecPriceModelVersionDetailRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}