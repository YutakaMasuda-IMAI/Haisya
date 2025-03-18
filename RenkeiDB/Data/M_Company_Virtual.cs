using System.Collections.Generic;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 会社情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class M_Company
    {
        public M_Company()
        {
            Share_Syaryos = new HashSet<T_Share_Syaryo>();
        }
        public virtual ICollection<T_Share_Syaryo> Share_Syaryos { get; set; } = new HashSet<T_Share_Syaryo>();
        public virtual ICollection<T_Portal_Info> Portal_Infos { get; set; } = new HashSet<T_Portal_Info>();
    }
}
