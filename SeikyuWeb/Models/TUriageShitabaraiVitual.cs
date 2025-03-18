#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 売上支払を表すクラス
    /// </summary>
    public partial class TUriageShitabarai
    {
        /// <summary>
        /// チェック支払詳細
        /// </summary>
        public virtual TCheckShitabaraiDetail CheckShitabaraiDetail { get; set; } = null!;
    }
}
