using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class BattenergyInfo5 : ASDU
    {
        /// <summary>
        /// 快换连接器连接故障
        /// </summary>
        [Property(0, 1)]
        public byte Fult1SwapContorContErr { get; set; }

        /// <summary>
        /// 快换连接器过温报警
        /// </summary>
        [Property(1, 2)]
        public byte Fult1SwapConOverTemp { get; set; }

        /// <summary>
        /// 快换连接器PTC开路或短路故障
        /// </summary>
        [Property(3, 1)]
        public byte Fult1SwapPTCErr { get; set; }

        /// <summary>
        /// SOC不可信标志（≥10Cycle不满充置出）
        /// </summary>
        [Property(4, 1)]
        public byte SOCUnavailableFlg { get; set; }

        /// <summary>
        /// 满放标志位
        /// </summary>
        [Property(5, 1)]
        public byte CellFullDisChrgFlg { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [Property(6, 26)]
        public int BattEngyInfo5_Reserved1 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [Property(32, 32)]
        public int BattEngyInfo5_Reserved2 { get; set; }
    }
}