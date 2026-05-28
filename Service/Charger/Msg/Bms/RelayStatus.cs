using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    /// <summary>
    /// 继电器状态
    /// </summary>
    public class RelayStatus : ASDU
    {
        /// <summary>
        /// 支路1正继电器状态
        /// </summary>
        [Property(0, 1)]
        public byte Branch1PosRlyStatus { get; set; }

        /// <summary>
        /// 支路1负继电器状态
        /// </summary>
        [Property(1, 1)]
        public byte Branch1NegRlyStatus { get; set; }

        /// <summary>
        /// 支路2正继电器状态
        /// </summary>
        [Property(2, 1)]
        public byte Branch2PosRlyStatus { get; set; }

        /// <summary>
        /// 支路2负继电器状态
        /// </summary>
        [Property(3, 1)]
        public byte Branch2NegRlyStatus { get; set; }

        /// <summary>
        /// 支路3正继电器状态
        /// </summary>
        [Property(4, 1)]
        public byte Branch3PosRlyStatus { get; set; }

        /// <summary>
        /// 支路3负继电器状态
        /// </summary>
        [Property(5, 1)]
        public byte Branch3NegRlyStatus { get; set; }

        /// <summary>
        /// 支路4正继电器状态
        /// </summary>
        [Property(6, 1)]
        public byte Branch4PosRlyStatus { get; set; }

        /// <summary>
        /// 支路4负继电器状态
        /// </summary>
        [Property(7, 1)]
        public byte Branch4NegRlyStatus { get; set; }

        /// <summary>
        /// 主正继电器状态
        /// </summary>
        [Property(8, 1)]
        public byte MainPosRlyStatus { get; set; }

        /// <summary>
        /// 主负继电器状态
        /// </summary>
        [Property(9, 1)]
        public byte MainNegRlyStatus { get; set; }

        /// <summary>
        /// 预充继电器状态
        /// </summary>
        [Property(10, 1)]
        public byte PreChrgRlyStatus { get; set; }

        /// <summary>
        /// 加热正继电器状态
        /// </summary>
        [Property(10, 1)]
        public byte HeatPosRlyStatus { get; set; }

        /// <summary>
        /// 加热负继电器状态
        /// </summary>
        [Property(12, 1)]
        public byte HeatNegRlyStatus { get; set; }

        /// <summary>
        /// 预留1
        /// </summary>
        [Property(13, 3)]
        public byte RlyStsReserved1 { get; set; }

        /// <summary>
        /// 支路1正继电器故障
        /// </summary>
        [Property(16, 2)]
        public byte Branch1PosRlyFlt { get; set; }

        /// <summary>
        /// 支路1负继电器故障
        /// </summary>
        [Property(18, 2)]
        public byte Branch1NegRlyFlt { get; set; }

        /// <summary>
        /// 支路2正继电器故障
        /// </summary>
        [Property(20, 2)]
        public byte Branch2PosRlyFlt { get; set; }

        /// <summary>
        /// 支路2负继电器故障
        /// </summary>
        [Property(22, 2)]
        public byte Branch2NegRlyFlt { get; set; }

        /// <summary>
        /// 支路3正继电器故障
        /// </summary>
        [Property(24, 2)]
        public byte Branch3PosRlyFlt { get; set; }

        /// <summary>
        /// 支路3负继电器故障
        /// </summary>
        [Property(26, 2)]
        public byte Branch3NegRlyFlt { get; set; }

        /// <summary>
        /// 支路4正继电器故障
        /// </summary>
        [Property(28, 2)]
        public byte Branch4PosRlyFlt { get; set; }

        /// <summary>
        /// 支路4负继电器故障
        /// </summary>
        [Property(30, 2)]
        public byte Branch4NegRlyFlt { get; set; }

        /// <summary>
        /// 主正继电器故障
        /// </summary>
        [Property(32, 2)]
        public byte MainPosRlyFlt { get; set; }

        /// <summary>
        /// 主负继电器故障
        /// </summary>
        [Property(34, 2)]
        public byte MainNegRlyFlt { get; set; }

        /// <summary>
        /// 预充继电器故障
        /// </summary>
        [Property(36, 2)]
        public byte PreChrgPosRlyFlt { get; set; }

        /// <summary>
        /// 加热正继电器故障
        /// </summary>
        [Property(38, 2)]
        public byte HeatPosRlyFlt { get; set; }

        /// <summary>
        /// 加热负继电器故障
        /// </summary>
        [Property(40, 2)]
        public byte HeatNegRlyFlt { get; set; }

        /// <summary>
        /// 支路1正继电器线圈故障
        /// </summary>
        [Property(42, 1)]
        public byte Branch1PosRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路1负继电器线圈故障
        /// </summary>
        [Property(43, 1)]
        public byte Branch1NegRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路2正继电器线圈故障
        /// </summary>
        [Property(44, 1)]
        public byte Branch2PosRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路2负继电器线圈故障
        /// </summary>
        [Property(45, 1)]
        public byte Branch2NegRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路3正继电器线圈故障
        /// </summary>
        [Property(46, 1)]
        public byte Branch3PosRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路3负继电器线圈故障
        /// </summary>
        [Property(47, 1)]
        public byte Branch3NegRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路4正继电器线圈故障
        /// </summary>
        [Property(48, 1)]
        public byte Branch4PosRlyCoilFlt { get; set; }

        /// <summary>
        /// 支路4负继电器线圈故障
        /// </summary>
        [Property(49, 1)]
        public byte Branch4NegRlyCoilFlt { get; set; }

        /// <summary>
        /// 主正继电器线圈故障
        /// </summary>
        [Property(50, 1)]
        public byte MainPosRlyCoilFlt { get; set; }

        /// <summary>
        /// 主负继电器线圈故障
        /// </summary>
        [Property(51, 1)]
        public byte MainNegRlyCoilFlt { get; set; }

        /// <summary>
        /// 预充继电器线圈故障
        /// </summary>
        [Property(52, 1)]
        public byte PreChrgPosRlyCoilFlt { get; set; }

        /// <summary>
        /// 加热正继电器线圈故障
        /// </summary>
        [Property(53, 1)]
        public byte HeatPosRlyCoilFlt { get; set; }

        /// <summary>
        /// 加热负继电器线圈故障
        /// </summary>
        [Property(54, 1)]
        public byte HeatNegRlyCoilFlt { get; set; }

        /// <summary>
        /// 预留2
        /// </summary>
        [Property(55, 9)]
        public ushort RlyStsReserved2 { get; set; }
    }
}