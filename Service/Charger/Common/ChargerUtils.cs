using System.Text;
using Service.Init;

namespace Service.Charger.Common;

public static class ChargerUtils
{
    #region 随机数

    /// <summary>
    /// 循环时用的随机数值
    /// </summary>
    private static UInt16 _cysSeqNum = 0;

    /// <summary>
    /// 鉴权随时数
    /// </summary>
    private static byte _randomNum = 10;

    /// <summary>
    /// 计算循环用UInt16随机数值
    /// </summary>
    /// <returns></returns>
    public static UInt16 GetUInt16SeqNum()
    {
        if (_cysSeqNum < 65535)
        {
            _cysSeqNum += 1;
        }
        else
        {
            _cysSeqNum = 1;
        }

        return _cysSeqNum;
    }

    /// <summary>
    /// 计算Byte随机数值
    /// </summary>
    /// <returns></returns>
    public static byte GetByteRandomNum()
    {
        if (_randomNum < 255)
        {
            _randomNum += 1;
        }
        else
        {
            _randomNum = 1;
        }

        return _randomNum;
    }

    public static string GenChargeOrderSn()
    {
        return StaticStationInfo.StationNo + DateTime.Now.ToString(ChargerConst.DateFormat) +
               GetRandomNumLimit99();
    }

    public static string GenChargeOrderNo(string chargerSn)
    {
        return StaticStationInfo.StationNo +chargerSn + DateTime.Now.ToString(ChargerConst.yyyyMMddHHmmss) +
               GetRandomNumLimit99();
    }
    /// <summary>
    /// 根据云平台下发,计算本地充电机枪号
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static byte GetTheGun(string number)
    {
        int parsedNumber;

        if (int.TryParse(number, out parsedNumber))
        {
            if (parsedNumber % 2 == 0)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            throw new ArgumentException("转换失败");
        }
    }

    public static string GetOutChargerCode(string number)
    {
        int parsedNumber;

        if (int.TryParse(number, out parsedNumber))
        {
            // 除2,向上取整,拿到本地充电机code
            int ceilResult = (int)Math.Ceiling(parsedNumber / 2.0);
            return "C200" + ceilResult.ToString();
        }
        else
        {
            throw new ArgumentException("转换失败");
        }

    }

    /// <summary>
    /// 计算Byte随机数值
    /// </summary>
    /// <returns></returns>
    public static byte GetRandomNumLimit99()
    {
        if (_randomNum < 99)
        {
            _randomNum += 1;
        }
        else
        {
            _randomNum = 10;
        }

        return _randomNum;
    }

    #endregion 随机数

    // <summary>
    // 获取到异或后的鉴权字节数组
    // </summary>
    // <param name="strAuthCodes">鉴权字符串</param>
    // <returns>异或后的鉴权字节数组</returns>
    public static byte[] GetAuthCodesResult(string strAuthCodes, byte key)
    {
        byte[] results = new byte[8];
        if (!String.IsNullOrEmpty(strAuthCodes))
        {
            byte[] authCodes = Encoding.ASCII.GetBytes(strAuthCodes);

            results[0] = Convert.ToByte(authCodes[0] ^ key);
            results[1] = Convert.ToByte(authCodes[1] ^ key);
            results[2] = Convert.ToByte(authCodes[2] ^ key);
            results[3] = Convert.ToByte(authCodes[3] ^ key);
            results[4] = Convert.ToByte(authCodes[4] ^ key);
            results[5] = Convert.ToByte(authCodes[5] ^ key);
            results[6] = Convert.ToByte(authCodes[6] ^ key);
            results[7] = Convert.ToByte(authCodes[7] ^ key);
        }

        return results;
    }
}
