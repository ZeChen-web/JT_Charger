using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///换电订单表
    ///</summary>
    [SugarTable("swap_order")]
    public partial class SwapOrder : BaseModel
    {
        public SwapOrder()
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
        /// Desc:车牌号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_no")]
        public string VehicleNo { get; set; }

        /// <summary>
        /// Desc:车辆mac
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_mac")]
        public string VehicleMac { get; set; }

        /// <summary>
        /// Desc:车辆vin码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_vin")]
        public string VehicleVin { get; set; }

        /// <summary>
        /// Desc:车辆进场时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_enter_time")]
        public DateTime? VehicleEnterTime { get; set; }

        /// <summary>
        /// Desc:车辆离场时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_leave_time")]
        public DateTime? VehicleLeaveTime { get; set; }

        /// <summary>
        /// Desc:换电开始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_begin_time")]
        public DateTime? SwapBeginTime { get; set; }

        /// <summary>
        /// Desc:换电结束时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_end_time")]
        public DateTime? SwapEndTime { get; set; }

        /// <summary>
        /// Desc:换电结果;0-未知；1-成功；2-失败
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_result")]
        public int? SwapResult { get; set; }

        /// <summary>
        /// Desc:失败原因
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "fail_reason")]
        public string FailReason { get; set; }

        /// <summary>
        /// Desc:上传云平台状态;0-未上传；1-已上传
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "cloud_report_status")]
        public int? CloudReportStatus { get; set; }

    
        /// <summary>
        /// 云平台订单号
        /// </summary>
        [SugarColumn(ColumnName = "cloud_sn")]
        public string? CloudSn { get; set; }
        /// <summary>
        /// Desc:换电类型:;1自动换电;2手动换电
        /// Default:0
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_way")]
        public int? SwapWay { get; set; }
    }
}