using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 案件の親顧客に関する情報を表します。
    /// </summary>
    public partial class TAnkenOyaKokyaku
    {
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 案件の順序
        /// </summary>
        public int AnkenOrder { get; set; }
        /// <summary>
        /// 顧客の順序
        /// </summary>
        public int KokyakuOrder { get; set; }
        /// <summary>
        /// 項目タイトル
        /// </summary>
        public string KomokuTitle { get; set; }
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int? KokyakuId { get; set; }
        /// <summary>
        /// 顧客名
        /// </summary>
        public string KokyakuName { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int? TantouId { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string TantouName { get; set; }
        /// <summary>
        /// 担当者電話番号
        /// </summary>
        public string TantouPhone { get; set; }
    }
}
