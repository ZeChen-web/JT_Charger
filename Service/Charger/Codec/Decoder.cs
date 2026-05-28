using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Utils;
using HybirdFrameworkDriver.Session;
using log4net;
using Newtonsoft.Json;
using Service.Charger.Common;
using Service.Charger.Msg;
using Service.Charger.Msg.Bms;
using Service.Charger.Msg.Charger.OutCharger.Req;
using Service.Charger.Msg.Charger.OutCharger.Resp;
using Service.Charger.Msg.Charger.Req;
using Service.Charger.Msg.Charger.Resp;

namespace Service.Charger.Codec;

public class Decoder : ByteToMessageDecoder
{

    private readonly IByteBuffer[] _delimiters = { Unpooled.CopiedBuffer(ChargerConst.StartChar) };

    private ILog Log(string? chargerSn)
    {
        if (ObjUtils.IsNotNullOrWhiteSpace(chargerSn))
        {
            return LogManager.GetLogger("Charger" + chargerSn);
        }
        return LogManager.GetLogger(typeof(Decoder));
    }
    protected override void Decode(IChannelHandlerContext context, IByteBuffer buffer, List<object> output)
    {
        string? chargerSn = ChannelUtils.GetAttr(context.Channel, ChargerConst.ChargerSn);
        IByteBuffer? delimiter = FindDelimiter(buffer);
        if (delimiter != null)
        {
            //分隔符索引
            int delimiterIndex = IndexOf(buffer, delimiter);
            //帧长索引
            int frameLengthIndex = delimiterIndex + delimiter.Capacity;

            if (delimiterIndex > 0)
            {
                buffer.SkipBytes(delimiterIndex);
                return;
            }

            if (buffer.ReadableBytes < frameLengthIndex + 2)
            {
                // 数据不足，等待更多数据
                return;
            }

            // 读取长度字段
            int frameLength = buffer.GetUnsignedShortLE(buffer.ReaderIndex + frameLengthIndex);
            //总帧长
            int totalFrameLength = delimiterIndex + delimiter.Capacity + 2 + frameLength;
            // 最小总帧长过滤
            if (totalFrameLength < 24)
            {
                return;
            }

            if (buffer.ReadableBytes < totalFrameLength)
            {
                // 数据不足，等待更多数据
                return;
            }

            byte[]? data = null;
            try
            {
                ASDU asdu = Parse(buffer, totalFrameLength, delimiter, out data);
                Log(chargerSn).Info($"receive {BitUtls.BytesToHexStr(data)}:{JsonConvert.SerializeObject(asdu)} from {chargerSn}");
                output.Add(asdu);
            }
            catch (Exception e)
            {
                Log(chargerSn).Error($"decode fail msg={BitUtls.BytesToHexStr(data)}");
            }
        }
    }

    private IByteBuffer? FindDelimiter(IByteBuffer buffer)
    {
        foreach (IByteBuffer delimiter in _delimiters)
        {
            int delimiterIndex = IndexOf(buffer, delimiter);
            if (delimiterIndex >= 0)
            {
                return delimiter;
            }
        }

        return null;
    }

    private static int IndexOf(IByteBuffer haystack, IByteBuffer needle)
    {
        for (int i = haystack.ReaderIndex; i < haystack.WriterIndex; i++)
        {
            int num = i;
            int j;
            for (j = 0; j < needle.Capacity && haystack.GetByte(num) == needle.GetByte(j); j++)
            {
                num++;
                if (num == haystack.WriterIndex && j != needle.Capacity - 1)
                {
                    return -1;
                }
            }

            if (j == needle.Capacity)
            {
                return i - haystack.ReaderIndex;
            }
        }

        return -1;
    }

