namespace Entity.Dto.Resp;

///<summary>
///仓位信息表
///</summary>
public partial class BinGunInfoResp 
{

    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 仓位编号
    /// </summary>
    public int No { get; set; }

    /// <summary>
    /// 仓位编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 仓位名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 充电机编号
    /// </summary>
    public string ChargerNo { get; set; }

    /// <summary>
    /// 充电枪编号
    /// </summary>
    public int ChargerGunNo { get; set; }
    /// <summary>
    /// 1枪2枪
    /// </summary>
    public int GunNo { get; set; }

    /// <summary>
    /// 充电状态;0-未知；1-正在充电；2-无电池；3-禁用 4-充電完成
    /// </summary>
    public int ChargeStatus { get; set; }

    /// <summary>
    /// 仓位状态;0-禁用；1-启用
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 最后结束充电时间 结束充电后更新
    /// </summary>
    public DateTime LastChargeFinishTime { get; set; }

    /// <summary>
    /// 0：不能 1：能
    /// </summary>
    public int CanChargeFlag { get; set; }

    /// <summary>
    /// 充电功率
    /// </summary>
    public float Power { get; set; }
    
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
    
    public int ChargingInterfaceDetectionOneTemp { get; set; }
    public int ChargingInterfaceDetectionTwoTemp { get; set; }
    public int ChargingInterfaceDetectionTheTemp { get; set; }
    public int ChargingInterfaceDetectionFourTemp { get; set; }
    
    /// <summary>
    /// Desc:充电机是否连接
    /// Default:
    /// Nullable:0
    /// </summary>
    public bool ChargeConnectFlag { get; set; }
    /// <summary>
    /// 车辆连接状态 0-未连接、1-已连接
    /// </summary>
    public bool VehicleConnStatus { get; set; }
    
    /// <summary>
    /// 充电桩充电枪座状态 0-已连接、1-未连接
    /// </summary>
    public bool ChargeStationGunHolderStatus { get; set; }
    
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
    /// 是否鉴权
    /// Default:
    /// Nullable:
    /// </summary>
    public bool IsAuthed { get; set; } = false;
    
    /// <summary>
    /// 车辆VIN码
    /// </summary>
    public string Vin { get; set; }
}