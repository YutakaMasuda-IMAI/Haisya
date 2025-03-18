using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報承認情報を表します。
    /// </summary>
    public partial class TNippouApproval
    {
        /// <summary>
        /// 日報承認ID
        /// </summary>
        public int NippouApprovalId { get; set; }
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// 承認グループID
        /// </summary>
        public int ApprovalGroupId { get; set; }
        /// <summary>
        /// 期限日時
        /// </summary>
        public DateTime LimitDateTime { get; set; }
        /// <summary>
        /// 承認結果
        /// </summary>
        public int ApprovalResult { get; set; }
        /// <summary>
        /// 結果日時
        /// </summary>
        public DateTime? ResultDatetime { get; set; }
        /// <summary>
        /// 注文メモ
        /// </summary>
        public string OrderMemo { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime? InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int? InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime? UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int? UpdateUser { get; set; }
    }
}
