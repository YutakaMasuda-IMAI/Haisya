using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 請求を表すクラス
    /// </summary>
    public partial class TCheckSeikyu
    {
        /// <summary>
        /// 請求ID
        /// </summary>
        public int CheckSeikyuId { get; set; }

        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 印刷パターン
        /// </summary>
        public int PrintPattern { get; set; }

        /// <summary>
        /// チェック区分
        /// </summary>
        public int CheckKubun { get; set; }

        /// <summary>
        /// チェックステータス
        /// </summary>
        public int CheckStatus { get; set; }

        /// <summary>
        /// 顧客支店ID
        /// </summary>
        public int CustomerBranchId { get; set; }

        /// <summary>
        /// 請求月
        /// </summary>
        public DateTime SeikyuMonth { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int ShimeDay { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int ZeiKubun { get; set; }

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
        /// 挿入日時
        /// </summary>
        public DateTime InsertDatetime { get; set; }

        /// <summary>
        /// 挿入ユーザー
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

        /// <summary>
        /// 開始日
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}
