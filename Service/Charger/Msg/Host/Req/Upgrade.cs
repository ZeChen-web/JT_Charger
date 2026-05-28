using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.14 监控平台下发充放电机程序升级指令
    /// </summary>
    public class Upgrade : ASDU
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        [Property(0, 160, PropertyReadConstant.Byte)]
        public string DownloadLink { get; set; }

        /// <summary>
        /// 操作标识  1：升级；2：回滚 备注：当值为 2 时，下载地址无意义
        /// </summary>
        [Property(160, 8)]
        public byte OperationalId { get; set; }


        #region MyRegion

        //[PropertyAttribute(0, 8)]
        //public byte Version1 { get; set; }
        //[PropertyAttribute(8, 8)]
        //public byte Version2 { get; set; }
        //[PropertyAttribute(16, 8)]
        //public byte Version3 { get; set; }
        //[PropertyAttribute(24, 32)]
        //public UInt32 Size { get; set; }
        //[PropertyAttribute(56, 8)]

        //public string Filename { get; set; }
        //public string MD5 { get; set; }

        //public byte Period { get; set; }
        //public byte MaxReSendTimes { get; set; }

        //public Upgrade(byte version1, byte version2, byte version3, UInt32 size, string filename, string md5,
        //    byte period, byte maxReSendTimes)
        //{
        //    this.Version1 = version1;
        //    this.Version2 = version2;
        //    this.Version3 = version3;

        //    this.Size = size;
        //    this.Filename = filename;
        //    this.MD5 = md5;
        //    this.Period = period;
        //    this.MaxReSendTimes = maxReSendTimes;

        //    this.FrameTypeNo = 45;
        //    //this.SetReason(3);
        //    this.MsgBodyCount = 1;
        //    //this.RecordType = 33;
        //}

        #endregion
    }
}