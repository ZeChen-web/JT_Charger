using HybirdFrameworkCore.Autofac.Attribute;

namespace ConsoleStarter;

public class TestPerson
{
    [PropertyAttribute(0, 4, PropertyReadConstant.Byte)]
    public int intParam { get; set; }


    [PropertyAttribute(32, 32)] public int intParamNull { get; set; }


    [PropertyAttribute(64, 32)] public uint uit { get; set; }


    [PropertyAttribute(96, 1, PropertyReadConstant.Byte)]
    public byte byteParam { get; set; }

    //-128-127
    [PropertyAttribute(104, 8)] public byte _sb { get; set; }


    [PropertyAttribute(112, 10, PropertyReadConstant.Byte)]
    public string s { get; set; }


    [PropertyAttribute(208, 2, PropertyReadConstant.Byte)]
    public short st { get; set; }


    [PropertyAttribute(224, 2, PropertyReadConstant.Byte)]
    public ushort ust { get; set; }


    [PropertyAttribute(240, 2, PropertyReadConstant.Byte)]
    public short I16 { get; set; }


    [PropertyAttribute(256, 2, PropertyReadConstant.Byte)]
    public ushort UI16 { get; set; }


    [PropertyAttribute(272, 4, PropertyReadConstant.Byte)]
    public int I32 { get; set; }


    [PropertyAttribute(304, 4, PropertyReadConstant.Byte)]
    public uint UI32 { get; set; }


    //[PropertyAttribute(336, 8, PropertyReadConstant.Byte)]
    //public Int64 I64 { get; set; }


    //[PropertyAttribute(400, 8, PropertyReadConstant.Byte)]
    //public UInt64 UI64 { get; set; }


    //[PropertyAttribute(336, 8, PropertyReadConstant.Byte)]
    //public long lg { get; set; }


    //[PropertyAttribute(400, 8, PropertyReadConstant.Byte)]
    //public ulong ulg { get; set; }


    [PropertyAttribute(336, 2, PropertyReadConstant.Byte, 0.01, 2)]
    public float ft { get; set; }


    [PropertyAttribute(352, 4, PropertyReadConstant.Byte, 0.001, 3)]
    public double de { get; set; }


    [PropertyAttribute(384, 4, PropertyReadConstant.Byte, 0.1, 2)]
    public float ft2 { get; set; }


    [PropertyAttribute(416, 4, PropertyReadConstant.Byte, 1, 0, 300)]
    public float ft3 { get; set; }

    [PropertyAttribute(448, 4, PropertyReadConstant.Byte, 0.01, 2)]
    public double d3 { get; set; }


    //[PropertyAttribute(416, 8, PropertyReadConstant.Byte, 0.01, 2)]
    //public double de2 { get; set; }


    //[PropertyAttribute(416, 1)]
    //public bool bl { get; set; }


    //[PropertyAttribute(0, 32)]
    //public byte[] bs { get; set; }

    //[PropertyAttribute(0, 32)]
    //public string[] ss { get; set; }
}