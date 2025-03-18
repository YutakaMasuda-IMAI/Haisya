using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 燃料費を表します。
    /// </summary>
    public partial class MFuelCost
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 燃料量
        /// </summary>
        public decimal FuelAmount { get; set; }
    }
}
