using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;
[Scope("SingleInstance")]
public class BatteryInfoRepository:BaseRepository<BatteryInfo>
{
    public BatteryInfoRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}