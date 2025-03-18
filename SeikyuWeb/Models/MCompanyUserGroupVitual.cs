using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーグループを表すクラス
    /// </summary>
    public partial class MCompanyUserGroup
    {
        public MCompanyUserGroup()
        {
            this.SeikuTantouCustomerBranchs = new HashSet<MCustomerBranch>();
            this.ShiharaiTantouCustomerBranchs = new HashSet<MCustomerBranch>();
            this.CompanyUserGroupUsers = new HashSet<MCompanyUserGroupUser>();
        }

        /// <summary>
        /// 請求担当顧客支店のコレクション
        /// </summary>
        public virtual ICollection<MCustomerBranch> SeikuTantouCustomerBranchs { get; set; } = new HashSet<MCustomerBranch>();
        
        /// <summary>
        /// 支払担当顧客支店のコレクション
        /// </summary>
        public virtual ICollection<MCustomerBranch> ShiharaiTantouCustomerBranchs { get; set; } = new HashSet<MCustomerBranch>();
        
        /// <summary>
        /// 会社ユーザーグループユーザーのコレクション
        /// </summary>
        public virtual ICollection<MCompanyUserGroupUser> CompanyUserGroupUsers { get; set; } = new HashSet<MCompanyUserGroupUser>();
    }
}
