using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.Charger.Client;
using Service.Charger.Msg.Charger.Resp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Const;
using Entity.DbModel.Station;
using Repository.Station;

namespace Service.Charger.Handler
{
    [Order(8)]
    [Scope("InstancePerDependency")]
    public class UpAlarmHandler : SimpleChannelInboundHandler<UpAlarm>, IBaseHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UpAlarmHandler));

        private EquipAlarmRecordRepository EquipAlarmRecordRepository { get; set; }
        private EquipAlarmProcessRecordRepository EquipAlarmProcessRecordRepository { get; set; }
        private EquipAlarmDefineRepository EquipAlarmDefineRepository { get; set; }

        public UpAlarmHandler(
            EquipAlarmRecordRepository _EquipAlarmRecordRepository,
            EquipAlarmProcessRecordRepository _EquipAlarmProcessRecordRepository,
            EquipAlarmDefineRepository _EquipAlarmDefineRepository)
        {
            EquipAlarmRecordRepository = _EquipAlarmRecordRepository;
            EquipAlarmProcessRecordRepository = _EquipAlarmProcessRecordRepository;
            EquipAlarmDefineRepository = _EquipAlarmDefineRepository;
        }

        /// <summary>
        /// 报警数据处理
        /// </summary>
        /// <param name="svalue">报警类型</param>
        /// <param name="equipCode">报警编码</param>
        /// <param name="sn">设备编码</param>
        private void AlarmMsg(bool bvalue, string equipCode, string sn)
        {
            if (bvalue)
            {
                var define = EquipAlarmDefineRepository.QueryByClause(i => i.EquipCode == equipCode);
                //查询数据表中是否有此报警信息，有则不用处理，无则添加实时报警
                EquipAlarmRecord equipAlarm =
                    EquipAlarmRecordRepository.QueryByClause(i => i.ErrorCode == sn + define.ErrorCode);
                if (equipAlarm == null) //实时报警表中没有这条报警信息
                {
                    EquipAlarmRecord record = new EquipAlarmRecord()
                    {
                        EquipTypeCode = equipAlarm.EquipTypeCode,
                        EquipCode = sn,
                        ErrorCode = equipAlarm.ErrorCode,
                        ErrorLevel = equipAlarm.ErrorLevel,
                        ErrorMsg = equipAlarm.ErrorMsg,
                        ProcessMethod = equipAlarm.ProcessMethod,
                        StartTime = DateTime.Now
                    };
                    EquipAlarmRecordRepository.Insert(record);
                }
            }
            else
            {
                var define = EquipAlarmDefineRepository.QueryByClause(i => i.EquipCode == equipCode);
                //查询数据表中是否有此报警信息，有则不用处理，无则添加实时报警
                EquipAlarmRecord equipAlarm =
                    EquipAlarmRecordRepository.QueryByClause(i => i.ErrorCode == sn + define.ErrorCode);
                if (equipAlarm != null) //报警消除实时报警里面数据还有此报警
                {
                    EquipAlarmProcessRecord EquipAlarmProcessRecord = new EquipAlarmProcessRecord();
                    EquipAlarmProcessRecord.EquipTypeCode = equipAlarm.EquipTypeCode;
                    EquipAlarmProcessRecord.EquipCode = equipAlarm.EquipCode;
                    EquipAlarmProcessRecord.ErrorCode = equipAlarm.ErrorCode;
                    EquipAlarmProcessRecord.ErrorLevel = equipAlarm.ErrorLevel;
                    EquipAlarmProcessRecord.ErrorMsg = equipAlarm.ErrorMsg;
                    EquipAlarmProcessRecord.ProcessMethod = equipAlarm.ProcessMethod;
                    EquipAlarmProcessRecord.StartTime = equipAlarm.StartTime;
                    EquipAlarmProcessRecord.ProcessTime = DateTime.Now;

                    EquipAlarmProcessRecordRepository.Insert(EquipAlarmProcessRecord);

                    EquipAlarmRecordRepository.Delete(equipAlarm);
                }
            }
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, UpAlarm msg)
        {
            if (ClientMgr.TryGetClient(ctx.Channel, out string sn, out var client))
            {
                Log.Info($"receive {msg} from {sn}");
                client.UpAlarm = msg;
                #region 故障处理

                string bms = "bms";
                List<string> lstAlarm = new List<string>();

                if (msg.SingleBattery == 1)
                {
                    lstAlarm.Add(bms + sn + "100");
                }
                else if (msg.SingleBattery == 2)
                {
                    lstAlarm.Add(bms + sn + "101");
                }

                if (msg.VoltageOvershoot == 2)
                {
                    lstAlarm.Add(bms + sn + "102");
                }

                if (msg.TemperatureExceedance == 1)
                {
                    lstAlarm.Add(bms + sn + "103");
                }
                else if (msg.TemperatureExceedance == 2)
                {
                    lstAlarm.Add(bms + sn + "104");
                }

                if (msg.TemperatureDifference == 2)
                {
                    lstAlarm.Add(bms + sn + "105");
                }

                if (msg.LowSOC == 1)
                {
                    lstAlarm.Add(bms + sn + "106");
                }

                if (msg.DischargeCurrent == 2)
                {
                    lstAlarm.Add(bms + sn + "107");
                }

                if (msg.ChargingCurrentLimit == 1)
                {
                    lstAlarm.Add(bms + sn + "108");
                }
                else if (msg.ChargingCurrentLimit == 2)
                {
                    lstAlarm.Add(bms + sn + "109");
                }

                if (msg.TotalTemp == 2)
                {
                    lstAlarm.Add(bms + sn + "110");
                }

                if (msg.HighVoltageLow == 2)
                {
                    lstAlarm.Add(bms + sn + "111");
                }

                if (msg.MonomerLimit == 1)
                {
                    lstAlarm.Add(bms + sn + "112");
                }
                else if (msg.MonomerLimit == 1)
                {
                    lstAlarm.Add(bms + sn + "113");
                }

                if (msg.VoltageDifference == 2)
                {
                    lstAlarm.Add(bms + sn + "114");
                }

                if (msg.TemperatureOvershoot == 1)
                {
                    lstAlarm.Add(bms + sn + "115");
                }
                else if (msg.TemperatureOvershoot == 2)
                {
                    lstAlarm.Add(bms + sn + "116");
                }

                if (msg.TempDifference == 2)
                {
                    lstAlarm.Add(bms + sn + "117");
                }

                if (msg.VeryLowSoc == 1)
                {
                    lstAlarm.Add(bms + sn + "118");
                }

                if (msg.DischargeCurrentLimit == 2)
                {
                    lstAlarm.Add(bms + sn + "119");
                }

                if (msg.ChargingCurrent == 2)
                {
                    lstAlarm.Add(bms + sn + "120");
                }

                if (msg.TotalTempLimit == 2)
                {
                    lstAlarm.Add(bms + sn + "121");
                }

                if (msg.HighVoltageInsulation == 1)
                {
                    lstAlarm.Add(bms + sn + "122");
                }

                if (msg.HardwareFailure == 1)
                {
                    lstAlarm.Add(bms + sn + "123");
                }

                if (msg.BatteryFaultCode != 0)
                {
                    lstAlarm.Add(bms + sn + msg.BatteryFaultCode.ToString());
                }

                SaveAlarmInfo(lstAlarm);

                #endregion
                
            }
        }

        #region 故障处理方法

        private void SaveAlarmInfo(List<string> lstAlarm)
        {
            string bms = "bms";

            if (lstAlarm.Count > 0)
            {
                #region 有报警比较两边差异，新出现的报警就添加，消失的报警就处理并记录

                var lstEquipAlarmRecord = EquipAlarmRecordRepository.QueryListByClause(i => i.EquipTypeCode == (int)EquipmentType.BMS);
                var sqllstAlarm = lstEquipAlarmRecord.Select(obj => obj.ErrorCode).ToList();

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
                            EquipAlarmDefineRepository.SelectByEquipCodeAndErrorCode((int)EquipmentType.BMS, bms);
                        if (alarmDefine != null)
                        {
                            EquipAlarmRecord record = new EquipAlarmRecord()
                            {
                                EquipTypeCode = alarmDefine.EquipTypeCode,
                                EquipCode = alarmDefine.EquipCode,
                                ErrorCode = alarmDefine.ErrorCode,
                                ErrorLevel = alarmDefine.ErrorLevel,
                                ErrorMsg = alarmDefine.ErrorMsg,
                                ProcessMethod = alarmDefine.ProcessMethod,
                                StartTime = DateTime.Now
                            };
                            EquipAlarmRecordRepository.Insert(record);
                        }
                    }
                }

                if (uniqueToList2.Count > 0)
                {
                    //这些是要清除实时报警，并且处理记录的。
                    // 使用LINQ找出ErrorCode在uniqueToList2中的EquipAlarmRecord对象
                    List<EquipAlarmRecord> filteredObjectList = lstEquipAlarmRecord
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

                #endregion
            }
            else
            {
                #region 没报警把已处理记录更新并删除实时报警

                var lstEquipAlarmRecord = EquipAlarmRecordRepository.QueryListByClause(i => i.EquipTypeCode == (int)EquipmentType.BMS);
                if (lstEquipAlarmRecord.Count > 0)
                {
                    foreach (var VARIABLE in lstEquipAlarmRecord)
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
                }

                EquipAlarmRecordRepository.Delete(lstEquipAlarmRecord);

                #endregion
            }
        }

        #endregion
        
    }
}