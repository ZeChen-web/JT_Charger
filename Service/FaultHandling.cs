using System.Collections.Concurrent;
using Autofac;
using Common.Const;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using Repository.Station;

namespace Service;

public class FaultHandling
{
    public static EquipAlarmDefineRepository EquipAlarmDefineRepository =
        AppInfo.Container.Resolve<EquipAlarmDefineRepository>();

    public static EquipAlarmRecordRepository EquipAlarmRecordRepository =
        AppInfo.Container.Resolve<EquipAlarmRecordRepository>();

    public static EquipAlarmProcessRecordRepository EquipAlarmProcessRecordRepository =
        AppInfo.Container.Resolve<EquipAlarmProcessRecordRepository>();

    //防止数据库一直在查数据吃性能

    #region 报警内容对应信息

    public static List<EquipAlarmDefine> _equipAlarmDefines { get; set; }

    public static List<EquipAlarmDefine> equipAlarmDefines
    {
        get
        {
            if (_equipAlarmDefines != null && _equipAlarmDefines.Count > 0)
            {
                return _equipAlarmDefines;
            }

            return EquipAlarmDefineRepository.Query();
        }
    }

    #endregion

    #region 实时报警信息

    public static DateTime getEquipAlarm { get; set; } = DateTime.Now.AddSeconds(-5);

    public static List<EquipAlarmRecord> _equipAlarmRecords { get; set; }

    public static List<EquipAlarmRecord> equipAlarmRecords
    {
        get
        {
            if ((getEquipAlarm - DateTime.Now).TotalSeconds >= 5)
            {
                getEquipAlarm = DateTime.Now;
                _equipAlarmRecords = EquipAlarmRecordRepository.Query();
            }

            return _equipAlarmRecords;
        }
    }

    #endregion

    // key: EquipType_EquipCode
    // value: 当前报警集合
    private static readonly ConcurrentDictionary<string, HashSet<string>> _runtimeAlarm
        = new();

    private static Dictionary<string, EquipAlarmDefine> _alarmDefineMap;

    private static Dictionary<string, EquipAlarmDefine> AlarmDefineMap
    {
        get
        {
            if (_alarmDefineMap != null)
                return _alarmDefineMap;

            var list = EquipAlarmDefineRepository.Query();

            _alarmDefineMap = list.ToDictionary(
                x => $"{x.EquipTypeCode}_{x.ErrorCode}",
                x => x);

            return _alarmDefineMap;
        }
    }


    public static void SaveAlarmInfo(
    Dictionary<string, bool> lstAlarm,
    EquipmentType equipTypeCode,
    string equipCode)
{
    var key = $"{equipTypeCode}_{equipCode}";

    var runtime = _runtimeAlarm.GetOrAdd(key, _ => new HashSet<string>());

    lock (runtime)
    {
        #region 新增报警（true 才算报警）

        var newAlarms = lstAlarm
            .Where(x => x.Value)              // ✅ 只处理 true
            .Select(x => x.Key)
            .Except(runtime)
            .ToList();

        foreach (var errorCode in newAlarms)
        {
            var mapKey = $"{(int)equipTypeCode}_{errorCode}";

            if (!AlarmDefineMap.TryGetValue(mapKey, out var define))
                continue;

            runtime.Add(errorCode);

            var record = new EquipAlarmRecord
            {
                EquipTypeCode = define.EquipTypeCode,
                EquipCode = equipCode,
                ErrorCode = errorCode,
                ErrorLevel = define.ErrorLevel,
                ErrorMsg = define.ErrorMsg,
                ProcessMethod = define.ProcessMethod,
                StartTime = DateTime.Now
            };

            EquipAlarmRecordRepository.Insert(record);
        }

        #endregion

        #region 消失报警（必须 value == false）

        var lostAlarms = runtime
            .Where(code => lstAlarm.ContainsKey(code) && lstAlarm[code] == false) // ✅ 关键改动
            .ToList();

        foreach (var errorCode in lostAlarms)
        {
            runtime.Remove(errorCode);

            var record = EquipAlarmRecordRepository
                .QueryListByClause(x =>
                    x.EquipTypeCode == (int)equipTypeCode &&
                    x.EquipCode == equipCode &&
                    x.ErrorCode == errorCode)
                .FirstOrDefault();

            if (record == null)
                continue;

            EquipAlarmProcessRecordRepository.Insert(
                new EquipAlarmProcessRecord
                {
                    EquipTypeCode = record.EquipTypeCode,
                    EquipCode = record.EquipCode,
                    ErrorCode = record.ErrorCode,
                    ErrorLevel = record.ErrorLevel,
                    ErrorMsg = record.ErrorMsg,
                    ProcessMethod = record.ProcessMethod,
                    StartTime = record.StartTime,
                    ProcessTime = DateTime.Now
                });

            EquipAlarmRecordRepository.Delete(record);
        }

        #endregion
    }
}

    /// <summary>
    /// 这个是故障触发直接保存到设备已处理报警
    /// </summary>
    /// <summary>
    /// 故障已结束事件（实时表可能不存在）
    /// 直接写历史记录
    /// </summary>
    public static void SaveAlarmInfoToProcessRecord(
        Dictionary<string, bool> lstAlarm,
        EquipmentType equipTypeCode,
        string equipCode)
    {
        if (lstAlarm == null || lstAlarm.Count == 0)
            return;

        foreach (var errorCode in lstAlarm.Keys)
        {
            // 1. 优先从定义表拿信息（不依赖实时表）
            var define = equipAlarmDefines.FirstOrDefault(x =>
                x.EquipTypeCode == (int)equipTypeCode &&
                x.ErrorCode == errorCode);

            EquipAlarmProcessRecord record = new EquipAlarmProcessRecord
            {
                EquipTypeCode = (int)equipTypeCode,
                EquipCode = equipCode,
                ErrorCode = errorCode,
                StartTime = DateTime.Now, // 没有起始时间，只能用当前时间
                ProcessTime = DateTime.Now
            };

            // 2. 如果有定义，补充信息
            if (define != null)
            {
                record.ErrorLevel = define.ErrorLevel;
                record.ErrorMsg = define.ErrorMsg;
                record.ProcessMethod = define.ProcessMethod;
            }

            // 3. 直接入历史表
            EquipAlarmProcessRecordRepository.Insert(record);
        }
    }
}