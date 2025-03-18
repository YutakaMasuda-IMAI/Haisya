using System.ComponentModel.DataAnnotations.Schema;

namespace RenkeiDB.Data
{
    /// <summary>
    /// ポータル情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class T_Portal_Info
    {
        public virtual M_Company Company { get; set; } = null!;
    }
}
