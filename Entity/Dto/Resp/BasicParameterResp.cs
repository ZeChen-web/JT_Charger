namespace Entity.Dto.Resp;

/// <summary>
/// 电池包基本参数2
/// </summary>
public class BasicParameterResp
{
    /// <summary>
    /// 记录类型
    /// </summary>
    public byte RecordType { get; set; }

    /// <summary>
    /// PGN 码
    /// </summary>
    public byte Pgn1 { get; set; }

    public byte Pgn2 { get; set; }
    public byte Pgn3 { get; set; }

    /// <summary>
    /// 电池编码
    /// </summary>
    public byte[] BatteryCode { get; set; }

    /// <summary>
    /// 产权标识
    /// </summary>
    public byte TitleMarke { get; set; }

    /// <summary>
    /// 电池成组厂商
    /// </summary>
    public string Batterymanufacturers { get; set; }

    /// <summary>
    /// 电池成组生产日期：年
    /// </summary>
    public byte GroupYear { get; set; }

    /// <summary>
    /// 电池成组生产日期：月
    /// </summary>
    public byte GroupMonth { get; set; }

    /// <summary>
    /// 电池成组生产日期：日
    /// </summary>
    public byte GroupDay { get; set; }

    /// <summary>
    /// 电池电芯生产厂商
    /// </summary>
    public byte[] BatteryManufacturer { get; set; }

    /// <summary>
    /// 电池电芯生产日期：年
    /// </summary>
    public byte CellYear { get; set; }

    /// <summary>
    /// 电池电芯生产日期：月
    /// </summary>
    public byte CellMonth { get; set; }

    /// <summary>
    /// 电池电芯生产日期：日
    /// </summary>
    public byte CellDay { get; set; }

    /// <summary>
    /// 电池箱电子控制单元生产厂商
    /// </summary>
    public byte[] Manufacturer { get; set; }

    /// <summary>
    /// 电池箱电子控制单元硬件版本
    /// </summary>
    public byte HardwareVersion { get; set; }

    /// <summary>
    /// 电池箱电子控制单元软件版本
    /// </summary>
    public byte SoftwareVersion { get; set; }

    /// <summary>
    /// 电池包序列号
    /// </summary>
    public byte[] SerialNumber { get; set; }

    /// <summary>
    /// 电池包型号
    /// </summary>
    public byte[] ModelNumber { get; set; }
}