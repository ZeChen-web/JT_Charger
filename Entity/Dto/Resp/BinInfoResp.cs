using HybirdFrameworkCore.Autofac.Attribute;

namespace Entity.Dto.Resp;

public partial class BinInfoResp
{
    public BinInfoResp()
    {
    }

    /// <summary>
    /// Desc:id
    /// Default:
    /// Nullable:False
    /// </summary>           
    public int Id { get; set; }

    /// <summary>
    /// Desc:仓位编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string No { get; set; }

    /// <summary>
    /// Desc:仓位编码
    /// Default:
    /// Nullable:True
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Desc:仓位名称
    /// Default:
    /// Nullable:True
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Desc:电池编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string BatteryNo { get; set; }

    /// <summary>
    /// Desc:充电机编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string ChargerNo { get; set; }

    /// <summary>
    /// Desc:充电枪编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string ChargerGunNo { get; set; }

    /// <summary>
    /// Desc:水冷编号
    /// Default:
    /// Nullable:True
    /// </summary>
    public string WaterCoolNo { get; set; }

    /// <summary>
    /// Desc:是否有电插头;0-无电插头；1-有电插头
    /// Default:
    /// Nullable:True
    /// </summary>
    public int? ElecPluginFlag { get; set; }

    /// <summary>
    /// Desc:电插头状态;0-未知；1-已经连接；2-未连接
    /// Default:
    /// Nullable:True
    /// </summary>
    public string ElecPluginStatus { get; set; }

    /// <summary>
    /// Desc:是否有水插头;0-无水插头；1-有水插头
    /// Default:
    /// Nullable:False
    /// </summary>
    public string WaterPluginFlag { get; set; }

    /// <summary>
    /// Desc:预约锁定;0-未锁定；1-锁定
    /// Default:
    /// Nullable:True
    /// </summary>
    public string AmtLock { get; set; }

    /// <summary>
    /// Desc:soc
    /// Default:
    /// Nullable:True
    /// </summary>
    public decimal? Soc { get; set; }

    /// <summary>
    /// Desc:soe
    /// Default:
    /// Nullable:True
    /// </summary>
    public decimal? Soe { get; set; }

    /// <summary>
    /// Desc:soh
    /// Default:
    /// Nullable:True
    /// </summary>
    public decimal? Soh { get; set; }

    /// <summary>
    /// Desc:电池入仓顺序
    /// Default:
    /// Nullable:True
    /// </summary>
    public int? BatteryEnterSeq { get; set; }

    /// <summary>
    /// Desc:充电状态;0-未知；1-正在充电；2-无电池；3-禁用；4-充电停止
    /// Default:0
    /// Nullable:True
    /// </summary>
    public int? ChargeStatus { get; set; }

    /// <summary>
    /// Desc:仓位状态;0-禁用；1-启用
    /// Default:1
    /// Nullable:True
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Desc:创建人
    /// Default:
    /// Nullable:True
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Desc:创建时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:True
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    /// Desc:更新人
    /// Default:
    /// Nullable:True
    /// </summary>
    public string UpdatedBy { get; set; }

    /// <summary>
    /// Desc:更新时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:True
    /// </summary>
    public DateTime? UpdatedTime { get; set; }

 
    /// <summary>
    /// Desc:功率
    /// Default:
    /// Nullable:9.9
    /// </summary>
    public float power { get; set; }


    /// <summary>
    /// Desc:缓存仓标记 0:不是 1:是
    /// Default:
    /// Nullable:0
    /// </summary>
    public int CacheBinFlag { get; set; }


    /// <summary>
    /// Desc:换电禁用标志 0:不可换电 1:可以换电
    /// Default:
    /// Nullable:0
    /// </summary>
    public int CanSwapFlag { get; set; }

    
    /// <summary>
    /// Desc:充电禁用标志 0:不可充电 1:可以充电
    /// Default:
    /// Nullable:0
    /// </summary>
    public int CanChargeFlag { get; set; }
    
    
    /// <summary>
    /// Desc:充电机是否连接
    /// Default:
    /// Nullable:0
    /// </summary>
    public bool ChargeConnectFlag { get; set; }
    /// <summary>
    /// Desc:本次充电时间
    /// Default:
    /// Nullable:0
    /// </summary>
    public ushort ChargingTime { get; set; }
    /// <summary>
    /// Desc:估算剩余充电时间
    /// Default:
    /// Nullable:0
    /// </summary>
    public ushort EstimatedRemainingTime { get; set; }
    /// <summary>
    /// Desc:单次充电电量
    /// Default:
    /// Nullable:0
    /// </summary>
    public float OnceElectricCharge { get; set; }
    /// <summary>
    /// BMS 需求电压
    /// Default:
    /// Nullable:0
    /// </summary>
    public float BmsNeedVoltage { get; set; }
    /// <summary>
    /// BMS 需求电流
    /// Default:
    /// Nullable:0
    /// </summary>
    public float BmsNeedCurrent { get; set; }
    /// <summary>
    /// 电池包总电流，充电为负值，放电为正
    /// Default:
    /// Nullable:0
    /// </summary>
    public float TotalCurrent { get; set; }
    /// <summary>
    /// 电芯温度最大值
    /// Default:
    /// Nullable:0
    /// </summary>
    public Int16 CellTemperatureMax { get; set; }
    /// <summary>
    /// 电芯温度最小值
    /// Default:
    /// Nullable:0
    /// </summary>
    public Int16 CellTemperatureMin { get; set; }
    /// <summary>
    /// 充电开始时间
    /// Default:
    /// Nullable:
    /// </summary>
    public DateTime? ChargingStartTime { get; set; }
    /// <summary>
    /// 充电结束时间
    /// Default:
    /// Nullable:
    /// </summary>
    public DateTime? ChargingStopTime { get; set; }
    /// <summary>
    /// 是否鉴权
    /// Default:
    /// Nullable:
    /// </summary>
    public bool IsAuthed { get; set; } = false;
    /// <summary>
    /// 在位状态：0-不在位；1-在位；其他-无效
    /// Default:
    /// Nullable:
    /// </summary>
    public int Exists { get; set; }
    /// <summary>
    /// 充电接口温度探头 1
    /// </summary>
    public Int16 ChargingInterfaceDetectionOneTemp { get; set; }
    /// <summary>
    /// 充电接口温度探头 2
    /// </summary>
    public Int16 ChargingInterfaceDetectionTwoTemp { get; set; }
    /// <summary>
    /// 充电接口温度探头 3
    /// </summary>
    public Int16 ChargingInterfaceDetectionTheTemp { get; set; }
    /// <summary>
    /// 充电接口温度探头 4
    /// </summary>
    public Int16 ChargingInterfaceDetectionFourTemp { get; set; }
}