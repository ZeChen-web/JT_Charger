using HybirdFrameworkCore.Autofac.Attribute;
using Service.Charger.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackExchange.Redis.LCSMatchResult;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.8 电池包实时遥信上报（站内充电模式有电池包时周期性上传）
    /// </summary>
    public class RemoteSignaling : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }
        /// <summary>
        /// BMS 当前均衡状态 0：不平衡 1：平衡
        /// </summary>
        [Property(15, 1)]
        public byte CurrentBMS { get; set; }
        /// <summary>
        /// 附件继电器状态 0：开启 1：闭合
        /// </summary>
        [Property(14, 1)]
        public byte AttachmentRelayStatus { get; set; }
        /// <summary>
        /// BMS 当前状态
        /// <para>0：高压开启</para>
        /// <para>1：预先充电</para>
        /// <para>2 高压关断</para>
        /// <para>3 高压上电故障</para>
        /// </summary>
        [Property(12, 2)]
        public byte CurrentStatusBMS { get; set; }
        /// <summary>
        /// B2V_ST1 的生命信  0~14 循环，15:信号无效
        /// </summary>
        [Property(8, 4)]
        public byte B2VST1Msg { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        [Property(23, 1)]
        public byte Hold { get; set; }
        /// <summary>
        /// 最高报警等级
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2: 二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// <para></para>
        /// </summary>
        [Property(21, 2)]
        public byte MaximumAlarmLevel { get; set; }
        /// <summary>
        /// 充电状态
        /// <para>0：可以充电</para>
        /// <para>1：正在充电</para>
        /// <para>2：充电结束</para>
        /// <para>3：充电故障</para>
        /// </summary>
        [Property(19, 2)]
        public byte ChargingState { get; set; }
        /// <summary>
        /// 充电模式
        /// <para>0：预留</para>
        /// <para>1：直流充电</para>
        /// <para>2：交流充电</para>
        /// <para>3：其他充电</para>
        /// </summary>
        [Property(17, 2)]
        public byte ChargingMode { get; set; }
        /// <summary>
        /// 充电枪连接状态 0： 未连接 1：连接
        /// </summary>
        [Property(16, 1)]
        public byte ConnectionStatus { get; set; }
        /// <summary>
        /// PACK 欠压报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(30, 2)]
        public byte PackUndervoltage { get; set; }
        /// <summary>
        /// PACK 过压报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(28, 2)]
        public byte PackOvervoltage { get; set; }
        /// <summary>
        /// 电芯温度过高报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(26, 2)]
        public byte ExcessiveTemperature { get; set; }
        /// <summary>
        /// 电芯温差异常报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(24, 2)]
        public byte AbnormalTemperatureDifference { get; set; }
        /// <summary>
        /// 绝缘报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(38, 2)]
        public byte InsulationAlarm { get; set; }
        /// <summary>
        /// 单体电压欠压报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(36, 2)]
        public byte UndervoltageAlarm { get; set; }
        /// <summary>
        /// 单体电压过高报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(34, 2)]
        public byte VoltageTooHigh { get; set; }
        /// <summary>
        /// SOC过低报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(32, 2)]
        public byte LowSOCAlarm { get; set; }
        /// <summary>
        /// 电芯温度过低报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(46, 2)]
        public byte LowTemperature { get; set; }
        /// <summary>
        /// 放电电流过大报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(44, 2)]
        public byte ExcessiveDischargeCurrent { get; set; }
        /// <summary>
        /// 充电电流过大报警
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(42, 2)]
        public byte ExcessiveChargingCurrent { get; set; }
        /// <summary>
        /// 单体压差过大
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(40, 2)]
        public byte IndividualPressureDifference { get; set; }
        /// <summary>
        /// BMS 系统不匹配报警 0：正 常 1：故障
        /// </summary>
        [Property(55, 1)]
        public byte BMSSystemMismatchAlarm { get; set; }
        /// <summary>
        /// BMS 内部通讯故障 0：正 常 1：故障
        /// </summary>
        [Property(54, 1)]
        public byte BMSCommunicationFailure { get; set; }
        /// <summary>
        /// SOC 跳变报警 0：正 常 1：故障
        /// </summary>
        [Property(53, 1)]
        public byte SOCJumpAlarm { get; set; }
        /// <summary>
        /// SOC 过高报警
        /// </summary>
        [Property(52, 1)]
        public byte SOCHighAlarm { get; set; }
        /// <summary>
        /// BMS 硬件故障
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(50, 2)]
        public byte BMSHardwareFailure { get; set; }
        /// <summary>
        /// 支路压差过大报警（存在并联支路的系统）
        /// <para>0：无故障</para>
        /// <para>1：一级报警故障</para>
        /// <para>2：二级普通故障</para>
        /// <para>3：三级严重故障</para>
        /// </summary>
        [Property(48, 2)]
        public byte BranchPressureDifference { get; set; }
        /// <summary>
        /// GBT32960.3 中规定的故障数目（当前时刻发生的） 0：正常 1：故障
        /// </summary>
        [Property(59, 5)]
        public byte CurrentTimeOccurrence { get; set; }
        /// <summary>
        /// 火灾报警 0：正常 1：故障
        /// </summary>
        [Property(58, 1)]
        public byte FireAlarm { get; set; }
        /// <summary>
        /// 烟雾报警 0：正常 1：故障
        /// </summary>
        [Property(57, 1)]
        public byte SmokeAlarm { get; set; }
        /// <summary>
        /// 高压互锁报警 0：正常 1：故障
        /// </summary>
        [Property(56, 1)]
        public byte HighVoltageInterlockAlarm { get; set; }
    }
}
