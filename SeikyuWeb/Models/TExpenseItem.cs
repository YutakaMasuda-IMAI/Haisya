using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 経費項目情報を表すクラス
    /// </summary>
    public partial class TExpenseItem
    {
        /// <summary>
        /// 経費項目ID
        /// </summary>
        public int ExpenseItemId { get; set; }
        /// <summary>
        /// 経費区分
        /// </summary>
        public int ExpenseKubun { get; set; }
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// 項目名
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 項目表示名
        /// </summary>
        public string ItemDisplay { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int DelFlg { get; set; }
    }
}
