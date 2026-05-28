using Entity.DbModel.Station;
using HybirdFrameworkCore.Autofac.Attribute;
using SqlSugar;

namespace Repository.Station;

[Scope("SingleInstance")]
public class ChargeOrderRepository : BaseRepository<ChargeOrder>
{
    public ChargeOrderRepository(ISqlSugarClient sqlSugar) : base(sqlSugar)
    {
    }

    /// <summary>
    /// 创建充电订单
    /// </summary>
    /// <param name="chargeOrder">订单编号</param>
    /// <param name="chargeOrderNo">云平台订单编号</param>
    /// <param name="chargerNo">充电机编号</param>
    /// <param name="chargerGunNo">充电枪编号</param>
    /// <param name="outChargerGunNo">站外充电枪编号，站外1枪或2枪</param>
    public void SaveChargeGunOrder(string chargeOrder,string chargeOrderNo, string chargerNo, string chargerGunNo,
        string outChargerGunNo)
    {
        ChargeOrder order = new ChargeOrder();
        order.Sn = chargeOrder;
        order.CmdStatus = 0;
        order.ChargerNo = chargerNo.Substring(4);
        order.ChargerGunNo = chargerGunNo;
        order.OutChargerGunNo = outChargerGunNo;
        order.ChargeMode = 1;
        order.StartMode = 1;
        order.CloudChargeOrder = chargeOrderNo;
        order.CreatedTime = DateTime.Now;
        order.Sign = 1;
        Insert(order);
    }

    public ChargeOrder? GetLatestChargeGunOrder(string pn, string chargerCode)
    {
        try
        {
            var chargeOrder = DbBaseClient.Queryable<ChargeOrder>()
                .Where(co => co.OutChargerGunNo == pn && co.ChargerNo == chargerCode)
                .OrderByDescending(co => co.CreatedTime)
                .First();
            return chargeOrder;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public ChargeOrder? QueryLatestByBatterySn(string? batterySn)
    {
        if (string.IsNullOrWhiteSpace(batterySn))
        {
            return null;
        }

        return QueryByClause(it => it.BatteryNo == batterySn, it => it.CreatedTime, OrderByType.Desc);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="swapOrderNo"></param>
    /// <param name="batteryNo"></param>
    /// <returns></returns>
    public List<ChargeOrder> QueryBySwapOrderAndBatterySn(string? swapOrderNo, string? batteryNo)
    {
        if (string.IsNullOrWhiteSpace(swapOrderNo) || string.IsNullOrWhiteSpace(batteryNo))
        {
            return new List<ChargeOrder>(0);
        }

        return QueryListByClause(it => it.SwapOrderSn == swapOrderNo && it.BatteryNo == batteryNo, it => it.CreatedTime,
            OrderByType.Asc);
    }
}