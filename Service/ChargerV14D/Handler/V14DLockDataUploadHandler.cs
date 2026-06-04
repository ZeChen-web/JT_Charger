using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;

namespace Service.ChargerV14D.Handler;

[Order(8)][Scope("InstancePerDependency")]
public class V14DLockDataUploadHandler : SimpleChannelInboundHandler<V14DLockDataUploadReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DLockDataUploadHandler));
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DLockDataUploadReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
            Log.Debug($"V14D LockDataUpload from {sn}, status={msg.LockStatus:X2}");
    }
}
