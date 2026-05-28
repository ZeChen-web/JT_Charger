using System.Text;
using Newtonsoft.Json;

namespace Common.Util;

/// <summary>
/// http工具类
/// </summary>
public static class HttpUtil
{
    private static readonly HttpClient httpClient = new HttpClient();

    /// <summary>
    /// 发送post请求
    /// </summary>
    /// <param name="data"></param>
    /// <param name="url"></param>
    /// <typeparam name="T"></typeparam>
    public static async void SendPostRequest<T>(T data, string url)
    {
        string jsonStr = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception e)
        {
            
        }
        
    }
}