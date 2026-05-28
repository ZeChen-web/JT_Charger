using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>9.5 计费模型设置 (0x58, 下行)</summary>
public class V14DBillingModelSetCmd : V14DBillingModelResp
{
    public override byte FrameType => V14DFrameType.BillingModelSet;
}
