using System.Linq.Expressions;
using Entity.DbModel.System;
using Entity.Dto.Req;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.System
{
    [Scope("SingleInstance")]
    public class SysConfigRepository : BaseRepository<SysConfig>
    {
        private ISqlSugarClient DbBaseClient;
        public SysConfigRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
        {
            DbBaseClient = sqlSugar;
        }

        public async Task<List<SysConfig>> SysConfigQueryPageAsync(
            bool isWhere1, Expression<Func<SysConfig, bool>> expression1,
            bool isWhere2, Expression<Func<SysConfig, bool>> expression2,
            bool isWhere3, Expression<Func<SysConfig, bool>> expression3,
            int pageNumber, int pageSize, RefAsync<int> totalNumber,
            PageConfigReq input, bool blUseNoLock = false)
        {
            var page = await DbBaseClient
                .Queryable<SysConfig>()
                .WhereIF(isWhere1, expression1)
                .WhereIF(isWhere2, expression2)
                .WhereIF(isWhere3, expression3)
                .OrderBuilder(input)
                .WithNoLockOrNot(blUseNoLock)
                .ToPageListAsync(pageNumber, pageSize, totalNumber);
            return page;
        }
    }
}