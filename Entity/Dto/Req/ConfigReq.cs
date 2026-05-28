using Entity.DbModel.System;
using HybirdFrameworkCore.Entity;
namespace Entity.Dto.Req
{
    public class ConfigReq : BaseIdReq
    {
    }

    public class PageConfigReq : QueryPageModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = "";

        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; } = "";
    }

    public class AddConfigReq : SysConfig
    {
    }

    public class UpdateConfigReq : AddConfigReq
    {
    }

    public class DeleteConfigReq : BaseIdReq
    {
    }
}
