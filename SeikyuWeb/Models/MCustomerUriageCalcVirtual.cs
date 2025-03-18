#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客売上計算を表します。
    /// </summary>
    public partial class MCustomerUriageCalc
    {
        /// <summary>
        /// 顧客支店
        /// </summary>
        public virtual MCustomerBranch CustomerBranch { get; set; } = null;
    }
}
