namespace WebApplication.Data
{
    /// <summary>
    /// 関係を定義するためのM_Report_Output_Itemの部分クラス
    /// </summary>
    public partial class M_Report_Output_Item
    {
        /// <summary>
        /// レポート出力項目マスター
        /// </summary>
        public virtual M_Report_Output_Item_Master Report_Output_Item_Master { get; set; } = null!;
    }
}
