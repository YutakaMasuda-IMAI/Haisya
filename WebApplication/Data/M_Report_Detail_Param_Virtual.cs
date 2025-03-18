namespace WebApplication.Data
{
    /// <summary>
    /// 関係を定義するためのM_Report_Detail_Paramの部分クラス
    /// </summary>
    public partial class M_Report_Detail_Param
    {
        /// <summary>
        /// レポート検索区分
        /// </summary>
        public virtual M_Report_Serch_Kubun Report_Serch_Kubun { get; set; }
    }
}
