using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;

namespace Service.ChargerV14D.Handler;

[Order(8)]
[Scope("InstancePerDependency")]
public class V14DTransactionRecordHandler : SimpleChannelInboundHandler<V14DTransactionRecordReq>, IBaseHandler
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(V14DTransactionRecordHandler));
    private ChargeOrderRepository _chargeOrderRepository;
    private SwapOrderBatteryRepository _swapOrderBatteryRepository;
    private SwapOrderRepository _swapOrder;
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="chargeOrderRepository"></param>
    public V14DTransactionRecordHandler(ChargeOrderRepository chargeOrderRepository,SwapOrderBatteryRepository swapOrderBattery,SwapOrderRepository swapOrder)
    {
        _chargeOrderRepository = chargeOrderRepository;
        _swapOrder = swapOrder;
        _swapOrderBatteryRepository = swapOrderBattery;
    }
    protected override void ChannelRead0(IChannelHandlerContext ctx, V14DTransactionRecordReq msg)
    {
        if (V14DClientMgr.TryGetClient(ctx.Channel, out var sn, out var client))
        {
            Log.Info($"V14D TransactionRecord from {sn}, tsn={msg.TransactionSN}, totalKWH={msg.TotalKWHValue:F4}");

            client.ChargeOrderNo= msg.TransactionSN;
            
            ChargeOrder db = _chargeOrderRepository.QueryByClause(it => it.Sn == msg.TransactionSN);

            DateTime startTime = msg.StartDateTime;
            DateTime endTime = msg.StartDateTime;

            if (db == null)
            {
                TimeSpan timeSpan = endTime - startTime;
                ChargeOrder chargeOrder = new ChargeOrder()
                {
                    Sn = client.ChargeOrderNo,
                    BatteryNo = client.BatteryNo,
                    StartTime = startTime,
                    EndTime = endTime,
                    //StartSoc = msg.SocBefore,
                    //StopSoc = msg.SocAfter,

                    ChargeTimeCount = (int)Math.Round(timeSpan.TotalMinutes),
                    ElecCount = Convert.ToDecimal(msg.TotalKWH),
                    AcElecCount = Convert.ToDecimal(msg.TotalKWH),
                    StartAcElec = Convert.ToDecimal(msg.MeterStart),
                    StopAcElec = Convert.ToDecimal(msg.MeterEnd),
                    //StartDcElec = Convert.ToDecimal(msg.DcMeterDataBefore),
                    //StopDcElec = Convert.ToDecimal(msg.DcMeterDataAfter),
                    SharpElecCount = Convert.ToDecimal(msg.PeakKWH),
                    PeakElecCount = Convert.ToDecimal(msg.ShoulderKWH),
                    FlatElecCount = Convert.ToDecimal(msg.FlatKWH),
                    ValleyElecCount = Convert.ToDecimal(msg.ValleyKWH),
                    ChargeMode = 0,
                    //StartMode = msg.StartMode,
                    StartType = 2
                };
                for (int i = 0; i < 3; i++)
                {
                    bool b = _chargeOrderRepository.InsertT(chargeOrder);
                    if (b)
                    {
                        Log.Info($"充电订单:{chargeOrder.Sn}插入成功 from {sn}");
                        break;
                    }
                    else
                    {
                        Log.Info($" error 充电订单:{chargeOrder.Sn}第{i}次插入失败 from {sn}");
                    }
                }
            }
            else
            {
                db.StartTime = startTime;
                db.EndTime = endTime;
                //db.StartSoc = msg.SocBefore;
                //db.StopSoc = msg.SocAfter;
                TimeSpan? timeSpan = (db.EndTime - db.StartTime);
                db.ChargeTimeCount = (int)Math.Round(Convert.ToDecimal(timeSpan?.TotalMinutes));
                db.ElecCount = Convert.ToDecimal(msg.TotalKWH);
                db.AcElecCount = Convert.ToDecimal(msg.TotalKWH);
                db.StartAcElec = Convert.ToDecimal(msg.MeterStart);
                db.StopAcElec = Convert.ToDecimal(msg.MeterEnd);
                //db.StartDcElec = Convert.ToDecimal(msg.DcMeterDataBefore);
                //db.StopDcElec = Convert.ToDecimal(msg.DcMeterDataAfter);
                db.ElecCount = Convert.ToDecimal(msg.TotalKWH);
                db.AcElecCount = Convert.ToDecimal(msg.TotalKWH);
                db.StartAcElec = Convert.ToDecimal(msg.MeterStart);
                db.StopAcElec = Convert.ToDecimal(msg.MeterEnd);
                //db.AcSharpElecCount = Convert.ToDecimal(acPowersPeriods[0]);
                //db.AcPeakElecCount = Convert.ToDecimal(acPowersPeriods[1]);
                //db.AcFlatElecCount = Convert.ToDecimal(acPowersPeriods[2]);
                //db.AcValleyElecCount = Convert.ToDecimal(acPowersPeriods[3]);
                //StartMode = msg.StartMode,
                db.StartType = 2;

                try
                {
                    if (db != null && db.BatteryNo != null)
                    {
                        var swap = _swapOrderBatteryRepository.GetOrderBattery(db.BatteryNo, startTime.ToString());

                        if (swap != null)
                        {
                            var swapOrder = _swapOrder.QueryByClause(i => i.Sn == swap.SwapOrderSn);
                            if (swapOrder != null)
                            {
                                db.CloudSn = swapOrder.CloudSn;
                                db.SwapOrderSn = swapOrder.Sn;
                                Log.Info($"充电订单:{db.Sn}；云站sn：{db.CloudSn}；换电订单号：{db.SwapOrderSn}更新成功 from {sn}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"充电订单 更新出现异常：{ex.ToString()}");
                }

                for (int i = 0; i < 3; i++)
                {
                    bool b = _chargeOrderRepository.Update(db);
                    if (b)
                    {
                        Log.Info($"充电订单:{db.Sn}更新成功 from {sn}");
                        break;
                    }
                    else
                    {
                        Log.Info($" error 充电订单:{db.Sn}第{i}次更新失败 from {sn}");
                    }
                }
            }

            
            var confirm = new V14DTransactionRecordConfirm(msg.TransactionSN) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(confirm);
        }
    }
}
