using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///电池运营模型详情
    ///</summary>
    [SugarTable("battery_op_model_detail")]
    public partial class BatteryOpModelDetail : BaseModel
    {
           public BatteryOpModelDetail(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:模型Id
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="model_id")]           
           public int? ModelId {get;set;}

           /// <summary>
           /// Desc:开始时间：06:00:00
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="start_time")]           
           public string StartTime {get;set;}

           /// <summary>
           /// Desc:结束时间：06:00:01
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="end_time")]           
           public string EndTime {get;set;}

           /// <summary>
           /// Desc:需要电池数量
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="battery_count")]           
           public int? BatteryCount {get;set;}

           /// <summary>
           /// Desc:需要电池类型
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="battery_type")]           
           public string BatteryType {get;set;}

         
    }
}
