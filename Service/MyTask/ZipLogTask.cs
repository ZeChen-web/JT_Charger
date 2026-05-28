using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;

namespace Service.MyTask;

[Scope]
public class ZipLogTask : LogZipTask
{
}
