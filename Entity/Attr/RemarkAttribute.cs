using System.Reflection;

namespace Entity.Attr;

public class RemarkAttribute : System.Attribute
{
    private string _Remark;

    public RemarkAttribute(string remark)
    {
        this._Remark = remark;
    }

    public string GetRemark()
    {
        return this._Remark;
    }
}

public static class RemarkExtend
{
    public static string GetRemark(this global::System.Enum value)
    {
        Type type = value.GetType();
        FieldInfo? fieldInfo = type.GetField(value.ToString());
        if (fieldInfo != null && fieldInfo.IsDefined(typeof(RemarkAttribute), true))
        {
            System.Attribute? attribute = fieldInfo.GetCustomAttribute(typeof(RemarkAttribute));
            if (attribute != null)
            {
                return ((RemarkAttribute)attribute).GetRemark();
            }
        }

        return value.ToString();
    }
}