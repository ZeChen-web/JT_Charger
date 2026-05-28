using SqlSugar;

namespace Entity.Base
{
    /// <summary>
    /// 框架实体基类Id
    /// </summary>
    public abstract class EntityBaseId
    {
        /// <summary>
        /// 雪花Id
        /// </summary>
        [SugarColumn(ColumnName = "id", ColumnDescription = "主键Id", IsPrimaryKey = true, IsIdentity = false)]
        public virtual long Id { get; set; }
    }
    /// <summary>
    /// 框架实体基类
    /// </summary>
    [SugarIndex("index_{table}_CT", nameof(CreateTime), OrderByType.Asc)]
    public abstract class EntityBase : EntityBaseId, IDeletedFilter
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnDescription = "创建时间", IsOnlyIgnoreUpdate = true, ColumnName = "create_time")]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnDescription = "更新时间", ColumnName = "update_time")]
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者Id", IsOnlyIgnoreUpdate = true, ColumnName = "create_user_id")]
        public virtual long? CreateUserId { get; set; }



        /// <summary>
        /// 创建者姓名
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者姓名", Length = 64, IsOnlyIgnoreUpdate = true, ColumnName = "create_user_name")]
        public virtual string? CreateUserName { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        [SugarColumn(ColumnDescription = "修改者Id", ColumnName = "update_user_id")]
        public virtual long? UpdateUserId { get; set; }


        /// <summary>
        /// 修改者姓名
        /// </summary>
        [SugarColumn(ColumnDescription = "修改者姓名", Length = 64, ColumnName = "update_user_name")]
        public virtual string? UpdateUserName { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        [SugarColumn(ColumnDescription = "软删除", ColumnName = "is_delete")]
        public virtual bool IsDelete { get; set; } = false;
    }


    /// <summary>
    /// 业务数据实体基类（数据权限）
    /// </summary>
    public abstract class EntityBaseData : EntityBase, IOrgIdFilter
    {
        /// <summary>
        /// 创建者部门Id
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者部门Id", IsOnlyIgnoreUpdate = true, ColumnName = "creat_org_id")]
        public virtual long? CreateOrgId { get; set; }


        /// <summary>
        /// 创建者部门名称
        /// </summary>
        [SugarColumn(ColumnDescription = "创建者部门名称", Length = 64, IsOnlyIgnoreUpdate = true, ColumnName = "creat_org_name")]
        public virtual string? CreateOrgName { get; set; }
    }
}
