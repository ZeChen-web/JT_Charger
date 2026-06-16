namespace Service.ChargerV14D.Common;

public static class ChargerConst
{

    public static Dictionary<(string, string), string> _dictionary = new Dictionary<(string, string), string>()
    {
        { ("12345678900001", "1"), "1" },
        { ("12345678900001", "2"), "2" },
        { ("12345678900002", "1"), "3" },
        { ("12345678900002", "2"), "4" },
        { ("12345678900003", "1"), "5" },
        { ("12345678900003", "2"), "6" },
        { ("12345678900004", "1"), "7" },
        { ("12345678900004", "2"), "8" },
        { ("12345678900005", "1"), "9" },
        { ("12345678900005", "2"), "10" },
        { ("12345678900006", "1"), "11" },
        { ("12345678900006", "2"), "12" },
    };
    public static string No(string chargerNo, string chargerGunNo)
    {
        return _dictionary.TryGetValue((chargerNo, chargerGunNo), out var no)
            ? no
            : string.Empty;
    }
}
