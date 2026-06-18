using Common.Const;
using DotNetty.Transport.Channels;
using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using log4net;
using Repository.Station;
using Service.ChargerV14D.Client;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Msg.Resp;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Handler;

/// <summary>8.7 交易记录报文 (0x3F，上行)。</summary>
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
        if (V14DClientMgr.TryGetClient(ctx.Channel, msg.Gun,out var sn, out var client))
        {
            Log.Info($"V14D TransactionRecord from {sn}, tsn={msg.TransactionSN}, totalKWH={msg.TotalKWH:F4}");

            #region 充电订单入库

            
            client.ChargeOrderNo= msg.TransactionSN;
            
            ChargeOrder db = _chargeOrderRepository.QueryByClause(it => it.Sn == msg.TransactionSN);

            DateTime startTime = msg.StartDateTime;
            DateTime endTime = msg.EndDateTime;

            if (db == null)
            {
                TimeSpan timeSpan = endTime - startTime;
                ChargeOrder chargeOrder = new ChargeOrder()
                {
                    No=ChargerConst.No(msg.PileCode,msg.Gun.ToString()),
                    Sn = client.ChargeOrderNo,
                    StartTime = startTime,
                    EndTime = endTime,
                    StopSoc = client.SOC,
                    ChargerGunNo = msg.Gun.ToString(),
                    ChargeTimeCount = (int)Math.Round(timeSpan.TotalMinutes),
                    ElecCount = Convert.ToDecimal(msg.TotalKWH),
                    StartDcElec = Convert.ToDecimal(msg.MeterStart),
                    StopDcElec = Convert.ToDecimal(msg.MeterEnd),
                    SharpElecCount = Convert.ToDecimal(msg.PeakKWH),
                    PeakElecCount = Convert.ToDecimal(msg.ShoulderKWH),
                    FlatElecCount = Convert.ToDecimal(msg.FlatKWH),
                    ValleyElecCount = Convert.ToDecimal(msg.ValleyKWH),
                    ChargeMode = 0,
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
                db.No = ChargerConst.No(msg.PileCode, msg.Gun.ToString());
                db.StartTime = startTime;
                db.EndTime = endTime;
                db.StopSoc = client.SOC;
                TimeSpan? timeSpan = (db.EndTime - db.StartTime);
                db.ChargeTimeCount = (int)Math.Round(Convert.ToDecimal(timeSpan?.TotalMinutes));
                db.ElecCount = Convert.ToDecimal(msg.TotalKWH);
                db.ChargerGunNo = msg.Gun.ToString();
                db.StartDcElec = Convert.ToDecimal(msg.MeterStart);
                db.StopDcElec = Convert.ToDecimal(msg.MeterEnd);
                db.SharpElecCount = Convert.ToDecimal(msg.PeakKWH);
                db.PeakElecCount = Convert.ToDecimal(msg.ShoulderKWH);
                db.FlatElecCount = Convert.ToDecimal(msg.FlatKWH);
                db.ValleyElecCount = Convert.ToDecimal(msg.ValleyKWH);
                //StartMode = msg.StartMode,

                try
                {
                    if (db != null && db.BatteryNo != null)
                    {
                        var swap = _swapOrderBatteryRepository.GetOrderBattery(db.BatteryNo, startTime.ToString());

                        if (swap != null)
                        {
                            var swapOrder = _swapOrder.QueryByClause(i => i.Sn == swap.SwapOrderSn);
                            if (swapOrder != null && swapOrder.CloudSn != null)
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

            #endregion

            #region 故障入库

            if (msg.StopReason!=0)//这里不是正常停机
            {
                Dictionary<string, bool> lstAlarm = new();
                lstAlarm.Add(msg.StopReason.ToString(),true);
            
                FaultHandling.SaveAlarmInfoToProcessRecord(lstAlarm,EquipmentType.Charger,msg.PileCode+msg.Gun);

                
            }

            #endregion
            
            
            var confirm = new V14DTransactionRecordConfirm(msg.TransactionSN) { SeqNo = msg.SeqNo };
            ctx.Channel.WriteAndFlushAsync(confirm);
        }
    }
}
