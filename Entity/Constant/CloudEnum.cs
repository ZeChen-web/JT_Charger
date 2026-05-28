using Entity.Attr;

namespace Entity.Constant;

/// <summary>
/// 
/// </summary>
public class CloudEnum
{
    /// <summary>
    /// 是否换电
    /// </summary>
    public enum WhetherToBattery : byte
    {
        [Remark("可以开始换电")] Success = 1,
        [Remark("终止换电")] StopBattery = 2
    }

    /// <summary>
    /// 尖峰平谷
    /// </summary>
    public enum PeriodNumber : int
    {
        [Remark("尖")] Pointed = 1,
        [Remark("峰")] Peak = 2,
        [Remark("平")] Flat = 3,
        [Remark("谷")] Valley = 4
    }

    /// <summary>
    /// 更新方式
    /// </summary>
    public enum RenewalMethod : int
    {
        [Remark("覆盖所有记录")] Cover = 1,
        [Remark("增量更新")] Increment = 2
    }

    /// <summary>
    /// 认证方式
    /// </summary>
    public enum AuthenticationMethod : int
    {
        [Remark("车牌号")] VehicleNo = 1,
        [Remark("rfid")] RFID = 2
    }

    /// <summary>
    /// 服务状态
    /// </summary>
    public enum ServiceStatus : int
    {
        [Remark("未知")] Unknown = 0,
        [Remark("换电站服务启用")] Start = 1,
        [Remark("换电站服务停用")] Stop = 2
    }

    /// <summary>
    /// 换电站类型
    /// </summary>
    public enum PowerStationType
    {
        // 单仓左
        [Remark("01")] Left,

        // 单仓右
        [Remark("02")] Righ,

        // 双仓
        [Remark("03")] Pair
    }

    /// <summary>
    /// 应答结果
    /// </summary>
    public enum Result : byte
    {
        [Remark("成功")] Success = 0,
        [Remark("失败")] Fail = 1
    }

    public enum ResultInt : byte
    {
        [Remark("成功")] Success = 0,
        [Remark("失败")] Fail = 1
    }

    /// <summary>
    /// 设备应答结果
    /// </summary>
    public enum EquipmentResult : int
    {
        [Remark("成功")] Success = 0,
        [Remark("设备不存在")] NoEquipment = 1,
        [Remark("系统未找到对应的设备编码")] NoEquipmentCode = 2,
        [Remark("设备数量和后台不一致当")] ItSNotTheSame = 3,
    }

    /// <summary>
    /// 签到应答结果
    /// </summary>
    public enum SignInResult : int
    {
        [Remark("成功")] Success = 0,
        [Remark("设备不存在")] NoEquipment = 1,
        [Remark("非法设备")] Rogue = 2,
        [Remark("站控软件版本错误")] VersionError = 3,
        [Remark("协议版本错误")] ProtocolError = 4
    }

    /// <summary>
    /// 车辆认证应答
    /// </summary>
    public enum VehicleAuthResult : int
    {
        [Remark("成功")] Success = 0,
        [Remark("没找到匹配的车辆（未入网）")] NoVehicle = 1,
        [Remark("没有找到车辆匹配的 VIN")] NoVehicleVIN = 2,
        [Remark("车辆已经进入黑名单")] Blacklist = 3,
        [Remark("账户余额不足")] TheBalanceIsInsufficient = 4,
        [Remark("未预约")] NoAppointments = 5,
        [Remark("没有招到对应的 rfid 卡号")] NoRfid = 6
    }

    /// <summary>
    /// 站内外鉴权方式
    /// </summary>
    public enum AuthMethod : int
    {
        [Remark("站内鉴权（默认）")] InsideTheStation = 0,
        [Remark("站外")] OffSite = 1,
    }

    /// <summary>
    /// 上传设备
    /// </summary>
    public enum UploadTheDevice : int
    {
        [Remark("不需要上传")] NoUpload = 0,
        [Remark("需要重新上传")] AnewUpload = 1,
        [Remark("初始上传")] PrimaryUpload = 2
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType : int
    {
        [Remark("应答")] Response = 1,
        [Remark("请求")] Request = 2
    }

    /// <summary>
    /// 加密方式
    /// </summary>
    public enum Encryption : int
    {
        [Remark("不加密")] NoEncrypt = 0,
        [Remark("AES")] AES = 1
    }

    /// <summary>
    /// 空调状态
    /// </summary>
    public enum AirConditioningStatus : byte
    {
        [Remark("停机")] Stop = 0,
        [Remark("开启")] Open = 1,
        [Remark("运行")] Run = 2
    }

    /// <summary>
    /// 空调模式
    /// </summary>
    public enum AirConditioningMode : byte
    {
        [Remark("制冷")] Refrigeration = 1,
        [Remark("制热")] Heating = 2,
        [Remark("其他")] Other = 3
    }

