using SqlSugar;

namespace Entity.DbModel.Station;

///<summary>
///仓位信息表
///</summary>
[SugarTable("bin_gun_info")]
public partial class BinGunInfo : BaseModel
{

    /// <summary>
    /// 
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
    public int Id { get; set; }

    /// <summary>
    /// 仓位编号
    /// </summary>
    [SugarColumn(ColumnName = "no")]
    public string No { get; set; }

    /// <summary>
    /// 仓位编码
    /// </summary>
    [SugarColumn(ColumnName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// 仓位名称
    /// </summary>
    [SugarColumn(ColumnName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// 充电机编号
    /// </summary>
    [SugarColumn(ColumnName = "charger_no")]
    public string ChargerNo { get; set; }

    /// <summary>
    /// 充电枪编号
    /// </summary>
    [SugarColumn(ColumnName = "charger_gun_no")]
    public string ChargerGunNo { get; set; }
    
    /// <summary>
    /// 1枪2枪
    /// </summary>
    [SugarColumn(ColumnName = "gun_no")]
    public int GunNo { get; set; }

    /// <summary>
    /// 充电状态;0-未知；1-正在充电；2-无电池；3-禁用 4-充電完成
    /// </summary>
    [SugarColumn(ColumnName = "charge_status")]
    public int ChargeStatus { get; set; }

    /// <summary>
    /// 仓位状态;0-禁用；1-启用
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int Status { get; set; }
    
    /// <summary>
    /// soc
    /// </summary>
    [SugarColumn(ColumnName = "soc")]
    public decimal Soc { get; set; }

    /// <summary>
    /// 最后结束充电时间 结束充电后更新
    /// </summary>
    [SugarColumn(ColumnName = "last_charge_finish_time")]
    public DateTime LastChargeFinishTime { get; set; }

    /// <summary>
    /// 0：不能 1：能
    /// </summary>
    [SugarColumn(ColumnName = "can_charge_flag")]
    public int CanChargeFlag { get; set; }

    /// <summary>
    /// 充电功率
    /// </summary>
    [SugarColumn(ColumnName = "power")]
    public float Power { get; set; }
}