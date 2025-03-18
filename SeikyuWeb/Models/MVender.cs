using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// MVender クラスは、仕入先情報を管理します。
    /// </summary>
    public partial class MVender
    {
        /// <summary>
        /// 仕入先ID
        /// </summary>
        public int VenderId { get; set; }
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// 仕入先コード
        /// </summary>
        public string VenderCode { get; set; }
        /// <summary>
        /// 仕入先名
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 仕入先名（カナ）
        /// </summary>
        public string VenderNameKana { get; set; }
        /// <summary>
        /// 仕入先名（略称）
        /// </summary>
        public string VenderNameAbbr { get; set; }
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
        /// 支払担当者ID
        /// </summary>
        public int ShiharaiTantouId { get; set; }
        /// <summary>
        /// 締日1
        /// </summary>
        public int? ClosingDate1 { get; set; }
        /// <summary>
        /// 締日
        /// </summary>
        public int ShimeDay { get; set; }
        /// <summary>
        /// 支払備考
        /// </summary>
        public string ShiharaiRemarks { get; set; }
        /// <summary>
        /// 支払仕入先ID
        /// </summary>
        public int ShiharaiVenderId { get; set; }
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
