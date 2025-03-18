using System.Collections.Generic;

namespace WebApplication.Data
{
    /// <summary>
    /// 関係を定義するためのM_Report_Serchの部分クラス
    /// </summary>
    public partial class M_Report_Serch
    {
        public M_Report_Serch()
        {
            this.Report_Serch_Kubun_List = new HashSet<M_Report_Serch_Kubun>();
        }

        /// <summary>
        /// レポート検索区分リスト
        /// </summary>
        public virtual ICollection<M_Report_Serch_Kubun> Report_Serch_Kubun_List { get; set; }
    }
}
