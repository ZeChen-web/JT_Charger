namespace Entity.Dto.Resp;

/// <summary>
/// 充电桩信息
/// </summary>
public class ChargerPileResp
{
    public ChargerPileResp()
    {
    }
    
    public string Sn { get; set; }
    /// <summary>
    /// 站外1枪是否充电
    /// </summary>
    public bool GunChargedOne { get; set; }
    /// <summary>
    /// 站外2枪是否充电
    /// </summary>
    public bool GunChargedTwo { get; set; }
    /// <summary>
    /// 站外1枪是否连接
    /// </summary>
    public bool ChargedPileOne { get; set; }
    /// <summary>
    /// 站外2枪是否连接
    /// </summary>
    public bool ChargedPileTwo { get; set; }
    /// <summary>
    /// 站外1枪充电功率
    /// </summary>
    public float? ChargePilePowerOne { get; set; }
    /// <summary>
    /// 站外2枪充电功率
    /// </summary>
    public float? ChargePilePowerTwo { get; set; }

}