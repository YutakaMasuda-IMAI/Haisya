using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客を表すクラス
    /// </summary>
    public partial class MCustomer
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// 他社フラグ
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
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
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
    }
}
