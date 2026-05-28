// See https://aka.ms/new-console-template for more information

using ConsoleStarter;
using log4net.Config;

internal class Program
{
    public static void Main(string[] args)
    {
        XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.xml"));
        ExportDb exportDb = new ExportDb();
        exportDb.Export();
    }
}