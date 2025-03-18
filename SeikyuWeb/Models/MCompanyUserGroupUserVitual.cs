#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 会社ユーザーグループユーザーを表すクラス
    /// </summary>
    public partial class MCompanyUserGroupUser
    {
        /// <summary>
        /// 会社ユーザーグループ
        /// </summary>
        public virtual MCompanyUserGroup CompanyUserGroup { get; set; } = null!;
        
        /// <summary>
        /// 会社ユーザー
        /// </summary>
        public virtual MCompanyUser CompanyUser { get; set; } = null!;
    }
}
