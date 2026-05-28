using System.Collections.Concurrent;
using System.Reflection;
using Autofac;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Entity;
using HybirdFrameworkCore.Redis;
using HybirdFrameworkCore.Utils;
using HybirdFrameworkDriver.Session;
using HybirdFrameworkDriver.TcpClient;
using log4net;
using Newtonsoft.Json;
using Repository.Station;
using Service.Charger.Codec;
using Service.Charger.Common;
using Service.Charger.Handler;
using Service.Charger.Msg;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;
using Service.Charger.Msg.Host.Req;
using Service.Charger.Msg.Host.Req.Bms;
using Service.Charger.Msg.Host.Req.OutCharger.Req;
using Service.Init;
using SqlSugar;

namespace Service.Charger.Client;

/// <summary>
///     示例程序
/// </summary>
[Scope("InstancePerDependency")]
public class ChargerClient : TcpClient<IBaseHandler, Decoder, Encoder>
{
    #region 属性

    /// <summary>
    /// 充电机编号
    /// </summary>
    public string Sn { get; set; }

    public readonly string Gun1 = "1";
    public readonly string Gun2 = "1";

    public ushort AuthTimes { get; set; } = 0;
    public bool IsAuthed { get; set; } = false;
    /// <summary>
    /// 参考 Service.Charger.Common.ChargingStatus
    /// </summary>
    public UInt16 ChargingStatus { get; set; }

    /// <summary>
    /// 是否已经开始充电
    /// </summary>
    public bool IsCharged { get; set; } = false;


    /// <summary>
    /// 站外两枪时是否在充电
    /// </summary>
    public ConcurrentDictionary<byte, bool> GunCharged = new ConcurrentDictionary<byte, bool>
    {
        [1] = false,
        [2] = false
    };

    /// <summary>
    /// 充电桩连接状态
    /// </summary>
    public ConcurrentDictionary<byte, bool> ChargedPile = new ConcurrentDictionary<byte, bool>
    {
        [1] = false,
        [2] = false
    };

    public bool IsStopped { get; set; } = false;
    public bool IsCanSendStopCmd { get; set; } = true;

    public DateTime? ChargingStartTime { get; set; }

    public DateTime? ChargingStopTime { get; set; }

    /// <summary>
    /// 电池包实时数据
    /// </summary>
    public BatteryPackData? BatteryPackData { get; set; }

    /// <summary>
    /// 电池包实时单体温度&单体电压数据
    /// </summary>
    public BatteryPackDataVoltage? BatteryPackDataVoltage { get; set; }

    /// <summary>
    /// 电池包上报累计充放电电量
    /// </summary>
    public BatteryPackTotalElectricity? BatteryPackTotalElectricity { get; set; }

    /// <summary>
    /// 电池包上报充放电口温度
    /// </summary>
    public BatteryPackPortTemperature? BatteryPackPortTemperature { get; set; }

    /// <summary>
    /// 电池包内部接触器状态和故障上报
    /// </summary>
    public BatteryPackStateAndFault? BatteryPackStateAndFault { get; set; }

    /// <summary>
    /// 充放电设备应答站功率调节指令
    /// </summary>
    public PowerRegulationRes PowerRegulationRes { get; set; }

    /// <summary>
    /// 电池包实时遥信上报（站内充电模式有电池包时周期性上传）
    /// </summary>
    public RemoteSignaling RemoteSignaling { get; set; }

    /// <summary>
    /// 充电机上报车辆 VIN
    /// </summary>
    public VehicleVIN VehicleVIN { get; set; }

    /// <summary>
    /// 充放电机应答辅助控制
    /// </summary>
    public AuxiliaryPowerRes AuxiliaryPowerRes { get; set; }

    /// <summary>
    /// 充放电上报交流电表数据（交流电表接到充电机上的情况）
    /// </summary>
    public AcMeter AcMeter { get; set; }

    /// <summary>
    ///充电机遥信数据
    /// </summary>
    public UploadRemoteSignalData UploadRemoteSignalData = new UploadRemoteSignalData();

    /// <summary>
    /// 充电机工作状态-从遥信数据包中得到。00H：待机 01H：工作 02H：工作完成 03H：充/放电暂停
    /// </summary>
    public byte Workstate { get; set; }

