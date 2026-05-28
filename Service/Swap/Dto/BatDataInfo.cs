namespace Service.Swap.Dto
{
    /// <summary>
    /// 4.2.11.1 换电站电池包数据信息
    /// </summary>
    public class BatDataInfo
    {
        /// <summary>
        /// 场站编码	换电站唯一码
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 换电站电池包总 数量
        /// </summary>
        public int batn { get; set; }

        /// <summary>
        /// 电池信息
        /// </summary>
        public List<SingleBatInfo> datainfo { get; set; }
    }
}
