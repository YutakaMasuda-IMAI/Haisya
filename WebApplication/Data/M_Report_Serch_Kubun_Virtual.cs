using System.Collections.Generic;

namespace WebApplication.Data
{
    /// <summary>
    /// 関係を定義するためのM_Report_Serch_Kubunの部分クラス
    /// </summary>
    public partial class M_Report_Serch_Kubun
    {
        public M_Report_Serch_Kubun()
        {
            Report_Serch_Item_List = new HashSet<M_Report_Serch_Item>();
            Report_Detail_Param_List = new HashSet<M_Report_Detail_Param>();
        }

        /// <summary>
        /// レポート検索
        /// </summary>
        public virtual M_Report_Serch Report_Serch { get; set; } = null!;

        /// <summary>
        /// レポート検索項目リスト
        /// </summary>
        public virtual ICollection<M_Report_Serch_Item> Report_Serch_Item_List { get; set; }

        /// <summary>
        /// レポート詳細パラメータリスト
        /// </summary>
        public virtual ICollection<M_Report_Detail_Param> Report_Detail_Param_List { get; set; }
    }
}
