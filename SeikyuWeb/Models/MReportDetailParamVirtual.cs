namespace SeikyuWeb.Models
{
    /// <summary>
    /// 定義された関係のための部分クラス M_Report_Detail_Param
    /// </summary>
    public partial class MReportDetailParam
    {
        /// <summary>
        /// レポート検索区分
        /// </summary>
        public virtual MReportSerchKubun Report_Serch_Kubun { get; set; }
    }
}
