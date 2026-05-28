namespace Service.Charger.Msg.Http.Resp;
/// <summary>
/// 9.2.1.8 站控响应充电枪停止充电操作
/// </summary>
public class PileStopChargeHttpRes
{
    /// <summary>
    /// 充电枪ID
    /// 0x01：充电枪1；0x02：充电枪2；0x03：双枪充电;(0x00&0xFF无效)
    /// </summary>
    public string pn { get; set; }

    /// <summary>
    /// 启动结果 0 成功 1 设备已停机 0xFF 其他
    /// </summary>
    public string rs { get; set; }
}