using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>11.4 远程更新应答 (0x93, 上行)</summary>
public class V14DRemoteUpdateReplyReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.RemoteUpdateReply;
    public string PileCode { get; set; } = "";
    public byte UpgradeStatus { get; set; } // 0x00成功 0x01编号错 0x02型号不匹配 0x03下载超时
    public V14DRemoteUpdateReplyReq() { }
    public V14DRemoteUpdateReplyReq(byte[] body)
    {
        if (body.Length < 8) return;
        PileCode = V14DUtils.BcdToString(body, 0, 7);
        UpgradeStatus = body[7];
    }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[8];
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, 0);
        b[7] = UpgradeStatus;
        return b;
    }
}
