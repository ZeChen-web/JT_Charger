using SqlSugar;

namespace Entity.DbModel.Station;

///<summary>
    ///设备报警处理记录
    ///</summary>
    [SugarTable("equip_alarm_process_record")]
    public partial class EquipAlarmProcessRecord : BaseModel
    {
        public EquipAlarmProcessRecord()
        {
        }

        /// <summary>
        /// Desc:id
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:设备类型编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "equip_type_code")]
        public int EquipTypeCode { get; set; }

        /// <summary>
        /// Desc:设备编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "equip_code")]
        public string EquipCode { get; set; }

        /// <summary>
        /// Desc:报警编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Desc:报警等级
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "error_level")]
        public string ErrorLevel { get; set; }

        /// <summary>
        /// Desc:报警描述
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Desc:处理方法
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "process_method")]
        public string ProcessMethod { get; set; }
        /// <summary>
        /// Desc:开始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_time")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Desc:处理时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "process_time")]
        public DateTime? ProcessTime { get; set; }

 
    }