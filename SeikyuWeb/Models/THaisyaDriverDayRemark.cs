using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車ドライバー日付備考情報を表すクラス
    /// </summary>
    public partial class THaisyaDriverDayRemark
    {
        /// <summary>
        /// ドライバーID
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
