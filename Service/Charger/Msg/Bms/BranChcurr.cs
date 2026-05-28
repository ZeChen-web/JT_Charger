using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BranChcurr : ASDU
    {
        /// <summary>
        /// 支路1电流
        /// </summary>
        [Property(0, 16)]
        public float Branch1Curr { get; set; }

        /// <summary>
        /// 支路2电流
        /// </summary>
        [Property(16, 16)]
        public float Branch2Curr { get; set; }

        /// <summary>
        /// 支路3电流
        /// </summary>
        [Property(32, 16)]
        public float Branch3Curr { get; set; }

        /// <summary>
        /// 支路4电流
        /// </summary>
        [Property(48, 16)]
        public float Branch4Curr { get; set; }
    }
}