using System.Collections.Generic;

namespace WebApplication.Common
{
    /// <summary>
    ///Class SystemConstantは、システムに定数を定義
    /// </summary>
    public class SystemConstants
    {
        /// <summary>
        ///Class Messageは、システムにメッセージを定義
        /// </summary>
        public static class Message
        {
            public const string DataNotFound = "データが存在しません。";
            public const string InternalServerError = "リクエストの処理中にエラーが発生しました。";
            public const string Error = "エラー。";
            public const string RequiredField = "{0}がありません。";
            public const string AllowValue = "{0}の設定に誤りがあります。";
            public const string InValidNumber = "{0}が数値ではありません。";
            public const string InValidDate = "{0}が日付ではありません。";
            public const string InValidString = "{0}が文字列ではありません。";
            public const string InValidParam = "{0}パラメーターが不正です。";
            public const string NoDataOutput = "出力するデータがありません。";
            public const string PropertyNotMatching = "{0}が{1}に存在しません。";
        }

        /// <summary>
        ///クラス共通帳票は共通帳票に定数を定義
        /// </summary>
        public static class ReportCommon
        {
            public static IEnumerable<string> ProcedureList = new List<string> {
                "Proc_V_ReportInvoiceList",
                "Proc_V_ReportInvoiceList1",
                "Proc_V_ReportInvoiceList2",
                "Proc_V_ReportBillList",
                "Proc_V_ReportBillList2",
                "Proc_V_ReportBillList3",
                "Proc_V_ReportBillList4",
                "Proc_V_ReportCarNumberContactSheetList",
                "Proc_V_HaisyaDataList",
                "Proc_V_ReportTransportOrderSheetList",
                "Proc_V_ReportTransportInstructionsSheetList",

                "Proc_R_HaisyaCheckList",
                "Proc_R_UnkoShijiList",
                "Proc_R_Hacchusho",
                "Proc_R_SyabanList",
                "Proc_R_UriageList",
                "Proc_R_UriageDetailList",
                "Proc_R_KotsuhiList",
                "Proc_R_KotsuhiDetailList",
                "Proc_R_UriageShukei",
                "Proc_R_DailyUriageShukei",
                "Proc_R_UriageNikki",
                "Proc_R_TokuisakiUriageSuii",
                "Proc_R_SeikyuMikakuteiList",
                "Proc_R_ShiireShiharaisaki",
                "Proc_R_ShitabaraiList",
                "Proc_R_ShitabaraiDetailList",
                "Proc_R_ShiharaisakiList",
                "Proc_R_ShitabaraiMikakuteiList",
                "Proc_R_NyukinList",
                "Proc_R_SeikyuList",
                "Proc_R_SeikyuDetailList",
                "Proc_R_KaikeiSystemRenkeiData",
                "Proc_R_RenkeiSetteiData",
                "Proc_R_JyomuinData",
                "Proc_R_JyomuinOutputData",
                "Proc_R_JikoDetailList",
                "Proc_R_SeikyuToiawaseList",
                "Proc_R_SeikyuPattern1",
                "Proc_R_SeikyuPattern2",
                "Proc_R_SeikyuPattern3",
                "Proc_R_SeikyuPattern4",
                "Proc_R_ShiharaiMikakuteiList",
                "Proc_R_ShiharaiDetailList",
            };
        }

        public static class ReportCommonVariable
        {
            public const string COMPANY_ID = "COMPANY_ID";
            public const string USER_ID = "USER_ID";
        }
    }
}
