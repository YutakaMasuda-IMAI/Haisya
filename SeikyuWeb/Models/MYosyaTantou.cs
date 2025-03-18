using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 他社担当者に関する情報を表します。
    /// </summary>
    public partial class MYosyaTantou
    {
        /// <summary>
        /// 担当者ID
        /// </summary>
        public int TantouId { get; set; }
        /// <summary>
        /// 他社ID
        /// </summary>
        public int YosyaId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 担当者コード
        /// </summary>
        public string TantouCode { get; set; }
        /// <summary>
        /// 部署名
        /// </summary>
        public string BusyoName { get; set; }
        /// <summary>
        /// 役職名
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string TantouName { get; set; }
        /// <summary>
        /// 担当者名（カナ）
        /// </summary>
        public string TantouNameKana { get; set; }
        /// <summary>
        /// 担当者名（略称）
        /// </summary>
        public string TantouNameAbbr { get; set; }
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// メールタイトル
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// メールアドレス1
        /// </summary>
        public string MailAddress1 { get; set; }
        /// <summary>
        /// メールアドレス2
        /// </summary>
        public string MailAddress2 { get; set; }
        /// <summary>
        /// 住所1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// 住所2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// 住所3
        /// </summary>
        public string Address3 { get; set; }
        /// <summary>
        /// 電話番号1
        /// </summary>
        public string Phone1 { get; set; }
        /// <summary>
        /// 電話番号2
        /// </summary>
        public string Phone2 { get; set; }
        /// <summary>
        /// FAX番号1
        /// </summary>
        public string Fax1 { get; set; }
        /// <summary>
        /// FAX番号2
        /// </summary>
        public string Fax2 { get; set; }
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
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
