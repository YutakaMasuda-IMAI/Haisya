using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class T_Share_Syaryo
    {
        public virtual T_Share_Syaryo_Detail Share_Syaryo_Detail { get; set; } = null!;
        public virtual M_Company Company { get; set; } = null!;
        public virtual M_CompanyBranch CompanyBranch { get; set; } = null!;
        public virtual M_CompanyUser_Group CompanyUserGroup { get; set; } = null!;
    }
}
