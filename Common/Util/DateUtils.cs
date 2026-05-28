namespace Common.Util;

public class DateUtils
{
    public static bool IsDateTimeToday(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return false;
        }

        DateTime startOfToday = DateTime.Today;
        DateTime endOfToday = startOfToday.AddDays(1).AddTicks(-1);
        return dateTime >= startOfToday && dateTime <= endOfToday;
    }


    /// 获取明天的0.0.0
    public static DateTime GetTomorrowFirst()
    {
        DateTime today = DateTime.Now; // 获取当前时间
        DateTime tomorrow = today.Date.AddDays(1); // 获取明天的日期
        return new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0); // 获取明天的0点
    }
    
    /// <summary>
    /// 组装只包含时分部分的时间
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    /// <returns></returns>
    public static string GetFormattedTime(byte hour, byte minute)
    {
        DateTime time = new DateTime(1, 1, 1, hour, minute, 0);
        return time.ToString("HH:mm");
    }
}