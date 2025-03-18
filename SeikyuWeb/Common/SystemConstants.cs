using System.Collections.Generic;

namespace SeikyuWeb.Common
{
    /// <summary>
    /// SystemConstants クラス
    /// </summary>
    public static class SystemConstants
    {
        /// <summary>
        /// 定数：メッセージ
        /// </summary>
        public static class Message
        {
            public const string DataNotFound = "ページが見つかりません。";
            public const string InternalServerError = "リクエストの処理中にエラーが発生しました。";
            public const string Unauthorized = "アクセスは許可されていません。";
            public const string Error = "エラー。";
            public const string RequiredField = "{0}がありません。";
            public const string InvalidUser = "ユーザーもしくはパスワードが存在しません。";
            public const string LockUser = "このアカウントはロックされています。";
            public const string OwnerFlagIncorrectly = "オーナーフラグの設定に誤りがあります。";
            public const string RequiredValue = "{0}の値は0または1でなければなりません。";
            public const string Logout = "ログアウトしました。";
            public const string FromDayGreaterThanToDay = "{1}には{0}以降の日付を指定して下さい。";
            public const string AllowValue = "{0}の設定に誤りがあります。";
            public const string InValidNumber = "{0}が数値ではありません。";
            public const string InValidDate = "{0}が日付ではありません。";
            public const string InValidString = "{0}が文字列ではありません。";
            public const string InValidParam = "{0}パラメーターが不正です。";
            public const string InValidEmail = "メールアドレスのフォーマットに誤りがあります。";
            public const string InvalidNumberLength = "{0}の桁数に誤りがあります";
            public const string InvalidBoolean = "{0}の設定に誤りがあります。";
            public const string MissingParams = "{0}または{1}がありません。";
            public const string Expire = "確認期限が過ぎました。";
            public const string DataUpdateChanged = "他のユーザーにより変更された為、更新できませんでした。";
            public const string ReportHtmlInvalid = "Report_Htmlパラメーターが不正です";
            public const string PDFInvalid = "PDFパラメーターが不正です";
            public const string PropertyNotMatching = "{0}が{1}に存在しません。";
            public const string NoDataOutput = "出力するデータがありません。";
        }

        /// <summary>
        /// 定数：デフォルト値
        /// </summary>
        public static class DefaultValue
        {
            public const string GuardCompany = "company";
            public const string GuardCustomer = "customer";
        }

        /// <summary>
        /// 定数：DelFlag
        /// </summary>
        public static class DelFlag
        {
            public const int NONE = 0;
            public const int YES = 1;
        }

        /// <summary>
        /// 定数：DisplayFlag
        /// </summary>
        public static class DisplayFlag
        {
            public const int NONE = 0;
            public const int YES = 1;
        }

        /// <summary>
        /// 定数：LockFlag
        /// </summary>
        public static class LockFlag
        {
            public const int NONE = 0;
            public const int YES = 1;
        }
        /// <summary>
        /// 定数：PortalKubun
        /// </summary>
        public static class PortalKubun
        {
            public const int 請求WEB = 2;
        }

        /// <summary>
        /// 定数：CheckStatus
        /// </summary>
        public static class CheckStatus
        {
            public const int 発行済 = 0;
            public const int 確認中 = 1;
            public const int 確認済 = 2;
        }

        /// <summary>
        /// 定数：CodeData
        /// </summary>
        public static class CodeData
        {
            public const int ZERO = 0;
            public const int ONE = 1;
            public const int 請求問合せ = 10;
            public const int 支払問合せ = 11;
            public const int 請求書 = 12;
            public const int 免税請求書 = 13;
            public const int FIFTEEN = 15;
        }

        /// <summary>
        /// 定数：CodeId
        /// </summary>
        public static class CodeId
        {
            public const int FIVE = 5;
            public const int TEN = 10;
        }

        /// <summary>
        /// 定数：CheckKubun
        /// </summary>
        public static class CheckKubun
        {
            public const int WEB = 1;
        }

        /// <summary>
        /// 定数：SeikyuKubun
        /// </summary>
        public static class SeikyuKubun
        {
            public const int WEB = 1;
        }

