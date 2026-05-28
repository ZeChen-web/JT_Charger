using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Charger.Req;

/// <summary>
/// 3.6.2.3 充放电机上传电池包编码（PGN:0x00F881）
/// </summary>
public class QueryBatterySnRes : ASDU
{
    /// <summary>
    /// 记录类型
    /// </summary>
    [Property(0, 8)]
    public byte RecordType { get; set; }


    [Property(8, 24)] public byte[] Pgn { get; set; }

    /// <summary>
    /// 电池编码 长度 0 "Do not transmit this Code"
    /// </summary>
    [Property(32, 8)]
    public byte BatterSnLength { get; set; }

    /// <summary>
    /// 电池厂家
    /// 1 "CATL" ， 2 "Li Shen"， 3 "MGL" ，
    /// 4 "SAMSUN"， 5 "LG"， 6"EVE",7"BYD"
    /// </summary>
    [Property(40, 8)]
    public byte BatterFactory { get; set; }

    /// <summary>
    /// 电池编码 (SN)字符 (ASCII) 27Byte
    /// 48 "0" 49 "1" 50 "2" 51 "3" 52 "4"
    /// 53"5" 54 "6" 55 "7" 56 "8" 57 "9" 65
    /// "A"66"B" 67 "C" 68"D" 69 "E" 70 "F"
    /// 71 "G"
    /// 72 "H" 73 "I" 74 "J" 75 "K" 76 "L"
    /// 77"M" 78 "N" 79 "O" 80 "P" 81 "Q" 82
    /// "R"83 "S" 84 "T" 85 "U" 86 "V" 87
    /// "W"88 "X" 89 "Y" 90 "Z
    /// </summary>
    [Property(start: 48, length: 27, PropertyReadConstant.Byte)]
    public byte[] BatterSnBytes { get; set; }
}