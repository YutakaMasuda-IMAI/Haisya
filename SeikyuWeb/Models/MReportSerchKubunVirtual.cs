using System.Collections.Generic;

namespace SeikyuWeb.Models
{
    public partial class MReportSerchKubun
    {
        public MReportSerchKubun()
        {
            Report_Serch_Item_List = new HashSet<MReportSerchItem>();
            Report_Detail_Param_List = new HashSet<MReportDetailParam>();
            Report_Output_Item_List = new HashSet<MReportOutputItem>();
        }

        public virtual MReportSerch Report_Serch { get; set; } = null!;
        public virtual ICollection<MReportSerchItem> Report_Serch_Item_List { get; set; }
        public virtual ICollection<MReportDetailParam> Report_Detail_Param_List { get; set; }
        public virtual ICollection<MReportOutputItem> Report_Output_Item_List { get; set; }
    }
}
