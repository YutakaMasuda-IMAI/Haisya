using System.Collections.Generic;

namespace HaisyaWeb.Common
{
    /// <summary>
    ///Class SystemConstantsはシステムに定数を定義
    /// </summary>
    public class SystemConstants
    {
        public const int SettionTimeOutException = 9999;

        /// <summary>
        ///Class Message はシステムにメッセージを定義
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
            public const string ReportNumberNotFound = "指定されたReport_Numberは存在しません。";
        }

        /// <summary>
        ///Class ReportCommonは共通帳票に定数を定義
        /// </summary>
        public static class ReportCommon
        {
            public static class ReportHtmlType
            {
                public const string List = "list";
                public const string InvoiceList = "invoice-list";
                public const string Bill1 = "bill-01";
                public const string Bill2 = "bill-02";
                public const string Bill3 = "bill-03";
                public const string Bill4 = "bill-04";
                public const string CarNumberContactSheetList = "car-number-contact-sheet";
                public const string OrderTag = "order-tag";
                public const string TransportOrderSheet = "transport-order-sheet";
                public const string TransportInstructionsSheet = "transport-instructions-sheet";
            }
            public static IEnumerable<string> ReportHtmlList = new List<string>
            {
                ReportHtmlType.List,
                ReportHtmlType.InvoiceList,
                ReportHtmlType.Bill1,
                ReportHtmlType.Bill2,
                ReportHtmlType.Bill3,
                ReportHtmlType.Bill4,
                ReportHtmlType.CarNumberContactSheetList,
                ReportHtmlType.OrderTag,
                ReportHtmlType.TransportOrderSheet,
                ReportHtmlType.TransportInstructionsSheet,
            };
        }

        /// <summary>
        /// Class ReportConstants はpdfファイル帳票で使用するファイル名を定義
        /// </summary>
        public static class ReportConstants
        {
            public const string TransportInstructionsSheetLogo = "transport-instructions-sheet-logo.png";
        }


        /// <summary>
        /// ゼンリン地図API
        /// 入出力座標の測地系を指定
        /// </summary>
        public static class Zenrin_datum
        {
            public const string 世界測地系 = "JGD";
            public const string 日本測地系 = "TOKYO";
            public const string 日本測地系_ゼンリンナビ地図 = "TOKYO_NAVI";
        }

        /// <summary>
        /// Layout
        /// </summary>
        public static class Layout
        {
            public const string MainLayout = "_Layout";
            public const string LayoutForRouteDetail = "_LayoutForRouteDetail";
            public const string DriverMenu = "DriverMenu";
            public const string MasterData1Menu = "MasterData1Menu";
            public const string LayoutForSettingWindow = "_LayoutForSettingWindow";
            public const string MasterDataMenu = "MasterDataMenu";
        }
    }
}
