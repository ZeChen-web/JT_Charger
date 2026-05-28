using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req
{
    /// <summary>
    /// 3.5.11 充放电机上报电表数据
    /// </summary>
    public class ElecMeterInfo : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 1, PropertyReadConstant.Byte)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 电表编号
        /// </summary>
        [Property(1, 1, PropertyReadConstant.Byte)]
        public byte MeterNo { get; set; }

        /// <summary>
        /// A相电压
        /// </summary>
        [Property(2, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float VoltageA { get; set; }

        /// <summary>
        /// B相电压
        /// </summary>
        [Property(6, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float VoltageB { get; set; }

        /// <summary>
        /// C相电压
        /// </summary>
        [Property(10, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float VoltageC { get; set; }

        /// <summary>
        /// A相电流
        /// </summary>
        [Property(14, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float CurrentA { get; set; }

        /// <summary>
        /// B相电流
        /// </summary>
        [Property(18, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float CurrentB { get; set; }

        /// <summary>
        /// C相电流
        /// </summary>
        [Property(22, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float CurrentC { get; set; }

        /// <summary>
        /// A相有功功率
        /// </summary>
        [Property(26, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float HpowerA { get; set; }

        /// <summary>
        /// B相有功功率
        /// </summary>
        [Property(30, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float HpowerB { get; set; }

        /// <summary>
        /// C相有功功率
        /// </summary>
        [Property(34, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float HpowerC { get; set; }

        /// <summary>
        /// A相无功功率
        /// </summary>
        [Property(38, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float WpowerA { get; set; }

        /// <summary>
        /// B相无功功率
        /// </summary>
        [Property(42, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float WpowerB { get; set; }

        /// <summary>
        /// C相无功功率
        /// </summary>
        [Property(46, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float WpowerC { get; set; }

        /// <summary>
        /// A相视在功率
        /// </summary>
        [Property(50, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float SpowerA { get; set; }

        /// <summary>
        /// B相视在功率
        /// </summary>
        [Property(54, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float SpowerB { get; set; }

        /// <summary>
        /// C相视在功率
        /// </summary>
        [Property(58, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float SpowerC { get; set; }

        /// <summary>
        /// A相功率因数字
        /// </summary>
        [Property(62, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float PoweryzA { get; set; }

        /// <summary>
        /// B相功率因数字
        /// </summary>
        [Property(66, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float PoweryzB { get; set; }

        /// <summary>
        /// C相功率因数字
        /// </summary>
        [Property(70, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float PoweryzC { get; set; }

        /// <summary>
        /// 电表瞬时有功总功率
        /// </summary>
        [Property(74, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float DibPower { get; set; }

        /// <summary>
        /// 电表瞬时无功总功率
        /// </summary>
        [Property(78, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float DbPower { get; set; }

        /// <summary>
        /// 电表瞬时视在总功率
        /// </summary>
        [Property(82, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float DbiPower { get; set; }

        /// <summary>
        /// 总功率因数字
        /// </summary>
        [Property(86, 4, PropertyReadConstant.Byte, 0.01, 2)]
        public float Poweryzz { get; set; }
    }
}