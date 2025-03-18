using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 日報情報を表します。
    /// </summary>
    public partial class TNippou
    {
        /// <summary>
        /// 日報ID
        /// </summary>
        public int NippouId { get; set; }
        /// <summary>
        /// 案件表示ID
        /// </summary>
        public int AnkenDisplayId { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int AnkenId { get; set; }
        /// <summary>
        /// 受領
        /// </summary>
        public int Receipt { get; set; }
        /// <summary>
        /// 受領日
        /// </summary>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// コメント
        /// </summary>
        public string Commnet { get; set; }
        /// <summary>
        /// 承認ステータス
        /// </summary>
        public int ApprovalStatus { get; set; }
        /// <summary>
        /// 連携ステータス
        /// </summary>
        public int RenkeiStatus { get; set; }
        /// <summary>
        /// 距離
        /// </summary>
        public double? Distance { get; set; }
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
        /// <summary>
        /// デジタコリンク結果
        /// </summary>
        public int DegitakoLinkResult { get; set; }
    }
}
