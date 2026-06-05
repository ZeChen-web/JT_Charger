using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;

namespace Service.MyTask;

/// <summary>
/// 电池在位信号
/// </summary>
[Scope]
public class BatteryExitTask : ITask
{
    private static bool _stop;

    public string Name()
    {
        return nameof(BatteryExitTask);
    }

    public int Interval()
    {
        return 1000 * 1;
    }

    public void Handle()
    {
        //TODO::这里需要增加电池在位信号的处理逻辑，目前先占位
    }
    public bool Stoped()
    {
        return _stop;
    }

    public void Stop()
    {
        _stop = true;
    }

    public void ResetStop()
    {
        _stop = false;
    }
}