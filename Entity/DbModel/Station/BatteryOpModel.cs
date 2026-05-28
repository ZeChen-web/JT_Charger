using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///电池运营模型
    ///</summary>
    [SugarTable("battery_op_model")]
    public partial class BatteryOpModel : BaseModel
    {
           public BatteryOpModel(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:模型id
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="model_id")]           
           public int? ModelId {get;set;}

         
    }
}
