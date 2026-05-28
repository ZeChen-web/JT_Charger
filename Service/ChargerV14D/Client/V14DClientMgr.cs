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

    /// <summary>所有连接的客户�?(key=序列�?</summary>
    public static readonly ConcurrentDictionary<string, V14DChargerClient> Dictionary = new();

    /// <summary>通过序列号获取客户端</summary>
    public static V14DChargerClient? GetBySn(string sn)
    {
        Dictionary.TryGetValue(sn, out var client);
        return client;
    }

    /// <summary>通过Channel获取客户�?/summary>
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

    /// <summary>注册客户�?/summary>
    public static void AddBySn(string sn, V14DChargerClient client)
    {
        Dictionary[sn] = client;
        Log.Info($"V14D client {sn} registered, total={Dictionary.Count}");
    }

    /// <summary>移除客户�?/summary>
    public static void RemoveBySn(string sn)
    {
        Dictionary.TryRemove(sn, out _);
        Log.Info($"V14D client {sn} removed, total={Dictionary.Count}");
    }

    /// <summary>初始化所有V1.4D充电桩连�?/summary>
    public static void InitClient()
    {
        var equipInfoRepo = AppInfo.Container.Resolve<EquipInfoRepository>();
        var equipNetInfoRepo = AppInfo.Container.Resolve<EquipNetInfoRepository>();
        var binInfoRepo = AppInfo.Container.Resolve<BinInfoRepository>();

        var equips = equipInfoRepo.QueryListByClause(e => e.TypeCode == (int)EquipmentType.Charger && e.Status == 1);
        if (!equips.Any())
        {
            Log.Warn("No V14D charger equipments found");
            return;
        }

        foreach (var equip in equips)
        {
            var netInfo = equipNetInfoRepo.QueryByClause(n => n.Code == equip.Code);
            if (netInfo == null)
            {
                Log.Warn($"No network info for V14D charger {equip.Code}");
                continue;
            }

            var binInfo = binInfoRepo.QueryByClause(b => b.ChargerNo == equip.Code && b.Status == 1);
            Task.Run(() => ConnClient(netInfo, binInfo, equip));
        }
    }

    private static void ConnClient(EquipNetInfo netInfo, BinInfo? binInfo, EquipInfo equipInfo)
    {
        var client = AppInfo.Container.Resolve<V14DChargerClient>();
        client.AutoReconnect = true;
        client.Sn = equipInfo.Code;
        client.EqmTypeNo = equipInfo.TypeCode?.ToString() ?? "";
        client.EqmCode = equipInfo.Code;

        client.ConnectedEventHandler += (sender, connected) =>
        {
            client.SessionAttr(equipInfo.Code, netInfo.DestAddr ?? "");
            Log.Info($"V14D charger {equipInfo.Code} connected, addr={netInfo.NetAddr}:{netInfo.NetPort}");
        };

        client.InitBootstrap(netInfo.NetAddr, int.Parse(netInfo.NetPort));
        bool connected = client.Connect();
        if (connected)
        {
            client.SessionAttr(equipInfo.Code, netInfo.DestAddr ?? "");
            AddBySn(equipInfo.Code, client);
        }
        else
        {
            Log.Error($"Failed to connect V14D charger {equipInfo.Code} at {netInfo.NetAddr}:{netInfo.NetPort}");
        }
    }
}

