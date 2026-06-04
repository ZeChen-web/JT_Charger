using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.8 交易记录确认 (0x40, 下行)</summary>
public class V14DTransactionRecordConfirm : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.TransactionRecordConfirm;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>处理结果，1 字节 BIN；具体取值按当前报文定义。</summary>
    public byte Result { get; set; }

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
