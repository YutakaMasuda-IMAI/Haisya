using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社支店を表すクラス
    /// </summary>
    public partial class MCompanyBranch
    {
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// ソート順
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 親支店ID
        /// </summary>
        public int OyaBranchId { get; set; }
        
        /// <summary>
        /// 支店コード
        /// </summary>
        public string BranchCode { get; set; }
        
        /// <summary>
        /// 支店名
        /// </summary>
        public string BranchName { get; set; }
        
        /// <summary>
        /// 支店名略称
        /// </summary>
        public string BranchNameAbbr { get; set; }
        
        /// <summary>
        /// 支店郵便番号1
        /// </summary>
        public string BranchPost1 { get; set; }
        
        /// <summary>
        /// 支店郵便番号2
        /// </summary>
        public string BranchPost2 { get; set; }
        
        /// <summary>
        /// 支店住所
        /// </summary>
        public string BranchAddress { get; set; }
        
        /// <summary>
        /// 支店電話番号
        /// </summary>
        public string BranchPhone { get; set; }
        
        /// <summary>
        /// 支店FAX番号
        /// </summary>
        public string BranchFax { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpDate { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
    }
}
