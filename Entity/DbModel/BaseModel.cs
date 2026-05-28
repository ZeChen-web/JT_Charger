using SqlSugar;

namespace Entity.DbModel;
/// <summary>
/// 业务model 基类
/// </summary>
public class BaseModel
{
    /// <summary>
    /// Desc:创建人
    /// Default:
    /// Nullable:True
    /// </summary>
    [SugarColumn(ColumnName = "created_by")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Desc:创建时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:True
    /// </summary>
    /// [SugarColumn(IsPrimaryKey = false, , ColumnDescription = "创建时间")]
    [SugarColumn(ColumnName = "created_time", InsertServerTime = true)]
    public DateTime? CreatedTime { get; set; }


    /// <summary>
    /// Desc:更新人
    /// Default:
    /// Nullable:True
    /// </summary>
    [SugarColumn(ColumnName = "updated_by")]
    public string? UpdatedBy { get; set; }
    
    /// <summary>
    /// Desc:更新时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:True
    /// </summary>
    [SugarColumn(ColumnName = "updated_time", UpdateServerTime = true)]
    public DateTime? UpdatedTime { get; set; } = DateTime.Now;

}