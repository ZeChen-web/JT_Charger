using SqlSugar;

namespace Entity.DbModel.Station;

/// <summary>
/// 交流电表实时插入数据
/// </summary>
[SugarTable("emeter_energy_change")]
public class EmeterEnergyChange
{
    /// <summary>
    /// 主键ID 与EmeterEnergy id 相同
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public virtual string Id { get; set; }
    /// <summary>
    /// 充电机电表编码 (充电机Sn)
    /// </summary>
    [SugarColumn(ColumnName = "code")]
    public virtual string Code { get; set; }
    /// <summary>
    /// 电表当前读数
    /// </summary>
    [SugarColumn(ColumnName = "value")]
    public virtual float Value { get; set; }
    /// <summary>
    /// 充电机上报时间
    /// </summary>
    [SugarColumn(ColumnName = "upload_time")]
    public virtual DateTime UploadTime { get; set; }
    /// <summary>
    /// 上报标识：0未上传 1上传
    /// </summary>
    [SugarColumn(ColumnName = "upload_flag")]
    public virtual int UploadFlag { get; set; }
    
}