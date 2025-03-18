using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客担当を表します。
    /// </summary>
    public partial class MCustomerTantou
    {
        /// <summary>
        /// 支払済みチェックのコレクション
        /// </summary>
        public virtual ICollection<TCheckShitabaraiDone> CheckShitabaraiDones { get; set; } = new HashSet<TCheckShitabaraiDone>();
    }
}
