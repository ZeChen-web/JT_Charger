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
using Service.Init;

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

            int modelno = StaticStationInfo.Ceid;
            
            var version = _versionRepository.QueryByClause(i => i.Version == modelno);
            if (version == null || version.Version == 0)
            {
                Log.Warn($"V14D BillingModelReq from {sn}, modelNo={modelno} not found, fallback to active version");
                version = _versionRepository.GetActiveVersion();
            }

            if (version != null && version.Version != 0)
            {
                resp.ModelNo = (ushort)version.Version;

                var details = _detailRepository.QueryByClauseToList(d => d.Version == version.Version);
                if (details.Count > 0)
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
}
