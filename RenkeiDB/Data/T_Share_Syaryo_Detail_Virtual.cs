using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 共有車両詳細情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class T_Share_Syaryo_Detail
    {
        public virtual T_Share_Syaryo Share_Syaryo { get; set; } = null!;
    }
}
