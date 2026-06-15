namespace Service.ChargerV14D.Common;

public static class ChargerConst
{

    public static readonly string DateFormat = "yyMMddHHmmss";
    public static readonly string yyyyMMddHHmmss = "yyyyMMddHHmmss";

    ///<summary>
    /// 启动方式
    /// 3- 0：运营平台启动；
    /// 4- 1：APP 启动；
    /// 5- 2: 本地启动
    /// </summary>
    /// <param name="startMode"></param>
    /// <returns></returns>
    public static int StartMode(int? startMode)
    {
        switch (startMode)
        {
            case 0: return 3;
            case 1: return 4;
            case 2: return 5;
            
            case 3: return 0;
            case 4: return 1;
            case 5: return 2;
        }

        return 0;
    }

    //充电及数据
    //00H：待机
    //01H：工作
    //02H：工作完成
    //03H: 故障
    
    //Desc:充电状态;0-待机；1-正在充电；2-无电池；3-禁用；4-充电完成5故障；
    
    /// <summary>
    /// 根据充电机状态设置数据库状态
    /// </summary>
    public static int WorkStatus(int workStatus)
    {
        switch (workStatus)
        {
            case 0: return 0;
            case 1: return 1;
            case 2: return 4;
            case 3: return 5;
        }

        return 0;
    }
}

/// <summary>
/// 充电状态。0:无效值；1:鉴权成功；2:鉴权失败；3：开始充电成功；4：开始充电失败；5：正在充电；6：停止充电成功；7：停止充电失败；
/// </summary>
public enum ChargingStatus
{
    UnKnown=0,
    StartChargingSuccess=1,

    StopChargingSuccess=4,
    Authed=5,
    AuthFailed=6,

    StartChargingFailed=7,
   // Charging,

    StopChargingFailed=8
}