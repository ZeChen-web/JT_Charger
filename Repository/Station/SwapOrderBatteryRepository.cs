using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope]
public class SwapOrderBatteryRepository:BaseRepository<SwapOrderBattery>
{
  
    public SwapOrderBatteryRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="batterySn"></param>
    /// <returns></returns>
    public SwapOrderBattery? QueryLatestOrderNoByBatterySn(string? batterySn)
    {
        if (string.IsNullOrWhiteSpace(batterySn))
        {
            return null;
        }
        return QueryByClause(it => it.DownBatteryNo == batterySn, it => it.CreatedTime, OrderByType.Desc);
    }
}