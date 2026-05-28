using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.8 交易记录确认 (0x40, 下行)</summary>
public class V14DTransactionRecordConfirm : V14DFrame
{
    public override byte FrameType => V14DFrameType.TransactionRecordConfirm;
    public string TransactionSN { get; set; } = ""; // 16B BCD
    public byte Result { get; set; } // 0x00上传成功 0x01非法账单

    public V14DTransactionRecordConfirm() { }
    public V14DTransactionRecordConfirm(string transactionSN, byte result = 0x00)
    { TransactionSN = transactionSN; Result = result; }

    public override byte[] GetBodyBytes()
    {
        var b = new byte[17];
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, 0);
        b[16] = Result;
        return b;
    }
}
