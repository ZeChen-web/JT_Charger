using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///仓位信息表
    ///</summary>
    [SugarTable("bin_info")]
    public partial class BinInfo : BaseModel
    {
        public BinInfo()
        {
        }

        /// <summary>
        /// Desc:id
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:仓位编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "no")]
        public string No { get; set; }

        /// <summary>
        /// Desc:仓位编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Desc:仓位名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 在位状态：0-不在位；1-在位；其他-无效
        /// </summary>
        [SugarColumn(ColumnName = "exists")]
        public int Exists { get; set; }

        /// <summary>
        /// Desc:电池编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "battery_no")]
        public string BatteryNo { get; set; }

        /// <summary>
        /// Desc:充电机编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "charger_no")]
        public string ChargerNo { get; set; }

        /// <summary>
        /// Desc:充电枪编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "charger_gun_no")]
        public string ChargerGunNo { get; set; }

        /// <summary>
        /// Desc:水冷编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "water_cool_no")]
        public string WaterCoolNo { get; set; }

        /// <summary>
        /// Desc:是否有电插头;0-无电插头；1-有电插头
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "elec_plugin_flag")]
        public int? ElecPluginFlag { get; set; }

        /// <summary>
        /// Desc:电插头状态;0-未知；1-已经连接；2-未连接
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "elec_plugin_status")]
        public string ElecPluginStatus { get; set; }
        
        /// <summary>
        /// Desc:最后结束充电时间 结束充电后更新
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "last_charge_finish_time")]
        public DateTime? LastChargeFinishTime { get; set; }

        /// <summary>
        /// Desc:是否有水插头;0-无水插头；1-有水插头
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "water_plugin_flag")]
        public string WaterPluginFlag { get; set; }

        /// <summary>
        /// Desc:预约锁定;0-未锁定；1-锁定
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "amt_lock")]
        public int? AmtLock { get; set; }

        /// <summary>
        /// Desc:soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "soc")]
        public decimal? Soc { get; set; }

        /// <summary>
        /// Desc:soe
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "soe")]
        public decimal? Soe { get; set; }

        /// <summary>
        /// Desc:soh
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "soh")]
        public decimal? Soh { get; set; }

        /// <summary>
        /// Desc:电池入仓顺序
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "battery_enter_seq")]
        public int? BatteryEnterSeq { get; set; }

        /// <summary>
        /// Desc:充电状态;0-未知；1-正在充电；2-无电池；3-禁用/故障 4-停止充电
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "charge_status")]
        public int? ChargeStatus { get; set; }

        /// <summary>
        /// Desc:仓位状态;0-禁用；1-启用
        /// Default:1
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public int? Status { get; set; }


        /// <summary>
        /// 缓存仓标记 0:不是 1:是
        /// </summary>
        [SugarColumn(ColumnName = "cache_bin_flag")]
        public int CacheBinFlag { get; set; }


        /// <summary>
        /// 换电禁用标志 0:不可换电 1:可以换电
        /// </summary>
        [SugarColumn(ColumnName = "can_swap_flag")]
        public int CanSwapFlag { get; set; }


        /// <summary>
        /// 充电禁用标志 0:不可充电 1:可以充电
        /// </summary>
        [SugarColumn(ColumnName = "can_charge_flag")]
        public int CanChargeFlag { get; set; }

        /// <summary>
        /// 电池入仓时间
        /// </summary>
        [SugarColumn(ColumnName = "in_time")]
        public DateTime? InTime { get; set; }
    }
}