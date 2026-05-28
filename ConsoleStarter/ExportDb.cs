using log4net;
using SqlSugar;

namespace ConsoleStarter;

public class ExportDb
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(ExportDb));

    private static readonly string[] UsedTable =
    {
        "elec_price_model_version",
        "elec_price_model_version_detail"
    };

    private readonly SqlSugarClient Db = new(new ConnectionConfig
    {
        ConnectionString =
            "server=180.76.133.253;Port=16306;Database=huanneng_dev;Uid=root;Pwd=Rszn123;Charset=utf8;",
        DbType = DbType.MySql,
        IsAutoCloseConnection = true,
        InitKeyType = InitKeyType.Attribute
    });

    public void Export()
    {
        List<DbTableInfo> tableInfoList = Db.DbMaintenance.GetTableInfoList(false);
        foreach (var tableInfo in tableInfoList)
            if (UsedTable.Contains(tableInfo.Name))
            {
                Log.Info($"{tableInfo.Name}:{tableInfo.Description}");

                List<DbColumnInfo> columnInfos = Db.DbMaintenance.GetColumnInfosByTableName(tableInfo.Name, false);
                foreach (var columnInfo in columnInfos)
                    Log.Info($"            {columnInfo.DbColumnName}:{columnInfo.ColumnDescription}");
            }

        Db.DbFirst
            .Where(it => UsedTable.Contains(it))
            .IsCreateAttribute() //创建sqlsugar自带特性
            .FormatFileName(ToPascalCase) //格式化文件名（文件名和表名不一样情况）
            .FormatClassName(ToPascalCase) //格式化类名 （类名和表名不一样的情况）
            .FormatPropertyName(ToPascalCase) //格式化属性名 （属性名和字段名不一样情况）
            .CreateClassFile("D:\\vsproject\\hn_back_charger\\Entity\\DbModel\\Station",
                "Entity.DbModel.Station");
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