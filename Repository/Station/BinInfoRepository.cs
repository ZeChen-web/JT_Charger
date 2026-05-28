using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class BinInfoRepository : BaseRepository<BinInfo>
{
    public BinInfoRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
    /// <summary>
    /// 禁用仓位
    /// </summary>
    /// <param name="id"></param>
    /// <returns>修改结果</returns>
    public bool UpdateStatus(int id)
    {
        return DbBaseClient
            .Updateable<BinInfo>()
            .SetColumns(bin => new BinInfo { Status = 0 })
            .Where(bin => bin.Id == id)
            .ExecuteCommandHasChange();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binNo"></param>
    /// <returns></returns>
    public BinInfo? QueryByBinNo(string binNo)
    {
        return this.QueryByClause(it => it.No == binNo);
    }
}