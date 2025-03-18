using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 会社ユーザーグループ情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class M_CompanyUser_Group
    {
        public M_CompanyUser_Group()
        {
            Share_Syaryos = new HashSet<T_Share_Syaryo>();
        }
        public virtual ICollection<T_Share_Syaryo> Share_Syaryos { get; set; }
    }
}
