using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客バックアップを表すクラス
    /// </summary>
    public partial class MCustomerBak20240718
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// 予社フラグ
        /// </summary>
        public int YosyaFlg { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 親顧客ID
        /// </summary>
        public int CustomerIdOya { get; set; }
        
        /// <summary>
        /// 顧客コード
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// 親顧客コード
        /// </summary>
        public string CustomerCodeOya { get; set; }
        
        /// <summary>
        /// 顧客名
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// 顧客名カナ
        /// </summary>
        public string CustomerNameKana { get; set; }
        
        /// <summary>
        /// 顧客名略称
        /// </summary>
        public string CustomerNameAbbr { get; set; }
        
        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; }
        
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
        /// 請求区分
        /// </summary>
        public int SeikyuKubun { get; set; }
        
        /// <summary>
        /// 請求日区分
        /// </summary>
        public int SeikyuDateKubun { get; set; }
        
        /// <summary>
        /// 通行料区分
        /// </summary>
        public int TollKubun { get; set; }
        
        /// <summary>
        /// 締日
        /// </summary>
        public int ShimeDay { get; set; }
        
        /// <summary>
        /// 案件備考
        /// </summary>
        public string AnkenRemarks { get; set; }
        
        /// <summary>
        /// 請求備考
        /// </summary>
        public string SeikyuRemarks { get; set; }
        
        /// <summary>
        /// 請求担当ID
        /// </summary>
        public int? SeikyuTantouId { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
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
        /// 請求先住所
        /// </summary>
        public string SeikyuAddress { get; set; }
        
        /// <summary>
        /// 請求先郵便番号
        /// </summary>
        public string SeikuyPostCode { get; set; }
        
        /// <summary>
        /// 請求先顧客ID
        /// </summary>
        public int SeikyuCustomerId { get; set; }
        
        /// <summary>
        /// 支払担当ID
        /// </summary>
        public int ShiharaiTantouId { get; set; }
        
        /// <summary>
        /// 支払締日
        /// </summary>
        public int ShiharaiShimeDay { get; set; }
        
        /// <summary>
        /// 支払備考
        /// </summary>
        public string ShiharaiRemarks { get; set; }
        
        /// <summary>
        /// 支払予社ID
        /// </summary>
        public int ShiharaiYosyaId { get; set; }
    }
}
