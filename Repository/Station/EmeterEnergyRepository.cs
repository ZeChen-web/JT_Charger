using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;
using Entity.DbModel.System;


namespace Repository.Station;

[Scope("SingleInstance")]
public class EmeterEnergyRepository : BaseRepository<EmeterEnergy>
{
    private ISqlSugarClient DbBaseClient;
    public EmeterEnergyRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
        DbBaseClient = sqlSugar;
    }
}