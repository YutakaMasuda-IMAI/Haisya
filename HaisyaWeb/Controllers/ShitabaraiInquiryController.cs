using HaisyaWeb.API.WebApp;
using HaisyaWeb.Common;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.ShitabaraiCheckDataModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 下払い問い合わせコントローラー
    /// </summary>
    public class ShitabaraiInquiryController : BaseController
    {
        public const int LAYOUT_CODE_ID = 11;

        private readonly ILogger<ShitabaraiInquiryController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="signInManager">サインインマネージャー</param>
        /// <param name="viewRenderService">ビューのレンダリングサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public ShitabaraiInquiryController(
            ILogger<ShitabaraiInquiryController> logger,
            SignInManager<ApplicationUser> signInManager,
            IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting)
            => (_logger, _signInManager, _mapApiSettiong, _viewRenderService) = (logger, signInManager, mapApiSetting.Value, viewRenderService);

        /// <summary>
        /// 下払い問い合わせ発行処理一覧
        /// [売上]-[支払問合せ]-[問合せ発行処理]
        /// </summary>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                DataListModel model = await CreateModel();
                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 請求書データの検索結果を表示する
        /// </summary>
        /// <param name="m">データリストモデル</param>
        /// <returns>ビュー</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Search(DataListModel m)
        {
            try
            {
                DataListModel searchResults = await SearchData(m);
                searchResults.SortParam = m.SortParam;
                return await PartialViewAsJson("_DataList", searchResults);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 初期表示データ作成
        /// </summary>
        /// <param name="s">検索モデル</param>
        /// <returns>データリストモデル</returns>
        private async Task<DataListModel> CreateModel(SearchModelForShitabaraiCheckDataList s = null)
        {
            // ログインユーザー取得
            V_LoginUser_Local loginUser = await GetLoginUser();

            return new DataListModel()
            {
                Company_ID = loginUser.Company_ID,
                Search = s ?? new()
                {
                    // TODO:GetUserGroupSelectは下払の情報を取るには、「UserGroupLists.Eigyo:3」を渡す
                    TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loginUser.Company_ID, UserGroupLists.Eigyo, false),
                    SelectShitabaraiTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loginUser.Company_ID,
                                                UserGroupLists.Eigyo, loginUser.User_ID) ?? "ALL",
                },
            };
        }

        /// <summary>
        /// 請求書のサマリーを表示する
        /// </summary>
        /// <param name="m">下払いサマリーモデル</param>
        /// <returns>ビュー</returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmPublishing(ShitabaraiSummModel m)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(m.Selected_layout))
                {
                    // レイアウトを取得
                    // TODO:下払の発行であるが、請求書レイアウトをコール
                    List<M_Report_Serch_Kubun_Local> reportSearchKubuns = await GetReportSearchKubun((int)Common.SystemEnums.ReportType.SEIKYUSHO);
                    m.Layouts = reportSearchKubuns.Select(x => new SelectListItem() { Value = $"{x.Report_Serch_Kubun_ID} {x.Report_Html}", Text = x.Display_Title, Selected = false });

                    // Get PDF flag
                    // TODO:下払の発行であるが、請求書レイアウトをコール
                    M_Report_Serch_Local reportSerch = await GetReportSearchData((int)Common.SystemEnums.ReportType.SEIKYUSHO);
                    if (reportSerch?.Report_Serch_ID > 0)
                        m.PDF_flag = reportSerch.Pdf_Flg;

                    // get default layout
                    m.Layout = m.Layouts.FirstOrDefault()?.Value;
                }
                if (m.View != ConfirmationViews.BULK)
                {
                    m.Shitabarai_list = new() { m.Shitabarai };
                }
                return await PartialViewAsJson("_Shitabarai", m);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 複数の請求書を一括発行する
        /// </summary>
        /// <param name="data">下払いサマリーモデル</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Bulk(ShitabaraiSummModel data)
        {
            try
            {
                HaisyaWeb.Dto.MsterDataCommonResultValDto_Local r = await PublishShitabaraiInquiry(data);
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
        /// <param name="data">下払いサマリーモデル</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Mail(ShitabaraiSummModel data)
        {
            try
            {
                MsterDataCommonResultValDto_Local r = await PublishShitabaraiInquiry(data);
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
        /// <param name="data">下払いサマリーモデル</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Print(ShitabaraiSummModel data)
        {
            try
            {
                MsterDataCommonResultValDto_Local r = await PublishShitabaraiInquiry(data);
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
        /// <param name="data">下払いサマリーモデル</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Preview(ShitabaraiSummModel data)
        {
            try
            {
                MsterDataCommonResultValDto_Local r = await PublishShitabaraiInquiry(data);
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
        /// 請求データを検索します。
        /// </summary>
        /// <param name="m">データリストモデル</param>
        /// <returns>請求データリスト</returns>
        private async Task<DataListModel> SearchData(DataListModel m)
        {
            // データチェック
            TryValidateModel(m);
            if (!ModelState.IsValid)
                return m;
            SearchModelForShitabaraiCheckDataList s = m.Search;

            if (m.Search != null)
            {
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

            // 月初
            string firstDay = null;
            if (s.SelectDay != null)
                firstDay = s.SelectDay?.ToString("yyyy/MM/01");

            DataListModel result = new DataListModel()
            {
                Search = s,
                ShitabaraiCheckDataList = m.Company_ID == 0 ? new() :
                    // 下払いチェック一覧データの取得（APIコール）
                    await new ShitabaraiDataCheckApi(_mapApiSettiong).GetShitabaraiCheckDataList(
                        m.Company_ID,
                        ParseDate(s.PublishDay)?.ToString("yyyy/MM/dd"),
                        s.Customer1.ToString(),
                        s.Customer2.ToString(),
                        firstDay,
                        string.IsNullOrWhiteSpace(s.SelectShimebi) ? null : Convert.ToInt32(s.SelectShimebi),
                        s.SelectZeiKubun,
                        "",
                        s.SelectShitabaraiTantou)
            };

            // Group
            var shitabaraiCheckDataGropuList = result.ShitabaraiCheckDataList.OrderBy(m => m.Yosya_Branch_ID).GroupBy(x => new
            {
                Shime_Day = x.Shime_Day
                ,
                Tax_category = x.Zei_Kubun
                ,
                Customer_Branch_ID = x.Yosya_Branch_ID
            });

            List<V_ShitabaraiCheckDataList_Local> invoiceCheckDataList_Local = new List<V_ShitabaraiCheckDataList_Local>();
            foreach (var target in shitabaraiCheckDataGropuList)
            {
                // 付随する支払IDリスト
                List<int?> Uriage_Shiharai_ID_List = new List<int?>();
                foreach (var invoiceCheckData in target)
                {
                    if (Uriage_Shiharai_ID_List.Contains(invoiceCheckData.Uriage_Shiharai_ID))
                        continue;
                    Uriage_Shiharai_ID_List.Add(invoiceCheckData.Uriage_Shiharai_ID);
                }

                var key = target.Key;
                foreach (var invoiceCheckData in target)
                {
                    V_ShitabaraiCheckDataList_Local groupList = new V_ShitabaraiCheckDataList_Local();
                    groupList = invoiceCheckData;
                    groupList.Uriage_Shiharai_ID_LIST = String.Join(",", Uriage_Shiharai_ID_List);

                    invoiceCheckDataList_Local.Add(groupList);
                    break;
                }
            }
            result.ShitabaraiCheckDataList = invoiceCheckDataList_Local;

            return result;
        }

        private static DateOnly? ParseDate(string s)
            => string.IsNullOrWhiteSpace(s) ? null : new(Convert.ToInt32(s[..4]), Convert.ToInt32(s[5..7]), Convert.ToInt32(s[8..10]));
        private static DateOnly ParseSelectDate(string s)
        {
            int yyyy = Convert.ToInt32(s[..4]);
            int mm = Convert.ToInt32(s[5..7]);
            DateOnly d = new DateOnly(yyyy, mm, 1);//.AddDays(DateTime.Now.Day - 1);
            if (d.Month != mm)
                d = new DateOnly(yyyy, mm, 1).AddMonths(1).AddDays(-1);
            return d;
        }

        /// <summary>
        /// モーダルのインデックスを表示します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="ModalType">モーダルタイプ</param>
        /// <returns>ビュー</returns>
        [HttpPost]
        public async Task<IActionResult> IndexModal(int CompanyID, string ModalType)
        {
            try
            {
                YosyaModel.YosyaDataListModel model = new()
                {
                    CompanyID = CompanyID,
                    ModalType = ModalType,
                };

                return await PartialViewAsJson("../Yosya/YousyaModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 発行処理
        /// </summary>
        /// <param name="data">下払いサマリーモデル</param>
        /// <returns>発行結果</returns>
        private async Task<MsterDataCommonResultValDto_Local> PublishShitabaraiInquiry(ShitabaraiSummModel data)
        {
            // ログインユーザー取得
            HaisyaWeb.Dto.V_LoginUser_Local u = await GetLoginUser();

            // 下払い問い合わせ発行処理
            using ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.PublishShitabaraiCheckDataList(new()
            {
                loginUserId = u.User_ID,
                companyID = u.Company_ID,
                inquiryType = (int)data.View,
                Print_Pattern = data.Print_Pattern,
                shitabaraiInquiryDataList = data.Shitabarai_list.Select(si => new ShitabaraiInquiryData_Local()
                {
                    printDate = ParseDate(data.Publish_day).Value,
                    shitabaraiMonth = ParseSelectDate(data.Select_day),
                    printToDate = ParseDate(data.Shitabarai_day_to) ?? DateOnly.FromDateTime(DateTime.Now),
                    ShitabaraiCheckData = new V_ShitabaraiCheckDataList_Local()
                    {
                        Shime_Day = si.Shime_Day,
                        Mail_Address1 = si.Mail_Address1,
                        Mail_Address2 = si.Mail_Address2,
                        Yosya_Branch_ID = si.Yosya_Branch_ID,
                        Uriage_Shiharai_ID = si.Uriage_Shiharai_ID,
                        Uriage_Shiharai_ID_LIST = si.Uriage_Shiharai_ID_LIST,
                        Check_Shitabarai_ID = si.Check_Shitabarai_ID,
                        Zei_Kubun = si.Zei_Kubun,
                    }
                }).ToList()
            });
        }

        /// <summary>
        /// M_Report_Serch by Report_Number詳細取得
        /// </summary>
        /// <param name="reportNum">検索帳票番号</param>
        /// <returns>M_Report_Serch_Localの詳細</returns>
        private async Task<Dto.M_Report_Serch_Local> GetReportSearchData(int reportNum)
        {
            using MasterDataApi apiM = new(_mapApiSettiong);
            Dto.M_Report_Serch_Local data = await apiM.GetReportSearch(reportNum);
            return data;
        }

        /// <summary>
        /// M_Report_Serch_Kubun by Report_Serch_ID
        /// </summary>
        /// <param name="ReportSerchID">検索帳票ID</param>
        /// <returns>
        /// M_Report_Serch_Kubun_Localの一覧
        /// </returns>
        private async Task<List<Dto.M_Report_Serch_Kubun_Local>> GetReportSearchKubun(int reportSearchID)
        {
            using MasterDataApi apiM = new(_mapApiSettiong);
            List<Dto.M_Report_Serch_Kubun_Local> data = await apiM.GetReportSearchKubunList(reportSearchID);
            return data;
        }
    }
}
