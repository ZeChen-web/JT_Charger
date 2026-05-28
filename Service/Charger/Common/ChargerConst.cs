using DotNetty.Common.Utilities;

namespace Service.Charger.Common;

public static class ChargerConst
{

    public static readonly AttributeKey<string> ChargerSn = AttributeKey<string>.ValueOf("charger_sn");
    public static readonly AttributeKey<string> EqmTypeNo = AttributeKey<string>.ValueOf("eqm_type_no");
    public static readonly AttributeKey<string> EqmCode = AttributeKey<string>.ValueOf("eqm_code");
    public static readonly AttributeKey<string> DestAddr = AttributeKey<string>.ValueOf("dest_addr");
    public static readonly string DateFormat = "yyMMddHHmmss";
    public static readonly string yyyyMMddHHmmss = "yyyyMMddHHmmss";

    public static readonly byte[] StartChar = { 0x68 /* ,0xEE*/ };
    public static readonly string AuthCode = "szhckj01";

    public static readonly byte[] BatteryBasicInfo2 =  { 0x00, 0xF8, 0x02 };
    public static readonly byte[] BatteryNo =  { 0x00, 0xF8, 0x81 };
    public static readonly byte[] BatteryBasicInfo =  { 0x00, 0xF8, 0x82 };
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
