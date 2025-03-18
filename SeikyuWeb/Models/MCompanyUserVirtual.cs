using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーを表すクラス
    /// </summary>
    public partial class MCompanyUser
    {
        public MCompanyUser()
        {
            this.CompanyUserGroupUsers = new HashSet<MCompanyUserGroupUser>();
        }

        /// <summary>
        /// 会社ユーザーグループユーザーのコレクション
        /// </summary>
        public virtual ICollection<MCompanyUserGroupUser> CompanyUserGroupUsers { get; set; } = new HashSet<MCompanyUserGroupUser>();
    }
}