    /// <summary>
    /// 充电机故障-遥信数据包总故障 00H正常、01H故障
    /// </summary>
    public bool TotalError { get; set; }

    /// <summary>
    /// 充电机告警-遥信数据包总告警 00H正常、01H告警
    /// </summary>
    public bool TotalWarning { get; set; }

    /// <summary>
    /// 充电机遥测数据
    /// </summary>
    public UploadTelemetryData UploadTelemetryData = new UploadTelemetryData();

    /// <summary>
    /// 充放电机上传单体动力蓄电池电压极值统计
    /// </summary>
    public VoltageExtremumStatistics? VoltageExtremumStatistics = new VoltageExtremumStatistics();

    public BatteryBaseInfo? BatteryBaseInfo = new();

    public VoltageCurrentSoc? VoltageCurrentSoc = new();

    public UpAlarm? UpAlarm = new();
    
    public UpBms? UpBms = new();
    public BasicParameter? BasicParameter = new();
    #region 充电桩数据

    public ConcurrentDictionary<byte, string> Vin = new()
    {
        [1] = "",
        [2] = "",
    };
    
    /// <summary>
    /// 工作状态 00H:待机、01H:工作、02H:工作完成、03H:充/放电暂停
    /// </summary>
    public ConcurrentDictionary<byte, byte> WorkStatus = new()
    {
        [1] = 0,
        [2] = 0,
    };
    
    /// <summary>
    /// 充电桩的遥测
    /// </summary>
    public ConcurrentDictionary<byte, PileUploadTelemetry> PileUploadTelemetry = new()
    {
        [1]=new PileUploadTelemetry(),
        [2]=new PileUploadTelemetry(),
    };

    /// <summary>
    /// 充电桩的遥信
    /// </summary>
    public ConcurrentDictionary<byte, PileUploadRemoteSignal> PileUploadRemoteSignal = new(){
        [1]=new PileUploadRemoteSignal(),
        [2]=new PileUploadRemoteSignal(),
    };

    #endregion

    /*
    /// <summary>
    /// 充电桩状态信息
    /// </summary>
    public ConcurrentDictionary<byte, ChargerPile> ChargerPile
    {
        get
        {
            RedisHelper? redisHelper = AppInfo.Container.Resolve<RedisHelper>();
            string strChargerPile = redisHelper.GetStrValue(Sn);

            ConcurrentDictionary<byte, ChargerPile> lstChargerPile =
                JsonConvert.DeserializeObject<ConcurrentDictionary<byte, ChargerPile>>(Sn);
            return lstChargerPile;
        }
        set
        {
            
        }
    }
    */

    /// <summary>
    /// 充电桩状态信息
    /// </summary>
    public ChargerPile ChargerPile1
    {
        get
        {
            RedisHelper? redisHelper = AppInfo.Container.Resolve<RedisHelper>();
            string strkey = Sn + Gun1;
            
            try
            {
                string strChargerPile = redisHelper.GetStrValue(strkey);

                if (string.IsNullOrEmpty(strChargerPile))
                    return new ChargerPile();
                return JsonConvert.DeserializeObject<ChargerPile>(strChargerPile);
            }
            catch (Exception e)
            {
                return new ChargerPile();
            }
        }
        set
        {
            RedisHelper? redisHelper = AppInfo.Container.Resolve<RedisHelper>();
            string strkey = Sn + Gun1;
            string strvalue = JsonConvert.SerializeObject(value);

            redisHelper.SetKeyValueStr(strkey, strvalue);
        }
    }
    /// <summary>
    /// 充电桩状态信息
    /// </summary>
    public ChargerPile ChargerPile2
    {
        get
        {
            RedisHelper? redisHelper = AppInfo.Container.Resolve<RedisHelper>();
            string strkey = Sn + Gun2;
            string strChargerPile = redisHelper.GetStrValue(strkey);

            if (string.IsNullOrEmpty(strChargerPile))
                return new ChargerPile();

            return JsonConvert.DeserializeObject<ChargerPile>(strChargerPile);
        }
        set
        {
            RedisHelper? redisHelper = AppInfo.Container.Resolve<RedisHelper>();
            string strkey = Sn + Gun2;
            string strvalue = JsonConvert.SerializeObject(value);

            redisHelper.SetKeyValueStr(strkey, strvalue);
        }
    }
    

