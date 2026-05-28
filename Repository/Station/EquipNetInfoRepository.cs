using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;
[Scope("SingleInstance")]
public class EquipNetInfoRepository:BaseRepository<EquipNetInfo>
{
    public EquipNetInfoRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}