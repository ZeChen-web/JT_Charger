using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///设备类型表
    ///</summary>
    [SugarTable("equip_type")]
    public partial class EquipType : BaseModel
    {
           public EquipType(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:类型编码;0-充电机；1-电表
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="code")]           
           public int? Code {get;set;}

           /// <summary>
           /// Desc:类型名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="name")]           
           public string Name {get;set;}

           /// <summary>
           /// Desc:是否启用;0-禁用；1-启用
           /// Default:1
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="enabled")]           
           public int? Enabled {get;set;}

         

    }
}