    public ASDU Parse(IByteBuffer byteBuffer, int totalFrameLength, IByteBuffer delimiter, out byte[] data)
    {
        data = new byte[totalFrameLength];
        byteBuffer.ReadBytes(data);

        int removeIndex = delimiter.Capacity;

        ushort cmd =data[14 + removeIndex];
        ushort recordType = data[23 + removeIndex];
        byte[] bytes = new byte[data.Length - (23 + removeIndex)];
        Array.Copy(data, 23 + removeIndex, bytes, 0, bytes.Length);
        ASDU asdu = cmd switch
        {
            5 => ModelConvert.Decode<UploadRemoteSignalData>(bytes),
            #region 45
            45 => recordType switch
            {
                1 => ModelConvert.Decode<FinishStartCharging>(bytes),
                3 => ModelConvert.Decode<FinishStopCharging>(bytes),
                6 => ModelConvert.Decode<PowerRegulationRes>(bytes),
                10 => ModelConvert.Decode<AuxiliaryPowerRes>(bytes),
                11 => ModelConvert.Decode<Login>(bytes),
                13 => ModelConvert.Decode<HeartBeat>(bytes),
                21 => ModelConvert.Decode<AdjustChargeRateRes>(bytes),
                25 => ModelConvert.Decode<AuthRes>(bytes),
                34 => ModelConvert.Decode<UpgradeRequestRes>(bytes),
                35 => ModelConvert.Decode<UplinkUpgrade>(bytes),
                41 => ModelConvert.Decode<VehicleVIN>(bytes),
                44 => ModelConvert.Decode<OfflineStopChargingRes>(bytes),
                48 => ModelConvert.Decode<ResponseSetting>(bytes),
                50 => ModelConvert.Decode<ChangeChargeModeRes>(bytes),
                52 => ModelConvert.Decode<AuthVINRes>(bytes),
                //75 => ModelConvert.Decode<UploadModuleStatus>(bytes),
                80 => ModelConvert.Decode<ModuleState>(bytes),
                81 => ModelConvert.Decode<RemoteSignaling>(bytes),
                82 => ModelConvert.Decode<BatteryPackData>(bytes),
                83 => ModelConvert.Decode<BatteryPackDataVoltage>(bytes),
                84 => ModelConvert.Decode<BatteryPackTotalElectricity>(bytes),
                85 => ModelConvert.Decode<BatteryPackPortTemperature>(bytes),
                86 => ModelConvert.Decode<BatteryPackStateAndFault>(bytes),
                87 => ModelConvert.Decode<AcMeter>(bytes),

                _ => throw new InvalidOperationException("This should never be reached"),
            },
            #endregion
            #region 46
            46 => recordType switch
            {
                2 => ModelConvert.Decode<Status>(bytes),
                3 => ModelConvert.Decode<Alarm1>(bytes),
                4 => ModelConvert.Decode<Alarm2>(bytes),
                5 => ModelConvert.Decode<PackInfo>(bytes),
                6 => ModelConvert.Decode<BranChcurr>(bytes),
                7 => ModelConvert.Decode<RelayStatus>(bytes),
                8 => ModelConvert.Decode<PackCurrlmt>(bytes),
                9 => ModelConvert.Decode<RunStatusInfo>(bytes),
                10 => ModelConvert.Decode<SumVolt>(bytes),
                11 => ModelConvert.Decode<SumTemp>(bytes),
                12 => ModelConvert.Decode<ChrgaccuInfo>(bytes),
                13 => ModelConvert.Decode<DisChrgaccuInfo>(bytes),
                14 => ModelConvert.Decode<REChrgaccuInfo>(bytes),
                15 => ModelConvert.Decode<IsoInfo>(bytes),
                16 => ModelConvert.Decode<BrachSumVolt1>(bytes),
                17 => ModelConvert.Decode<BrachSumVolt2>(bytes),
                18 => ModelConvert.Decode<BrachSumVolt3>(bytes),
                19 => ModelConvert.Decode<BrachSumTemp1>(bytes),
                20 => ModelConvert.Decode<BrachSumTemp2>(bytes),
                21 => ModelConvert.Decode<Volt01>(bytes),
                22 => ModelConvert.Decode<Volt02>(bytes),
                23 => ModelConvert.Decode<Volt03>(bytes),
                24 => ModelConvert.Decode<Volt04>(bytes),
                25 => ModelConvert.Decode<Volt05>(bytes),
                26 => ModelConvert.Decode<Volt06>(bytes),
                27 => ModelConvert.Decode<Volt07>(bytes),
                28 => ModelConvert.Decode<Volt08>(bytes),
                29 => ModelConvert.Decode<Volt09>(bytes),
                30 => ModelConvert.Decode<Volt10>(bytes),
                31 => ModelConvert.Decode<Volt11>(bytes),
                32 => ModelConvert.Decode<Volt12>(bytes),
                33 => ModelConvert.Decode<Volt13>(bytes),
                34 => ModelConvert.Decode<Volt14>(bytes),
                35 => ModelConvert.Decode<Volt15>(bytes),
                36 => ModelConvert.Decode<Volt16>(bytes),
                37 => ModelConvert.Decode<Volt17>(bytes),
                38 => ModelConvert.Decode<Volt18>(bytes),
                39 => ModelConvert.Decode<Volt19>(bytes),
                40 => ModelConvert.Decode<Volt20>(bytes),
                41 => ModelConvert.Decode<Volt21>(bytes),
                42 => ModelConvert.Decode<Volt22>(bytes),
                43 => ModelConvert.Decode<Volt23>(bytes),
                44 => ModelConvert.Decode<Volt24>(bytes),
                45 => ModelConvert.Decode<Volt25>(bytes),
                46 => ModelConvert.Decode<Volt26>(bytes),
                47 => ModelConvert.Decode<Volt27>(bytes),
                48 => ModelConvert.Decode<Volt28>(bytes),
                49 => ModelConvert.Decode<Volt29>(bytes),
                50 => ModelConvert.Decode<Volt30>(bytes),
                51 => ModelConvert.Decode<Temp1>(bytes),
                52 => ModelConvert.Decode<Temp2>(bytes),
                53 => ModelConvert.Decode<Temp3>(bytes),
                54 => ModelConvert.Decode<Temp4>(bytes),
                55 => ModelConvert.Decode<Temp5>(bytes),
                56 => ModelConvert.Decode<Temp6>(bytes),
                57 => ModelConvert.Decode<CscSumVolt>(bytes),
                58 => ModelConvert.Decode<CscSumTemp>(bytes),
                59 => ModelConvert.Decode<TimingInfo>(bytes),
                60 => ModelConvert.Decode<UpBms>(bytes),
                61 => ModelConvert.Decode<UpAlarm>(bytes),
                62 => ModelConvert.Decode<VoltageCurrentSoc>(bytes),
                65 => ModelConvert.Decode<VoltageExtremumStatistics>(bytes),
                66 => ModelConvert.Decode<Detectionpointextremumdata>(bytes),
                72 => ModelConvert.Decode<BasicParameter>(bytes),
                81 => ModelConvert.Decode<QueryBatterySnRes>(bytes),
                82 => ModelConvert.Decode<BatteryBaseInfo>(bytes),
                142 => ModelConvert.Decode<BattenergyInfo1>(bytes),
                143 => ModelConvert.Decode<BattenergyInfo2>(bytes),
                144 => ModelConvert.Decode<BattenergyInfo3>(bytes),
                145 => ModelConvert.Decode<BattenergyInfo4>(bytes),
                146 => ModelConvert.Decode<BattenergyInfo5>(bytes),
                147 => ModelConvert.Decode<BattenergyInfo6>(bytes),
                148 => ModelConvert.Decode<SysCode>(bytes),
                _ => throw new InvalidOperationException("This should never be reached"),
            },
            51 => recordType switch
            {
                2 => ModelConvert.Decode<PileStartChargeRes>(bytes),
                3 => ModelConvert.Decode<PileStopChargeRes>(bytes),
                5 => ModelConvert.Decode<PileStartChargeCompleteReq>(bytes),
                7 => ModelConvert.Decode<PileChargeCompleteReq>(bytes),
                12 => ModelConvert.Decode<PileUploadTelemetry>(bytes),
                11 => ModelConvert.Decode<PileUploadRemoteSignal>(bytes),
                13 => ModelConvert.Decode<PileUploadChargeRecord>(bytes),
                10 => ModelConvert.Decode<PileAdjustPowerRes>(bytes),
                _ => throw new InvalidOperationException("This should never be reached"),
            },
            #endregion

            42 => ModelConvert.Decode<RecordCharge>(bytes),
            11 => ModelConvert.Decode<UploadTelemetryData>(bytes),
            49 => ModelConvert.Decode<RemoteStartChargingRes>(bytes),
            50 => ModelConvert.Decode<RemoteStopChargingRes>(bytes),

            _ => new ASDU()
        };
        ASDU.ParseHeader(data, asdu);

        return asdu;
    }
}
