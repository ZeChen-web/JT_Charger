using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class BatteryOpModelRepository : BaseRepository<BatteryOpModel>
{
    public BatteryOpModelRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}