        /// <summary>
        /// 定数：StatusCheckSeikyu
        /// </summary>
        public static class StatusCheckSeikyu
        {
            public const string Confirmed = "確認済";
            public const string Unconfirmed = "未確認";
            public const string Checking = "確認中";
        }

        /// <summary>
        /// 定数：Zeikubun
        /// </summary>
        public static class ZeiKubun
        {
            //Taxabel
            public const int 課税 = 0;
            //Tax-free
            public const int 非課税 = 1;
        }

        /// <summary>
        /// 定数：PrintKubun
        /// </summary>
        public static class PrintKubun
        {
            //UnPrint
            public const int 未印刷 = 2;
            //Printed
            public const int 印刷済 = 3;
        }

        /// <summary>
        /// 定数：StatusPrint
        /// </summary>
        public static class StatusPrint
        {
            public const string Printed = "印刷済";
            public const string Unprinted = "未印刷";
        }

        /// <summary>
        /// 定数：DataKubun
        /// </summary>
        public static class DataKubun
        {
            public const int 案件明細 = 3;
        }
        /// <summary>
        /// 定数：DateFormat
        /// </summary>
        public static class DateFormat
        {
            public const string DATE_NO_SLASH = "yyyyMMdd";
            public const string DATE = "yyyy/MM/dd";
            public const string DATE_TIME = "yyyy/MM/dd HH:mm:ss";
            public const string DATE_JP = "yyyy年MM月dd日";
            public const string MONTH_JP = "yyyy年MM月";
            public const string YEAR_MONTH = "yyyy/MM";
        }

        /// <summary>
        /// 定数：PrintShitabaraiDetail
        /// </summary>
        public static class PrintShitabaraiDetail
        {
            /// <summary>
            /// 定数：DataKubun
            /// </summary>
            public static class DataKubun
            {
                public const int ITEM_DETAILS = 3;
            }
        }

        /// <summary>
        /// 定数：Flag
        /// </summary>
        public static class Flag
        {
            public const int FALSE = 0;
            public const int TRUE = 1;
        }

        /// <summary>
        /// Class ReportCommonは共通帳票に定数を定義
        /// </summary>
        public static class ReportCommon
        {
            public static class ReportHtmlType
            {
                //public const string InvoiceList = "invoice-list";
                public const string Bill1 = "bill-01";
                public const string Bill2 = "bill-02";
                public const string Bill3 = "bill-03";
                public const string Bill4 = "bill-04";
            }

            public static IEnumerable<string> ReportHtmlList = new List<string>
            {
                //ReportHtmlType.InvoiceList,
                ReportHtmlType.Bill1,
                ReportHtmlType.Bill2,
                ReportHtmlType.Bill3,
                ReportHtmlType.Bill4,
            };
            
            public static IEnumerable<string> ReportHtmlSeikyushoList = new List<string>
            {
                ReportHtmlType.Bill1,
                ReportHtmlType.Bill2,
                ReportHtmlType.Bill3,
                ReportHtmlType.Bill4,
            };
        }
        
        /// <summary>
        /// 定数：ShimeDate
        /// </summary>
        public static class ShimeDate
        {
            public const int QueryAll = 31;
        }

        /// <summary>
        /// 定数：プロシージャ名です
        /// </summary>
        public static class ProcName
        {
            public const string AnkenDataList = "Proc_V_AnkenDataList";
            public const string SeikyuCheckDataList = "Proc_V_SeikyuCheckDataList";
            public const string AnkenList = "Proc_R_AnkenList";
        }

        /// <summary>
        /// 定数：プロシージャモデル名
        /// </summary>
        public static class ProcModelName
        {
            public const string AnkenDataList = "V_AnkenDataList";
            public const string SeikyuCheckSeikyuDataList = "V_SeikyuCheckDataList";
            public const string AnkenList = "R_AnkenList";
        }

        /// <summary>
        /// constant: Procedure parameter name
        /// </summary>
        public static class ProcParamName
        {
            public const string KUBUN = "KUBUN";
            public const string CHECK_ID = "CHECK_ID";
        }
    }
}