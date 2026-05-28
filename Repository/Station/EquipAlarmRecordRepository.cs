using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope]
public class EquipAlarmRecordRepository : BaseRepository<EquipAlarmRecord>
{
    public EquipAlarmRecordRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}