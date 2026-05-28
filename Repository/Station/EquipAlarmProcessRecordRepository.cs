using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope]
public class EquipAlarmProcessRecordRepository : BaseRepository<EquipAlarmProcessRecord>
{
    public EquipAlarmProcessRecordRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}