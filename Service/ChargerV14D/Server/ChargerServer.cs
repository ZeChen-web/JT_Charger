using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkDriver.TcpServer;
using Service.ChargerV14D.Codec;
using Service.ChargerV14D.Handler;
using Service.ChargerV14D.Msg;

namespace Service.ChargerV14D.Server;

[Scope]
public class ChargerServer : TcpServer<IBaseHandler, V14DDecoder, V14DEncoder>
{
    #region send

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="sn">充电机编号</param>
    /// <param name="msg">消息</param>
    public void SendShapParametersReq(string sn, V14DFrame msg)
    {
        SessionMgr.GetSession(sn).Send(msg);
    }

    #endregion
}