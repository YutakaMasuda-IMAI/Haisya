using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 勤怠情報を表します。
    /// </summary>
    public partial class TKintai
    {
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 連続日数
        /// </summary>
        public int RenzokuDays { get; set; }
        /// <summary>
        /// 労働時間
        /// </summary>
        public double WorkTime { get; set; }
    }
}
