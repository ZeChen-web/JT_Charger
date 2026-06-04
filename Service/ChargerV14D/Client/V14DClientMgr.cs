using System.Collections.Concurrent;
using Autofac;
using Common.Const;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkDriver.Session;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Common;

namespace Service.ChargerV14D.Client;

/// <summary>V1.4D 协议客户端管理器</summary>
[Scope]
public static class V14DClientMgr
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DClientMgr));

    /// <summary>所有连接的客户(key=序列</summary>
    public static readonly ConcurrentDictionary<string, V14DChargerClient> Dictionary = new();

    /// <summary>通过序列号获取客户端</summary>
    public static V14DChargerClient? GetBySn(string sn)
    {
        Dictionary.TryGetValue(sn, out var client);
        return client;
    }

    /// <summary>通过Channel获取客户/summary>
    public static bool TryGetClient(IChannel channel, out string sn, out V14DChargerClient? client)
    {
        sn = ChannelUtils.GetAttr(channel, V14DConst.PileSn);
        if (string.IsNullOrWhiteSpace(sn))
        {
            client = null;
            return false;
        }
        return Dictionary.TryGetValue(sn, out client);
    }

    /// <summary>注册客户/summary>
    public static void AddBySn(string sn, V14DChargerClient client)
    {
        Dictionary[sn] = client;
        Log.Info($"V14D client {sn} registered, total={Dictionary.Count}");
    }

    /// <summary>移除客户/summary>
    public static void RemoveBySn(string sn)
    {
        Dictionary.TryRemove(sn, out _);
        Log.Info($"V14D client {sn} removed, total={Dictionary.Count}");
    }

    /// <summary>初始化所有V1.4D充电桩连接 (客户端模式已废弃，服务端模式请使用ServerMgr.InitServer)</summary>
    [Obsolete("服务端模式下不再使用客户端主动连接，请使用ServerMgr.InitServer")]
    public static void InitClient()
    {
        Log.Warn("V14DClientMgr.InitClient is obsolete in server mode. Use ServerMgr.InitServer instead.");
    }
}

