namespace Entity.Dto.Resp;

/// <summary>
/// 电池包报警状态
/// </summary>
public class UpAlarmResp
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public byte RecordType { get; set; }

    /// <summary>
    /// PGN 码
    /// </summary>
    public byte Pgn1 { get; set; }

    public byte Pgn2 { get; set; }
    public byte Pgn3 { get; set; }

    /// <summary>
    /// 单体蓄电池或蓄电池模块电压越限
    /// </summary>
    public byte SingleBattery { get; set; }

    /// <summary>
    /// 电压偏差越限
    /// </summary>
    public byte VoltageOvershoot { get; set; }

    /// <summary>
    /// 温度越限
    /// </summary>
    public byte TemperatureExceedance { get; set; }

    /// <summary>
    /// 温度偏差越限
    /// </summary>
    public byte TemperatureDifference { get; set; }

    /// <summary>
    /// SOC 低
    /// </summary>
    public byte LowSOC { get; set; }

    /// <summary>
    /// 放电电流越限
    /// </summary>
    public byte DischargeCurrent { get; set; }

    /// <summary>
    /// 充电电流限
    /// </summary>
    public byte ChargingCurrentLimit { get; set; }

    /// <summary>
    /// 总正负极柱温度越限
    /// </summary>
    public byte TotalTemp { get; set; }

    /// <summary>
    /// 电池系统故障码
    ///
    /// 0x1011 单体温度过高一级
    /// 0x1012 单体温度过高二级
    /// 0x1013 单体温度过高三级
    /// 0x1021 单体温度过低一级
    /// 0x1022 单体温度过低二级
    /// 0x1023 单体温度过低三级
    /// 0x1031 单体过压一级
    /// 0x1032 单体过压二级
    /// 0x1033 单体过压三级
    /// 0x1041 单体欠压一级
    /// 0x1042 单体欠压二级
    /// 0x1043 单体欠压三级
    /// 0x10A1 电池包总压过高一级
    /// 0x10A2 电池包总压过高二级
    /// 0x10A3 电池包总压过高三级
    /// 0x10B1 电池包总压过低一级
    /// 0x10B2 电池包总压过低二级
    /// 0x10B3 电池包总压过低三级
    /// 0x1061 放电过流一级
    /// 0x1062 放电过流二级
    /// 0x1063 放电过流三级
    /// 0x1091 充电过流一级
    /// 0x1092 充电过流二级
    /// 0x1093 充电过流三级
    /// 0x10E1 单体压差过大一级
    /// 0x10E2 单体压差过大二级
    /// 0x10E3 单体压差过大三级
    /// 0x10D1 单体温差过大一级
    /// 0x10D2 单体温差过大二级
    /// 0x10D3 单体温差过大三级
    /// 0x10C1 绝缘过低一级
    /// 0x10C2 绝缘过低二级
    /// 0x10C3 绝缘过低三级
    /// 0x10F1 SOC 过低一级
    /// 0x10F2 SOC 过低二级
    /// 0x10F3 SOC 过低三级
    /// 0x1111 供电电压过低一级
    /// 0x1112 供电电压过低二级
    /// 0x1113 供电电压过低三级
    /// 0x1121 供电电压过高一级
    /// 0x1122 供电电压过高二级
    /// 0x1123 供电电压过高三级
    /// 0x1141 电池温升过快一级
    /// 0x1142 电池温升过快二级
    /// 0x1143 电池温升过快三级
    /// 0x1103 Pack 回路断开
    /// 0x2013 从板通讯丢失
    /// 0x2033 充电机丢失
    /// 0x2043 绝缘仪通讯丢失
    /// 0x2053 电流传感器通讯丢失
    /// 0x2073 热管理机组通讯丢失
    /// 0x20E3 电流传感器采样异常
    /// 0x20D3 绝缘采样异常
    /// 0x4013 主正继电器开路故障
    /// 0x4023 主负继电器开路故障
    /// 0x40F2 主正继电器粘连故障
    /// 0x4102 主负继电器粘连故障
    /// 0x5013 外部短路故障
    /// 0x5032 高压互锁故障
    /// 0x5053 CC2 电压异常
    /// 0x5153 充电枪连接故障
    /// 0x6033 充电系统不匹配故障
    /// 0x6023 充电电流异常
    /// 0x7022 均衡故障
    /// 0x8023 火灾报警
    /// 0x8013 自保护故障
    /// 0x8033 电池过放
    /// 0x8043 电池过充
    /// 0x5123 预充故障
    /// 0x90B3 电池电压采样线开路
    /// 0x90C3 电池温度采样线开路
    /// 0x90E3 电池电压采样异常
    /// 0x90F3 电池温度采样异常
    /// </summary>
    public short BatteryFaultCode { get; set; }

    /// <summary>
    /// 高压绝缘低
    /// </summary>
    public byte HighVoltageLow { get; set; }

    /// <summary>
    /// 单体蓄电池或蓄电池模块电压越极限
    /// </summary>
    public byte MonomerLimit { get; set; }

    /// <summary>
    /// 电压偏差越极限
    /// </summary>
    public byte VoltageDifference { get; set; }

    /// <summary>
    /// 温度越极限
    /// </summary>
    public byte TemperatureOvershoot { get; set; }

    /// <summary>
    /// 温度偏差越极限
    /// </summary>
    public byte TempDifference { get; set; }

    /// <summary>
    /// SOC 极低
    /// </summary>
    public byte VeryLowSoc { get; set; }

    /// <summary>
    /// 放电电流越极限
    /// </summary>
    public byte DischargeCurrentLimit { get; set; }

    /// <summary>
    /// 充电电流越极限
    /// </summary>
    public byte ChargingCurrent { get; set; }

    /// <summary>
    /// 总正负极柱温度越极限
    /// </summary>
    public byte TotalTempLimit { get; set; }

    /// <summary>
    /// 高压绝缘极低
    /// </summary>
    public byte HighVoltageInsulation { get; set; }

    /// <summary>
    /// 硬件故障
    /// </summary>
    public byte HardwareFailure { get; set; }

    /// <summary>
    /// 保留
    /// </summary>
    public byte Reserve { get; set; }
}