using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 会社支店情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class M_CompanyBranch
    {
        public M_CompanyBranch()
        {
            Share_Syaryos = new HashSet<T_Share_Syaryo>();
        }
        public virtual ICollection<T_Share_Syaryo> Share_Syaryos { get; set; }

    }
}
