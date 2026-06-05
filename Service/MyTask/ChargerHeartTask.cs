/*
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.AutoTask;
using log4net;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.MyTask;

[Scope]
public class ChargerHeartTask : ITask
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(ChargerHeartTask));
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
        /*if (ServerMgr.Dictionary.IsEmpty)
            return;#1#

        var now = DateTime.Now;
        foreach (var kv in ServerMgr.Dictionary)
        {
            var client = kv.Value;
            if (!client.IsLoggedIn)
                continue;

            // 超过30秒未收到心跳则判定离线
            if (client.LastHeartbeat != null && (now - client.LastHeartbeat.Value).TotalSeconds < 30)
                continue;

            Log.Warn($"Charger {kv.Key} heartbeat timeout, LastHeartbeat={client.LastHeartbeat}, closing connection");

            client.IsLoggedIn = false;
            client.HeartTime = null;

            try
            {
                V14DHeartbeatResp heartbeatResp = new V14DHeartbeatResp(0x01) { SeqNo = 0 };
                client.Channel?.WriteAndFlushAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"Error closing channel for {kv.Key}", ex);
            }

            ServerMgr.Dictionary.TryRemove(kv.Key, out _);
        }
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
*/
