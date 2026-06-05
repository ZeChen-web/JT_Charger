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

[Order(8)][Scope("InstancePerDependency")]
public class V14DBillingModelVerifyHandler : SimpleChannelInboundHandler<V14DBillingModelVerifyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelVerifyHandler));
    ElecPriceModelVersionRepository _versionRepository =AppInfo.Container.Resolve<ElecPriceModelVersionRepository>();
    ElecPriceModelVersionDetailRepository _detailRepository =AppInfo.Container.Resolve<ElecPriceModelVersionDetailRepository>();
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelVerifyReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo}");
            // TODO::回复计费模型一致
            
            #region 计费模型查询

            var resp = new V14DBillingModelVerifyResp(msg.PileCode, msg.ModelNo, 0x00) { SeqNo = msg.SeqNo };

            ElecPriceModelVersion elecPriceModelVersion=_versionRepository.QueryByClause(i=> i.Version == msg.ModelNo);
            if (elecPriceModelVersion.Version!=0)
            {
                
            }
            else
            {
                resp.Result = 0x01;// 不一致
                ctx.Channel.WriteAndFlushAsync(resp);
            }
            
            #endregion
            
        }
    }
}
