using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 専属情報を表すクラス
    /// </summary>
    public partial class VSenzoku
    {
        /// <summary>
        /// 専属ID
        /// </summary>
        public int SenzokuId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        /// <summary>
        /// 顧客支店コード
        /// </summary>
        public string CustomerBranchCode { get; set; }
        /// <summary>
        /// 顧客支店名
        /// </summary>
        public string CustomerBranchName { get; set; }
        /// <summary>
        /// 顧客支店名略称
        /// </summary>
        public string CustomerBranchNameAbbr { get; set; }
        /// <summary>
        /// 担当者コード
        /// </summary>
        public string TantouCode { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string TantouName { get; set; }
        /// <summary>
        /// 担当者名略称
        /// </summary>
        public string TantouNameAbbr { get; set; }
        /// <summary>
        /// 顧客担当者ID
        /// </summary>
        public int KokyakuTantouId { get; set; }
        /// <summary>
        /// 専属名
        /// </summary>
        public string SenzokuName { get; set; }
        /// <summary>
        /// 専属名略称
        /// </summary>
        public string SenzokuNameAbbr { get; set; }
    }
}
