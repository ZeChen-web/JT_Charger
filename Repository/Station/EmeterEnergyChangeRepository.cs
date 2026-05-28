using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;
using Entity.DbModel.System;

namespace Repository.Station;

[Scope("SingleInstance")]
public class EmeterEnergyChangeRepository : BaseRepository<EmeterEnergyChange>
{
    private ISqlSugarClient DbBaseClient;
    public EmeterEnergyChangeRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
        DbBaseClient = sqlSugar;
    }
}