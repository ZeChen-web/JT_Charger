using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///充电订单;充电订单表
    ///</summary>
    [SugarTable("charge_order")]
    public partial class ChargeOrder : BaseModel
    {
        public ChargeOrder()
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
        /// Desc:订单编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "sn")]
        public string Sn { get; set; }

        /// <summary>
        /// Desc:电池编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "battery_no")]
        public string BatteryNo { get; set; }

        /// <summary>
        /// Desc:启动报文状态;0-初始化；1-启动成功；2-启动失败
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "cmd_status")]
        public int? CmdStatus { get; set; }

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
        /// Desc:站外充电枪编号,1枪或2枪
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "out_charger_gun_no")]
        public string OutChargerGunNo { get; set; }

        /// <summary>
        /// Desc:0：站内充电 1：站外充电
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "charge_mode")]
        public int? ChargeMode { get; set; }

        /// <summary>
        /// Desc:1：站控启动 2：本地启动
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_mode")]
        public int? StartMode { get; set; }

        /// <summary>
        /// Desc:充电开始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Desc:充电结束时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "end_time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Desc:充电开始soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_soc")]
        public int? StartSoc { get; set; }

        /// <summary>
        /// Desc:充电结束soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "stop_soc")]
        public int? StopSoc { get; set; }

        /// <summary>
        /// Desc:充电时长
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "charge_time_count")]
        public int? ChargeTimeCount { get; set; }

        /// <summary>
        /// Desc:充电电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "elec_count")]
        public decimal? ElecCount { get; set; }

        /// <summary>
        /// Desc:交流电表量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ac_elec_count")]
        public decimal? AcElecCount { get; set; }

        /// <summary>
        /// Desc:充电开始交流表电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_ac_elec")]
        public decimal? StartAcElec { get; set; }

        /// <summary>
        /// Desc:充电结束交流表电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "stop_ac_elec")]
        public decimal? StopAcElec { get; set; }

        /// <summary>
        /// Desc:充电开始直流表电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_dc_elec")]
        public decimal? StartDcElec { get; set; }

        /// <summary>
        /// Desc:充电结束直流表电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "stop_dc_elec")]
        public decimal? StopDcElec { get; set; }

        /// <summary>
        /// Desc:停止原因;0：满电自动停止；1-人工停止
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "stop_reason")]
        public int? StopReason { get; set; }

        /// <summary>
        /// Desc:尖时段电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "sharp_elec_count")]
        public decimal? SharpElecCount { get; set; }

        /// <summary>
        /// Desc:峰时段电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "peak_elec_count")]
        public decimal? PeakElecCount { get; set; }

        /// <summary>
        /// Desc:平时段电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "flat_elec_count")]
        public decimal? FlatElecCount { get; set; }

        /// <summary>
        /// Desc:谷时段电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "valley_elec_count")]
        public decimal? ValleyElecCount { get; set; }

        /// <summary>
        /// Desc:尖时段交流电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ac_sharp_elec_count")]
        public decimal? AcSharpElecCount { get; set; }

        /// <summary>
        /// Desc:峰时段交流电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ac_peak_elec_count")]
        public decimal? AcPeakElecCount { get; set; }

        /// <summary>
        /// Desc:平时段交流电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ac_flat_elec_count")]
        public decimal? AcFlatElecCount { get; set; }

        /// <summary>
        /// Desc:谷时段交流电量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ac_valley_elec_count")]
        public decimal? AcValleyElecCount { get; set; }

        /// <summary>
        /// Desc:电价版本号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "elec_price_model_version")]
        public string ElecPriceModelVersion { get; set; }

        /// <summary>
        /// Desc:换电订单编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_order_sn")]
        public string SwapOrderSn { get; set; }

        /// <summary>
        /// Desc:上传云平台状态;0-未上传；1-已上传
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "cloud_report_status")]
        public int CloudReportStatus { get; set; }


        /// <summary>
        /// Desc:云平台订单编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "cloud_charge_order")]
        public string CloudChargeOrder { get; set; }

        /// <summary>
        /// Desc:是否可以上传云平台
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "can_upload")]
        public int CanUpload { get; set; }

        /// <summary>
        /// 启动方式：0-站控自动启动，1-站控手动启动，2-充电机启动
        /// </summary>
        [SugarColumn(ColumnName = "start_type")]
        public int StartType { get; set; }
        
        
        /// <summary>
        /// 上报次数
        /// </summary>
        [SugarColumn(ColumnName = "reporting_times")]
        public int ReportingTimes { get; set; }

        /// <summary>
        /// 站内外订单标识 0站内1站外
        /// </summary>
        [SugarColumn(ColumnName = "sign")]
        public int Sign { get; set; } = 0;


    }
}
