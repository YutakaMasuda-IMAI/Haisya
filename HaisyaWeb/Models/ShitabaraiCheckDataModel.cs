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
    /// 下払いチェックデータモデル
    /// </summary>
    public class ShitabaraiCheckDataModel
    {
        /// <summary>
        /// 下払いサマリモデル
        /// </summary>
        public class ShitabaraiSummModel
        {
            private static readonly string[] CONFIRM_MSGS =
            {
                "メールでの下払い問い合わせを実施しますか？",
                "メールでの下払い問い合わせを実施しますか？",
                "印刷での下払い問い合わせを実施しますか？",
                "下払い問い合わせを印刷しますか？",
            };

            private static readonly string[] DESCRIPTION =
            {
                "下払い問い合わせのステータスを問い合わせ済みに変更します。",
                "下払い問い合わせのステータスを問い合わせ済みに変更します。",
                "下払い問い合わせのステータスを問い合わせ済みに変更します。",
                "確認用の印刷であるため、問い合わせステータスの変更はありません。",
            };

            public ConfirmationViews View { get; set; }

            public string Confirm_message
            {
                get => CONFIRM_MSGS[(int)View];
                set { }
            }

            public string Description
            {
                get => DESCRIPTION[(int)View];
                set { }
            }

            [Display(Name = "下払月")]
            public string Select_day { get; set; }

            public int Select_shimebi { get; set; }

            [Display(Name = "発行日")]
            public string Publish_day { get; set; }

            string _shitabarai_day_to;
            [Display(Name = "期間")]
            public string Shitabarai_day_to
            {
                get
                {
                    if (!string.IsNullOrWhiteSpace(_shitabarai_day_to))
                        return _shitabarai_day_to;

                    if (Select_shimebi < 1)
                        return "";

                    DateTime now = DateTime.Now.Date;                // System date
                    DateTime day = add_days_but_in_month_range(
                        new DateTime(Convert.ToInt32(Select_day[..4]), Convert.ToInt32(Select_day[5..7]), 1), Select_shimebi - 1);

                    // 請求月と締日から期間を計算する
                    day = now < day ? day.AddDays(-1) :
                        (Select_shimebi == 1 ? new DateTime(now.Year, now.Month, 1).AddDays(-1) : add_days_but_in_month_range(new DateTime(now.Year, now.Month, 1), Select_shimebi - 1 - 1));

                    return $"{day:yyyy年MM月dd日}({new CultureInfo("ja-JP").DateTimeFormat.GetShortestDayName(day.DayOfWeek)})";
                }
                set => _shitabarai_day_to = value;
            }

            /// <summary>
            /// 選択日付を解析する
            /// </summary>
            /// <param name="s">日付文字列</param>
            /// <returns>解析された日付</returns>
            private static DateTime ParseSelectDate(string s)
            {
                var yyyy = Convert.ToInt32(s[..4]);
                var mm = Convert.ToInt32(s[5..7]);
                var d = new DateTime(yyyy, mm, 1).AddDays(DateTime.Now.Day - 1);
                if (d.Month != mm)
                    d = new DateTime(yyyy, mm, 1).AddMonths(1).AddDays(-1);
                return d;
            }

            private ShitabaraiInfo _shitabarai;
            [Display(Name = "傭車")]
            public ShitabaraiInfo Shitabarai
            {
                get => View == ConfirmationViews.BULK ? Shitabarai_list?.FirstOrDefault() : _shitabarai;
                set => _shitabarai = View == ConfirmationViews.BULK ? null : value;
            }

            private List<ShitabaraiInfo> _shitabarai_list;
            public List<ShitabaraiInfo> Shitabarai_list
            {
                get => View == ConfirmationViews.BULK ? _shitabarai_list : new() { _shitabarai };
                set => _shitabarai_list = View == ConfirmationViews.BULK ? value : null;
            }

            public IEnumerable<SelectListItem> Layouts { set; get; }
            [Display(Name = "レイアウト")]
            public string Selected_layout
            {
                get => View == ConfirmationViews.PRINT || View == ConfirmationViews.PREVIEW ? $"{InvoiceController.LAYOUT_SELECTED_CODE_DATA}" : null;
                set { }
            }

            /// <summary>
            /// 月範囲内で日付を追加する
            /// </summary>
            /// <param name="d">基準日</param>
            /// <param name="days">追加日数</param>
            /// <returns>追加後の日付</returns>
            private static DateTime add_days_but_in_month_range(DateTime d, int days)
            {
                var r = d.AddDays(days);
                return r.Year == d.Year && r.Month == d.Month ? r : days > 0 ? new DateTime(d.Year, d.Month, 1).AddMonths(1).AddDays(-1) : new DateTime(d.Year, d.Month, 1);
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
        /// 下払い情報
        /// </summary>
        public class ShitabaraiInfo
        {
            public string Yosya_Name { get; set; }

            public int? Shime_Day { get; set; }
            public string Mail_Address1 { get; set; }
            public string Mail_Address2 { get; set; }
            public int? Yosya_Branch_ID { get; set; }
            public int Uriage_Shiharai_ID { get; set; }
            public string Uriage_Shiharai_ID_LIST { get; set; }
            public int Check_Shitabarai_ID { get; set; }
            public int? Zei_Kubun { get; set; }
        }

        /// <summary>
        /// データリストモデル
        /// </summary>
        public class DataListModel
        {
            public int Company_ID { get; set; }

            /// <summary>
            /// 検索項目
            /// </summary>
            public SearchModelForShitabaraiCheckDataList Search { get; set; }

            /// <summary>
            /// 傭車一覧
            /// </summary>
            public List<V_ShitabaraiCheckDataList_Local> ShitabaraiCheckDataList { get; set; }

            /// <summary>
            /// レイアウトオプション
            /// </summary>
            public List<SelectListItem> LayoutOption { get; set; }

            /// <summary> 
            /// ソート順
            /// </summary>
            public DataListSortModel SortParam { get; set; }
        }

        /// <summary>
        /// 下払いチェックデータリストの検索モデル
        /// </summary>
        public class SearchModelForShitabaraiCheckDataList : CommonModel
        {
            [Required]
            public override string SelectDay { get; set; } = $"{DateTime.Now:yyyy年MM月}";

            new public string SelectTantou { get; set; }

            [Display(Name = "下払担当")]
            public string SelectShitabaraiTantou { set; get; }

            /// <summary>
            /// 税区分
            /// 0:課税、1:非課税
            /// </summary>
            [Display(Name = "税区分")]
            public int SelectZeiKubun { set; get; } = InvoiceModel.TAX_CATEGORY_TAX;

            /// <summary>
            /// 傭車先（得意先1）
            /// </summary>
            [Display(Name = "傭車先")]
            public int? Customer1 { get; set; }

            /// <summary>
            /// 傭車先（得意先1）
            /// </summary>
            public string Customer_name1 { get; set; }

            /// <summary>
            /// 傭車先（得意先2）
            /// </summary>
            [Display(Name = "傭車先")]
            public int? Customer2 { get; set; }

            /// <summary>
            /// 傭車先（得意先2）
            /// </summary>
            public string Customer_name2 { get; set; }

            /// <summary>
            /// 発行日
            /// </summary>
            [Required]
            [Display(Name = "発行日")]
            public string PublishDay { get; set; } = $"{DateTime.Now:yyyy年MM月dd日}({new CultureInfo("ja-JP").DateTimeFormat.GetShortestDayName(DateTime.Now.DayOfWeek)})";

            /// <summary>
            /// 下払年月日（まで）
            /// </summary>
            [Display(Name = " 下払年月日（まで）")]
            public string ShitabaraiDayTo { get; set; }
        }
    }
}
