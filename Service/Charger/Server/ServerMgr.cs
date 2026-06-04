using System.Collections.Concurrent;
using System.Threading.Channels;
using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkDriver.Session;
using Service.Charger.Client;
using Service.Charger.Common;

namespace Service.Charger.Server;

public class ServerMgr
{
    
    public static readonly ConcurrentDictionary<string, ChargerClient> Dictionary = new();

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
    public static bool TryGetClient(IChannel channel, out string sn, out ChargerClient? server)
    {
        string? snt = ChannelUtils.GetAttr(channel, ChargerConst.ChargerSn);

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

    public static void AddBySn(string sn, ChargerClient server)
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
    
    public static ChargerClient? GetBySn(string sn)
    {
        Dictionary.TryGetValue(sn, out var o);
        return o;
    }
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="sn"></param>
    /// <param name="destAddr"></param>
    public  static void SessionAttr(IChannel channel,string sn, string destAddr)
    {
        ChannelUtils.AddAttr(channel, ChargerConst.ChargerSn, sn);
        ChannelUtils.AddAttr(channel, ChargerConst.EqmTypeNo, sn);
        ChannelUtils.AddAttr(channel, ChargerConst.EqmCode, sn);
        ChannelUtils.AddAttr(channel, ChargerConst.DestAddr, destAddr);
    }

    
}