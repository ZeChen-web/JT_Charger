using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.2 运营平台确认启动充电 (0x32, 下行)</summary>
public class V14DConfirmStartChargeResp : V14DFrame
{
    public override byte FrameType => V14DFrameType.ConfirmStartCharge;
    public string TransactionSN { get; set; } = "";
    public string PileCode { get; set; } = "";
    public byte Gun { get; set; }
    public string LogicCardNo { get; set; } = "";   // 8B
    public uint Balance { get; set; }                // 4B 保留2位小�?
    public byte AuthResult { get; set; }             // 0x00澶辫触 0x01鎴愬姛
    public byte FailReason { get; set; }

    public V14DConfirmStartChargeResp() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[38];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        var card = global::System.Text.Encoding.ASCII.GetBytes((LogicCardNo ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(card, 0, b, o, 8); o += 8;
        BitConverter.GetBytes(Balance).CopyTo(b, o); o += 4;
        b[o++] = AuthResult; b[o] = FailReason;
        return b;
    }
}


