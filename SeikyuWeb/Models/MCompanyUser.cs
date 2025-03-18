using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーを表すクラス
    /// </summary>
    public partial class MCompanyUser
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// 支店ID
        /// </summary>
        public int BranchId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 社員番号
        /// </summary>
        public int? EmployeeNumber { get; set; }
        
        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 担当フラグ
        /// </summary>
        public bool TantouFlg { get; set; }
        
        /// <summary>
        /// 営業フラグ
        /// </summary>
        public bool EigyoFlg { get; set; }
        
        /// <summary>
        /// 請求フラグ
        /// </summary>
        public bool SeikyuFlg { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpDate { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 担当ID
        /// </summary>
        public int TntouId { get; set; }
    }
}
