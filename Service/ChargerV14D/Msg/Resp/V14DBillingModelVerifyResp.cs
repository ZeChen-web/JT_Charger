using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>6.6 计费模型验证请求应答 (0x06, 下行)。
/// 协议 body 长度 90 字节，与 V14DBillingModelResp (0x0A) 结构完全相同。
/// 平台下发完整计费模型，充电桩自行比对计费模型编号判断是否一致。
/// </summary>
public class V14DBillingModelVerifyResp : V14DBillingModelResp
{
    /// <summary>帧类型，1 字节 BIN；固定为 0x06 (BillingModelVerifyResp)。</summary>
    public override byte FrameType => V14DFrameType.BillingModelVerifyResp;
}
