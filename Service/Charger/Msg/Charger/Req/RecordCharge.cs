using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.6 主动上送充电记录
    /// </summary>
    public class RecordCharge : ASDU
    {
        /// <summary>
        /// 充电流水号
        /// </summary>
        [Property(0, 32, PropertyReadConstant.Byte)]
        public string ChargerOrderNo { get; set; }

        /// <summary>
        /// 充电开始时间-秒
        /// </summary>
        [Property(256, 8)]
        public byte StartSecond { get; set; }

        /// <summary>
        /// 充电开始时间-分
        /// </summary>
        [Property(264, 8)]
        public byte StartMinute { get; set; }

        /// <summary>
        /// 充电开始时间-时
        /// </summary>
        [Property(272, 8)]
        public byte StartHour { get; set; }

        /// <summary>
        /// 充电开始时间-日
        /// </summary>
        [Property(280, 8)]
        public byte StartDay { get; set; }

        /// <summary>
        /// 充电开始时间-月
        /// </summary>
        [Property(288, 8)]
        public byte StartMonth { get; set; }

        /// <summary>
        /// 充电开始时间-年
        /// </summary>
        [Property(296, 8)]
        public byte StartYear { get; set; }

        /// <summary>
        /// 充电结束时间-秒
        /// </summary>
        [Property(304, 8)]
        public byte EndSecond { get; set; }

        /// <summary>
        /// 充电结束时间-分
        /// </summary>
        [Property(312, 8)]
        public byte EndMinute { get; set; }

        /// <summary>
        /// 充电结束时间-时
        /// </summary>
        [Property(320, 8)]
        public byte EndHour { get; set; }

        /// <summary>
        /// 充电结束时间-日
        /// </summary>
        [Property(328, 8)]
        public byte EndDay { get; set; }

        /// <summary>
        /// 充电结束时间-月
        /// </summary>
        [Property(336, 8)]
        public byte EndMonth { get; set; }

        /// <summary>
        /// 充电结束时间-年
        /// </summary>
        [Property(344, 8)]
        public byte EndYear { get; set; }

        /// <summary>
        /// 充电开始时间  秒-分-时-日-月-年
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 充电结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 1 枪充电前电能表数据
        /// </summary>
        [Property(352, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float FirstGunEnergyMeterDataBefore { get; set; }

        /// <summary>
        /// 1 枪充电后电能表数据
        /// </summary>
        [Property(384, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float FirstGunEnergyMeterDataAfter { get; set; }

        /// <summary>
        /// 2 枪充电前电能表数据
        /// </summary>
        [Property(416, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float SecondGunEnergyMeterDataBefore { get; set; }

        /// <summary>
        /// 2 枪充电后电能表数据
        /// </summary>
        [Property(448, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float SecondGunEnergyMeterDataAfter { get; set; }

        /// <summary>
        /// 充电电量
        /// </summary>
        [Property(480, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPower { get; set; }

        /// <summary>
        /// 充电前SOC
        /// </summary>
        [Property(512, 8)]
        public byte SocBefore { get; set; }

        /// <summary>
        /// 充电后SOC
        /// </summary>
        [Property(520, 8)]
        public byte SocAfter { get; set; }

        /// <summary>
        /// 充电时段数量
        /// </summary>
        [Property(528, 8)]
        public byte ChargingTimeCount { get; set; }

        #region 直流峰谷电

        /// <summary>
        /// 时段1 开始时间 时
        /// </summary>
        [Property(536, 8)]
        public byte StartTime1 { get; set; }

        /// <summary>
        /// 时段1 开始时间 分
        /// </summary>
        [Property(544, 8)]
        public byte StartTimeMinute1 { get; set; }

        /// <summary>
        /// 时段1 电量
        /// </summary>
        [Property(552, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime1 { get; set; }

        /// <summary>
        /// 时段1 标识
        /// </summary>
        [Property(584, 8)]
        public byte FlagOfTime1 { get; set; }

        /// <summary>
        /// 时段2 开始时间 时
        /// </summary>
        [Property(536 + 56, 8)]
        public byte StartTime2 { get; set; }

        /// <summary>
        /// 时段2 开始时间 分
        /// </summary>
        [Property(544 + 56, 8)]
        public byte StartTimeMinute2 { get; set; }

        /// <summary>
        /// 时段2 电量
        /// </summary>
        [Property(552 + 56, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime2 { get; set; }

        /// <summary>
        /// 时段2 标识
        /// </summary>
        [Property(584 + 56, 8)]
        public byte FlagOfTime2 { get; set; }

        /// <summary>
        /// 时段3 开始时间 时
        /// </summary>
        [Property(536 + 56 * 2, 8)]
        public byte StartTime3 { get; set; }

        /// <summary>
        /// 时段3 开始时间 分
        /// </summary>
        [Property(544 + 56 * 2, 8)]
        public byte StartTimeMinute3 { get; set; }

        /// <summary>
        /// 时段3 电量
        /// </summary>
        [Property(552 + 56 * 2, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime3 { get; set; }

        /// <summary>
        /// 时段3 标识
        /// </summary>
        [Property(584 + 56 * 2, 8)]
        public byte FlagOfTime3 { get; set; }

        /// <summary>
        /// 时段4 开始时间 时
        /// </summary>
        [Property(536 + 56 * 3, 8)]
        public byte StartTime4 { get; set; }

        /// <summary>
        /// 时段4 开始时间 分
        /// </summary>
        [Property(544 + 56 * 3, 8)]
        public byte StartTimeMinute4 { get; set; }

        /// <summary>
        /// 时段4 电量
        /// </summary>
        [Property(552 + 56 * 3, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime4 { get; set; }

        /// <summary>
        /// 时段4 标识
        /// </summary>
        [Property(584 + 56 * 3, 8)]
        public byte FlagOfTime4 { get; set; }

        /// <summary>
        /// 时段5 开始时间 时
        /// </summary>
        [Property(536 + 56 * 4, 8)]
        public byte StartTime5 { get; set; }

        /// <summary>
        /// 时段5 开始时间 分
        /// </summary>
        [Property(544 + 56 * 4, 8)]
        public byte StartTimeMinute5 { get; set; }

        /// <summary>
        /// 时段5 电量
        /// </summary>
        [Property(552 + 56 * 4, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime5 { get; set; }

        /// <summary>
        /// 时段5 标识
        /// </summary>
        [Property(584 + 56 * 4, 8)]
        public byte FlagOfTime5 { get; set; }

        /// <summary>
        /// 时段6 开始时间 时
        /// </summary>
        [Property(536 + 56 * 5, 8)]
        public byte StartTime6 { get; set; }

        /// <summary>
        /// 时段6 开始时间 分
        /// </summary>
        [Property(544 + 56 * 5, 8)]
        public byte StartTimeMinute6 { get; set; }

        /// <summary>
        /// 时段6 电量
        /// </summary>
        [Property(552 + 56 * 5, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime6 { get; set; }

        /// <summary>
        /// 时段6 标识
        /// </summary>
        [Property(584 + 56 * 5, 8)]
        public byte FlagOfTime6 { get; set; }

        /// <summary>
        /// 时段7 开始时间 时
        /// </summary>
        [Property(536 + 56 * 6, 8)]
        public byte StartTime7 { get; set; }

        /// <summary>
        /// 时段7 开始时间 分
        /// </summary>
        [Property(544 + 56 * 6, 8)]
        public byte StartTimeMinute7 { get; set; }

        /// <summary>
        /// 时段7 电量
        /// </summary>
        [Property(552 + 56 * 6, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime7 { get; set; }

        /// <summary>
        /// 时段7 标识
        /// </summary>
        [Property(584 + 56 * 6, 8)]
        public byte FlagOfTime7 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(536 + 56 * 7, 8)]
        public byte StartTime8 { get; set; }

        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(544 + 56 * 7, 8)]
        public byte StartTimeMinute8 { get; set; }

        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(552 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime8 { get; set; }

        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(584 + 56 * 7, 8)]
        public byte FlagOfTime8 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(592 + 56 * 7, 8)]
        public byte StartTime9 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(600 + 56 * 7, 8)]
        public byte StartTimeMinute9 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(608 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime9 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(640 + 56 * 7, 8)]
        public byte FlagOfTime9 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(648 + 56 * 7, 8)]
        public byte StartTime10 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(656 + 56 * 7, 8)]
        public byte StartTimeMinute10 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(664 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime10 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(696 + 56 * 7, 8)]
        public byte FlagOfTime10 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(704 + 56 * 7, 8)]
        public byte StartTime11 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(712 + 56 * 7, 8)]
        public byte StartTimeMinute11 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(720 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime11 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(752 + 56 * 7, 8)]
        public byte FlagOfTime11 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(760 + 56 * 7, 8)]
        public byte StartTime12 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(768 + 56 * 7, 8)]
        public byte StartTimeMinute12 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(776 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime12 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(808 + 56 * 7, 8)]
        public byte FlagOfTime12 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(816 + 56 * 7, 8)]
        public byte StartTime13 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(824 + 56 * 7, 8)]
        public byte StartTimeMinute13 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(832 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime13 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(864 + 56 * 7, 8)]
        public byte FlagOfTime13 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(872 + 56 * 7, 8)]
        public byte StartTime14 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(880 + 56 * 7, 8)]
        public byte StartTimeMinute14 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(888 + 56 * 7, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float ChargingPowerOfTime14 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(920 + 56 * 7, 8)]
        public byte FlagOfTime14 { get; set; }

        #endregion
        
        /// <summary>
        /// 充电模式 0：站内充电 1：站外充电
        /// </summary>
        [Property(984 + 336, 8)]
        public byte ChargeMode { get; set; }

        /// <summary>
        /// 启动模式 0: 站控启动 1：本地启动
        /// </summary>
        [Property(992 + 336, 8)]
        public byte StartMode { get; set; }

        /// <summary>
        /// 充电前直流表值
        /// </summary>
        [Property(1000 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]

        public float DcMeterDataBefore { get; set; }

        /// <summary>
        /// 充电后直流表值
        /// </summary>
        [Property(1032 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float DcMeterDataAfter { get; set; }

        /// <summary>
        /// 充电前交流表值
        /// </summary>
        [Property(1064 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]

        public float AcMeterDataBefore { get; set; }

        /// <summary>
        /// 充电后交流表值
        /// </summary>
        [Property(1096 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcMeterDataAfter { get; set; }

        /// <summary>
        /// 交流表充电量
        /// </summary>
        [Property(1128 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcMeterElecCount { get; set; }

        #region 交流峰谷电

        /// <summary>
        /// 时段1 开始时间 时
        /// </summary>
        [Property(1160 + 336, 8)]
        public byte AcStartTime1 { get; set; }

        /// <summary>
        /// 时段1 开始时间 分
        /// </summary>
        [Property(1168 + 336, 8)]
        public byte AcStartTimeMinute1 { get; set; }

        /// <summary>
        /// 时段1 电量
        /// </summary>
        [Property(1176 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime1 { get; set; }

        /// <summary>
        /// 时段1 标识
        /// </summary>
        [Property(1208 + 336, 8)]
        public byte AcFlagOfTime1 { get; set; }

        /// <summary>
        /// 时段2 开始时间 时
        /// </summary>
        [Property(1160 + 56 + 336, 8)]
        public byte AcStartTime2 { get; set; }

        /// <summary>
        /// 时段2 开始时间 分
        /// </summary>
        [Property(1168 + 56 + 336, 8)]
        public byte AcStartTimeMinute2 { get; set; }

        /// <summary>
        /// 时段2 电量
        /// </summary>
        [Property(1176 + 56 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime2 { get; set; }

        /// <summary>
        /// 时段2 标识
        /// </summary>
        [Property(1208 + 56 + 336, 8)]
        public byte AcFlagOfTime2 { get; set; }

        /// <summary>
        /// 时段3 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 2 + 336, 8)]
        public byte AcStartTime3 { get; set; }

        /// <summary>
        /// 时段3 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 2 + 336, 8)]
        public byte AcStartTimeMinute3 { get; set; }

        /// <summary>
        /// 时段3 电量
        /// </summary>
        [Property(1176 + 56 * 2 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime3 { get; set; }

        /// <summary>
        /// 时段3 标识
        /// </summary>
        [Property(1208 + 56 * 2 + 336, 8)]
        public byte AcFlagOfTime3 { get; set; }

        /// <summary>
        /// 时段4 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 3 + 336, 8)]
        public byte AcStartTime4 { get; set; }

        /// <summary>
        /// 时段4 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 3 + 336, 8)]
        public byte AcStartTimeMinute4 { get; set; }

        /// <summary>
        /// 时段4 电量
        /// </summary>
        [Property(1176 + 56 * 3 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime4 { get; set; }

        /// <summary>
        /// 时段4 标识
        /// </summary>
        [Property(1208 + 56 * 3 + 336, 8)]
        public byte AcFlagOfTime4 { get; set; }

        /// <summary>
        /// 时段5 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 4 + 336, 8)]
        public byte AcStartTime5 { get; set; }

        /// <summary>
        /// 时段5 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 4 + 336, 8)]
        public byte AcStartTimeMinute5 { get; set; }

        /// <summary>
        /// 时段5 电量
        /// </summary>
        [Property(1176 + 56 * 4 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime5 { get; set; }

        /// <summary>
        /// 时段5 标识
        /// </summary>
        [Property(1208 + 56 * 4 + 336, 8)]
        public byte AcFlagOfTime5 { get; set; }

        /// <summary>
        /// 时段6 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 5 + 336, 8)]
        public byte AcStartTime6 { get; set; }

        /// <summary>
        /// 时段6 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 5 + 336, 8)]
        public byte AcStartTimeMinute6 { get; set; }

        /// <summary>
        /// 时段6 电量
        /// </summary>
        [Property(1176 + 56 * 5 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime6 { get; set; }

        /// <summary>
        /// 时段6 标识
        /// </summary>
        [Property(1208 + 56 * 5 + 336, 8)]
        public byte AcFlagOfTime6 { get; set; }

        /// <summary>
        /// 时段7 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 6 + 336, 8)]
        public byte AcStartTime7 { get; set; }

        /// <summary>
        /// 时段7 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 6 + 336, 8)]
        public byte AcStartTimeMinute7 { get; set; }

        /// <summary>
        /// 时段7 电量
        /// </summary>
        [Property(1176 + 56 * 6 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime7 { get; set; }

        /// <summary>
        /// 时段7 标识
        /// </summary>
        [Property(1208 + 56 * 6 + 336, 8)]
        public byte AcFlagOfTime7 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1160 + 56 * 7 + 336, 8)]
        public byte AcStartTime8 { get; set; }

        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1168 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute8 { get; set; }

        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1176 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime8 { get; set; }

        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1208 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime8 { get; set; }
        
        
        
        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1216 + 56 * 7 + 336, 8)]
        public byte AcStartTime9 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1224 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute9 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1232 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime9 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1240 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime9 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1248 + 56 * 7 + 336, 8)]
        public byte AcStartTime10 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1256 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute10 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1264 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime10 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1272 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime10 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1280 + 56 * 7 + 336, 8)]
        public byte AcStartTime11 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1288 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute11 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1296 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime11 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1304 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime11 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1312 + 56 * 7 + 336, 8)]
        public byte AcStartTime12 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1320 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute12 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1328 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime12 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1336 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime12 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1344 + 56 * 7 + 336, 8)]
        public byte AcStartTime13 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1352 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute13 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1360 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime13 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1368 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime13 { get; set; }

        /// <summary>
        /// 时段8 开始时间 时
        /// </summary>
        [Property(1376 + 56 * 7 + 336, 8)]
        public byte AcStartTime14 { get; set; }
        /// <summary>
        /// 时段8 开始时间 分
        /// </summary>
        [Property(1384 + 56 * 7 + 336, 8)]
        public byte AcStartTimeMinute14 { get; set; }
        /// <summary>
        /// 时段8 电量
        /// </summary>
        [Property(1392 + 56 * 7 + 336, 32, PropertyReadConstant.Bit, 0.01, 2)]
        public float AcChargingPowerOfTime14 { get; set; }
        /// <summary>
        /// 时段8 标识
        /// </summary>
        [Property(1400 + 56 * 7 + 336, 8)]
        public byte AcFlagOfTime14 { get; set; }

        
        #endregion
    }
}