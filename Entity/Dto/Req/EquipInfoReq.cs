using Entity.DbModel.Station;
using HybirdFrameworkCore.Entity;
namespace Entity.Dto.Req;

public class EquipInfoReq : BaseIdReq
{
}

public class PageEquipInfoReq : QueryPageModel
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 设备编码;0-充电机；1-电表
    /// </summary>
    public string Code { get; set; } = "";

    /// <summary>
    /// 设备状态
    /// </summary>
    public int? Status { get; set; }
}

public class AddEquipInfoReq : EquipInfo
{
}

public class UpdateEquipInfoReq : AddEquipInfoReq
{
}

public class DeleteEquipInfoReq : BaseIdReq
{
}