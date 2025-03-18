namespace SeikyuWeb.Models
{
    /// <summary>
    /// 定義された関係のための部分クラス M_Report_Serch_Item
    /// </summary>
    public partial class MReportSerchItem
    {
        /// <summary>
        /// レポート検索区分
        /// </summary>
        public virtual MReportSerchKubun Report_Serch_Kubun { get; set; } = null!;
    }
}
