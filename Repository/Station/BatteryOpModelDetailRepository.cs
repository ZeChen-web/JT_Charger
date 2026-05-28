using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class BatteryOpModelDetailRepository : BaseRepository<BatteryOpModelDetail>
{
    public BatteryOpModelDetailRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}