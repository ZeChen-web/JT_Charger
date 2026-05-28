using System.ComponentModel.DataAnnotations;
using Common.Enum;
using Entity.Base;
using SqlSugar;

namespace Entity.DbModel.System
{
    /// <summary>
    /// 系统参数配置表
    /// </summary>
    [SugarTable("sys_config")]
    public partial class SysConfig : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(ColumnDescription = "名称", Length = 64, ColumnName = "name")]
        [Required, MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [SugarColumn(ColumnDescription = "编码", Length = 64, ColumnName = "code")]
        [MaxLength(64)]
        public string? Code { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [SugarColumn(ColumnDescription = "属性值", Length = 64, ColumnName = "value")]
        [MaxLength(64)]
        public string? Value { get; set; }

        /// <summary>
        /// 是否是内置参数（Y-是，N-否）
        /// </summary>
        [SugarColumn(ColumnDescription = "是否是内置参数", ColumnName = "sys_flag")]
        public YesNoEnum SysFlag { get; set; }

        /// <summary>
        /// 分组编码
        /// </summary>
        [SugarColumn(ColumnDescription = "分组编码", Length = 64, ColumnName = "group_code")]
        [MaxLength(64)]
        public string? GroupCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnDescription = "排序", ColumnName = "order_no")]
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDescription = "备注", Length = 256, ColumnName = "remark")]
        [MaxLength(256)]
        public string? Remark { get; set; }
    }
}