    /// <summary>
    /// 工作状态
    /// </summary>
    public enum WorkingStatus : int
    {
        [Remark("未知")] Unknown = 0,
        [Remark("空闲")] FreeTime = 1,
        [Remark("工作中")] Work = 2,
        [Remark("工作完成")] WorkDone = 3,
    }

    /// <summary>
    /// 换电状态
    /// </summary>
    public enum BatterySwapStatus : int
    {
        [Remark("未知")] Unknown = 0,
        [Remark("空闲")] FreeTime = 1,
        [Remark("占位")] Occupancy = 2,
        [Remark("换电准备")] PrepareForBatterySwapping = 3,
        [Remark("换电开始")] TheBatterySwapBegins = 4,
        [Remark("换电中")] BatterySwapping = 5,
        [Remark("换电完成")] TheBatterySwapIsComplete = 6,
        [Remark("换电中故障，等待修复")] TheFaultIsToBeFixed = 7,
        [Remark("换电中故障，修复完成")] TheFaultFixIsComplete = 8,
        [Remark("换电暂停")] TheBatterySwapIsSuspended = 9,
        [Remark("换电继续")] TheBatterySwapContinues = 10,
        [Remark("换电完成，车辆未驶离")] TheBatterySwapWasCompletedAndDidNotLeave = 11,
        [Remark("换电完成，车辆驶离")] FinishDrivingAwayTheVehicle = 12
    }

    /// <summary>
    /// 是否有车
    /// </summary>
    public enum WhetherThereIsACarOrNot : int
    {
        [Remark("未知")] Unknown = 0,
        [Remark("有车")] HaveVehicle = 1,
        [Remark("无车")] NoVehicle = 2,
    }

    /// <summary>
    /// 锁止状态
    /// </summary>
    public enum LockedState : int
    {
        [Remark("未知")] Unknown = 0,
        [Remark("加锁状态")] LockedStatus = 1,
        [Remark("解锁状态")] UnlockStatus = 2,
    }

    /// <summary>
    /// 故障等级
    /// </summary>
    public enum FailureLevel : int
    {
        [Remark("正常")] Normal = 0,
        [Remark("故障等级一")] FaultLevelOne = 1,
        [Remark("故障等级二")] FaultLeveTwo = 2,
        [Remark("故障等级三")] FaultLeveThree = 3,
        [Remark("故障等级四")] FaultLeveFour = 4,
        [Remark("故障等级五")] FaultLeveFive = 5,
        [Remark("故障等级六")] FaultLeveSix = 6
    }

    /// <summary>
    /// 运行方式
    /// </summary>
    public enum EscalationMethod : int
    {
        [Remark("自动")] Automatic = 1,
        [Remark("人工手动")] Manual = 2
    }

    /// <summary>
    /// 充电方式
    /// </summary>
    public enum ChargingMethod : int
    {
        [Remark("站内充电")] OnSiteCharging = 0,
        [Remark("站外充电")] ChargingOffSite = 1
    }

    /// <summary>
    /// 运营状态
    /// </summary>
    public enum OperationalStatus : int
    {
        [Remark("营业状态")] BusinessStatus = 1,
        [Remark("暂停营业状态")] TemporarilyClosed = 2,
        [Remark("设备维护状态")] DeviceMaintenanceStatus = 3,
        [Remark("歇业状态")] ClosedStatus = 4
    }

    /// <summary>
    /// 订单取消
    /// </summary>
    public enum OrderCancellation : byte
    {
        [Remark("正常")] Normal = 0,
        [Remark("取消")] Cancel = 1
    }

    /// <summary>
    /// 是否成功换电
    /// </summary>
    public enum WhetherTheBatterySwapIsSuccessful : byte
    {
        [Remark("失败")] Fail = 0,
        [Remark("成功")] Success = 1
    }

    /// <summary>
    /// 换电结果
    /// </summary>
    public enum BatterySwapResults : byte
    {
        [Remark("正常")] Normal = 0,
        [Remark("失败")] Fail = 1
    }

    /// <summary>
    /// 是否是离线订单
    /// </summary>
    public enum OrderType : int
    {
        [Remark("不是")] No = 0,
        [Remark("是")] Yes = 1
    }

    /// <summary>
    /// 电池预约
    /// </summary>
    public enum BatteryAppointments : int
    {
        [Remark("预约电池失败")] Fail = 1,
        [Remark("换电站无电池可预约")] NoBattery = 2,
        [Remark("预约成功")] Success = 2
    }

    /// <summary>
    /// 换电请求应答
    /// </summary>
    public enum AnsweringTheBatterySwapRequest : int
    {
        [Remark("换电请求成功")] TheBatterySwapRequestIsSuccessful = 1,
        [Remark("换电请求失败")] TheBatterySwapRequestFailed = 2,
        [Remark("取消换电请求成功")] CancelTheBatterySwapRequestIsSuccessful = 3,
        [Remark("取消换电请求失败")] TCancelheBatterySwapRequestFailed = 2
    }
}