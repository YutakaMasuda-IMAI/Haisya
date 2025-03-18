namespace SeikyuWeb.Models
{
    /// <summary>
    /// 定義された関係のための部分クラス M_Report_Output_Item
    /// </summary>
    public partial class MReportOutputItem
    {
        /// <summary>
        /// レポート出力項目マスター
        /// </summary>
        public virtual MReportOutputItemMaster ReportOutputItemMaster { get; set; } = null!;
        
        /// <summary>
        /// レポート検索区分
        /// </summary>
        public virtual MReportSerchKubun Report_Serch_Kubun { get; set; } = null!;
    }
}
