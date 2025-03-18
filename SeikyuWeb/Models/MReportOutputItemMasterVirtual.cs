using System.Collections.Generic;

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 定義された関係のための部分クラス M_Report_Output_Item_Master
    /// </summary>
    public partial class MReportOutputItemMaster
    {
        /// <summary>
        /// レポート出力項目リスト
        /// </summary>
        public virtual ICollection<MReportOutputItem> Report_Output_Item_List { get; set; }
    }
}
