using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Common;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Host.Resp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Const;
using Entity.DbModel.Station;
using Repository;
using Repository.Station;

namespace Service.Charger.Handler
{
    /// <summary>
    /// 遥信数据上报Handler
    /// </summary>
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class UploadRemoteSignalDataHandler : SimpleChannelInboundHandler<UploadRemoteSignalData>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UploadRemoteSignalDataHandler));
        private BinInfoRepository BinInfoRepository { get; set; }
        private EquipAlarmRecordRepository EquipAlarmRecordRepository { get; set; }
        private  EquipAlarmProcessRecordRepository EquipAlarmProcessRecordRepository { get; set; }
        private  EquipAlarmDefineRepository EquipAlarmDefineRepository { get; set; }
        public UploadRemoteSignalDataHandler(BinInfoRepository _binInfoRepository,
            EquipAlarmRecordRepository _EquipAlarmRecordRepository,
            EquipAlarmProcessRecordRepository _EquipAlarmProcessRecordRepository,
            EquipAlarmDefineRepository _EquipAlarmDefineRepository)
        {
            BinInfoRepository = _binInfoRepository;
            EquipAlarmRecordRepository = _EquipAlarmRecordRepository;
            EquipAlarmProcessRecordRepository = _EquipAlarmProcessRecordRepository;
            EquipAlarmDefineRepository = _EquipAlarmDefineRepository;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, UploadRemoteSignalData msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            {
                //存储日志
                Log.Info($"receive {msg} from {sn}");

                client.Workstate = msg.WorkStatus;
                client.IsCharged = msg.WorkStatus == 1 ? true : false;
                client.TotalError = msg.TotalError;
                client.TotalWarning = msg.TotalWarning;
                client.UploadRemoteSignalData = msg;

                //Desc:充电状态;0-未知；1-正在充电；2-无电池；3-禁用；4-充电完成
                if (msg.WorkStatus == 1)
                    BinInfoRepository.Update(i => i.ChargeStatus == msg.WorkStatus, i => i.ChargerNo == sn);
                else if (msg.WorkStatus == 2 || msg.WorkStatus == 0)
                {
                    BinInfoRepository.Update(i => i.ChargeStatus == 4, i => i.ChargerNo == sn);
                }

                #region 充电机故障显示

                //if (msg.TotalWarning||msg.TotalError)
                {
                    var lstEquipAlarmDefine = EquipAlarmDefineRepository.QueryListByClause(i => i.EquipCode == "充电机");

                    List<string> lstAlarm = new List<string>();

                    void Alarm(string number)
                    {
                        var alarm = lstEquipAlarmDefine.Where(i => i.ErrorCode == number);
                        if (alarm.Count() > 0)
                            lstAlarm.Add(sn + alarm.ToList()[0].ErrorCode);
                    }

                    if (msg.EmergencyStop)
                        Alarm("1");
                    if (msg.SmokeFault)
                        Alarm("2");
                    if (msg.ChargeACInputCircuitBreakerFault)
                        Alarm("3");
                    if (msg.DcBusPositElecContactorRefuFault)
                        Alarm("4");
                    if (msg.DcBusNegatElecContactorRefuFault)
                        Alarm("5");
                    if (msg.DcBusPositElecFusesFault)
                        Alarm("6");
                    if (msg.DDcBusNegatElecFusesFault)
                        Alarm("7");
                    if (msg.ChargingInterfaceLockError)
                        Alarm("8");
                    if (msg.ChargerFanError)
                        Alarm("9");
                    if (msg.ArresterError)
                        Alarm("10");
                    if (msg.InsulationDetectionAlarm)
                        Alarm("11");
                    if (msg.InsulationDetectionError)
                        Alarm("12");
                    if (msg.BatteryPolarityReverseError)
                        Alarm("13");
                    if (msg.VeConGuidanceFailure)
                        Alarm("14");
                    if (msg.ChargingOverTempError)
                        Alarm("15");
                    if (msg.InterfaceOverFaulty)
                        Alarm("16");
                    if (msg.ChargingGunNotHomingError)
                        Alarm("17");
                    if (msg.BmsConnError)
                        Alarm("18");
                    if (msg.ChargerInputOverVoltageError)
                        Alarm("19");
                    if (msg.ChargerInputUnderVoltageError)
                        Alarm("20");
                    if (msg.DcBusOutputOverVoltageError)
                        Alarm("21");
                    if (msg.DcBusOutputUnderVoltageError)
                        Alarm("22");
                    if (msg.DcBusOutputOverCurrentError)
                        Alarm("23");
                    if (!msg.VehicleConnStatus)
                        Alarm("24");
                    if (!msg.ChargeStationGunHolderStatus)Alarm("25");
                    if (!msg.ChargingInterfaceLockStatus)Alarm("26");
                    if (msg.PositiveDcTransmissionContactorStatus)
                        Alarm("27");
                    if (msg.NegativeDcTransmissionContactorStatus)
                        Alarm("28");
                    if (msg.EntranceGuardError)
                        Alarm("29");
                    if (msg.PConA3dhesionFailure)
                        Alarm("30");
                    if (msg.NConadhesionFailure)
                        Alarm("31");
                    if (msg.ReliefCircuitError)
                        Alarm("32");
                    if (msg.ConActivated)
                        Alarm("33");
                    if (msg.ConAdhesionFailure)
                        Alarm("34");
                    if (msg.AuxiliaryPowerError)
                        Alarm("35");
                    if (msg.ModuleOutputReverseError)
                        Alarm("36");
                    if (msg.AcContactorStatus)
                        Alarm("37");
                    if (msg.ChargingGunOverTempWarning)
                        Alarm("38");
                    if (msg.ChargerOverTempWarning)
                        Alarm("39");
                    if (msg.MeterConnError)
                        Alarm("40");
                    if (msg.MeterDataError)
                        Alarm("41");
                    if (msg.WaterloggingWarning)
                        Alarm("42");
                    if (msg.ReversePowerWarning)
                        Alarm("43");
                    if (msg.BatteryPackAuxiliaryPowerStatus)
                        Alarm("44");


                    //查询当前充电机的实时报警信息
                    var lstNowEquipAlarmRecord = EquipAlarmRecordRepository.QueryListByClause(i => i.EquipCode == sn);
                    var sqllstAlarm = lstNowEquipAlarmRecord.Select(obj => obj.ErrorCode).ToList(); //当前报警列表

                    // 找出实时报警中存在但数据库中不存在的元素  
                    List<string> uniqueToList1 = lstAlarm.Except(sqllstAlarm).ToList();

                    // 找出数据库中存在但实时报警中不存在的元素  
                    List<string> uniqueToList2 = sqllstAlarm.Except(lstAlarm).ToList();
                    if (uniqueToList1.Count > 0)
                    {
                        //这里要添加新的报警数据
                        foreach (var errorCode in uniqueToList1)
                        {
                            EquipAlarmDefine? alarmDefine =
                                EquipAlarmDefineRepository.QueryByClause(i => i.ErrorCode == errorCode.Replace(sn, ""));
                            if (alarmDefine != null)
                            {
                                EquipAlarmRecord record = new EquipAlarmRecord()
                                {
                                    EquipTypeCode = alarmDefine.EquipTypeCode,
                                    EquipCode = sn,
                                    ErrorCode = errorCode,
                                    ErrorLevel = alarmDefine.ErrorLevel,
                                    ErrorMsg = alarmDefine.ErrorMsg,
                                    ProcessMethod = alarmDefine.ProcessMethod,
                                    StartTime = DateTime.Now
                                };
                                EquipAlarmRecordRepository.Insert(record);
                            }
                        }
                    }
                    else if (uniqueToList2.Count > 0)
                    {
                        //这些是要清除实时报警，并且处理记录的。
                        // 使用LINQ找出ErrorCode在uniqueToList2中的EquipAlarmRecord对象  
                        List<EquipAlarmRecord> filteredObjectList = lstNowEquipAlarmRecord
                            .Where(obj => uniqueToList2.Contains(obj.ErrorCode))
                            .ToList();
                        foreach (var VARIABLE in filteredObjectList)
                        {
                            EquipAlarmProcessRecord EquipAlarmProcessRecord = new EquipAlarmProcessRecord();
                            EquipAlarmProcessRecord.EquipTypeCode = VARIABLE.EquipTypeCode;
                            EquipAlarmProcessRecord.EquipCode = VARIABLE.EquipCode;
                            EquipAlarmProcessRecord.ErrorCode = VARIABLE.ErrorCode;
                            EquipAlarmProcessRecord.ErrorLevel = VARIABLE.ErrorLevel;
                            EquipAlarmProcessRecord.ErrorMsg = VARIABLE.ErrorMsg;
                            EquipAlarmProcessRecord.ProcessMethod = VARIABLE.ProcessMethod;
                            EquipAlarmProcessRecord.StartTime = VARIABLE.StartTime;
                            EquipAlarmProcessRecord.ProcessTime = DateTime.Now;

                            EquipAlarmProcessRecordRepository.Insert(EquipAlarmProcessRecord);
                        }

                        EquipAlarmRecordRepository.Delete(filteredObjectList);
                    }
                }
                //else
                //{
                /*var lstNowEquipAlarmRecord = EquipAlarmRecordRepository.QueryListByClause(i => i.EquipCode == sn);
                if (lstNowEquipAlarmRecord.Count > 0)
                {
                    foreach (var VARIABLE in lstNowEquipAlarmRecord)
                    {
                        EquipAlarmProcessRecord EquipAlarmProcessRecord = new EquipAlarmProcessRecord();
                        EquipAlarmProcessRecord.EquipTypeCode = VARIABLE.EquipTypeCode;
                        EquipAlarmProcessRecord.EquipCode = VARIABLE.EquipCode;
                        EquipAlarmProcessRecord.ErrorCode = VARIABLE.ErrorCode;
                        EquipAlarmProcessRecord.ErrorLevel = VARIABLE.ErrorLevel;
                        EquipAlarmProcessRecord.ErrorMsg = VARIABLE.ErrorMsg;
                        EquipAlarmProcessRecord.ProcessMethod = VARIABLE.ProcessMethod;
                        EquipAlarmProcessRecord.StartTime = VARIABLE.StartTime;
                        EquipAlarmProcessRecord.ProcessTime=DateTime.Now;

                        EquipAlarmProcessRecordRepository.Insert(EquipAlarmProcessRecord);
                    }
                }
                EquipAlarmRecordRepository.Delete(i=>i.Id>0);*/
                //}

                #endregion
            }
        }
    }
}