using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払情報を表します。
    /// </summary>
    public partial class TShitabarai
    {
        /// <summary>
        /// 支払ID
        /// </summary>
        public int ShitabaraiId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 支払区分
        /// </summary>
        public int ShitabaraiKubun { get; set; }
        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int PrintPattern { get; set; }
        /// <summary>
        /// 傭車支店ID
        /// </summary>
        public int YosyaBranchId { get; set; }
        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }
        /// <summary>
        /// 支払月
        /// </summary>
        public DateTime ShitabaraiMonth { get; set; }
        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }
        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DelDatetime { get; set; }
        /// <summary>
        /// 印刷日時
        /// </summary>
        public DateTime PrintDatetime { get; set; }
        /// <summary>
        /// 印刷日
        /// </summary>
        public DateTime PrintDate { get; set; }
        /// <summary>
        /// 印刷終了日
        /// </summary>
        public DateTime? PrintToDate { get; set; }
        /// <summary>
        /// メールアドレス1
        /// </summary>
        public string MailAddress1 { get; set; }
        /// <summary>
        /// メールアドレス2
        /// </summary>
        public string MailAddress2 { get; set; }
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
