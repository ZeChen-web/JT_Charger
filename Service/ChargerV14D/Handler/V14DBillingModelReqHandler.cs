using Autofac;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Redis;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DBillingModelReqHandler : SimpleChannelInboundHandler<V14DBillingModelReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelReqHandler));
    ElecPriceModelVersionRepository _versionRepository = AppInfo.Container.Resolve<ElecPriceModelVersionRepository>();
    RedisHelper _redisHelper =AppInfo.Container.Resolve<RedisHelper>();
    ElecPriceModelVersionDetailRepository _detailRepository =
        AppInfo.Container.Resolve<ElecPriceModelVersionDetailRepository>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D BillingModelReq from {sn}");
            
            var resp = new V14DBillingModelResp
            {
                SeqNo = msg.SeqNo,
                PileCode = msg.PileCode,
            };

            #region 计费模型查询与填充

            int modelno = 0;
            var strModelno = _redisHelper.GetStrValue("ModelNo");
            if (!string.IsNullOrEmpty(strModelno))
            {
                modelno = Convert.ToByte(strModelno);
            }
            
            var version = _versionRepository.QueryByClause(i => i.Version == modelno);
            if (version == null || version.Version == 0)
            {
                Log.Warn($"V14D BillingModelReq from {sn}, modelNo={modelno} not found, fallback to active version");
                version = GetActiveVersion();
            }

            if (version != null && version.Version != 0)
            {
                resp.ModelNo = (ushort)version.Version;

                var details = _detailRepository.QueryByClauseToList(d => d.Version == version.Version);
                if (details != null && details.Count > 0)
                {
                    resp.PopulateFromDetails(details);
                }
                else
                {
                    Log.Warn($"V14D BillingModelReq from {sn}, version={version.Version} has no details");
                }
            }
            else
            {
                Log.Warn($"V14D BillingModelReq from {sn}, no active billing model available to send");
            }

            #endregion

            ctx.Channel.WriteAndFlushAsync(resp);
        }
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
