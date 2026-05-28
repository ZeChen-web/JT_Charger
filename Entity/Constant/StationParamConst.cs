namespace Entity.Constant;

public class StationParamConst
{
    #region 换电站基础信息

    /// <summary>
    /// 换电站编码
    /// </summary>
    public static readonly string StationNo = "Station.StationNo";

    /// <summary>
    /// 换电站名称
    /// </summary>
    public static readonly string StationName = "Station.StationName";

    /// <summary>
    /// 站类型
    /// </summary>
    public static readonly string StationType = "Station.StationType";

    /// <summary>
    /// 换电站识别号
    /// </summary>
    public static readonly string StationSn = "Station.StationSn";

    /// <summary>
    /// 地理位置
    /// </summary>
    public static readonly string StationLocation = "Station.StationLocation";

    /// <summary>
    /// 经度
    /// </summary>
    public static readonly string Longitude = "Station.Longitude";

    /// <summary>
    /// 纬度
    /// </summary>
    public static readonly string Latitude = "Station.Latitude";

    /// <summary>
    /// 区域编号
    /// </summary>
    public static readonly string AreaCode = "Station.AreaCode";

    /// <summary>
    /// 区域名称
    /// </summary>
    public static readonly string AreaName = "Station.AreaName";

    /// <summary>
    /// 营运开始时间
    /// </summary>
    public static readonly string OperationStartTime = "Station.OperationStartTime";

    /// <summary>
    /// 营运结束时间
    /// </summary>
    public static readonly string OperationEndTime = "Station.OperationEndTime";

    /// <summary>
    /// 服务状态
    /// </summary>
    public static readonly string Sevstatus = "Station.Sevstatus";

  

    /// <summary>
    /// 投放时间
    /// </summary>
    public static readonly string? LaunchTime = "Station.LaunchTime";

    /// <summary>
    /// 联系方式
    /// </summary>
    public static readonly string ContactWay = "Station.ContactWay";

    /// <summary>
    /// 负责人
    /// </summary>
    public static readonly string Principal = "Station.Principal";

    /// <summary>
    /// 所属运营企业
    /// </summary>
    public static readonly string StationCompany = "Station.StationCompany";

    /// <summary>
    /// 所属运营企业统一社会信用代码
    /// </summary>
    public static readonly string SocialCreditCode = "Station.SocialCreditCode";

    /// <summary>
    /// 站控主机软件版本号
    /// </summary>
    public static readonly string StationSftVer = "Station.StationSftVer";

    /// <summary>
    /// 供应商代码
    /// </summary>
    public static readonly string SupplierCode = "Station.SupplierCode";

    /// <summary>
    /// 换电站基础信息版本号
    /// </summary>
    public static readonly string StationVersion = "Station.StationVersion";

    /// <summary>
    /// 换电站硬件版本
    /// </summary>
    public static readonly string? HardwareVersion = "Station.HardwareVersion";

    /// <summary>
    /// 封面图片文件
    /// </summary>
    public static readonly string Cover = "Station.Cover";

    /// <summary>
    /// 总体故障等级
    /// </summary>
    public static readonly string? Faultlevel = "Station.Faultlevel";

    /// <summary>
    /// 加解锁方式
    /// </summary>
    public static readonly string? LockType = "Station.LockType";

    /// <summary>
    /// 进入方式
    /// </summary>
    public static readonly string? AccessType = "Station.AccessType";

    /// <summary>
    /// 举升方式
    /// </summary>
    public static readonly string? RiseType = "Station.RiseType";

    /// <summary>
    /// 创建时间
    /// </summary>
    public static readonly string CreateTime = "Station.CreateTime";

    /// <summary>
    /// 修改时间
    /// </summary>
    public static readonly string? ModifyTime = "Station.ModifyTime";

    /// <summary>
    /// 配电容量（kVA）
    /// </summary>
    public static readonly string? DistributionCapacity = "Station.DistributionCapacity";

    /// <summary>
    /// 总功率（kW）
    /// </summary>
    public static readonly string? TotalPower = "Station.TotalPower";

    /// <summary>
    /// 省份
    /// </summary>
    public static readonly string StationProvince = "Station.StationProvince";

    /// <summary>
    /// 城市
    /// </summary>
    public static readonly string StationCity = "Station.StationCity";

    /// <summary>
    /// 组织机构ID
    /// </summary>
    public static readonly string? OrganizationId = "Station.OrganizationId";

    /// <summary>
    /// 站控电脑MAC地址
    /// </summary>
    public static readonly string StationMac = "Station.StationMac";

    #endregion 换电站基础信息

    #region 系统管理-基础设置
    //提交灯光日间时间
    public static readonly string SetLightDayStartTime = "Station.LightDayStartTime";
    public static readonly string SetLightDayEndTime = "Station.LightDayEndTime";

    
    
    
    #endregion 系统管理-基础设置
    
    //选包策略中最后结束充电时间需要>此值
    public static readonly string SwapFinishChargeTime = "Station.SwapFinishChargeTime";

    //选包策略换电Soc
    public static readonly string SwapSoc = "Station.SwapSoc";

    //充电soc
    public static readonly string ChargeSoc = "Station.ChargeSoc";
    public static readonly string ChargePower = "Station.ChargePower";
    public static readonly string AutoChargeEnabled = "Station.AutoChargeEnabled";
    public static readonly string Eid = "Station.Eid";
    //运营模型
    public static readonly string Oid = "Station.Oid";
    //电价模型
    public static readonly string Ceid = "Station.Ceid";

    public static readonly string StationStatus = "Station.StationStatus";
    public static readonly string StationWay = "Station.StationWay";
    public static readonly string StationModel = "Station.StationModel";

    #region cloud param

    public static readonly string CloudServerIp = "Cloud.CloudServerIp";
    public static readonly string CloudServerPort = "Cloud.CloudServerPort";
    public static readonly string CloudClientId = "Cloud.CloudClientId";
    public static readonly string CloudUsername = "Cloud.CloudUsername";
    public static readonly string CloudPassword = "Cloud.CloudPassword";
    public static readonly string CloudSubTopic = "Cloud.CloudSubTopic";
    public static readonly string CloudPubTopic = "Cloud.CloudPubTopic";

    #endregion
}