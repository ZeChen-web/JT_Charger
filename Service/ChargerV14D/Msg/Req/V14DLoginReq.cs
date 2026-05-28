using System.Text;
using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Msg.Req;

/// <summary>
/// 6.1 充电桩登录认�?(帧类�?0x01, 上行)
/// </summary>
public class V14DLoginReq : V14DFrame
{
    public override byte FrameType => V14DFrameType.Login;

    /// <summary>桩编�?(7字节BCD, 不足7位补0)</summary>
    public string PileCode { get; set; } = "";

    /// <summary>桩类�?(0=直流, 1=交流)</summary>
    public byte PileType { get; set; }

    /// <summary>充电枪数�?/summary>
    public byte GunCount { get; set; }

    /// <summary>通信协议版本 (版本号乘10, V1.4=0x0E)</summary>
    public byte ProtocolVersion { get; set; }

    /// <summary>程序版本 (8字节ASCII, 不足8位补�?</summary>
    public string ProgramVersion { get; set; } = "";

    /// <summary>网络链接类型</summary>
    public byte NetworkType { get; set; }

    /// <summary>SIM卡号 (10字节BCD, 不足10位补�?</summary>
    public string SimCard { get; set; } = "";

    /// <summary>杩愯惀鍟?/summary>
    public byte Operator { get; set; }

    public V14DLoginReq() { }

    /// <summary>从body字节数组解析</summary>
    public V14DLoginReq(byte[] body)
    {
        if (body.Length < 30) return;
        int offset = 0;

        PileCode = V14DUtils.BcdToString(body, offset, 7); offset += 7;
        PileType = body[offset]; offset += 1;
        GunCount = body[offset]; offset += 1;
        ProtocolVersion = body[offset]; offset += 1;
        ProgramVersion = V14DUtils.ReadAscii(body, offset, 8); offset += 8;
        NetworkType = body[offset]; offset += 1;
        SimCard = V14DUtils.BcdToString(body, offset, 10); offset += 10;
        Operator = body[offset];
    }

    public override byte[] GetBodyBytes()
    {
        var body = new byte[30];
        int offset = 0;

        var pileCodeBcd = V14DUtils.StringToBcd(PileCode, 7);
        Array.Copy(pileCodeBcd, 0, body, offset, 7); offset += 7;
        body[offset++] = PileType;
        body[offset++] = GunCount;
        body[offset++] = ProtocolVersion;
        V14DUtils.WriteAscii(ProgramVersion, body, offset, 8); offset += 8;
        body[offset++] = NetworkType;
        var simBcd = V14DUtils.StringToBcd(SimCard, 10);
        Array.Copy(simBcd, 0, body, offset, 10); offset += 10;
        body[offset] = Operator;

        return body;
    }
}


