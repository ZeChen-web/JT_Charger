using System.Reflection;

namespace Entity.Attr;

public class InfoAttribute : System.Attribute
{
    private string _Led;
    private string _Sound;

    public InfoAttribute(string led, string sound)
    {
        this._Led = led;
        this._Sound = sound;
    }

    public string GetLed()
    {
        return this._Led;
    }

    public string GetSound()
    {
        return _Sound;
    }
}

public static class InfoExtend
{
    public static string GetLed(this Enum value)
    {
        Type type = value.GetType();
        FieldInfo? fieldInfo = type.GetField(value.ToString());
        if (fieldInfo != null && fieldInfo.IsDefined(typeof(InfoAttribute), true))
        {
            System.Attribute? attribute = fieldInfo.GetCustomAttribute(typeof(InfoAttribute));
            if (attribute != null)
            {
                return ((InfoAttribute)attribute).GetLed();
            }
        }

        return value.ToString();
    }

    public static string GetSound(this Enum value)
    {
        Type type = value.GetType();
        FieldInfo? fieldInfo = type.GetField(value.ToString());
        if (fieldInfo != null && fieldInfo.IsDefined(typeof(InfoAttribute), true))
        {
            System.Attribute? attribute = fieldInfo.GetCustomAttribute(typeof(InfoAttribute));
            if (attribute != null)
            {
                return ((InfoAttribute)attribute).GetSound();
            }
        }

        return value.ToString();
    }
}