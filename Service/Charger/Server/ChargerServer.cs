using HybirdFrameworkCore.Autofac.Attribute;
using HybirdFrameworkDriver.TcpServer;
using Service.Charger.Codec;
using Service.Charger.Handler;
using Service.Charger.Msg;
using Service.Charger.Msg.Host.Req;

namespace Service.Charger.Server;

[Scope]
public class ChargerServer : TcpServer<IBaseHandler, Decoder, Encoder>
{
    #region send

    /// <summary>
    /// 发送
    /// </summary>
    /// <param name="sn">充电机编号</param>
    /// <param name="msg">消息</param>
    public void SendShapParametersReq(string sn, APCI msg)
    {
        SessionMgr.GetSession(sn).Send(msg);
    }

    #endregion
}