    /// <summary>
    /// 充电桩功率
    /// </summary>
    public ConcurrentDictionary<byte, float> ChargePilePower = new();

    /// <summary>
    ///充电机实时充电功率
    /// </summary>
    public float RealTimeChargePower { get; set; } = 0;

    /// <summary>
    /// 心跳-桩状态
    /// </summary>
    public byte PileState { get; set; }

    /// <summary>
    /// 电池编码
    /// </summary>
    public string? BatteryNo { get; set; }

    /// <summary>
    /// 电池厂家
    /// </summary>
    public byte? BatteryFactory { get; set; }

    /// <summary>
    /// 电池仓编号
    /// </summary>
    public string? BinNo { get; set; }

    /// <summary>
    /// 远程升级-监控网关上送升级完成确认帧
    /// </summary>
    public UplinkUpgrade UplinkUpgrade { get; set; }

    /// <summary>
    /// 充电订单号
    /// </summary>
    public string? ChargeOrderNo { get; set; }

    /// <summary>
    /// 当前指令
    /// </summary>
    public string? CurrentCmd { get; set; }

    /// <summary>
    /// 当前接收报文
    /// </summary>
    public string? CurrentMsg { get; set; }

    #endregion

    #region db

    private ChargeOrderRepository _chargeOrderRepository;
    private BinInfoRepository _binInfoRepository;
    private ElecPriceModelVersionDetailRepository _elecPriceModelVersionDetailRepository;

    private EquipInfoRepository _equipInfoRepository;

    #endregion

    public ChargerClient(ChargeOrderRepository chargeOrderRepository, BinInfoRepository binInfoRepository,
        ElecPriceModelVersionDetailRepository elecPriceModelVersionDetailRepository,
        EquipInfoRepository equipInfoRepository)
    {
        _chargeOrderRepository = chargeOrderRepository;
        _binInfoRepository = binInfoRepository;
        _elecPriceModelVersionDetailRepository = elecPriceModelVersionDetailRepository;
        _equipInfoRepository = equipInfoRepository;
    }

