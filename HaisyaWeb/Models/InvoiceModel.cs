using HaisyaWeb.Common;
using HaisyaWeb.Controllers;
using HaisyaWeb.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 請求モデルクラス
    /// </summary>
    public class InvoiceModel
    {
        public const int TAX_CATEGORY_TAX = 0;              // 0:課税
        public const int TAX_CATEGORY_FREE = 1;             // 1:非課税

        /// <summary>
        /// 請求・請求問合わせ定数
        /// </summary>
        public enum BizTypes
        {
            INVOICE,
            INQUIRY
        };

        /// <summary>
        /// 請求サマリーモデルクラス
        /// </summary>
        public class InvoiceSummModel
        {
            /// <summary>
            /// コントローラー
            /// </summary>
            public string Controller { get; set; }
            /// <summary>
            /// 確認メッセージ
            /// </summary>
            private static readonly string[][] CONFIRM_MSGS =
            {
                new[]
                {
                    "メールでの請求を実施しますか？",
                    "メールでの請求を実施しますか？",
                    "請求書を印刷しますか？",
                    "請求書を印刷しますか？",
                },
                new[]
                {
                    "メールでの請求問合せを実施しますか？",
                    "メールでの請求問合せを実施しますか？",
                    "請求問合せを印刷しますか？",
                    "請求問合せを印刷しますか？",
                },
            };

            /// <summary>
            /// 確認説明
            /// </summary>
            private static readonly string[][] DESCRIPTION =
            {
                new[]
                {
                    "請求書の発行ステータスを発行済みに変更します。",
                    "請求書の発行ステータスを発行済みに変更します。",
                    "請求書の発行ステータスを発行済みに変更します。",
                    "確認書用の印刷のため、発行ステータスの変更はありません。",
                },
                new[]
                {
                    "請求問合せのステータスを問合せ済みに変更します。",
                    "請求問合せのステータスを問合せ済みに変更します。",
                    "請求問合せのステータスを問合せ済みに変更します。",
                    "確認用の印刷のため、問合せステータスの変更はありません。",
                }
            };

            /// <summary>
            /// 請求種別
            /// </summary>
            public BizTypes Biz { get; set; }

            /// <summary>
            /// 確認モダール
            /// </summary>
            public ConfirmationViews View { get; set; }

            /// <summary>
            /// 確認メッセージ
            /// </summary>
            public string Confirm_message
            {
                get => CONFIRM_MSGS[(int)Biz][(int)View];
                set { }
            }

            /// <summary>
            /// 確認説明
            /// </summary>
            public string Description
            {
                get => DESCRIPTION[(int)Biz][(int)View];
                set { }
            }

            /// <summary>
            /// 請求月
            /// </summary>
            [Display(Name = "請求月")]
            public string Year_month { get; set; }

            /// <summary>
            /// 締め日
            /// </summary>
            public int Closing_days { get; set; }

            /// <summary>
            /// 発行日
            /// </summary>
            [Display(Name = "発行日")]
            public string Publish_date { get; set; }

            /// <summary>
            /// 年度末発行フラグ
            /// </summary>
            public int NendomatsuFlg { get; set; }

            /// <summary>
            /// 期間
            /// </summary>
            string _sale_date;

            /// <summary>
            /// 期間
            /// </summary>
            [Display(Name = "期間")]
            public string Sale_date
            {
                get
                {
                    // 期間が指定されている場合はその値を返す
                    if (!string.IsNullOrWhiteSpace(_sale_date))
                        return _sale_date;
                    // ※一覧画面でも売上年月日（まで）が入力されて無く、締日も設定されてない請求先はNullで問題ない
                    if (Closing_days < 1)
                        return "";

                    DateTime now = DateTime.Now.Date;                // System date
                    DateTime day = add_days_but_in_month_range(
                        new DateTime(Convert.ToInt32(Year_month[..4]), Convert.ToInt32(Year_month[5..7]), 1), Closing_days - 1);

                    // 請求月と締日から期間を計算する
                    day = now < day ? day.AddDays(-1) :
                        (Closing_days == 1 ? new DateTime(now.Year, now.Month, 1).AddDays(-1) : add_days_but_in_month_range(new DateTime(now.Year, now.Month, 1), Closing_days - 1 - 1));

                    return $"{day:yyyy年MM月dd日}({new CultureInfo("ja-JP").DateTimeFormat.GetShortestDayName(day.DayOfWeek)})";
                }
                set => _sale_date = value;
            }

            /// <summary>
            /// 月の範囲内で日数を加算する
            /// </summary>
            /// <param name="d">日付</param>
            /// <param name="days">日数</param>
            /// <returns>加算後の日付</returns>
            private static DateTime add_days_but_in_month_range(DateTime d, int days)
            {
                DateTime r = d.AddDays(days);
                return r.Year == d.Year && r.Month == d.Month ? r : days > 0 ? new DateTime(d.Year, d.Month, 1).AddMonths(1).AddDays(-1) : new DateTime(d.Year, d.Month, 1);
            }

            /// <summary>
            /// 請求情報
            /// </summary>
            private InvoiceInfo _invoice;

            /// <summary>
            /// 請求先
            /// </summary>
            [Display(Name = "請求先")]
            public InvoiceInfo Invoice
            {
                get => View == ConfirmationViews.BULK ? Invoice_list?.FirstOrDefault() : _invoice;
                set
                {
                    // 一括の場合は請求先をリストに追加する
                    if (View != ConfirmationViews.BULK)
                    {
                        _invoice = value;
                        Invoice_list = new() { _invoice };
                    }
                    else _invoice = null;
                }
            }

            /// <summary>
            /// 請求一覧
            /// </summary>
            public List<InvoiceInfo> Invoice_list { get; set; } = new();

            /// <summary>
            /// レイアウト
            /// </summary>
            public IEnumerable<SelectListItem> Layouts { set; get; }

            /// <summary>
            /// レイアウト
            /// </summary>
            [Display(Name = "レイアウト")]
            public string Selected_layout
            {
                get => View == ConfirmationViews.PRINT || View == ConfirmationViews.PREVIEW ? $"{InvoiceController.LAYOUT_SELECTED_CODE_DATA}" : null;
                set { }
            }

            /// <summary>
            /// PDFフラグ
            /// </summary>
            public int PDF_flag  { set; get; }

            /// <summary>
            /// レイアウト
            /// </summary>
            [Display(Name = "レイアウト")]
            public string Layout  { set; get; }

            /// <summary>
            /// Print_Pattern
            /// </summary>
            [Display(Name = "Print_Pattern")]
            public int Print_Pattern { set; get; }
            
        }

        /// <summary>
        /// 請求情報クラス
        /// </summary>
        public class InvoiceInfo
        {
            /// <summary>
            /// 請求先
            /// </summary>
            public string Customer_Name { get; set; }

            /// <summary>
            /// 請求月
            /// </summary>
            public DateOnly Seikyu_Month { get; set; }

            /// <summary>
            /// 締め日
            /// </summary>
            public int Shime_Day { get; set; }

            /// <summary>
            /// 発行日
            /// </summary>
            public string Publish_date { get; set; }

            /// <summary>
            /// メール１
            /// </summary>
            public string Mail_address1 { get; set; }

            /// <summary>
            /// メール２
            /// </summary>
            public string Mail_address2 { get; set; }

            /// <summary>
            /// 得意先ブランチID
            /// </summary>
            public int Customer_Branch_ID { get; set; }

            /// <summary>
            /// 売上運賃ID
            /// </summary>
            public int Uriage_Unchin_ID { get; set; }
            /// <summary>
            /// 売上運賃IDリスト（カンマ区切り）
            /// </summary>
            public string Uriage_Unchin_ID_LIST { get; set; }

            /// <summary>
            /// 請求期間開始
            /// </summary>
            public DateOnly From_Date { get; set; }

            /// <summary>
            /// 請求期間終了
            /// </summary>
            public DateOnly To_Date { get; set; }

            /// <summary>
            /// 指定期間終了
            /// </summary>
            public DateOnly? SeikyuDate_To { get; set; }

            /// <summary>
            /// 年度末印刷フラグ
            /// </summary>
            public int Nendomatsu_Flg { get; set; }

            /// <summary>
            /// ロー
            /// </summary>
            public int SelectRow { get; set; }

            /// <summary>
            /// オーダー
            /// </summary>
            public int Order { get; set; }
        }

        /// <summary>
        /// データリストモデルインターフェース
        /// </summary>
        public interface IDataListModel
        {
            /// <summary>
            /// コントローラー
            /// </summary>
            string Controller { get; }

            /// <summary>
            /// 検索モデル
            /// </summary>
            SearchModelForInvoiceList Search { get; set; }
            /// <summary>
            /// 請求データリスト
            /// </summary>
            List<V_InvoiceDataList_Local> InvoiceDataLists { get; set; }
            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// データリストモデルクラス
        /// </summary>
        /// <typeparam name="T">請求データリストの型</typeparam>
        public class DataListModel<T> : IDataListModel
            where T : V_InvoiceDataList_Local, new()
        {
            public string Controller => typeof(T) == typeof(V_InvoiceDataList_Local) ? "Invoice" : "InvoiceInquiry";

            public SearchModelForInvoiceList Search { get; set; }
            public List<T> InvoiceDataLists { get; set; }

            List<V_InvoiceDataList_Local> IDataListModel.InvoiceDataLists
            {
                get => InvoiceDataLists?.Cast<V_InvoiceDataList_Local>().ToList();
                set => InvoiceDataLists = value?.Cast<T>().ToList();
            }
            /// <summary> ソート順 </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 検索モデルクラス
        /// </summary>
        public class SearchModelForInvoiceList
        {
            /// <summary>
            /// 会社ID
            /// </summary>
            public int CompanyID { set; get; }

            /// <summary>
            /// 請求担当
            /// </summary>
            public IEnumerable<SelectListItem> SeikyuTantouSelectList { set; get; }

            /// <summary>
            /// 年月
            /// </summary>
            [Required]
            [Display(Name = "年月")]
            [StringLength(8, ErrorMessage = "Must be in format yyyy年MM月", MinimumLength = 8)]
            public string Year_month { get; set; } = $"{DateTime.Now:yyyy年MM月}";

            /// <summary>
            /// 締日
            /// </summary>
            [Required]
            [Range(0, 31)]
            [Display(Name = "締日")]
            public int? Closing_days { get; set; }

            /// <summary>
            /// 請求担当
            /// </summary>
            [Display(Name = "請求担当")]
            public int? Seikyu_Tantou_Name { get; set; }

            /// <summary>
            /// 得意先1
            /// </summary>
            [Display(Name = "得意先")]
            public int? Customer1 { get; set; }

            /// <summary>
            /// 得意先1
            /// </summary>
            public string Customer_name1 { get; set; }

            /// <summary>
            /// 得意先2
            /// </summary>
            [Display(Name = "得意先")]
            public int? Customer2 { get; set; }

            /// <summary>
            /// 得意先2
            /// </summary>
            public string Customer_name2 { get; set; }

            /// <summary>
            /// 発行日
            /// </summary>
            [Required]
            [Display(Name = "発行日")]
            public string Publish_date { get; set; }
            
            /// <summary>
            /// 売上年月日（まで）
            /// </summary>
            [Display(Name = "売上年月日（まで）")]
            public string Sale_date { get; set; }
        }
    }
}