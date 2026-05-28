using HybirdFrameworkCore.Autofac.Attribute;

namespace Service.Charger.Msg.Host.Req
{
    /// <summary>
    /// 3.4.10 站控设置尖峰平谷时间段标识
    /// </summary>
    public class SetPeakValleyTime : ASDU
    {
        /// <summary>
        /// 记录类型
        /// </summary>
        [Property(0, 8)]
        public byte RecordType { get; set; }

        /// <summary>
        /// 时段个数
        /// </summary>
        [Property(8, 8)]
        public byte NumberTime { get; set; }

        /// <summary>
        /// 时段1开始时间 时
        /// </summary>
        [Property(16, 8)]
        public byte StartHH1 { get; set; }

        /// <summary>
        /// 时段1开始时间 分
        /// </summary>
        [Property(24, 8)]
        public byte StartMM1 { get; set; }

        /// <summary>
        /// 时段1尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(32, 8)]
        public byte TimePeak1 { get; set; }


        /// <summary>
        /// 时段2开始时间 时
        /// </summary>
        [Property(40, 8)]
        public byte StartHH2 { get; set; }

        /// <summary>
        /// 时段2开始时间
        /// </summary>
        [Property(48, 8)]
        public byte StartMM2 { get; set; }

        /// <summary>
        /// 时段2尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(56, 8)]
        public byte TimePeak2 { get; set; }


        /// <summary>
        /// 时段3开始时间
        /// </summary>
        [Property(64, 8)]
        public byte StartHH3 { get; set; }

        /// <summary>
        /// 时段3开始时间
        /// </summary>
        [Property(72, 8)]
        public byte StartMM3 { get; set; }

        /// <summary>
        /// 时段3尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(80, 8)]
        public byte TimePeak3 { get; set; }

        /// <summary>
        /// 时段4开始时间
        /// </summary>
        [Property(88, 8)]
        public byte StartHH4 { get; set; }

        /// <summary>
        /// 时段4开始时间
        /// </summary>
        [Property(96, 8)]
        public byte StartMM4 { get; set; }

        /// <summary>
        /// 时段4尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(104, 8)]
        public byte TimePeak4 { get; set; }

        /// <summary>
        /// 时段5开始时间
        /// </summary>
        [Property(112, 8)]
        public byte StartHH5 { get; set; }

        /// <summary>
        /// 时段5开始时间
        /// </summary>
        [Property(120, 8)]
        public byte StartMM5 { get; set; }

        /// <summary>
        /// 时段5尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(128, 8)]
        public byte TimePeak5 { get; set; }

        /// <summary>
        /// 时段6开始时间
        /// </summary>
        [Property(136, 8)]
        public byte StartHH6 { get; set; }

        /// <summary>
        /// 时段6开始时间
        /// </summary>
        [Property(144, 8)]
        public byte StartMM6 { get; set; }

        /// <summary>
        /// 时段6尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(152, 8)]
        public byte TimePeak6 { get; set; }

        /// <summary>
        /// 时段7开始时间
        /// </summary>
        [Property(160, 8)]
        public byte StartHH7 { get; set; }

        /// <summary>
        /// 时段7开始时间
        /// </summary>
        [Property(168, 8)]
        public byte StartMM7 { get; set; }

        /// <summary>
        /// 时段7尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(176, 8)]
        public byte TimePeak7 { get; set; }

        /// <summary>
        /// 时段8开始时间
        /// </summary>
        [Property(184, 8)]
        public byte StartHH8 { get; set; }

        /// <summary>
        /// 时段8开始时间
        /// </summary>
        [Property(192, 8)]
        public byte StartMM8 { get; set; }

        /// <summary>
        /// 时段8尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(200, 8)]
        public byte TimePeak8 { get; set; }

        /// <summary>
        /// 时段9开始时间
        /// </summary>
        [Property(208, 8)]
        public byte StartHH9 { get; set; }
        /// <summary>
        /// 时段9开始时间
        /// </summary>
        [Property(216, 8)]
        public byte StartMM9 { get; set; }
        /// <summary>
        /// 时段9尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(224, 8)]
        public byte TimePeak9 { get; set; }

        /// <summary>
        /// 时段10开始时间
        /// </summary>
        [Property(232, 8)]
        public byte StartHH10 { get; set; }
        /// <summary>
        /// 时段10开始时间
        /// </summary>
        [Property(240, 8)]
        public byte StartMM10 { get; set; }
        /// <summary>
        /// 时段10尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(248, 8)]
        public byte TimePeak10 { get; set; }

        /// <summary>
        /// 时段11开始时间
        /// </summary>
        [Property(256, 8)]
        public byte StartHH11 { get; set; }
        /// <summary>
        /// 时段11开始时间
        /// </summary>
        [Property(264, 8)]
        public byte StartMM11 { get; set; }
        /// <summary>
        /// 时段11尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(272, 8)]
        public byte TimePeak11 { get; set; }

        /// <summary>
        /// 时段12开始时间
        /// </summary>
        [Property(280, 8)]
        public byte StartHH12 { get; set; }
        /// <summary>
        /// 时段12开始时间
        /// </summary>
        [Property(288, 8)]
        public byte StartMM12 { get; set; }
        /// <summary>
        /// 时段12尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(296, 8)]
        public byte TimePeak12 { get; set; }

        /// <summary>
        /// 时段13开始时间
        /// </summary>
        [Property(314, 8)]
        public byte StartHH13 { get; set; }
        /// <summary>
        /// 时段13开始时间
        /// </summary>
        [Property(322, 8)]
        public byte StartMM13 { get; set; }
        /// <summary>
        /// 时段13尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(330, 8)]
        public byte TimePeak13 { get; set; }

        /// <summary>
        /// 时段14开始时间
        /// </summary>
        [Property(338, 8)]
        public byte StartHH14 { get; set; }
        /// <summary>
        /// 时段14开始时间
        /// </summary>
        [Property(346, 8)]
        public byte StartMM14 { get; set; }
        /// <summary>
        /// 时段14尖峰标识  1尖 2峰 3平 4谷
        /// </summary>
        [Property(354, 8)]
        public byte TimePeak14 { get; set; }

        public SetPeakValleyTime()
        {
            FrameTypeNo = 45;
            MsgBodyCount = 1;
            TransReason = 3;
            PublicAddr = 0;
            MsgBodyAddr = new byte[] { 0, 0, 0 };

            RecordType = 47;
        }
    }
}