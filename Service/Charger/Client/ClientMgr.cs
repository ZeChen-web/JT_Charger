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
using Service.Charger.Common;

namespace Service.Charger.Client;

/// <summary>
///     示例程序
/// </summary>
[Scope]
public static class ClientMgr
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(ClientMgr));

    public static readonly ConcurrentDictionary<string, ChargerClient> Dictionary = new();

    public static ChargerClient? GetBySn(string sn)
    {
        Dictionary.TryGetValue(sn, out var o);
        return o;
    }

    /// <summary>
    /// 通过channel获取client
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="sn"></param>
    /// <param name="client">获取不到，client则为空</param>
    /// <returns></returns>
    public static bool TryGetClient(IChannel channel, out string sn, out ChargerClient? client)
    {
        string? snt = ChannelUtils.GetAttr(channel, ChargerConst.ChargerSn);

        if (!string.IsNullOrWhiteSpace(snt))
        {
            var chargerClient = GetBySn(snt);
            if (chargerClient != null)
            {
                sn = snt;
                client = chargerClient;
                return true;
            }
        }

        sn = string.Empty;
        client = null;
        return false;
    }

    public static void AddBySn(string sn, ChargerClient client)
    {
        Dictionary[sn] = client;
    }

    //TODO 连接、鉴权，开始充电，结束充电，设置尖峰平谷，读取尖峰平谷，发送功率调节指令，发送辅助源控制指令，下发掉线停止充电，
    public static void InitClient()
    {
        EquipInfoRepository equipInfoRepository = AppInfo.Container.Resolve<EquipInfoRepository>();
        EquipNetInfoRepository netInfoRepository = AppInfo.Container.Resolve<EquipNetInfoRepository>();
        BinInfoRepository binInfoRepository = AppInfo.Container.Resolve<BinInfoRepository>();
        List<EquipInfo> equipInfos =
            equipInfoRepository.QueryListByClause(it => it.TypeCode == (int)EquipmentType.Charger);
        if (equipInfos.Count > 0)
        {
            Dictionary<string, EquipInfo> set = equipInfos.ToDictionary(it => it.Code, it => it);
            List<EquipNetInfo> equipNetInfos = netInfoRepository.QueryListByClause(it => set.Keys.Contains(it.Code));
            Dictionary<string, BinInfo> binInfoMap = binInfoRepository
                .QueryListByClause(it => set.Keys.Contains(it.ChargerNo))
                .ToDictionary(it => it.ChargerNo, it => it);
            foreach (EquipNetInfo netInfo in equipNetInfos)
            {
                Task.Run(() =>
                {
                    binInfoMap.TryGetValue(netInfo.Code, out var binInfo);
                    ConnClient(netInfo, binInfo);
                });
            }
        }
    }

    private static void ConnClient(EquipNetInfo netInfo, BinInfo? binInfo)
    {
        Log.Info($"begin to connect {netInfo.Code} {netInfo.NetAddr}:{netInfo.NetPort}");
        ChargerClient client = AppInfo.Container.Resolve<ChargerClient>();
        client.AutoReconnect = true;
        client.Sn = netInfo.Code;
        client.BinNo = binInfo?.No;
        client.BatteryNo = binInfo?.BatteryNo;
        client.LogName = "Charger" + netInfo.Code;
        client.ConnectedEventHandler += (sender, b) =>
        {
            client.SessionAttr(netInfo.Code, netInfo.DestAddr);
            client.SendAuth();
        };
        client.InitBootstrap(netInfo.NetAddr, int.Parse(netInfo.NetPort));

        Task.Run(() =>
        {
            client.Connect();
            client.SessionAttr(netInfo.Code, netInfo.DestAddr);
            Log.Info($"succeed to connect {netInfo.Code} {netInfo.NetAddr}:{netInfo.NetPort}");
        });

        AddBySn(netInfo.Code, client);
        Log.Info($"begin to connect {netInfo.Code} {netInfo.NetAddr}:{netInfo.NetPort}");
    }
}
