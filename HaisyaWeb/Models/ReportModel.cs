using HaisyaWeb.Dto;
using System.Collections.Generic;

namespace HaisyaWeb.Models
{
    public class ReportModel
    {
        /// <summary>
        /// 帳票検索の Dto
        /// </summary>
        public class ReportSearchModel
        {
            public int Company_ID { get; set; }
            public M_Report_Serch_Local Report_Serch { get; set; }
            public List<M_Report_Serch_Kubun_Local> Report_Serch_Kubun_List { get; set; }
            public List<M_Report_Serch_Item_Local> Report_Serch_Item_List { get; set; }
            public List<GroupItem> Group_Items { get; set; }
        }

        /// <summary>
        /// グループ項目及び項目検索帳票リストの Dto
        /// </summary>
        public class GroupItem
        {
            public string Display_Title { get; set; }
            public string Display_Message { get; set; }
            public int Row_Order { get; set; }
            public int NotNull_Flg { get; set; }
            public List<M_Report_Serch_Item_Local> Items { get; set; }
        }

        /// <summary>
        /// 検索項目のDto
        /// </summary>
        public class SearchItem
        {
            public int Report_Serch_Kubun_ID { get; set; }
        }

        /// <summary>
        /// 共通帳票のDto
        /// </summary>
        public class PDFReportModel : ReportCommonDtoLocal
        {
            public List<GroupHeader> Group_Headers { get; set; } = new List<GroupHeader>();

            public PDFBill Bill { get; set; }
            public PDFCarNumberContactSheet CarNumberContactSheet { get; set; }
            public PDFOrderTag OrderTag { get; set; }
            public PDFTransportOrderSheet TransportOrderSheet { get; set; }
            public PDFTransportInstructionsSheet TransportInstructionsSheet { get; set; }
        }

        /// <summary>
        ///データ PDF billの Dto
        /// </summary>
        public class PDFBill
        {
            public List<V_ReportBillList_Local> BillList { get; set; }
            public List<V_ReportBillList2_Local> BillList2 { get; set; }
            public List<V_ReportBillList3_Local> BillList3 { get; set; }
            public List<V_ReportBillList4_Local> BillList4 { get; set; }
            public T_Nyukin_Local Nyukin { get; set; }
            public T_Print_Seikyu_Local PrintSeikyu { get; set; }
            public int? SeikyuUnchin { get; set; }

        }

        /// <summary>
        ///データ pdf order tagの Dto
        /// </summary>
        public class PDFOrderTag
        {
            public List<V_ReportOrderTagList_Local> HaisyaDataList { get; set; }
        }

        /// <summary>
        ///データグループヘッダーの Dto
        /// </summary>
        public class GroupHeader
        {
            public int Row_Order { get; set; }
            public List<M_Report_Output_Item_Local> Items { get; set; }
        }

        /// <summary>
        ///パラメータ出力検索のDto
        /// </summary>
        public class SearchExportParam
        {
            public int SerchKubunID { get; set; }
            public string searchJson { get; set; }
            public string SelectedIDs { get; set; }
        }

        /// <summary>
        /// 帳票出力データ設定のDto
        /// </summary>
        public class SettingReportOutputModel
        {
            public string ReportName { get; set; }
            public string DisplayTitle { get; set; }
            public int ItemMasterSelected { get; set; }
            public int ItemSelected { get; set; }
            public int DataUnsaved { get; set; } = 0;

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchReportOutputModel Search { get; set; }

            /// <summary>
            /// ReportOutputMasterItems
            /// </summary>
            public List<M_ReportOutputItemMasterDto> ReportOutputMasterItems { get; set; } = new List<M_ReportOutputItemMasterDto>();

            /// <summary>
            /// ReportOutputItems
            /// </summary>
            public List<M_ReportOutputItemDto> ReportOutputItems { get; set; } = new List<M_ReportOutputItemDto>();

        }

        /// <summary>
        /// 項目マスタ出力帳票のDto
        /// </summary>
        public class M_ReportOutputItemMasterDto : M_Report_Output_Item_Master_Local
        {
            public bool IsAdded { get; set; } = false;
        }

        /// <summary>
        /// 項目出力帳票のDto
        /// </summary>
        public class M_ReportOutputItemDto : M_Report_Output_Item_Local
        {
            public string Display_Title { get; set; } = "";
            public int Report_ItemFlg { get; set; } = 0;
        }

        /// <summary>
        /// 出力帳票検索のDto
        /// </summary>
        public class SearchReportOutputModel
        {
            public int SettingType { get; set; } = 1;
            public int KubunID { get; set; }
            public int CompanyID { get; set; }
            public int UserID { get; set; } = 0;

        }

        /// <summary>
        /// 車番連絡シートリストをバーチャルビュー
        /// </summary>
        public class PDFCarNumberContactSheet : PDFCarNumberContactSheetInfo
        {
            public List<V_ReportCarNumberContactSheetList_Local> CarNumberContactSheetList { get; set; } = new List<V_ReportCarNumberContactSheetList_Local>();
        }

        /// <summary>
        /// 車番連絡情報シートをバーチャルビュー
        /// </summary>
        public class PDFCarNumberContactSheetInfo
        {
            public V_ReportCarNumberContactSheetList_Local CommonInfo { get; set; } = new V_ReportCarNumberContactSheetList_Local();
        }

        /// <summary>
        /// transport order シートをバーチャルビュー
        /// </summary>
        public class PDFTransportOrderSheet
        {
            public List<V_ReportTransportOrderSheetList_Local> TransportOrderSheetList { get; set; } = new List<V_ReportTransportOrderSheetList_Local>();
        }

        /// <summary>
        /// transport instructionシートをバーチャルビュー
        /// </summary>
        public class PDFTransportInstructionsSheet
        {
            public List<V_ReportTransportInstructionsSheetList_Local> TransportInstructionsSheetList { get; set; } = new List<V_ReportTransportInstructionsSheetList_Local>();
            public string base64Image { get; set; }
        }
    }
}
