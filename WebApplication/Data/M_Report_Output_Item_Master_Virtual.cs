using System.Collections.Generic;

namespace WebApplication.Data
{
    /// <summary>
    /// 関係を定義するためのM_Report_Output_Item_Masterの部分クラス
    /// </summary>
    public partial class M_Report_Output_Item_Master
    {
        /// <summary>
        /// レポート出力項目リスト
        /// </summary>
        public virtual ICollection<M_Report_Output_Item> Report_Output_Item_List{ get; set; }
    }
}
