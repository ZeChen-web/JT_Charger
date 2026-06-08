namespace Service.ChargerV14D.Common;

/// <summary>V1.4D 协议帧类型码定义 (奇数=充电桩上行, 偶数=平台下行)</summary>
public static class V14DFrameType
{
    // === 注册心跳帧 ===
    public const byte Login = 0x01;                    // 充电桩登录认证 (上行)
    public const byte LoginResp = 0x02;                // 登录认证应答 (下行)
    public const byte Heartbeat = 0x03;                // 充电桩心跳包 (上行)
    public const byte HeartbeatResp = 0x04;            // 心跳包应答 (下行)
    public const byte BillingModelVerify = 0x05;       // 计费模型验证请求 (上行)
    public const byte BillingModelVerifyResp = 0x06;   // 计费模型验证请求应答 (下行)
    public const byte BillingModelReq = 0x09;          // 充电桩计费模型请求 (上行)
    public const byte BillingModelResp = 0x0A;         // 计费模型请求应答 (下行)

    // === 实时数据帧 ===
    public const byte ReadRealTimeData = 0x12;         // 读取实时监测数据 (下行)
    public const byte UploadRealTimeData = 0x13;       // 上传实时监测数据 (上行)
    public const byte ChargeHandshake = 0x15;          // 充电握手 (上行)
    public const byte ParamConfig = 0x17;              // 参数配置 (上行)
    public const byte ChargeEnd = 0x19;                // 充电结束 (上行)
    public const byte ErrorMsg = 0x1B;                 // 错误报文 (上行)
    public const byte BmsAbort = 0x1D;                 // 充电阶段BMS中止 (上行)
    public const byte ChargerAbort = 0x21;             // 充电阶段充电机中止 (上行)
    public const byte BmsDemandOutput = 0x23;          // 充电过程BMS需求与充电机输出 (上行)
    public const byte BmsInfo = 0x25;                  // 充电过程BMS信息 (上行)

    // === 运营交互帧 ===
    public const byte ApplyStartCharge = 0x31;         // 充电桩主动申请启动充电 (上行)
    public const byte ConfirmStartCharge = 0x32;       // 运营平台确认启动充电 (下行)
    public const byte RemoteStartChargeReply = 0x33;   // 远程启动充电命令回复 (上行)
    public const byte RemoteStartCharge = 0x34;        // 运营平台远程控制启机 (下行)
    public const byte RemoteStopChargeReply = 0x35;    // 远程停机命令回复 (上行)
    public const byte RemoteStopCharge = 0x36;         // 运营平台远程停机 (下行)
    public const byte TransactionRecord = 0x3F;        // 交易记录 (上行)
    public const byte TransactionRecordConfirm = 0x40; // 交易记录确认 (下行)
    public const byte BalanceUpdateReply = 0x41;       // 余额更新应答 (上行)
    public const byte BalanceUpdate = 0x42;            // 远程账户余额更新 (下行)
    public const byte CardSyncReply = 0x43;            // 卡数据同步应答 (上行)
    public const byte CardSync = 0x44;                 // 离线卡数据同步 (下行)

    // === 运营平台设置帧 ===
    public const byte ParamSetReply = 0x51;            // 充电桩工作参数设置应答 (上行)
    public const byte ParamSet = 0x52;                 // 充电桩工作参数设置 (下行)
    public const byte TimeSyncReply = 0x55;            // 对时设置应答 (上行)
    public const byte TimeSync = 0x56;                 // 对时设置 (下行)
    public const byte BillingModelSetReply = 0x57;     // 计费模型设置应答 (上行)
    public const byte BillingModelSet = 0x58;          // 计费模型设置 (下行)

    // === 车位锁通信 ===
    public const byte LockDataUpload = 0x61;           // 地锁数据上送 (上行)
    public const byte LockControl = 0x62;              // 遥控地锁升锁与降锁命令 (下行)
    public const byte LockCommandReply = 0x63;         // 充电桩返回数据 (上行)

    // === 电池在仓信号 ===
    public const byte BatteryInBinSignal = 0x78;       // 站控发送电池在仓数据 (下行)

    // === 远程维护 ===
    public const byte RemoteRestartReply = 0x91;       // 远程重启应答 (上行)
    public const byte RemoteRestart = 0x92;            // 远程重启 (下行)
    public const byte RemoteUpdateReply = 0x93;        // 远程更新应答 (上行)
    public const byte RemoteUpdate = 0x94;             // 远程更新 (下行)
    public const byte QRCodeReply = 0x9B;              // 二维码应答 (上行)
    public const byte QRCodeDistribute = 0x9C;         // 下发二维码 (下行)

    // === 电池信息 ===
    public const byte BatteryInfoQuery = 0x72;         // 平台下发获取电池基本信息 (下行)
    public const byte BatteryInfoReport = 0x73;        // 电池基本信息应答 (上行)
    public const byte BatteryStatusReport = 0x75;      // 电池状态上报 (上行)

    // === VIN相关 ===
    public const byte VINQuery = 0xAD;                 // VIN查询 (下行)
    public const byte VINQueryReply = 0xAE;            // VIN查询应答 (上行)
    public const byte PlatformVINStartCharge = 0xAF;   // 平台VIN启动充电 (下行)
}
