using System.Collections.Concurrent;
using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkDriver.Session;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Server;

public class ServerMgr
{

    public static readonly ConcurrentDictionary<string, V14DChargerClient> Dictionary = new();

    public static ChargerServer? Server;

    public static void InitServer(int port)
    {
        Server = AppInfo.Container.Resolve<ChargerServer>();
        Server.Start(port);
    }

    /// <summary>
    /// 通过channel获取server
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="sn"></param>
    /// <param name="server">获取不到，client则为空</param>
    /// <returns></returns>
    public static bool TryGetClient(IChannel channel, out string sn, out V14DChargerClient? server)
    {
        string? snt = ChannelUtils.GetAttr(channel, V14DConst.PileSn);

        if (!string.IsNullOrWhiteSpace(snt))
        {
            var chargerServer = GetBySn(snt);
            if (chargerServer != null)
            {
                sn = snt;
                server = chargerServer;
                return true;
            }
        }

        sn = string.Empty;
        server = null;
        return false;
    }

    public static void AddBySn(string sn, V14DChargerClient server)
    {
        if (Dictionary.ContainsKey(sn))
        {
            Dictionary[sn] = server;
        }
        else
        {
            Dictionary.TryAdd(sn, server);
        }
    }

    public static V14DChargerClient? GetBySn(string sn)
    {
        Dictionary.TryGetValue(sn, out var o);
        return o;
    }

    /// <summary>
    /// 通过sn在Servers列表中查找对应client的session，发送消息
    /// </summary>
    public static void SessionAttr(IChannel channel, string sn, string destAddr)
    {
        ChannelUtils.AddAttr(channel, V14DConst.PileSn, sn);
        ChannelUtils.AddAttr(channel, V14DConst.PileCode, sn);
    }
}