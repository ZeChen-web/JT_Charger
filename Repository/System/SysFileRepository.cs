using Entity.DbModel.System;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.System
{
    [Scope("SingleInstance")]
    public class SysFileRepository : BaseRepository<SysFile>
    {
        private ISqlSugarClient DbBaseClient;
        public SysFileRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
        {
            DbBaseClient = sqlSugar;
        }

    }
}
