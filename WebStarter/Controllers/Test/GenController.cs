using Microsoft.AspNetCore.Mvc;
using Service.RealTime;
using SqlSugar;

namespace WebStarter.Controllers.Test;

[ApiController]
[Route("[controller]")]
public class GenController : ControllerBase
{
    private readonly ISqlSugarClient _sqlSugarClient;

    public GenController(ISqlSugarClient sqlSugarClient)
    {
        _sqlSugarClient = sqlSugarClient;

    }

    /// <summary>
    /// 生成文件
    /// </summary>
    /// <returns></returns>
    [HttpGet("gen/t/{id}")]
    public void Get(int id)
    {
        Console.WriteLine();
        _sqlSugarClient.DbFirst
            .IsCreateAttribute() //创建sqlsugar自带特性
            .FormatFileName(it => ToPascalCase(it)) //格式化文件名（文件名和表名不一样情况）
            .FormatClassName(it => ToPascalCase(it)) //格式化类名 （类名和表名不一样的情况）
            .FormatPropertyName(it => ToPascalCase(it)) //格式化属性名 （属性名和字段名不一样情况）
            .CreateClassFile("D:\\lxw\\work\\pro\\c#\\hn_back_main\\Entity\\DbModel\\Station",
                "Entity.DbModel.Station");


        Console.WriteLine("生成完毕");
    }

    /// <summary>
    /// 测试发送
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    [HttpGet("send/{msg}")]
    public string? Send(string msg)
    {
        return RealtimeClient.SendWithResult(msg);
    }

    /// <summary>
    /// 测试发送
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    [HttpGet("send3/{msg}")]
    public void Send3(string msg)
    {
         RealtimeClient.SendMsg(msg);
    }


    static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        string[] strings = input.Split("_");
        string res = "";
        foreach (var s in strings)
        {
            string first = s.First().ToString().ToUpper();
            string te = first + s.Substring(1);
            res += te;
        }

        return res;
    }


}
