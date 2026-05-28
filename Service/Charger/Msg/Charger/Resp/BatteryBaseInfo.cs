using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Resp;

/// <summary>
/// 3.6.2.4 充放电机上传电池包基本信息（PGN:0x00F882）
/// </summary>
public class BatteryBaseInfo: ASDU
{
    [Property(0, 24)]
    public string Pgn { get; set; }
    
    /// <summary>
    /// 电池包额定量度
    /// </summary>
    [Property(0, 16)]
    public byte RatedMeasurement{get;set;}
    /// <summary>
    /// 电池包额定电压
    /// </summary>
    [Property(16, 16)]
    public byte PackVoltage{get;set;}
    /// <summary>
    /// 电池包额定总能量
    /// </summary>
    [Property(32, 16)]
    public byte RatedBatteryPack{get;set;}
    /// <summary>
    /// 电池冷却方式
    /// </summary>
    [Property(48, 8)]
    public byte BatteryCoolingSystem{get;set;}
    /// <summary>
    /// 电池类型
    /// </summary>
    [Property(56, 8)]
    public byte BatteryType{get;set;}
    /// <summary>
    /// 电池系统中 CSC总的数目（电池监控单元数目）
    /// </summary>
    [Property(64, 8)]
    public byte BatteryCSCSNumber{get;set;}
    /// <summary>
    /// PACK 中单体电芯的总数目
    /// </summary>
    [Property(72, 16)]
    public byte PACKNumber{get;set;} 
    /// <summary>
    /// PACK 中电芯温度点（探针）的总数目
    /// </summary>
    [Property(88, 16)]
    public byte PACKTemperatureNumber{get;set;}
}