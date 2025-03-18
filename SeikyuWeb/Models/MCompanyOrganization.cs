using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社組織を表すクラス
    /// </summary>
    public partial class MCompanyOrganization
    {
        /// <summary>
        /// 会社組織ID
        /// </summary>
        public int CompanyOrganizationId { get; set; }
        
        /// <summary>
        /// 会社ID
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// 親会社組織ID
        /// </summary>
        public int CompanyOrganizationIdOya { get; set; }
        
        /// <summary>
        /// 組織コード
        /// </summary>
        public string OrganizationCode { get; set; }
        
        /// <summary>
        /// 組織名
        /// </summary>
        public string OrganizationName { get; set; }
        
        /// <summary>
        /// 組織名略称
        /// </summary>
        public string OrganizationNameAbbr { get; set; }
        
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public bool DelFlg { get; set; }
        
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime UpDate { get; set; }
    }
}
