using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// TCheckShitabarai クラス
    /// </summary>
    public partial class TCheckShitabarai
    {
        /// <summary>
        /// 支払チェック詳細のコレクション
        /// </summary>
        public virtual ICollection<TCheckShitabaraiDetail> CheckShitabaraiDetails { get; set; } = new HashSet<TCheckShitabaraiDetail>();

        /// <summary>
        /// 支払チェック完了
        /// </summary>
        public virtual TCheckShitabaraiDone CheckShitabaraiDone { get; set; } = null;

        /// <summary>
        /// 顧客支店
        /// </summary>
        public virtual MCustomerBranch CustomerBranch { get; set; } = null;
    }
}
