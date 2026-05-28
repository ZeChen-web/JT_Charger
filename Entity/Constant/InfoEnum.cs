using System.Runtime.InteropServices;
using Entity.Attr;

namespace Entity.Constant;

public class InfoEnum
{
    public enum SwapInfo : ushort
    {
    
       
        [Info("欢迎光临换电站！(正在营业)","欢迎光临换电站！,本站正在营业中")]WelcomeInfo=1,
        [Info("标签读写失败", "标签读写失败")] ErrorReadRfid ,

        [Info("车辆连接失败", "车辆连接失败，请联系站务人员")] ErrorTBoxConn ,
        [Info("云端校验失败", "云端校验失败，请联系站务人员")] ErrorCloudCheck ,
        [Info("车辆已到位", "车辆已到位")] InfoCarInPosition ,
        [Info("车辆到位超时", "车辆到位超时")] ErrorCarInPositionTimeout,
        [Info("云平台下发换电失败", "云平台下发换电超时")] CloudSendSwapError ,

     
        [Info("解锁车辆失败", "解锁车辆失败")] ErrUnLockCar ,
        [Info("选包失败，请驶离", "选包失败，请驶离")] ErrorSelectPack ,
        [Info("通道拍照定位失败，请重新调整车辆位置", "通道拍照定位失败，请重新调整车辆位置")]ErrChannelStatus,
        
        
        [Info("电池拆卸中，请稍后", "电池拆卸中，请稍后")] InfoUnPack ,
      
        [Info("电池安装中，请稍后", "电池安装中，请稍后")] InfoPack ,
        [Info("电池包已安装完成", "电池包已安装完成")] InfoPackFinish ,
        [Info("航车已回归安全位置", "航车已回归安全位置")] InfoToSafePosition ,
        [Info("换电已完成，请驶离", "换电已完成，请驶离")] InfoCarLeave ,
        [Info("换电失败，请驶离", "换电失败，请驶离")] ErrInfoCarLeave ,
        [Info("电池入仓中，请稍后", "电池入仓中，请稍后")] InfoOldBatteryCarryIn ,
        [Info("电池出仓中，请稍后", "电池出仓中，请稍后")] InfoNewBatteryCarryOut ,
        [Info("车辆上锁失败", "车辆上锁失败")] ErrLockCar ,
        [Info("通道的电池仓无可用换电电池","通道的电池仓无可用换电电池")] NoBatteryErr,
        [Info("结束充电电池数量不足","通道的电池仓无可用换电电池")] LessOfFinishChargingErr,
        [Info("空仓数量不足","通道的电池仓无可用换电电池")] LessOfEmptyBinErr,
        [Info("符合soc限制数量不足","通道的电池仓无可用换电电池")] LessOfSocErr,
        [Info("结束充电大于3分钟的数量不足","通道的电池仓无可用换电电池")] LessOf3MinuteErr,
        [Info("换电站处于手动模式，不能自动换电","换电站处于手动模式，不能自动换电")] InfoStationModel,
        
        [Info("换电站处于本地模式，不能远程换电","换电站处于本地模式，不能远程换电")] InfoStationModelRemoteErr,
        
        [Info("车辆驶入","车辆驶入")] CarInInfo,
        [Info("换电任务启动","换电任务启动")] StartSwapInfo,
        [Info("航车拍照中，请稍后","航车拍照中，请稍后")] CarTakePhotoInfo,
        [Info("启动换电失败，请联系站务人员","启动换电失败，请联系站务人员")] ErrStartSwap,
    }
    

    public enum SelectBinStatusInfo : byte
    {
        [Remark("通道的电池仓无可用换电电池")] NoBattery,
        [Remark("可以换电")] Success,
        [Remark("结束充电电池数量不足")] LessOfFinishCharging,
        [Remark("空仓数量不足")] LessOfEmptyBin,
        [Remark("符合soc限制数量不足")] LessOfSoc,
        [Remark("结束充电大于3分钟的数量不足")] LessOf3Minute,
        [Remark("预约电池异常")] AmtError,
    }

   
    public enum AmtOrderStatus : byte
    {
        [Remark("预约成功")] Success = 1,
        [Remark("预约取消")] Cancel = 2,
        [Remark("预约失败")] Fail = 3,
        [Remark("换电完成")] SwapFinish = 4,
        [Remark("换电失败")] SwapFail = 5,
        [Remark("换电中")] Swapping = 6,
        [Remark("预约过期")] Expire = 7,
    }
    //云平台上报步序

    public enum BusinessSwappingForCloudState : byte
    {
        [Remark("未知")] UnKnown,
        [Remark("空闲")] Idle,
        [Remark("占位")] TakeUp,
        [Remark("换电准备")] SwapReady,
        [Remark("开始换电")] BeginSwap,
        [Remark("换电中")] Swapping,
        [Remark("换电完成")] SwapFinish,
        [Remark("换电中故障，等待修复")] SwappingErrWait,
        [Remark("换电中故障，修复完成")] SwappingErrDone,
        [Remark("换电暂停")] SwapPause,
        [Remark("换电继续")] SwapContinue,
        [Remark("换电完成（车辆未驶离）")] SwapDoneWithVel,
        [Remark("换电完成（车辆驶离）")] SwapDoneWithoutVel
    }
    
    //小步状态
    public enum BusinessSwappingStep 
    {
        [Remark("空闲")] Idel,
        [Remark("车辆到站(入口雷达检测到车辆驶入)")] RadarInFlag,
        [Remark("rfid扫描完成")] RfidReadFlag,
        [Remark("云平台车辆认证")] CloudVelCheckFlag,
        [Remark("车辆到位")] CarInPositionFlag,
        [Remark("云平台下发换电指令")] CloudCarCanStartFlag,
        [Remark("车辆解锁")] VelUnlockFlag,
        [Remark("下发plc选包")] DistributeSelectPackFlag,
     
        [Remark("开始换电")] StartSwappingFlag,
        [Remark("拆旧电池完成")] UnOldBatteryFlag,
        [Remark("入库旧电池完成")] StorageOldBatteryFlag,
        [Remark("搬运新电池完成")] OutNewBatteryFlag,
        [Remark("安装新电池完成")] InstallNewBatteryFlag,
        [Remark("安装完成")] FinishNewBatteryFlag,
        [Remark("车辆上锁")] VelLockFlag,
        [Remark("换电完成（车辆驶离）")] RadarOutFlag,
        [Remark("换电失败（车辆驶离）")] RadarOutFailFlag,
    }

    



    public enum AmtBatLockStatus 
    {
        UnLock = 0,
        Lock = 1
    }

    public enum SwapOrderResult : byte
    {
        Success = 1,
        Fail = 2
    }
}