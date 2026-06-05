using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope]
public class SwapOrderBatteryRepository : BaseRepository<SwapOrderBattery>
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


    private string _strSql(string downBatteryNo, string startTime)
    {
        return @$"
SELECT * FROM swap_order_battery 
WHERE down_battery_no = '{downBatteryNo}'
  AND (
    id = (
        SELECT id 
        FROM swap_order_battery 
        WHERE down_battery_no = '{downBatteryNo}'
          AND updated_time > '{startTime}'
          AND TIMESTAMPDIFF(SECOND, '{startTime}', updated_time) <= 180
        ORDER BY updated_time
        LIMIT 1
    )
    OR
    id = (
        SELECT id 
        FROM swap_order_battery 
        WHERE down_battery_no = '{downBatteryNo}'
          AND updated_time <= '{startTime}'
          AND NOT EXISTS (
              SELECT 1 
              FROM swap_order_battery t2 
              WHERE t2.down_battery_no = '{downBatteryNo}'
                AND t2.updated_time > '{startTime}'
                AND TIMESTAMPDIFF(SECOND, '{startTime}', t2.updated_time) <= 180
          )
        ORDER BY updated_time DESC
        LIMIT 1
    )
  )
LIMIT 1;";
    }

    public SwapOrderBattery GetOrderBattery(string downBatteryNo, string startTime)
    {
        var sqlQueryable = this.SqlQueryable2(_strSql(downBatteryNo, startTime));
        if (sqlQueryable != null)
        {
            if (sqlQueryable.Count > 0)
            {
                SwapOrderBattery pageList = sqlQueryable?.First();
                return pageList;
            }
        }

        return null;
    }
}