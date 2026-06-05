using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///电价模型详情
    ///</summary>
    [SugarTable("elec_price_model_version_detail")]
    public partial class ElecPriceModelVersionDetail : BaseModel
    {
        public ElecPriceModelVersionDetail()
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
        /// Desc:版本号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "version")]
        public int Version { get; set; }

        /// <summary>
        /// Desc:开始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_hour")]
        public int StartHour { get; set; }

        /// <summary>
        /// Desc:开始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "start_minute")]
        public int StartMinute { get; set; }

        /// <summary>
        /// Desc:结束时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "end_hour")]
        public int EndHour { get; set; }

        /// <summary>
        /// Desc:结束时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "end_minute")]
        public int EndMinute { get; set; }

        /// <summary>
        /// Desc:价格;以分为单位存储
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "price")]
        public int Price { get; set; }
        /// <summary>
        /// Desc:服务费;以分为单位存储
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "price_serice")]
        public int PriceSerice { get; set; }

        /// <summary>
        /// Desc:尖峰平谷类型;1-尖；2-峰；3-平；4-谷
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "type")]
        public int Type { get; set; }

        /// <summary>
        /// Desc:所需电池数量
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "battery_count")]
        public int BatteryCount { get; set; }

      
    }
}