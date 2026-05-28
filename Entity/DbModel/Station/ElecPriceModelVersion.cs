using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///电价模型板板;电价模型版本表，生失效时间左开右闭且不能重叠
    ///</summary>
    [SugarTable("elec_price_model_version")]
    public partial class ElecPriceModelVersion : BaseModel
    {
           public ElecPriceModelVersion(){


           }
           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:版本号;版本号，唯一
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="version")]           
           public int Version {get;set;}

           /// <summary>
           /// Desc:生效时间;生效时间（左开右闭）
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="start_time")]           
           public DateTime? StartTime {get;set;}

           /// <summary>
           /// Desc:失效时间;失效时间（左开右闭）
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="end_time")]           
           public DateTime? EndTime {get;set;}

        

    }
}
