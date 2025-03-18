#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 支払変更を表すクラス
    /// </summary>
    public partial class TCheckShitabaraiChange
    {
        /// <summary>
        /// 支払詳細
        /// </summary>
        public virtual TCheckShitabaraiDetail CheckShitabaraiDetail { get; set; } = null!;
    }
}
