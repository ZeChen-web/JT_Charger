using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;
[Scope("SingleInstance")]
public class EquipTypeRepository:BaseRepository<EquipType>
{
    public EquipTypeRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}