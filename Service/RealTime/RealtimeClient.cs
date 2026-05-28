using log4net;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace Service.RealTime;

public class RealtimeClient
{
    /// <summary>
    /// 循环时用的随机数值
    /// </summary>
    private static UInt16 _cysSeqNum = 0;


    /// <summary>
    /// 计算循环用UInt16随机数值
    /// </summary>
    /// <returns></returns>
    public static UInt16 GetUInt16SeqNum()
    {
        if (_cysSeqNum < 65535)
        {
            _cysSeqNum += 1;
        }
        else
        {
            _cysSeqNum = 1;
        }

        return _cysSeqNum;
    }

    private static readonly ILog Log = LogManager.GetLogger(typeof(RealtimeClient));

    private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        DateFormatString = "yyyy-MM-dd HH:mm:ss",
        NullValueHandling = NullValueHandling.Ignore
    };

    private static HubConnection? hubConnection;

    public static void Init()
    {
        hubConnection = new HubConnectionBuilder().WithUrl("http://127.0.0.1:5034/realtime").WithAutomaticReconnect()
            .Build();
        hubConnection.ServerTimeout = TimeSpan.FromMinutes(1);
        hubConnection.KeepAliveInterval = TimeSpan.FromMinutes(1);
        hubConnection.StartAsync();
        hubConnection.Reconnected += (s) =>
        {
            Log.Info($"{s} connected");
            return Task.CompletedTask;
        };
        hubConnection.On("ReceiveMsg", (string msg) => { Log.Info($"receive {msg}"); });
    }

    public static void SendMsg(string msg)
    {
        hubConnection?.SendAsync("SendMsg", "host", msg);
    }

    public static void SendWithoutResult(string msg)
    {
        hubConnection?.SendAsync("InvokeWithoutResult", JsonConvert.SerializeObject(new RtMsg()
        {
            Id = GetUInt16SeqNum(),
            Cmd = "cmd",
            Datetime = DateTime.Now,
            FromUser = "charger",
            Msg = msg
        }));
    }

    public static string? SendWithResult(string msg)
    {
        var result = hubConnection?.InvokeAsync<RtMsg>("Invoke", new RtMsg()
        {
            Id = GetUInt16SeqNum(),
            Cmd = "cmd",
            Datetime = DateTime.Now,
            FromUser = "charger",
            Msg = msg
        }).Result;

        Log.Info($"result={result}");
        return result.FromUser;
    }
}
