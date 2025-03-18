using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客担当配車グループを表します。
    /// </summary>
    public partial class MCustomerTantouHaisyaGroup
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
    }
}
