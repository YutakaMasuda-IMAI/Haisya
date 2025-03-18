using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社情報を表すクラス
    /// </summary>
    public partial class MCompany
    {
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 会社コード
        /// </summary>
        public string CompanyCode { get; set; }
        
        /// <summary>
        /// 会社名
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// 会社名略称
        /// </summary>
        public string CompanyNameAbbr { get; set; }
        
        /// <summary>
        /// 郵便番号1
        /// </summary>
        public string Post1 { get; set; }
        
        /// <summary>
        /// 郵便番号2
        /// </summary>
        public string Post2 { get; set; }
        
        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// FAX番号
        /// </summary>
        public string Fax { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 配車Webライセンス
        /// </summary>
        public bool HaisyaWebLicense { get; set; }
        
        /// <summary>
        /// 請求Webライセンス
        /// </summary>
        public bool SeikyuWebLicense { get; set; }
        
        /// <summary>
        /// 連携Webライセンス
        /// </summary>
        public bool RenkeiWebLicense { get; set; }
    }
}
