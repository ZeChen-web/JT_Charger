using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Bms
{
    public class SysCode : ASDU
    {
        /// <summary>
        /// 电池编码信息帧序号  Battery Code number of frame SN
        /// </summary>
        [Property(0, 8)]
        public byte SNFrameNo { get; set; }

        /// <summary>
        /// 电池编码长度 Length of battery code
        /// </summary>
        [Property(8, 8)]
        public byte SNSysCodeLength { get; set; }

        /// <summary>
        /// 电池编码(SN)字符1,7,13,19(ASCII) Battery Code(SN) Character 1,7,13,19(ASCII)
        /// </summary>
        [Property(16, 8)]
        public byte SNPackCodeB1 { get; set; }

        /// <summary>
        /// 电池编码(SN)字符2,8,14,20(ASCII)  Battery Code(SN) Character 2,8,14,20(ASCII)
        /// </summary>
        [Property(24, 8)]
        public byte SNPackCodeB2 { get; set; }

        /// <summary>
        /// 电池编码(SN)字符3,9,15,21(ASCII) Battery Code(SN) Character 3,9,15,21(ASCII)
        /// </summary>
        [Property(32, 8)]
        public byte SNPackCodeB3 { get; set; }

        /// <summary>
        /// 电池编码(SN)字符4,10,16,22(ASCII)  Battery Code(SN) Character4,10,16,22(ASCII)
        /// </summary>
        [Property(40, 8)]
        public byte SNPackCodeB4 { get; set; }

        /// <summary>
        /// 电池编码(SN)字符5,11,17,23(ASCII)  Battery Code(SN) Character 5,11,17,23(ASCII)
        /// </summary>
        [Property(48, 8)]
        public byte SNPackCodeB5 { get; set; }

        /// <summary>
        /// 电池编码(SN)字符6,12,18,24(ASCII)  Battery Code(SN) Character 6,12,18,24(ASCII)
        /// </summary>
        [Property(56, 8)]
        public byte SNPackCodeB6 { get; set; }


        public string SNPackCodeString { get; set; }
    }
}