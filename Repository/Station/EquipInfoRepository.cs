using System.Linq.Expressions;
using Entity.DbModel.Station;
using Entity.Dto.Req;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;
[Scope("SingleInstance")]
public class EquipInfoRepository: BaseRepository<EquipInfo>
{
    private ISqlSugarClient DbBaseClient;

    public EquipInfoRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
        DbBaseClient = sqlSugar;
    }
    
    public async Task<List<EquipInfo>> EquipInfoQueryPageAsync(
        bool isWhere1, Expression<Func<EquipInfo, bool>> expression1,
        bool isWhere2, Expression<Func<EquipInfo, bool>> expression2,
        bool isWhere3, Expression<Func<EquipInfo, bool>> expression3,
        int pageNumber, int pageSize, RefAsync<int> totalNumber,
        PageEquipInfoReq input, bool blUseNoLock = false)
    {
        var page = await DbBaseClient
            .Queryable<EquipInfo>()
            .WhereIF(isWhere1, expression1)
            .WhereIF(isWhere2, expression2)
            .WhereIF(isWhere3, expression3)
            .OrderBuilder(input, "", "Id", false)
            .WithNoLockOrNot(blUseNoLock)
            .ToPageListAsync(pageNumber, pageSize, totalNumber);
        
        
        return page;
    }

    public float? QueryPowerByCode(string code)
    {
        EquipInfo? queryByClause = QueryByClause(i => i.Code == code);
        if (queryByClause==null || queryByClause.ChargePower == null)
        {
            return null;
        }

        return queryByClause.ChargePower;

    }
}