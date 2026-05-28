using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>8.4 远程启动充电命令回复 (0x33, 上行)</summary>
public class V14DRemoteStartChargeReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteStartChargeReply;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public byte Result { get; set; }       // 0x00失败 0x01成功
    public byte FailReason { get; set; }   // 0x00无 0x01设备编号不匹配 0x02枪已在充电 0x03设备故障 0x04设备离线 0x05未插枪

    public V14DRemoteStartChargeReplyReq() { }
    public V14DRemoteStartChargeReplyReq(byte[] body)
    {
        if (body.Length < 26) return;
        int o = 0;
        TransactionSN = V14DUtils.BcdToString(body, o, 16); o += 16;
        PileCode = V14DUtils.BcdToString(body, o, 7); o += 7;
        Gun = body[o++];
        Result = body[o++];
        FailReason = body[o];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[26];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun; b[o++] = Result; b[o] = FailReason;
        return b;
    }
}
