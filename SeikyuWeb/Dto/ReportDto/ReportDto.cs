using SeikyuWeb.Models;
using SeikyuWeb.Models.ReportCommons;
using System.Collections.Generic;

namespace SeikyuWeb.Dto.ReportDto
{
    /// <summary>
    /// レポートのDTO
    /// </summary>
    public class ReportDto
    {
        /// <summary>
        /// 共通帳票のDto
        /// </summary>
        public class PDFReportModel : ReportCommonDto
        {
            /// <summary>グループヘッダーリスト</summary>
            public List<GroupHeader> Group_Headers { get; set; } = new List<GroupHeader>();
            /// <summary>PDF請求書</summary>
            public PDFBill Bill { get; set; }
        }

        /// <summary>
        /// データ PDF請求書のDto
        /// </summary>
        public class PDFBill
        {
            /// <summary>請求書リスト</summary>
            public List<V_ReportBillList> BillList { get; set; }
            /// <summary>請求書リスト2</summary>
            public List<V_ReportBillList2> BillList2 { get; set; }
            /// <summary>請求書リスト3</summary>
            public List<V_ReportBillList3> BillList3 { get; set; }
            /// <summary>請求書リスト4</summary>
            public List<V_ReportBillList4> BillList4 { get; set; }
            /// <summary>入金情報</summary>
            public TNyukin Nyukin { get; set; }
            /// <summary>請求運賃</summary>
            public int? SeikyuUnchin { get; set; }
        }

        /// <summary>
        /// データグループヘッダーのDto
        /// </summary>
        public class GroupHeader
        {
            /// <summary>行順序</summary>
            public int Row_Order { get; set; }
            /// <summary>項目リスト</summary>
            public List<MReportOutputItem> Items { get; set; }
        }
    }
}
