namespace Entity.Dto.Resp;

/// <summary>
/// 3.6.2.4 充放电机上传电池包基本信息（PGN:0x00F882）
/// </summary>
public class BatteryBaseInfoResp
{
    public string Pgn { get; set; }
    
    /// <summary>
    /// 电池包额定量度
    /// </summary>
    public byte RatedMeasurement{get;set;}
    /// <summary>
    /// 电池包额定电压
    /// </summary>
    public byte PackVoltage{get;set;}
    /// <summary>
    /// 电池包额定总能量
    /// </summary>
    public byte RatedBatteryPack{get;set;}
    /// <summary>
    /// 电池冷却方式
    /// </summary>
    public byte BatteryCoolingSystem{get;set;}
    /// <summary>
    /// 电池类型
    /// </summary>
    public byte BatteryType{get;set;}
    /// <summary>
    /// 电池系统中 CSC总的数目（电池监控单元数目）
    /// </summary>
    public byte BatteryCSCSNumber{get;set;}
    /// <summary>
    /// PACK 中单体电芯的总数目
    /// </summary>
    public byte PACKNumber{get;set;} 
    /// <summary>
    /// PACK 中电芯温度点（探针）的总数目
    /// </summary>
    public byte PACKTemperatureNumber{get;set;}
}