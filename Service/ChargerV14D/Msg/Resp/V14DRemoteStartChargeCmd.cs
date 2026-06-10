using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Resp;

/// <summary>8.3 运营平台远程控制启机命令报文 (0x34，下行)。</summary>
public class V14DRemoteStartChargeCmd : V14DFrame
{
    /// <summary>帧类型，1 字节 BIN；由具体报文类型固定。</summary>
    public override byte FrameType => V14DFrameType.RemoteStartCharge;
    /// <summary>交易流水号，16 字节 BCD。</summary>
    public string TransactionSN { get; set; } = "";
    /// <summary>桩编号，7 字节 BCD；不足 7 位左补 0。</summary>
    public string PileCode { get; set; } = "";
    /// <summary>枪号，1 字节 BIN。</summary>
    public byte Gun { get; set; }
    /// <summary>逻辑卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string LogicCardNo { get; set; } = "";
    /// <summary>物理卡号，8 字节 ASCII/BIN，右补 0。</summary>
    public string PhysicalCardNo { get; set; } = "";
    /// <summary>账户余额，4 字节 BIN；金额按协议精度换算。</summary>
    public uint Balance { get; set; }
    /// <summary>停机密码，6 字节 ASCII，右补 0。</summary>
    public string StopPassword { get; set; } = "";

    public V14DRemoteStartChargeCmd() { }
    public override byte[] GetBodyBytes()
    {
        var b = new byte[50];
        int o = 0;
        V14DUtils.StringToBcd(TransactionSN, 16).CopyTo(b, o); o += 16;
        V14DUtils.StringToBcd(PileCode, 7).CopyTo(b, o); o += 7;
        b[o++] = Gun;
        var lc = global::System.Text.Encoding.ASCII.GetBytes((LogicCardNo ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(lc, 0, b, o, 8); o += 8;
        var pc = global::System.Text.Encoding.ASCII.GetBytes((PhysicalCardNo ?? "").PadRight(8, '\0').Substring(0, 8));
        Array.Copy(pc, 0, b, o, 8); o += 8;
        BitConverter.GetBytes(Balance).CopyTo(b, o); o += 4;
        V14DUtils.WriteAscii(StopPassword, b, o, 6);
        return b;
    }
}

