using System.ComponentModel.DataAnnotations;

namespace Entity.Dto.Req
{
    /// <summary>
    /// 主键Id输入参数
    /// </summary>
    public class BaseIdReq
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        //[DataValidation(ValidationTypes.Numeric)]
        public virtual long Id { get; set; }
    }
}
