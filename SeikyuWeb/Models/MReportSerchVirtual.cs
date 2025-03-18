using System.Collections.Generic;

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 定義された関係のための部分クラス M_Report_Serch
    /// </summary>
    public partial class MReportSerch
    {
        public MReportSerch()
        {
            this.Report_Serch_Kubun_List = new HashSet<MReportSerchKubun>();
        }

        /// <summary>
        /// レポート検索区分リスト
        /// </summary>
        public virtual ICollection<MReportSerchKubun> Report_Serch_Kubun_List { get; set; }
    }
}
