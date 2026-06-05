using Autofac;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBillingModelVerifyHandler : SimpleChannelInboundHandler<V14DBillingModelVerifyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelVerifyHandler));
    ElecPriceModelVersionRepository _versionRepository = AppInfo.Container.Resolve<ElecPriceModelVersionRepository>();

    ElecPriceModelVersionDetailRepository _detailRepository =
        AppInfo.Container.Resolve<ElecPriceModelVersionDetailRepository>();

    /// <summary>DB type 到协议费率号的映射: 1=尖→0x00, 2=峰→0x01, 3=平→0x02, 4=谷→0x03</summary>
    private static readonly Dictionary<int, byte> TypeToRateSegment = new()
    {
        { 1, 0x00 }, { 2, 0x01 }, { 3, 0x02 }, { 4, 0x03 }
    };

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelVerifyReq msg)
    {
        if (!V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            return;

        Log.Info($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo}");

        var resp = new V14DBillingModelVerifyResp
        {
            SeqNo = msg.SeqNo,
            PileCode = msg.PileCode,
            ModelNo = msg.ModelNo
        };

        #region 计费模型查询与填充

        // 1. 查询充电桩上报的计费模型版本
        var version = _versionRepository.QueryByClause(i => i.Version == msg.ModelNo);
        if (version.Version == 0)
        {
            Log.Warn($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo} not found, fallback to active version");
            version = GetActiveVersion();
        }
        else
        {
            List<ElecPriceModelVersionDetail> detailAll = _detailRepository.Query();
            ElecPriceModelVersionDetail? detail1 = detailAll.FirstOrDefault(d => d.Type == 1);
            ElecPriceModelVersionDetail? detail2 = detailAll.FirstOrDefault(d => d.Type == 2);
            ElecPriceModelVersionDetail? detail3 = detailAll.FirstOrDefault(d => d.Type == 3);
            ElecPriceModelVersionDetail? detail4 = detailAll.FirstOrDefault(d => d.Type == 4);

            resp.PeakElecRate = (uint)(detail1?.Price * 1000 ?? 0);
            resp.PeakServiceRate = (uint)(detail1?.PriceSerice * 1000 ?? 0);
            resp.ShoulderElecRate = (uint)(detail2?.Price * 1000 ?? 0);
            resp.ShoulderServiceRate = (uint)(detail2?.PriceSerice * 1000 ?? 0);
            resp.FlatElecRate = (uint)(detail3?.Price * 1000 ?? 0);
            resp.FlatServiceRate = (uint)(detail3?.PriceSerice * 1000 ?? 0);
            resp.ValleyElecRate = (uint)(detail4?.Price * 1000 ?? 0);
            resp.ValleyServiceRate = (uint)(detail4?.PriceSerice * 1000 ?? 0);

            List<ElecPriceModelVersionDetail> detail = detailAll
                .Where(d => d.Version == version.Version)
                .ToList();
            for (int i = 0; i < detail.Count; i++)
            {
                resp.RateSegments[i] = TypeToRateSegment.GetValueOrDefault(detail[i].Type, (byte)0x02);
            }

            resp.ModelNo = (ushort)version.Version;

            Log.Warn($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo} expired, fallback to active version");
            version = GetActiveVersion();
        }

        #endregion

        ctx.Channel.WriteAndFlushAsync(resp);
    }

    /// <summary>查询当前时间处于有效期内的计费模型版本（左开右闭）</summary>
    private ElecPriceModelVersion? GetActiveVersion()
    {
        var now = DateTime.Now;
        var allVersions = _versionRepository.Query();
        if (allVersions == null || allVersions.Count == 0)
            return null;

        return allVersions
            .Where(v => (!v.StartTime.HasValue || now > v.StartTime.Value)
                        && (!v.EndTime.HasValue || now <= v.EndTime.Value))
            .OrderByDescending(v => v.Version)
            .FirstOrDefault();
    }
}