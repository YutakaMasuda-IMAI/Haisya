using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 配車車番連絡情報を表すクラス
    /// </summary>
    public partial class THaisyaSyabanRenraku
    {
        /// <summary>
        /// 車番連絡ID
        /// </summary>
        public int SyabanRenrakuId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 印刷日
        /// </summary>
        public DateTime PrintDate { get; set; }
        /// <summary>
        /// グループID
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime Day { get; set; }
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TantouId { get; set; }
        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }
        /// <summary>
        /// メールアドレス1
        /// </summary>
        public string MailAddress1 { get; set; }
        /// <summary>
        /// メールアドレス2
        /// </summary>
        public string MailAddress2 { get; set; }
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }
        /// <summary>
        /// 登録ユーザー
        /// </summary>
        public int InsertUser { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdateDatetime { get; set; }
        /// <summary>
        /// 更新ユーザー
        /// </summary>
        public int UpdateUser { get; set; }
    }
}
