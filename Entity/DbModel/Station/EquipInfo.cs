using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///设备信息表
    ///</summary>
    [SugarTable("equip_info")]
    public partial class EquipInfo : BaseModel
    {
           public EquipInfo(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true,ColumnName="id")]
           public int Id {get;set;}

           /// <summary>
           /// Desc:设备编码
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="code")]           
           public string Code {get;set;}

           /// <summary>
           /// Desc:设备名称
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="name")]           
           public string Name {get;set;}

           /// <summary>
           /// Desc:设备类型编码
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="type_code")]           
           public int? TypeCode {get;set;}

           /// <summary>
           /// Desc:设备状态;0-未知；1-正常；2-报警；3-停用
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="status")]           
           public int? Status {get;set;}

          
           
           /// <summary>
           /// Desc:0-手动;1-自动充电
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="auto_charge")]           
           public int? AutoCharge {get;set;}
           
           /// <summary>
           /// 充电功率
           /// </summary>
           [SugarColumn(ColumnName="charge_power")]      
           public float? ChargePower { get; set; }
    }
}
