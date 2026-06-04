using DotNetty.Common.Utilities;

namespace Service.ChargerV14D.Common;

public static class V14DConst
{
    /// <summary>帧起始标志</summary>
    public const byte StartFlag = 0x68;

    /// <summary>加密标志：不加密</summary>
    public const byte EncryptNone = 0x00;

    /// <summary>加密标志：3DES</summary>
    public const byte Encrypt3DES = 0x01;

    /// <summary>最小帧长度: 0x68 + DataLen(1) + SeqNo(2) + EncryptFlag(1) + FrameType(1) + CRC(2) = 8</summary>
    public const int MinFrameLength = 8;

    /// <summary>最大数据域长度</summary>
    public const int MaxDataLen = 200;

    // Channel attribute keys
    public static readonly AttributeKey<string> PileCode = AttributeKey<string>.ValueOf("v14d_pile_code");
    public static readonly AttributeKey<string> PileSn = AttributeKey<string>.ValueOf("v14d_pile_sn");
}

/// <summary>充电桩状态 (0x13报文)</summary>
public enum PileStatus : byte
{
    Offline = 0x00,
    Fault = 0x01,
    Idle = 0x02,
    Charging = 0x03,
    PluggedNotCharging = 0x04,
    ChargeComplete = 0x05
}

/// <summary>枪是否归位</summary>
public enum GunHomeStatus : byte
{
    No = 0x00,
    Yes = 0x01,
    Unknown = 0x02
}

/// <summary>网络链接类型</summary>
public enum NetworkType : byte
{
    SIM = 0x00,
    LAN = 0x01,
    WAN = 0x02,
    Other = 0x03
}

/// <summary>运营商</summary>
public enum OperatorType : byte
{
    Mobile = 0x00,
    Telecom = 0x02,
    Unicom = 0x03,
    Other = 0x04
}

/// <summary>启动方式</summary>
public enum StartMethod : byte
{
    Card = 0x01,
    Account = 0x02,
    VIN = 0x03
}

/// <summary>交易标识</summary>
public enum TradeFlag : byte
{
    App = 0x01,
    Card = 0x02,
    OfflineCard = 0x04,
    VIN = 0x05
}
