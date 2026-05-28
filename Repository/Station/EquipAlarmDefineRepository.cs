using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope]
public class EquipAlarmDefineRepository : BaseRepository<EquipAlarmDefine>
{
    public EquipAlarmDefineRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="equipTypeCode">0-充电机;1-电表；2-水冷；3-plc；</param>
    /// <param name="equipCode"></param>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public EquipAlarmDefine? SelectByEquipCodeAndErrorCode(int equipTypeCode, string equipCode)
    {
        return this.QueryByClause(it => it.EquipTypeCode == equipTypeCode &&
                                        it.EquipCode == equipCode);
    }
}