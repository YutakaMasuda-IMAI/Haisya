using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ごとのデフォルト待機時間料金を表します。
    /// </summary>
    public partial class MDefaultMoneyWaitTimeForCompany
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// エリア
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 車種サイズ
        /// </summary>
        public string SyasyuSize { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// 2時間以上の金額
        /// </summary>
        public decimal AmountOver2Hour { get; set; }
    }
}
