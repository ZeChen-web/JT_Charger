using Autofac;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac;
using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkCore.Redis;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DBillingModelVerifyHandler : SimpleChannelInboundHandler<V14DBillingModelVerifyReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DBillingModelVerifyHandler));
    RedisHelper _redisHelper = AppInfo.Container.Resolve<RedisHelper>();

    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DBillingModelVerifyReq msg)
    {
        if (!V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            return;

        Log.Info($"V14D BillingModelVerify from {sn}, modelNo={msg.ModelNo}");

        // 从Redis获取平台当前生效的计费模型版本号
        int modelno = 0;
        var strModelno = _redisHelper.GetStrValue("ModelNo");
        if (!string.IsNullOrEmpty(strModelno))
        {
            int.TryParse(strModelno, out modelno);
        }

        byte result;
        if (modelno == msg.ModelNo)
        {
            result = 0x00; // 一致
        }
        else
        {
            result = 0x01; // 不一致
            Log.Warn($"V14D BillingModelVerify from {sn}, charger modelNo={msg.ModelNo} != platform modelNo={modelno}");
        }

        var resp = new V14DBillingModelVerifyResp(msg.PileCode, msg.ModelNo, result)
        {
            SeqNo = msg.SeqNo
        };
        ctx.Channel.WriteAndFlushAsync(resp);
    }
}
