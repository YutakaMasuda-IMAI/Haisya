#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCheckShitabaraiDetail クラス
    /// </summary>
    public partial class TCheckShitabaraiDetail
    {
        /// <summary>
        /// 支払チェック
        /// </summary>
        public virtual TCheckShitabarai CheckShitabarai { get; set; } = null!;

        /// <summary>
        /// 売上支払
        /// </summary>
        public virtual TUriageShitabarai UriageShitabarai { get; set; } = null!;

        /// <summary>
        /// 支払チェック変更
        /// </summary>
        public virtual TCheckShitabaraiChange CheckShitabaraiChange { get; set; } = null;
    }
}