    private ILog Log()
    {
        var name = "Charger" + this.Sn;
        ILog logger = LogManager.GetLogger(name);

        Console.WriteLine(name + "-" + logger.GetHashCode());
        return logger;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="asdu"></param>
    public void ReceiveMsgHandle(ASDU asdu)
    {
        this.CurrentMsg = CurrentCmd = JsonConvert.SerializeObject(asdu, Formatting.Indented) + "\r\n" +
                                       BitUtls.BytesToHexStr(asdu.ToBytes());
    }

    #region 发送指令

    private ushort IncreAuthTimes()
    {
        if (AuthTimes < 65535)
        {
            AuthTimes += 1;
        }
        else
        {
            AuthTimes = 1;
        }

        return AuthTimes;
    }


    /// <summary>
    /// 发送鉴权----并且发送电价配置
    /// </summary>
    public Result<bool> SendAuth()
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        byte authCodeKey = ChargerUtils.GetByteRandomNum(); //鉴码KEY[随机数]
        byte[] authCodes = ChargerUtils.GetAuthCodesResult(ChargerConst.AuthCode, authCodeKey); //鉴权码
        Auth auth = new Auth(IncreAuthTimes(), authCodes, authCodeKey);
        CurrentCmd = JsonConvert.SerializeObject(auth, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(auth.ToBytes());
        this.Channel.WriteAndFlushAsync(auth);
        

        try
        {
            Log().Info("Auth after send SetPeakValleyTime");
            ConcurrentDictionary<string, ChargerClient> chargerClients = ClientMgr.Dictionary;
            if (chargerClients.Values.Count <= 0)
            {
                return Result<bool>.Fail();
            }

            foreach (var chargerClientsValue in chargerClients.Values)
            {
                if (chargerClientsValue.Connected)
                {
                    chargerClientsValue.SendSetPeakValleyTime(BulidSetPeakValleyTimeObj(StaticStationInfo.Ceid));
                }
            }
        }
        catch (Exception e)
        {
            Log().Info($"Auth after send SetPeakValleyTime deeor:{e}");
        }
        return Result<bool>.Success();
    }

    /// <summary>
    /// 监控平台发送远程开始充电指令
    /// </summary>
    /// <param name="socLimit">SOC限制.百分比</param>
    /// <param name="changePowerCmdType">功率调节指令类型.默认1 绝对功率值</param>
    /// <param name="changePower">1kw/位,默认3600</param>
    /// <param name="chargeOrderNo">充电流水号</param>
    public Result<string> SendRemoteStartCharging(byte socLimit, float changePower = 360, byte changePowerCmdType = 1,
        string? chargeOrderNo = null)
    {
        if (!Connected)
        {
            return Result<string>.Fail($"充电机{BinNo}未连接");
        }

        if (string.IsNullOrWhiteSpace(chargeOrderNo))
        {
            chargeOrderNo = ChargerUtils.GenChargeOrderSn();
        }

        Log().Info(
            $"SendRemoteStartCharging soc={socLimit}, changePower={changePower}, changePowerCmdType={changePowerCmdType}, chargeOrderNo={chargeOrderNo}");
        var remoteStartCharging = new RemoteStartCharging(socLimit, changePowerCmdType, changePower, chargeOrderNo);
        CurrentCmd = JsonConvert.SerializeObject(remoteStartCharging, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(remoteStartCharging.ToBytes());
        this.Channel.WriteAndFlushAsync(remoteStartCharging);

        return Result<string>.Success(chargeOrderNo);
    }

    /// <summary>
    /// 监控平台发送远程停止充电指令
    /// </summary>
    /// <param name="reason">0 正常停机 1 服务器发现桩异常,强制停机</param>
    public Result<bool> SendRemoteStopCharging(byte reason = 0)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        RemoteStopCharging remoteStopCharging = new RemoteStopCharging(reason);
        CurrentCmd = JsonConvert.SerializeObject(remoteStopCharging, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(remoteStopCharging.ToBytes());
        this.Channel.WriteAndFlushAsync(remoteStopCharging);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 监控平台发送功率调节指令
    /// </summary>
    /// <param name="expectedOperatingPower">期望运行功率</param>
    public Result<bool> SendPowerRegulation(float expectedOperatingPower)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        PowerRegulation powerRegulation = new PowerRegulation(expectedOperatingPower);
        this.Channel.WriteAndFlushAsync(powerRegulation);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 倍率 例如，0.单5C位该0值.1C为 5 ,1C 时该值为 10
    /// </summary>
    /// <param name="rate"></param>
    public Result<bool> SendAdjustChargeRate(float rate)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        AdjustChargeRate adjustChargeRate = new AdjustChargeRate(rate);
        this.Channel.WriteAndFlushAsync(adjustChargeRate);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 监控平台下发辅源控制指令
    /// </summary>
    /// <param name="openFlag">打开辅助电源标志 1：电池包辅助电源导通 0：电池包辅助电源断开</param>
    public Result<bool> SendAuxiliaryPower(byte openFlag)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        AuxiliaryPower auxiliaryPower = new AuxiliaryPower(openFlag);
        CurrentCmd = JsonConvert.SerializeObject(auxiliaryPower, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(auxiliaryPower.ToBytes());
        this.Channel.WriteAndFlushAsync(auxiliaryPower);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 监控平台下发电池仓的状态
    /// </summary>
    /// <param name="battery">是否有电池 0:无电池 1：有电池</param>
    /// <param name="connectionState">电接头连接状态 0:未连接 1: 已连接</param>
    /// <param name="waterCondition">水接头状态 0:未连接 1: 已连接</param>
    public Result<bool> SendBatteryHolderStatus(byte battery, byte connectionState, byte waterCondition)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        BatteryHolderStatus batteryHolderStatus = new BatteryHolderStatus(battery, connectionState, waterCondition);
        CurrentCmd = JsonConvert.SerializeObject(batteryHolderStatus, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(batteryHolderStatus.ToBytes());
        this.Channel.WriteAndFlushAsync(batteryHolderStatus);
        return Result<bool>.Success();
    }


    /// <summary>
    /// 站控下发 VIN 鉴权的结果
    /// </summary>
    /// <param name="vinresult">VIN 鉴权结果 1:通过 2 不通过</param>
    public Result<bool> SendAuthenticationVIN(byte vinresult)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        AuthenticationVIN authenticationVIN = new AuthenticationVIN(vinresult);
        this.Channel.WriteAndFlushAsync(authenticationVIN);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 远程升级-站级监控升级请求下发
    /// </summary>
    /// <param name="executionControl">执行控制 0x01：立即执行 0x02：空闲执行</param>
    /// <param name="downloadTimeout">下载超时时间</param>
    /// <param name="versionNumber">版本号</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="fileSize">文件大小</param>
    /// <param name="mD5Verification">MD5校验值</param>
    /// <param name="url">URL（文件路径）</param>
    public Result<bool> SendUpgradeRequest(byte executionControl, byte downloadTimeout, string versionNumber,
        string fileName, uint fileSize, string mD5Verification, string url)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        UpgradeRequest upgradeRequest = new UpgradeRequest(executionControl, downloadTimeout, versionNumber, fileName,
            fileSize, mD5Verification, url);
        this.Channel.WriteAndFlushAsync(upgradeRequest);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 设置尖峰平谷时间段
    /// </summary>
    /// <param name="setPeakValleyTime"></param>
    public Result<bool> SendSetPeakValleyTime(SetPeakValleyTime setPeakValleyTime)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }




        setPeakValleyTime= GetMsgContents(setPeakValleyTime);



        CurrentCmd = JsonConvert.SerializeObject(setPeakValleyTime, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(setPeakValleyTime.ToBytes());

        this.Channel.WriteAndFlushAsync(setPeakValleyTime);

        Log().Info($"SendSetPeakValleyTime{CurrentCmd} to chargeOrderNo={BinNo}");

        return Result<bool>.Success();
    }

    /// <summary>
    /// 3.4.7 监控平台下发掉线停止充电
    /// </summary>
    /// <param name="enabled"> 0：不使能 1：使能</param>
    public Result<bool> SendOfflineStopCharging(byte enabled)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        OfflineStopCharging offlineStopCharging = new OfflineStopCharging(enabled);
        CurrentCmd = JsonConvert.SerializeObject(offlineStopCharging, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(offlineStopCharging.ToBytes());
        this.Channel.WriteAndFlushAsync(offlineStopCharging);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 3.4.12 站控设备切换站内/站外充电切换
    /// </summary>
    /// <param name="chargeMode">00:无效 01:站内 02:站外</param>
    public Result<bool> SendChangeChargeMode(byte chargeMode)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        ChangeChargeMode req = new ChangeChargeMode(chargeMode);
        this.Channel.WriteAndFlushAsync(req);
        return Result<bool>.Success();
    }

    /// <summary>
    /// 3.7.1 监控平台远程启动充电桩充电
    /// </summary>
    /// <param name="pn">充电枪ID号</param>
    /// <param name="socValue">SOC 限制</param>
    /// <param name="changePower">功率调节指令类型</param>
    /// <param name="changePowerCmdType">功率调节参数</param>
    /// <param name="chargeOrderNo"></param>
    /// <returns>充电流水号</returns>
    public Result<string> SendStartOutCharger(byte pn, byte socValue, short changePower = 360,
        byte changePowerCmdType = 1,
        string? chargeOrderNo = null)
    {
        if (!Connected)
        {
            return Result<string>.Fail($"充电机{BinNo}未连接");
        }

        if (string.IsNullOrWhiteSpace(chargeOrderNo))
        {
            chargeOrderNo = ChargerUtils.GenChargeOrderSn();
        }

        Log().Info(
            $"SendStartOutCharger pn={pn}, socValue={socValue}, changePower={changePower}, changePowerCmdType={changePowerCmdType}, chargeOrderNo={chargeOrderNo}");

        PileStartCharge pileStartCharge =
            new PileStartCharge(pn, socValue, changePowerCmdType, changePower, chargeOrderNo);

        this.Channel.WriteAndFlushAsync(pileStartCharge);

        return Result<string>.Success(chargeOrderNo);
    }

    /// <summary>
    /// 3.7.3 监控平台远程停止充电桩充电
    /// </summary>
    /// <param name="pn"></param>
    /// <param name="stopReason"></param>
    /// <returns></returns>
    public Result<bool> SendStopOutCharger(byte pn, byte stopReason)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"充电机{BinNo}未连接");
        }

        Log().Info(
            $"SendStartOutCharger pn={pn}, stopReason={stopReason}");
        PileStopCharge pileStopCharge = new PileStopCharge(pn, stopReason);
        this.Channel.WriteAndFlushAsync(pileStopCharge);

        return Result<bool>.Success();
    }

