using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class SwapOrderRepository : BaseRepository<SwapOrder>
{
    public SwapOrderRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
}