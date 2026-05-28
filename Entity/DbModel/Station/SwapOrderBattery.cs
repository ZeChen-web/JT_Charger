using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///换电订单电池
    ///</summary>
    [SugarTable("swap_order_battery")]
    public partial class SwapOrderBattery : BaseModel
    {

        /// <summary>
        /// Desc:id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:换电订单编号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "swap_order_sn")]
        public string? SwapOrderSn { get; set; }

        /// <summary>
        /// Desc:亏电包编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_no")]
        public string? DownBatteryNo { get; set; }

        /// <summary>
        /// Desc:亏电包soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_soc")]
        public decimal? DownBatterySoc { get; set; }

        /// <summary>
        /// Desc:亏电包soe
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_soe")]
        public decimal? DownBatterySoe { get; set; }

        /// <summary>
        /// Desc:亏电包真实soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_real_soc")]
        public decimal? DownBatteryRealSoc { get; set; }

        /// <summary>
        /// Desc:亏电包上次换电结算时soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_last_soc")]
        public decimal? DownBatteryLastSoc { get; set; }

        /// <summary>
        /// Desc:亏电包上次换电结算时soe
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_last_soe")]
        public decimal? DownBatteryLastSoe { get; set; }

        /// <summary>
        /// Desc:亏电包站内充电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_in_chage_elec_count")]
        public decimal? DownBatteryInChageElecCount { get; set; }

        /// <summary>
        /// Desc:亏电包站外插枪充电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_out_chage_elec_count")]
        public decimal? DownBatteryOutChageElecCount { get; set; }

        /// <summary>
        /// Desc:亏电包站外回充能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_out_re_chagre_count")]
        public decimal? DownBatteryOutReChagreCount { get; set; }

        /// <summary>
        /// Desc:亏电包站外放电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_in_dischage_elec_count")]
        public decimal? DownBatteryInDischageElecCount { get; set; }

        /// <summary>
        /// Desc:亏电包站内放电电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_out_dischage_elec_count")]
        public decimal? DownBatteryOutDischageElecCount { get; set; }

        /// <summary>
        /// Desc:放电池仓位号 亏电包仓号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "down_battery_bin_no")]
        public int? DownBatteryBinNo { get; set; }

        /// <summary>
        /// Desc:满电包编码
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_no")]
        public string? UpBatteryNo { get; set; }

        /// <summary>
        /// Desc:满电包soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_soc")]
        public decimal? UpBatterySoc { get; set; }

        /// <summary>
        /// Desc:满电包soe
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_soe")]
        public decimal? UpBatterySoe { get; set; }

        /// <summary>
        /// Desc:满电包真实soc
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_real_soc")]
        public decimal? UpBatteryRealSoc { get; set; }

        /// <summary>
        /// Desc:满电包站内充电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_in_chage_elec_count")]
        public decimal? UpBatteryInChageElecCount { get; set; }

        /// <summary>
        /// Desc:满电包站外插枪充电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_out_chage_elec_count")]
        public decimal? UpBatteryOutChageElecCount { get; set; }

        /// <summary>
        /// Desc:满电包站外回充能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_out_re_chagre_count")]
        public decimal? UpBatteryOutReChagreCount { get; set; }

        /// <summary>
        /// Desc:满电包站外放电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_in_dischage_elec_count")]
        public decimal? UpBatteryInDischageElecCount { get; set; }

        /// <summary>
        /// Desc:满电包站内放电电能量（累计）
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_out_dischage_elec_count")]
        public decimal? UpBatteryOutDischageElecCount { get; set; }

        /// <summary>
        /// Desc:取电池仓位号 满电包仓号
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "up_battery_bin_no")]
        public int? UpBatteryBinNo { get; set; }

     
    }
}
