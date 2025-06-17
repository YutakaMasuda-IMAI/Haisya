using HaisyaWeb.API.WebApp;
using HaisyaWeb.Common;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.InvoiceModel;

namespace HaisyaWeb.Controllers
{
    public class ClosingController : BaseController
    {
        public const int LAYOUT_CODE_ID = 11;
        public const string LAYOUT_SELECTED_CODE_DATA = "1";    // Aパターン

        protected readonly ILogger<InvoiceController> _logger;

        protected Task<V_LoginUser_Local> _get_login_user;

        public ClosingController(
            ILogger<InvoiceController> logger,
            SignInManager<ApplicationUser> signInManager,
            IOptions<MapApiSettings> mapApiSetting,
            IViewRenderService viewRenderService)
            => (_logger, _signInManager, _mapApiSettiong, _viewRenderService) = (logger, signInManager, mapApiSetting.Value, viewRenderService);

        /// <summary>
        /// 請求書データのインデックスページを表示する
        /// [売上]-[請求問合せ]-[問合せ発行処理]
        /// </summary>
        /// <returns>インデックスページのビュー</returns>
        public virtual async Task<IActionResult> Index()
        {
            try
            {
                // モデルを作成
                DataListModel<V_InvoiceDataList_Local> model = await CreateModel<V_InvoiceDataList_Local>();
                if (string.IsNullOrEmpty(model?.Search?.Publish_date))
                {
                    CultureInfo japaneseCulture = new CultureInfo("ja-JP");
                    model.Search.Publish_date = DateTime.Now.ToString("yyyy年MM月dd日(ddd)", japaneseCulture);
                }
                return View(model);
            }
            // エラーが発生した場合
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }
        /// <summary>
        /// 請求書データの検索結果を表示する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Search(DataListModel<V_InvoiceDataList_Local> m)
        {
            try
            {
                // 請求データを検索
                IDataListModel searchResults = await SearchData(m);
                return PartialView("_DataList", searchResults);
            }
            // エラーが発生した場合
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 請求書のサマリーを表示する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmPublishing(InvoiceSummModel m)
        {
            try
            {
                SetBizType(m);
                // 請求書のサマリーを表示
                if (!string.IsNullOrWhiteSpace(m.Selected_layout))
                {
                    // レイアウトを取得
                    m.Layouts = await SearchCommonService.GetReportSearchKubunList(_mapApiSettiong, (int)Common.SystemEnums.ReportType.SEIKYUSHO, null);
                }
                if (m.View != ConfirmationViews.BULK)
                {
                    // 請求書の住所を設定
                    m.Invoice_list = new() { m.Invoice };
                }
                return PartialView("_Closing", m);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 業務種別を設定する
        /// </summary>
        /// <param name="m"></param>
        protected virtual void SetBizType(InvoiceSummModel m) => m.Biz = BizTypes.INVOICE;

        /// <summary>
        /// 複数の請求書を一括発行する
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Bulk(InvoiceSummModel data)
        {
            // 複数の請求書を一括発行する処理を実装する
            try
            {
                var r = await Publish(data);
                if (r.RetrunFlg)
                {
                    // TODO: Dosomething here
                }
                return Json(new { data = r });
            }
            catch (Exception x)
            {
                return await JsonError(x);
            }
        }

        /// <summary>
        /// 請求書をメールで発行する
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Mail(InvoiceSummModel data)
        {
            // 請求書のメール発行処理を実装する
            try
            {
                var r = await Publish(data);
                if (r.RetrunFlg)
                {
                    // TODO: Send mail here
                }
                return Json(new { data = r });
            }
            catch (Exception x)
            {
                return await JsonError(x);
            }
        }

        /// <summary>
        /// 請求書を印刷する
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Print(InvoiceSummModel data)
        {
            // 請求書の印刷処理を実装する
            try
            {
                var r = await Publish(data);
                if (r.RetrunFlg)
                {
                    // TODO: 請求書の印刷処理を実装する
                }
                return Json(new { data = r });
            }
            catch (Exception x)
            {
                return await JsonError(x);
            }
        }

        /// <summary>
        /// 請求書のプレビューを印刷します。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<IActionResult> Preview(InvoiceSummModel data)
        {
            // 請求書のプレビュー処理を実装する
            try
            {
                var r = await Publish(data);
                if (r.RetrunFlg)
                {
                    // TODO: 請求書のプレビュー処理を実装する
                }
                return Json(new { data = r });
            }
            catch (Exception x)
            {
                return await JsonError(x);
            }
        }

        /// <summary>
        /// 発行処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual async Task<MsterDataCommonResultValDto_Local> Publish(InvoiceSummModel data)
        {
            // ログインユーザー取得
            _get_login_user ??= GetLoginUser();
            V_LoginUser_Local u = await _get_login_user;

            // 下払い問い合わせ発行処理
            using InvoiceDataApi api = new(_mapApiSettiong);
            return await api.PublishInvoiceDataList(new()
            {
                LoginUserId = u.User_ID,
                CompanyId = u.Company_ID,
                InquiryType = (InquiryTypes)data.View,
                DataList = DataList<V_InvoiceDataList_Local>(data),
            });
        }

        /// <summary>
        /// データリストを作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static List<InquiryData_Local<T>> DataList<T>(InvoiceSummModel data)
            where T : V_InvoiceDataList_Local, new()
            => data.Invoice_list.Select(si => new InquiryData_Local<T>()
            {
                PrintDate = ParseDate(data.Publish_date).Value,
                Month = ParseSelectDate(data.Year_month),
                PrintToDate = ParseDate(data.Sale_date) ?? DateTime.Now.Date,
                Data = New<T>(si)
            }).ToList();

        /// <summary>
        /// モデルを作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ii"></param>
        /// <returns></returns>
        private static T New<T>(InvoiceInfo ii)
            where T : V_InvoiceDataList_Local, new()
        {
            T t = new T()
            {
                Seikyu_ID = ii.SelectRow,
                Shime_Day = ii.Shime_Day,
                Mail_Address1 = ii.Mail_address1,
                Mail_Address2 = ii.Mail_address2,
                Customer_Branch_ID = ii.Customer_Branch_ID,
            };
            if (t is V_InvoiceCheckDataList_Local tt)
            {
                tt.Check_Seikyu_ID = ii.SelectRow;
                tt.Uriage_Unchin_ID = ii.Uriage_Unchin_ID;
            }
            return t;
        }

        /// <summary>
        /// モデルを作成します。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected async Task<DataListModel<T>> CreateModel<T>(SearchModelForInvoiceList s = null)
            where T : V_InvoiceDataList_Local, new()
        {
            // ログインユーザーを取得
            _get_login_user ??= GetLoginUser();
            V_LoginUser_Local u = await _get_login_user;
            int c = u.Company_ID;
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Invoicelist = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, u.Company_ID, UserGroupLists.Seikyu, false);
            int Invoice_person = (int)TantouLists.ALL;

            if (int.TryParse(Invoicelist.FirstOrDefault(m => m.Selected == true).Value, out int Invoice_person_selected))
            {
                // ログインユーザーがあれば選択
                Invoice_person = Invoice_person_selected;
            }

            // モデルを作成
            return new DataListModel<T>()
            {
                Search = s ?? new()
                {
                    CompanyID = (await _get_login_user).Company_ID,
                    SeikyuTantouSelectList = Invoicelist,
                    Seikyu_Tantou_Name = Invoice_person,
                },
            };
        }

        /// <summary>
        /// 請求データを検索します。
        /// </summary>
        /// <param name="m"></param>
        /// <returns>invoice data list</returns>
        protected virtual async Task<IDataListModel> SearchData(IDataListModel m)
        {
            ModelState.Clear();

            if (m.Search != null)
            {
                if (m.Search.Seikyu_Tantou_Name == null)
                    m.Search.Seikyu_Tantou_Name = 0;

                if (m.Search.Customer2 == null)
                {
                    if (m.Search.Customer1 != null)
                        m.Search.Customer2 = m.Search.Customer1;
                    else
                        m.Search.Customer2 = 99999999;
                }

                if (m.Search.Customer1 == null)
                    m.Search.Customer1 = 0;
            }
            // モデルの検証
            TryValidateModel(m);
            if (!ModelState.IsValid)
                return m;
            SearchModelForInvoiceList s = m.Search;

            DataListModel<V_InvoiceDataList_Local> result = new DataListModel<V_InvoiceDataList_Local>()
            {
                // 検索条件を設定
                Search = s,
                InvoiceDataLists = s.CompanyID == 0 ? new() :
                    // 請求データを取得
                    await new InvoiceDataApi(_mapApiSettiong).GetInvoiceDataList(
                        s.CompanyID,
                        new DateTime(Convert.ToInt32(s.Year_month[..4]), Convert.ToInt32(s.Year_month[5..7]), 1),
                        s.Closing_days.Value,
                        s.Seikyu_Tantou_Name,
                        s.Customer1,
                        s.Customer2,
                        ParseDate(s.Publish_date).Value,
                        null),
            };

            result.InvoiceDataLists = result.InvoiceDataLists.ToList();
            return result;
        }

        /// <summary>
        /// 日付をパース
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected static DateTime? ParseDate(string s)
            => string.IsNullOrWhiteSpace(s) ? null : new(Convert.ToInt32(s[..4]), Convert.ToInt32(s[5..7]), Convert.ToInt32(s[8..10]));

        private static DateTime ParseSelectDate(string s)
        {
            int yyyy = Convert.ToInt32(s[..4]);
            int mm = Convert.ToInt32(s[5..7]);
            DateTime d = new DateTime(yyyy, mm, 1);
            return d;
        }
    }
}
