using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HybirdFrameworkCore.Utils;
using HybirdFrameworkDriver.Session;
using log4net;
using Newtonsoft.Json;
using Service.ChargerV14D.Common;
using Service.ChargerV14D.Msg;
using Service.ChargerV14D.Msg.Req;
using Service.ChargerV14D.Server;

namespace Service.ChargerV14D.Codec;

/// <summary>V1.4D 协议解码器</summary>
public class V14DDecoder : ByteToMessageDecoder
{
    private static readonly byte[] StartDelimiter = { V14DConst.StartFlag };
    private ILog Log(string? chargerSn)
    {
        if (ObjUtils.IsNotNullOrWhiteSpace(chargerSn))
        {
            return LogManager.GetLogger( chargerSn);
        }
        return LogManager.GetLogger(typeof(V14DDecoder));
    }

    protected override void Decode(IChannelHandlerContext context, IByteBuffer buffer, List<object> output)
    {
        //string? pileCode = ChannelUtils.GetAttr(context.Channel, V14DConst.PileSn);
        //string? pileCode = V14DClientMgr.GetBySn(context.Channel.Id.ToString())?.Sn;
        string? pileCode = ChannelUtils.GetAttr(context.Channel, V14DConst.PileSn);
        
        // 查找起始标志 0x68
        int delimiterIndex = IndexOf(buffer, StartDelimiter);
        if (delimiterIndex < 0) return;

        // 跳过起始标志前的数据
        if (delimiterIndex > 0)
        {
            buffer.SkipBytes(delimiterIndex);
            return;
        }

        // 需要至少: 0x68 + DataLen(1) = 2 字节来读取长度
        if (buffer.ReadableBytes < 2) return;

        // 读取数据长度 (起始标志后1字节)
        int dataLen = buffer.GetByte(buffer.ReaderIndex + 1) & 0xFF;

        // 总帧长 = 起始标志(1) + DataLen(1) + DataLen + CRC(2)
        int totalFrameLength = 1 + 1 + dataLen + 2;

        // 最小帧长校验
        if (totalFrameLength < V14DConst.MinFrameLength) return;

        // 数据域长度校验
        if (dataLen > V14DConst.MaxDataLen) return;

        // 数据不足, 等待更多数据
        if (buffer.ReadableBytes < totalFrameLength) return;

        byte[]? frameData = null;
        try
        {
            frameData = new byte[totalFrameLength];
            buffer.ReadBytes(frameData);

            // 验证CRC
            /*if (!V14DUtils.Crc16Verify(frameData))
            {
                _log.Warn($"V14D CRC verify failed from {pileCode}, frame={BitUtls.BytesToHexStr(frameData)}");
                return;
            }*/

            // 提取帧类型和Body
            byte frameType = V14DFrame.GetFrameType(frameData);
            byte[] body = V14DFrame.ExtractBody(frameData);

            // 根据帧类型分发创建消息对象 (仅处理上行消息)
            V14DFrame? msg = frameType switch
            {
                V14DFrameType.Login => new V14DLoginReq(body),
                V14DFrameType.Heartbeat => new V14DHeartbeatReq(body),
                V14DFrameType.BillingModelVerify => new V14DBillingModelVerifyReq(body),
                V14DFrameType.BillingModelReq => new V14DBillingModelReq(body),
                V14DFrameType.UploadRealTimeData => new V14DRealTimeDataReq(body),
                V14DFrameType.ChargeHandshake => new V14DChargeHandshakeReq(body),
                V14DFrameType.ParamConfig => new V14DParamConfigReq(body),
                V14DFrameType.ChargeEnd => new V14DChargeEndReq(body),
                V14DFrameType.ErrorMsg => new V14DErrorMsgReq(body),
                V14DFrameType.BmsAbort => new V14DBmsAbortReq(body),
                V14DFrameType.ChargerAbort => new V14DChargerAbortReq(body),
                V14DFrameType.BmsDemandOutput => new V14DBmsDemandOutputReq(body),
                V14DFrameType.BmsInfo => new V14DBmsInfoReq(body),
                V14DFrameType.ApplyStartCharge => new V14DApplyStartChargeReq(body),
                V14DFrameType.RemoteStartChargeReply => new V14DRemoteStartChargeReplyReq(body),
                V14DFrameType.RemoteStopChargeReply => new V14DRemoteStopChargeReplyReq(body),
                V14DFrameType.TransactionRecord => new V14DTransactionRecordReq(body),
                V14DFrameType.BalanceUpdateReply => new V14DBalanceUpdateReplyReq(body),
                V14DFrameType.CardSyncReply => new V14DCardSyncReplyReq(body),
                V14DFrameType.ParamSetReply => new V14DParamSetReplyReq(body),
                V14DFrameType.TimeSyncReply => new V14DTimeSyncReplyReq(body),
                V14DFrameType.BillingModelSetReply => new V14DBillingModelSetReplyReq(body),
                V14DFrameType.LockDataUpload => new V14DLockDataUploadReq(body),
                V14DFrameType.LockCommandReply => new V14DLockCommandReplyReq(body),
                V14DFrameType.RemoteRestartReply => new V14DRemoteRestartReplyReq(body),
                V14DFrameType.RemoteUpdateReply => new V14DRemoteUpdateReplyReq(body),
                V14DFrameType.QRCodeReply => new V14DQRCodeReplyReq(body),
                V14DFrameType.VINQueryReply => new V14DVINQueryReplyReq(body),
                _ => null
            };

            if (msg != null)
            {
                V14DFrame.ParseHeader(frameData, msg);
                Log(pileCode).Info($"V14D Receive [{frameType:X2}] {BitUtls.BytesToHexStr(frameData)} : {JsonConvert.SerializeObject(msg)} from {pileCode}");
                output.Add(msg);
            }
            else
            {
                Log(pileCode).Info($"V14D Receive unknown FrameType=0x{frameType:X2} from {pileCode}:{BitUtls.BytesToHexStr(frameData)} ");
            }
        }
        catch (Exception e)
        {
            Log(pileCode).Info($"V14D decode fail, data={BitUtls.BytesToHexStr(frameData)}", e);
        }
    }

    private static int IndexOf(IByteBuffer haystack, byte[] needle)
    {
        for (int i = haystack.ReaderIndex; i < haystack.WriterIndex; i++)
        {
            int num = i;
            int j;
            for (j = 0; j < needle.Length && haystack.GetByte(num) == needle[j]; j++)
            {
                num++;
                if (num == haystack.WriterIndex && j != needle.Length - 1)
                    return -1;
            }
            if (j == needle.Length)
                return i - haystack.ReaderIndex;
        }
        return -1;
    }
}
