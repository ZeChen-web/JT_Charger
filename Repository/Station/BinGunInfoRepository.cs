using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class BinGunInfoRepository: BaseRepository<BinGunInfo>
{
    public BinGunInfoRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binNo"></param>
    /// <returns></returns>
    public BinGunInfo? QueryByBinNo(string binNo)
    {
        return this.QueryByClause(it => it.No == binNo);
    }
}