    /// <summary>
    /// 3.7.9 监控平台发送充电桩功率调节指令
    /// </summary>
    /// <param name="pn"></param>
    /// <param name="expectedOperatingPower"></param>
    /// <returns></returns>
    public Result<bool> SendPileAdjustPower(byte pn, float expectedOperatingPower)
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        PileAdjustPower powerRegulation = new PileAdjustPower(pn, expectedOperatingPower);
        this.Channel.WriteAndFlushAsync(powerRegulation);
        return Result<bool>.Success();
    }

    /// <summary>
    ///
    /// </summary>
    public Result<bool> SendQueryBattery()
    {
        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        QueryBattery queryBattery = new QueryBattery(ChargerConst.BatteryNo);
        CurrentCmd = JsonConvert.SerializeObject(queryBattery, Formatting.Indented) + "\r\n" +
                     BitUtls.BytesToHexStr(queryBattery.ToBytes());
        this.Channel.WriteAndFlushAsync(queryBattery);
        return Result<bool>.Success();
    }

    #endregion


    #region 启动充电

    /// <summary>
    ///
    /// </summary>
    public Result<bool> StartCharge(byte chargeSoc, float chargePower, int startType)
    {
        if (string.IsNullOrWhiteSpace(BinNo))
        {
            return Result<bool>.Fail("charger init error with no BinNo");
        }

        if (!Connected)
        {
            return Result<bool>.Fail($"charger-{BinNo} disconnect");
        }

        BinInfo binInfo = _binInfoRepository.QueryByClause(it => it.Code == BinNo);
        if (binInfo == null)
        {
            return Result<bool>.Fail($"charger-{BinNo} not exist");
        }

        var equipInfo = _equipInfoRepository.QueryByClause(it => it.Code ==binInfo.ChargerNo && it.Status==1);
        if (equipInfo == null)
        {
            return Result<bool>.Fail($"charger-{binInfo.ChargerNo} status abnormal");
        }

        BatteryNo = binInfo.BatteryNo;
        if (string.IsNullOrWhiteSpace(BatteryNo) || "-1" == BatteryNo)
        {
            return Result<bool>.Fail($"charger-{BinNo} battery not exist");
        }

        if (binInfo.AmtLock == 1)
        {
            return Result<bool>.Fail($"仓-{BinNo} 被锁定");
        }

        if (binInfo.CanChargeFlag == 0)
        {
            return Result<bool>.Fail($"仓-{BinNo} 被禁用");
        }

        RedisHelper redisHelper = AppInfo.Container.Resolve<RedisHelper>();

        string? lockKey = redisHelper.GetStrValue($"chargeNo{BinNo}Start");
        if (!string.IsNullOrWhiteSpace(lockKey))
        {
            return Result<bool>.Success(true, $"charger-{BinNo} is starting");
        }

        redisHelper.SetKeyValueStr($"chargeNo{BinNo}Start", DateTime.Now.ToString("f"), TimeSpan.FromMinutes(1));

        Result<string> chargeOrderNo = SendRemoteStartCharging(chargeSoc, chargePower);
        if (!chargeOrderNo.IsSuccess)
        {
            return Result<bool>.Fail(chargeOrderNo.Msg);
        }

        SwapOrderBatteryRepository swapOrderBatteryRepository = AppInfo.Container.Resolve<SwapOrderBatteryRepository>();
        SwapOrderBattery? swapOrder = swapOrderBatteryRepository.QueryLatestOrderNoByBatterySn(BatteryNo);

        ChargeOrderNo = chargeOrderNo.Data;
        _chargeOrderRepository.Insert(new ChargeOrder()
        {
            Sn = ChargeOrderNo,
            BatteryNo = BatteryNo,
            CmdStatus = 0,
            ChargerNo = BinNo,
            ChargeMode = 1,
            SwapOrderSn = swapOrder?.SwapOrderSn,
            StartMode = 1,
            StartType = startType
        });

        return Result<bool>.Success(true, "发送成功");
    }

    #endregion

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public bool Connect()
    {
        base.BaseConnect();
        Log().Info($"charger {Sn} connect succeed");
        return Connected;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="destAddr"></param>
    public void SessionAttr(string sn, string destAddr)
    {
        ChannelUtils.AddAttr(Channel, ChargerConst.ChargerSn, sn);
        ChannelUtils.AddAttr(Channel, ChargerConst.EqmTypeNo, sn);
        ChannelUtils.AddAttr(Channel, ChargerConst.EqmCode, sn);
        ChannelUtils.AddAttr(Channel, ChargerConst.DestAddr, destAddr);
    }


    /// <summary>
    /// 获取尖峰平谷字节数组
    /// </summary>
    /// <param name="auth"></param>
    /// <returns>鉴权消息体字节数组</returns>
    private SetPeakValleyTime GetMsgContents(SetPeakValleyTime timeRng)
    {
        for (int i = 0; i < timeRng.NumberTime; i++)
        {

            PropertyInfo propertyStartTimePeriod1 = timeRng.GetType().GetProperty("StartHH" + (i + 1));
            propertyStartTimePeriod1.SetValue(timeRng, BitUtls.ByteToBCD(Convert.ToByte(timeRng.GetType().GetProperty("StartHH" + (i + 1)).GetValue(timeRng))));


            PropertyInfo propertyStartTimePeriod2 = timeRng.GetType().GetProperty("StartMM" + (i + 1));
            propertyStartTimePeriod2.SetValue(timeRng, BitUtls.ByteToBCD(Convert.ToByte(timeRng.GetType().GetProperty("StartMM" + (i + 1)).GetValue(timeRng))));

            PropertyInfo propertyTimePeriodPeakIden = timeRng.GetType().GetProperty("TimePeak" + (i + 1));
            propertyTimePeriodPeakIden.SetValue(timeRng, timeRng.GetType().GetProperty("TimePeak" + (i + 1)).GetValue(timeRng));



        }

        return timeRng;
    }

    
    
    
    public SetPeakValleyTime BulidSetPeakValleyTimeObj(int version)
    {
        List<ElecPriceModelVersionDetail> elecPriceModelVersionDetails =
            _elecPriceModelVersionDetailRepository.QueryListByClause(u => u.Version == version, u => u.Id,
                OrderByType.Asc);
        SetPeakValleyTime setPeakValleyTime = new SetPeakValleyTime()
        {
            NumberTime = Convert.ToByte(elecPriceModelVersionDetails.Count),
            StartHH1 = (byte)(elecPriceModelVersionDetails.Count > 0?Convert.ToByte(elecPriceModelVersionDetails[0].StartHour) :0),
            StartHH2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].StartHour) : 0),
            StartHH3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].StartHour):0),
            StartHH4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].StartHour):0),
            StartHH5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].StartHour):0),
            StartHH6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].StartHour):0),
            StartHH7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].StartHour):0),
            StartHH8 = (byte)(elecPriceModelVersionDetails.Count > 7?Convert.ToByte(elecPriceModelVersionDetails[7].StartHour) : 0),
            StartHH9 = (byte)(elecPriceModelVersionDetails.Count > 8?Convert.ToByte(elecPriceModelVersionDetails[8].StartHour) : 0),
            StartHH10 = (byte)(elecPriceModelVersionDetails.Count > 9?Convert.ToByte(elecPriceModelVersionDetails[9].StartHour) : 0),
            StartHH11 = (byte)(elecPriceModelVersionDetails.Count > 10?Convert.ToByte(elecPriceModelVersionDetails[10].StartHour) : 0),
            StartHH12 = (byte)(elecPriceModelVersionDetails.Count > 11?Convert.ToByte(elecPriceModelVersionDetails[11].StartHour) : 0),
            StartHH13 = (byte)(elecPriceModelVersionDetails.Count > 12?Convert.ToByte(elecPriceModelVersionDetails[12].StartHour) : 0),
            StartHH14 = (byte)(elecPriceModelVersionDetails.Count > 13?Convert.ToByte(elecPriceModelVersionDetails[13].StartHour) : 0),

            StartMM1 = (byte)(elecPriceModelVersionDetails.Count > 0?Convert.ToByte(elecPriceModelVersionDetails[0].StartMinute):0),
            StartMM2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].StartMinute):0),
            StartMM3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].StartMinute):0),
            StartMM4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].StartMinute):0),
            StartMM5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].StartMinute):0),
            StartMM6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].StartMinute):0),
            StartMM7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].StartMinute) : 0),
            StartMM8 = (byte)(elecPriceModelVersionDetails.Count > 7 ? Convert.ToByte(elecPriceModelVersionDetails[7].StartMinute) : 0),
            StartMM9 = (byte)(elecPriceModelVersionDetails.Count > 8 ? Convert.ToByte(elecPriceModelVersionDetails[8].StartMinute) : 0),
            StartMM10 = (byte)(elecPriceModelVersionDetails.Count > 9 ? Convert.ToByte(elecPriceModelVersionDetails[9].StartMinute) : 0),
            StartMM11 = (byte)(elecPriceModelVersionDetails.Count > 10 ? Convert.ToByte(elecPriceModelVersionDetails[10].StartMinute) : 0),
            StartMM12 = (byte)(elecPriceModelVersionDetails.Count > 11 ? Convert.ToByte(elecPriceModelVersionDetails[11].StartMinute) : 0),
            StartMM13 = (byte)(elecPriceModelVersionDetails.Count > 12 ? Convert.ToByte(elecPriceModelVersionDetails[12].StartMinute) : 0),
            StartMM14 = (byte)(elecPriceModelVersionDetails.Count > 13 ? Convert.ToByte(elecPriceModelVersionDetails[13].StartMinute) : 0),


            TimePeak1 = (byte)(elecPriceModelVersionDetails.Count > 0 ? Convert.ToByte(elecPriceModelVersionDetails[0].Type):0),
            TimePeak2 = (byte)(elecPriceModelVersionDetails.Count > 1?Convert.ToByte(elecPriceModelVersionDetails[1].Type):0),
            TimePeak3 = (byte)(elecPriceModelVersionDetails.Count > 2?Convert.ToByte(elecPriceModelVersionDetails[2].Type):0),
            TimePeak4 = (byte)(elecPriceModelVersionDetails.Count > 3?Convert.ToByte(elecPriceModelVersionDetails[3].Type):0),
            TimePeak5 = (byte)(elecPriceModelVersionDetails.Count > 4?Convert.ToByte(elecPriceModelVersionDetails[4].Type):0),
            TimePeak6 = (byte)(elecPriceModelVersionDetails.Count > 5?Convert.ToByte(elecPriceModelVersionDetails[5].Type):0),
            TimePeak7 = (byte)(elecPriceModelVersionDetails.Count > 6?Convert.ToByte(elecPriceModelVersionDetails[6].Type):0),
            TimePeak8 = (byte)(elecPriceModelVersionDetails.Count > 7?Convert.ToByte(elecPriceModelVersionDetails[7].Type):0),
            TimePeak9 = (byte)(elecPriceModelVersionDetails.Count > 8?Convert.ToByte(elecPriceModelVersionDetails[8].Type):0),
            TimePeak10 = (byte)(elecPriceModelVersionDetails.Count > 9?Convert.ToByte(elecPriceModelVersionDetails[9].Type):0),
            TimePeak11 = (byte)(elecPriceModelVersionDetails.Count > 10 ? Convert.ToByte(elecPriceModelVersionDetails[10].Type):0),
            TimePeak12 = (byte)(elecPriceModelVersionDetails.Count > 11?Convert.ToByte(elecPriceModelVersionDetails[11].Type):0),
            TimePeak13 = (byte)(elecPriceModelVersionDetails.Count > 12?Convert.ToByte(elecPriceModelVersionDetails[12].Type):0),
            TimePeak14 = (byte)(elecPriceModelVersionDetails.Count > 13?Convert.ToByte(elecPriceModelVersionDetails[13].Type):0),
        };
        return setPeakValleyTime;
    }
}
