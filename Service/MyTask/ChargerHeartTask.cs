using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;

namespace Service.MyTask;


[Scope]
public class ChargerHeartTask: ITask
{
    private static bool _stop;

    public string Name()
    {
        return nameof(ChargerHeartTask);
    }

    public int Interval()
    {
        return 1000 * 1;
    }

    public void Handle()
    {
        
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