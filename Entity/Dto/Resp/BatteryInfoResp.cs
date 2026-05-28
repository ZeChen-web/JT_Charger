namespace Entity.Dto.Resp;

public class BatteryInfoResp
{
    /// <summary>
    /// 仓位编码
    /// </summary>
    public string BinCode { get; set; }
    
    /// <summary>
    /// 仓位编码
    /// </summary>
    public string BinNo { get; set; }

    /// <summary>
    /// 仓位名称
    /// </summary>
    public string BinName { get; set; }

    /// <summary>
    /// 充电机编号
    /// </summary>
    public string ChargerNo { get; set; }


    public string? BatteryNo { get; set; }

    /// <summary>
    /// 遥测数据
    /// </summary>
    public UploadTelemetryDataResp UploadTelemetryDataResp { get; set; }

    /// <summary>
    /// 电池包基本信息
    /// </summary>
    public BatteryBaseInfoResp BatteryBaseInfoResp { get; set; }

    /// <summary>
    /// 电池包基本参数2
    /// </summary>
    public BasicParameterResp BasicParameterResp { get; set; }

    /// <summary>
    /// 电池包基本状态
    /// </summary>
    public UpBmsResp UpBmsResp { get; set; }

    /// <summary>
    /// 电池包报警状态
    /// </summary>
    public UpAlarmResp UpAlarmResp { get; set; }

    /// <summary>
    /// 电池包编码和SOC数据
    /// </summary>
    public VoltageCurrentSocResp VoltageCurrentSocResp { get; set; }

    /// <summary>
    /// 电压温度极值统计
    /// </summary>
    public VoltageExtremumStatisticsResp VoltageExtremumStatisticsResp { get; set; }
}