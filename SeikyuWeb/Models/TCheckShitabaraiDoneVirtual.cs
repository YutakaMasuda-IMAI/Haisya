#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCheckShitabaraiDone クラス
    /// </summary>
    public partial class TCheckShitabaraiDone
    {
        /// <summary>
        /// 支払チェック
        /// </summary>
        public virtual TCheckShitabarai CheckShitabarai { get; set; } = null;

        /// <summary>
        /// 顧客担当
        /// </summary>
        public virtual MCustomerTantou CustomerTantou { get; set; } = null!;
    }
}
