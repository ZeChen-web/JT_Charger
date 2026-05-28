using System.ComponentModel;
using System.Reflection;
using Entity.Attr;

namespace Entity.Constant;

public class BaseEnumExtensions
{
    //根据枚举获取注释(使用remark注解)
    public static string GetDescription(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        RemarkAttribute attribute =
            Attribute.GetCustomAttribute(field, typeof(RemarkAttribute)) as RemarkAttribute;

        return attribute == null ? value.ToString() : attribute.GetRemark();
    }

    //根据code获取枚举
    public static T GetEnumByCode<T>(int code) where T : Enum
    {
        return (T)Enum.ToObject(typeof(T), code);
    }

    //根据code获取值
    public static string GetEnumDescriptionByCode<T>(int code) where T : Enum
    {
        return GetDescription((T)Enum.ToObject(typeof(T), code));
    }

    public static string GetNameByEnum<T>(Enum value) where T : Enum
    {
        return Enum.GetName(typeof(T), value);
    }
}