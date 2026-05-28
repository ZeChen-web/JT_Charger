using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity.DbModel.Station
{
    ///<summary>
    ///设备通信信息表
    ///</summary>
    [SugarTable("equip_net_info")]
    public partial class EquipNetInfo : BaseModel
    {
           public EquipNetInfo(){


           }
           /// <summary>
           /// Desc:设备id
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
           /// Desc:连接地址
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="net_addr")]           
           public string NetAddr {get;set;}

           /// <summary>
           /// Desc:连接端口
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="net_port")]           
           public string NetPort {get;set;}

           /// <summary>
           /// Desc:目的地址;（十六进制，如0a,02,03,04）
           /// Default:
           /// Nullable:True
           /// </summary>
           [SugarColumn(ColumnName="dest_addr")]           
           public string DestAddr {get;set;}


    }
}
