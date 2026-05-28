using Autofac;
using Entity.Constant;
using HybirdFrameworkCore.Autofac;
using Service.System;

namespace Service.Init;

/// <summary>
/// 换电站基本信息静态数据
/// </summary>
public class StaticStationInfo
{


    public static int StationStatus
    {
        get => int.Parse(Resolve(StationParamConst.StationStatus));
        set => Set(StationParamConst.StationStatus, value);
    }
    public static string OperationStartTime
    {
        get => Resolve(StationParamConst.OperationStartTime);
        set => Set(StationParamConst.OperationStartTime, value);
    }
    public static string OperationEndTime
    {
        get => Resolve(StationParamConst.OperationEndTime);
        set => Set(StationParamConst.OperationEndTime, value);
    }
    public static string StationWay
    {
        get => Resolve(StationParamConst.StationWay);
        set => Set(StationParamConst.StationWay, value);
    }
    public static string StationModel
    {
        get => Resolve(StationParamConst.StationModel);
        set => Set(StationParamConst.StationModel, value);
    }
    
    public static string StationName
    {
        get => Resolve(StationParamConst.StationName);
        set => Set(StationParamConst.StationName, value);
    }

   

    public static string StationNo
    {
        get => Resolve(StationParamConst.StationNo);
        set => Set(StationParamConst.StationNo, value);
    }
    public static string StationSn
    {
        get => Resolve(StationParamConst.StationSn);
        set => Set(StationParamConst.StationSn, value);
    }
    
    public static string SwapFinishChargeTime
    {
        get => Resolve(StationParamConst.SwapFinishChargeTime);
        set => Set(StationParamConst.SwapFinishChargeTime, value);
    }
    
    public static float SwapSoc
    {
        get => float.Parse(Resolve(StationParamConst.SwapSoc));
        set => Set(StationParamConst.SwapSoc, value);
    }

    #region 充电相关

  public static int Eid
    {
        get => int.Parse(Resolve(StationParamConst.Eid));
        set => Set(StationParamConst.Eid, value);
    }

    public static string Oid
    {
        get => Resolve(StationParamConst.Oid);
        set => Set(StationParamConst.Oid, value);
    }

    public static int Ceid
    {
        get => int.Parse(Resolve(StationParamConst.Ceid));
        set => Set(StationParamConst.Ceid, value);
    }

    public static byte ChargeSoc
    {
        get => byte.Parse(Resolve(StationParamConst.ChargeSoc));
        set => Set(StationParamConst.ChargeSoc, value);
    }
    
    public static float ChargePower
    {
        get => float.Parse(Resolve(StationParamConst.ChargePower));
        set => Set(StationParamConst.ChargeSoc, value);
    }

    /// <summary>
    /// 0-关闭 1-开启
    /// </summary>
    public static byte AutoChargeEnabled
    {
        get => byte.Parse(Resolve(StationParamConst.AutoChargeEnabled));
        set => Set(StationParamConst.AutoChargeEnabled, value);
    }
    

    #endregion
    


    #region cloud

    public static string CloudServerIp
    {
        get => Resolve(StationParamConst.CloudServerIp);
        set => Set(StationParamConst.CloudServerIp, value);
    }

    public static int CloudServerPort
    { 
        get
        {
            string port = Resolve(StationParamConst.CloudServerPort);
            if (string.IsNullOrWhiteSpace(port))
            {
                return 33000;
            }
            return int.Parse(port);
        }
        set => Set(StationParamConst.CloudServerPort, value);
    }

    public static string CloudClientId
    {
        get => Resolve(StationParamConst.CloudClientId);
        set => Set(StationParamConst.CloudClientId, value);
    }

    public static string CloudUsername
    {
        get => Resolve(StationParamConst.CloudUsername);
        set => Set(StationParamConst.CloudUsername, value);
    }

    public static string CloudPassword
    {
        get => Resolve(StationParamConst.CloudPassword);
        set => Set(StationParamConst.CloudPassword, value);
    }

    public static string CloudSubTopic
    {
        get => Resolve(StationParamConst.CloudSubTopic);
        set => Set(StationParamConst.CloudSubTopic, value);
    }

    public static string CloudPubTopic
    {
        get => Resolve(StationParamConst.CloudPubTopic);
        set => Set(StationParamConst.CloudPubTopic, value);
    }

    #endregion

    #region db method

    private static SysConfigService _sysConfigService;

    private static string Resolve(string key)
    {
        if (_sysConfigService == null)
        {
            _sysConfigService = AppInfo.Container.Resolve<SysConfigService>();
        }

        string? s = _sysConfigService.Get(key);
        if (s != null)
        {
            return s;
        }

        return "";
    }

    private static void Set(string key, object value)
    {
        if (_sysConfigService == null)
        {
            _sysConfigService = AppInfo.Container.Resolve<SysConfigService>();
        }

        _sysConfigService.Set(key, value);
    }

    #endregion
}