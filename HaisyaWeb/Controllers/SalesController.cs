using HaisyaWeb.API.WebApp;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.SalesModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 売上コントローラー
    /// </summary>
    public class SalesController : BaseController
    {
        private readonly ILogger<SalesController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public SalesController(ILogger<SalesController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 売上処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;
                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 日報承認処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ApprovalIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 2;
                return View("Index", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 問合せ処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> InquiryIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 2;
                model.Search.SelectSyoriKubun = 0;
                return View("../Inquiry/Index", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 変更承認処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FixAmountApprovalIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 2;
                model.Search.SelectSyoriKubun = 0;
                return View("../FixAmountApproval/Index", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// Index2処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index2(AnkenModel.SearchModelForAnkenList param)
        {
            try
            {
                DataListModel model = await CreateModel();
                if (param.SelectDay != null)
                {
                    model.Search.SelectDay = param.SelectDay;
                    model.Search.SelectTantou = param.SelectTantou;
                    model.Search.SelectSyasyu = param.SelectSyasyu;
                    model.Search.SelectKata = param.SelectKata;
                    model.Search.SelectGroup = param.SelectGroup;
                    model.Search.BackMenuAction = "AnkenList";
                }
                return View("Index", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel()
        {
            SearchModelForSalesList param = new()
            {
                SelectGroup = 0,
                SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
            };
            
            param.SelectTantou ??= "ALL";

            DataListModel model = new() { };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            SearchModelForSalesList search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SelectTantou = param.SelectTantou,
                SelectTab = param.SelectTab,
                SelectDay = param.SelectDay,
                SelectGroup = param.SelectGroup,
            };

            model.Search = search;
            return model;
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForSalesList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                IEnumerable<Dto.V_AnkenDataList_Local> list = await GetAnkenDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0, 0, 0);

                List<AnkenDataList> listData = new();

                if (list != null)
                {
                    foreach (var data in list)
                    {
                        listData.Add(new AnkenDataList(data));
                    }

                    if (param.SelectTantou != null && !"ALL".Equals(param.SelectTantou))
                    {
                        listData = listData.Where(m => m.TantouID == int.Parse(param.SelectTantou)).ToList();
                    }

                    if (param.SelectSyasyu != null)
                    {
                        listData = listData.Where(m => m.SyasyuDisplay.Contains(param.SelectSyasyu)).ToList();
                    }

                    if (param.SelectKata != null)
                    {
                        listData = listData.Where(m => m.Kata.Contains(param.SelectKata)).ToList();
                    }

                    if (param.SelectDay != null)
                    {
                        listData = listData.Where(m => m.START_PointDate == DateTime.Parse(param.SelectDay)).ToList();
                    }
                }

                model.AnkenDataLists = listData;

                string html = "../Inquiry/DataListForNone";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataListForToiawase(SearchModelForSalesList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                IEnumerable<Dto.M_Customer_Local> list = await GetTokuisakiDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0);

                List<TokuisakiList> listData = new();

                foreach (var data in list)
                {
                    listData.Add(new TokuisakiList(data));
                }

                model.TokuisakiLists = listData;

                string html = "../Inquiry/DataListForNone";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：承認一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataListForFixApproval(SearchModelForSalesList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                IEnumerable<Dto.M_Customer_Local> list = await GetTokuisakiDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0);

                List<TokuisakiList> listData = new();

                foreach (var data in list)
                {
                    listData.Add(new TokuisakiList(data));
                }

                model.TokuisakiLists = listData;

                string html = "../FixAmountApproval/DataListForNone";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="CcompanyID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_AnkenDataList_Local>> GetAnkenDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID, int iSenzokuID)
        {
            using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.V_AnkenDataList_Local> ankenDataList = await api.GetAnkenDataList(targetDate, null, null, iCcompanyID, iCustomerID, iBranchID, iSenzokuID);
            return ankenDataList;
        }

        /// <summary>
        /// JSON：得意先一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="CcompanyID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.M_Customer_Local>> GetTokuisakiDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iBranchID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Customer_Local> dataList = await api.GetCustomerList(iCcompanyID, null, null, null);
            return dataList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> ToiawaseDetailIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                //if (param.SelectDay != null)
                //{
                //    model.Search.SelectDay = param.SelectDay;
                //    model.Search.SelectTantou = param.SelectTantou;
                //    model.Search.SelectSyasyu = param.SelectSyasyu;
                //    model.Search.SelectKata = param.SelectKata;
                //    model.Search.SelectGroup = param.SelectGroup;
                //    model.Search.BackMenu = "AnkenList";
                //}
                return View("../Inquiry/Detail", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataListForToiawaseDetail(SearchModelForSalesList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                IEnumerable<Dto.V_AnkenDataList_Local> list = await GetAnkenDataList(DateTime.Parse("2023/09/19").ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0, 0, 0);

                List<AnkenDataList> listData = new();

                foreach (var data in list)
                {
                    listData.Add(new AnkenDataList(data));
                }

                model.AnkenDataLists = listData;


                string html = "../Inquiry/DataListForAnken";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> FixApprovalDetailIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                //if (param.SelectDay != null)
                //{
                //    model.Search.SelectDay = param.SelectDay;
                //    model.Search.SelectTantou = param.SelectTantou;
                //    model.Search.SelectSyasyu = param.SelectSyasyu;
                //    model.Search.SelectKata = param.SelectKata;
                //    model.Search.SelectGroup = param.SelectGroup;
                //    model.Search.BackMenu = "AnkenList";
                //}
                return View("../Inquiry/Detail", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataListForFixApprovalDetail(SearchModelForSalesList param)
        {
            try
            {
                DataListModel model = new() { };

                if (param == null) { return null; }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                IEnumerable<Dto.V_AnkenDataList_Local> list = await GetAnkenDataList(DateTime.Parse("2023/09/19").ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0, 0, 0);

                List<AnkenDataList> listData = new();

                //Parallel.ForEach(list, data =>
                //{
                //    listData.Add(new AnkenDataList(data));
                //});

                foreach (var data in list)
                {
                    listData.Add(new AnkenDataList(data));
                }

                model.AnkenDataLists = listData;


                string html = "../FixAmountApproval/DataListForAnken";

                //switch (param.SelectGroup)
                //{
                //    case 1:
                //        html += "DataListForStatus";
                //        break;
                //    case 2:
                //        html += "DataListForStep";
                //        break;
                //    case 3:
                //        html += "DataListForSyasyu";
                //        break;
                //    case 4:
                //        html += "DataListForTantou";
                //        break;
                //    default:
                //        html += "DataListForNone";
                //        break;

                //}
                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 専属月清算データを取得し、ビューを返します。
        /// </summary>
        /// <param name="date">清算日付（オプション）</param>
        /// <param name="Senzoku_ID">専属ID</param>
        /// <returns>専属月清算用のビュー</returns>
        public async Task<IActionResult> GetMonthlySettlementData(DateTime? date, int Senzoku_ID)
        {
            try
            {
                // SalesDataApi インスタンスを作成し、API を使用して専属データを取得
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);

                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                MasterDataApi masterDataApi = new(_mapApiSettiong);

                // SENZOKU_ID と date に基づいて専属データを取得
                SenzokuModel SenzokuData = await api.GetSenzokuData(Senzoku_ID, date); // SENZOKU_IDが前ページから送られてくる

                SenzokuData.SenzokuDataList = SenzokuData.SenzokuDataList.Where(x => x.AnkenData?.SenzokuID == Senzoku_ID).ToList();
                // 専属データに対して、ログインユーザーの会社IDとユーザーIDを設定

                List<int> uriageIds = SenzokuData.SenzokuDataList.Select(x => x.Uriage.Uriage_ID).ToList();
                SenzokuData.TotalUriageUnsyuList = SenzokuData.TotalUriageUnsyuList.Where(x => uriageIds.Contains(x.UriageId)).ToList();
                SenzokuData.SeikyuDate = date ?? DateTime.Now;
                SenzokuData.BurdenList = await masterDataApi.GetBurdenList(loguinUser.Company_ID);
                SenzokuData.User_ID = loguinUser.User_ID;
                SenzokuData.Company_ID = loguinUser.Company_ID;
                SenzokuData.OperationDateCount = SenzokuData.SenzokuDataList.Where(x => x.Uriage.Uriage_ID > 0).Select(data => data.Uriage?.Haisya_Date.Date).Distinct().Count();

                // 専属月清算用のビューを返す
                return await PartialViewAsJson("MonthlySettlementData", SenzokuData, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 指定された案件ID、処理カテゴリ、および区分に基づいて売上データを取得し、ビューを返します。
        /// </summary>
        /// <param name="data"></param>
        /// <returns>売上データ用のビュー</returns>
        public async Task<IActionResult> GetSales(GetSalesDto data)
        {
            int Anken_ID = data.Anken_ID;
            int processingCategory = data.processingCategory;
            // 登録区分：0:請求/1:下払
            int? kubun = data.Search?.SelectGroup;
            int Uriage_ID = data.Uriage_ID;

            try
            {
                using SalesDataApi api = new(_mapApiSettiong);
                using HaisyaDataApi apiHaisya = new(_mapApiSettiong);
                V_LoginUser_Local loguinUser = await GetLoginUser();
                MasterDataApi masterDataApi = new(_mapApiSettiong);

                // 処理カテゴリに基づいて処理カテゴリ文字列を決定
                string processingCategoryString = processingCategory switch
                {
                    0 => kubun switch
                    {
                        0 => "案件請求",
                        1 => "案件下払",
                        _ => "案件請求"
                    },
                    1 => "専属月額",
                    2 => "直接入力",
                    _ => ""
                };

                // 処理カテゴリが「専属月額」の場合、専属月清算データを取得してビューを返す
                if (processingCategoryString == "専属月額")
                {
                    //専属月額の場合、Uriage_IDではなく、SENZOKU_IDとして使用
                    SenzokuModel SenzokuData = await api.GetSenzoku(data.Uriage_ID); // SENZOKU_IDが前ページから送られてく
                    SenzokuData.SeikyuDate = DateTime.TryParse(data.Search.SelectDay, out DateTime selectDay) ? selectDay : DateTime.Now;
                    // 専属月清算用のビューを返す

                    if (data.Search != null)
                    {
                        SenzokuData.Search = data.Search;
                    }
                    else
                    {
                        SenzokuData.Search = new SalesPaymentModel.SearchModelForSalesPaymentList();
                    }

                    SenzokuData.Search.BackMenuAction = "DataList";

                    return View("ExclusiveMonthlySettlement", SenzokuData);
                }
                // 案件データを取得
                IEnumerable<V_HaisyaDataList_Local> ankenData = await GetHaisyaDataList(null, null, null, loguinUser.Company_ID, 0, 0);
                V_HaisyaDataList_Local filtered = ankenData.Where(item => item.Anken_ID == Anken_ID).FirstOrDefault();
                List<V_HaisyaDataList_Local> filteredList = ankenData.Where(item => item.Anken_ID == Anken_ID).ToList();

                // 売上データの初期化
                Dto.SalesModel SalesData = new();

                if (filtered != null)
                {
                    SalesData = await api.GetSales(Anken_ID, filtered.KokyakuId, filtered.Driver_ID ?? 0, filtered.Haisya_ID ?? 0);
                    SalesData.CustomerUriageCalcData = await masterDataApi.GetCustomerUriageCalcData(SalesData.Customer.Customer_Branch_ID);
                    
                    SalesData.HaisyaData = filtered;

                    SalesData.HaisyaDataList = filteredList;
                    SalesData.AnkenId = Anken_ID;
                    SalesData.PaidUser = new();
                }
                else
                {
                    // 売上IDがある場合
					if ((Anken_ID == 0) && (Uriage_ID > 0))
					{
                        SalesData = await api.GetUriagebyId(Uriage_ID);
                    }
                    else
					{
                        SalesData = await api.GetSales(0, 0, 0, 0);
                    }
                    using MasterDataApi mapi = new(_mapApiSettiong);
                    SalesData.PaidUser = await mapi.GetCompanyUserList(loguinUser.Company_ID, "seikyu");
                    SalesData.HaisyaData = new();
                    SalesData.HaisyaDataList = new();
                    SalesData.AnkenId = 0;
                }

                // 赤伝黒伝登録
                if (SalesData.IsSeikyuCommitted || SalesData.IsShitabaraiCommitted)
				{
                    if (SalesData.IsSeikyuCommitted && (kubun == 0))
                    {
                        // 請求の赤黒判定があり、登録区分が請求を選択
                        processingCategoryString = "赤伝黒伝登録請求";
                    }
                    else if(SalesData.IsShitabaraiCommitted && (kubun == 1))
                    {
                        // 下払の赤黒判定があり、登録区分が下払を選択
                        processingCategoryString = "赤伝黒伝登録下払";
                    }
                }
                // 特定の処理カテゴリに対する読み取り専用設定
                if (processingCategoryString == "赤伝黒伝登録下払" || processingCategoryString == "案件下払")
                {
                    // 赤伝黒伝登録下払　又は、案件下払　の時は、請求は読み取り専用
                    SalesData.IsRequestReadOnly = true;
                }

                if (processingCategoryString == "赤伝黒伝登録請求" || processingCategoryString == "赤伝黒伝登録下払")
                {
                    //SalesData.IsUnderlingReadOnly = true;
                }

                // 売上データに処理カテゴリとフィルタの情報を設定
                SalesData.ProcessingCategory = processingCategoryString;
                SalesData.NumOfFilter = data.kubun == 3 ? 3 : processingCategory;
                SalesData.NumOfInq = kubun ?? 0;
                SalesData.Company_ID = loguinUser.Company_ID;
                SalesData.User_ID = loguinUser.User_ID;
                SalesData.BurdenList = await masterDataApi.GetBurdenList(loguinUser.Company_ID);

                // 各種負担料金の合計を計算
                decimal? NinushiFutanTotal = 0;
                decimal? KaishaFutanTotal = 0;
                decimal? KojinFutanTotal = 0;
                decimal? TatekaekinTotal = 0;

                if (SalesData.NippouToll != null)
                {
                    foreach (var toll in SalesData.NippouToll)
                    {
                        switch (toll.Futan_Kubun)
                        {
                            case 1:
                                // 個人負担
                                KojinFutanTotal += toll.料金;
                                break;
                            case 2:
                                // 会社負担
                                KaishaFutanTotal += toll.料金;
                                break;
                            case 3:
                                // 荷主負担
                                NinushiFutanTotal += toll.料金;
                                break;
                        }
                    }
                }

                if (SalesData.NippouTollOther != null)
                {
                    foreach (var toll in SalesData.NippouTollOther)
                    {
                        switch (toll.Futan_Kubun)
                        {
                            case 3:
                                // 荷主負担
                                // 負担が荷主負担の時に請求情報の立替金初期値に加算する。
                                TatekaekinTotal += toll.Toll_Fee;
                                break;
                        }
                    }
                }
                SalesData.KojinFutanTotal = KojinFutanTotal;
                SalesData.NinushiFutanTotal = NinushiFutanTotal;
                SalesData.KaishaFutanTotal = KaishaFutanTotal;
                // 負担が荷主負担の時に請求情報の立替金初期値に加算する。
                SalesData.TatekaekinTotal = TatekaekinTotal + NinushiFutanTotal;

                // 案件ポイントのAnken_Orderが最大の値のポイントのみ絞込み（他は履歴の為）
                if (SalesData.AnkenPoint != null && SalesData.AnkenPoint.Any())
				{
                    int Anken_Latest_Order = filtered.Anken_Latest_Order;
                    SalesData.AnkenPoint = SalesData.AnkenPoint.Where(p => p.Anken_Order == Anken_Latest_Order).ToList();
                }

                // 案件ポイントの日付を取得
                if (SalesData.AnkenPoint != null && SalesData.AnkenPoint.Any())
                {
                    // 開始
                    T_Anken_Point_Local start = SalesData.AnkenPoint.Where(p => p.Kubun == 1)
                                              .OrderBy(p => p.PointDate)
                                              .FirstOrDefault();

                    int HH = 0;
                    int mm = 0;
                    if (!string.IsNullOrWhiteSpace(start.PointTime))
					{
                        HH = int.Parse(start.PointTime.Substring(0, 2));
                        mm = int.Parse(start.PointTime.Substring(3, 2));
                    }

                    SalesData.StartDateTime = new DateTime(((DateTime)(start.PointDate)).Year,
                                                ((DateTime)(start.PointDate)).Month,
                                                ((DateTime)(start.PointDate)).Day,
                                                HH,
                                                mm,
                                                0);

                    // 終了
                    T_Anken_Point_Local end = SalesData.AnkenPoint.Where(p => p.Kubun == 9)
                                            .OrderBy(p => p.PointDate)
                                            .LastOrDefault();

                    HH = 0;
                    mm = 0;
                    if (!string.IsNullOrWhiteSpace(end.PointTime))
                    {
                        HH = int.Parse(end.PointTime.Substring(0, 2));
                        mm = int.Parse(end.PointTime.Substring(3, 2));
                    }


                    SalesData.EndDateTime = new DateTime(((DateTime)(end.PointDate)).Year,
                                                ((DateTime)(end.PointDate)).Month,
                                                ((DateTime)(end.PointDate)).Day,
                                                HH,
                                                mm,
                                                0);
                }

                // 各種選択リストを設定
                SalesData.SeikyuKubunDto = SalesData.SeikyuKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SalesData.KazeiKubunDto = SalesData.KazeiKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SalesData.UnitDto = SalesData.Unit.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SelectListItem blankOption = new SelectListItem
                {
                    Value = "",
                    Text = "" // You can customize this text as needed
                };

                SalesData.SalesKubunDto = new List<SelectListItem> { blankOption }
                    .Concat(SalesData.SalesKubun.Select(item => new SelectListItem
                    {
                        Value = item.Code_Data,
                        Text = item.Code_Name
                    }))
                    .ToList();

                SalesData.SalesBusinessSegmentDto = new List<SelectListItem> { blankOption }.Concat(SalesData.SalesBusinessSegment.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                })).ToList();

                // 個人運収-区分
                SalesData.KojinUnsyuKubunDto = SalesData.KojinUnsyuKubun.Select(item => new SelectListItem
                {
                    Value = item.KojinUnsyuKubun_ID.ToString(),
                    Text = item.Kubun_Name
                }).ToList();

                SalesData.UserGroupDto = SalesData.UserGroup.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SalesData.PaidUserDto = SalesData.PaidUser.Select(item => new SelectListItem
                {
                    Value = $"{item.User_ID}",
                    Text = item.Display_Name
                }).ToList();

                SalesData.AdvanceOverpaymentKubunDto = SalesData.AdvanceOverpaymentKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SalesData.OverpaymentKubunDto = SalesData.OverpaymentKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                SalesData.UriageFutanList = new()
                {
                    new()
                    {
                        // Anken_ID = param.Anken_ID,
                        Sort = 0,
                        futan_Kubun = 0,
                    }
                };

				#region 日報情報から生成される会社負担金額の算出
				// 通行料金日報(SalesData.NippouToll)から生成された会社負担
				// 他通行料金日報(Model.NippouTollOther)から生成された会社負担
				int futan_Kubun = 0;
                decimal KaishaFutanTotalTollFee = 0;

                // 通行料金日報(SalesData.NippouToll)から生成された会社負担があれば表示対象
                if (SalesData.KaishaFutanTotal > 0)
                {
                    KaishaFutanTotalTollFee += (decimal)SalesData.KaishaFutanTotal;
                }
                // 他通行料金日報(Model.NippouTollOther)から生成された会社負担があれば表示対象
                if (SalesData.NippouTollOther != null)
                {
                    foreach (var toll in SalesData.NippouTollOther)
                    {
                        switch (toll.Futan_Kubun)
                        {
                            case 2:
                                // 会社負担
                                KaishaFutanTotalTollFee += toll.Toll_Fee;
                                break;
                        }
                    }
                }
                if (KaishaFutanTotalTollFee > 0)
                {
                    // 会社負担のデータ無
                    if (SalesData?.UriageFutan?.Count() == 0)
                    {
                        SalesData.UriageFutanAdd = new List<T_Uriage_Futan_Local>
                        {
                            new()
                            {
                                futan_Kubun = futan_Kubun,
                                FutanPrice = KaishaFutanTotalTollFee,
                            }
                        };
                    }
                }
                SalesData.KaishaFutanTotalTollFee = KaishaFutanTotalTollFee;
                #endregion

                SalesData.SelectedUnitId = 0;

                if (SalesData.UriageUnsyu != null && SalesData.HaisyaData != null && SalesData?.HaisyaData?.Driver_ID > 0)
                    SalesData.UriageUnsyu = SalesData.UriageUnsyu.Where(x => x.Driver_ID == SalesData.HaisyaData.Driver_ID).ToList();

                #region 売上運賃、売上下払、売上運収にコミット情報を設定
                if (SalesData.CommitSeikyu?.Count > 0)
				{
                    foreach (T_Uriage_Unchin_Local uriageUnchin in SalesData.UriageUnchin)
                    {
                        var seikyuDetail = SalesData.SeikyuDetail.Where(w => (w.Uriage_Unchin_ID == uriageUnchin.Uriage_Unchin_ID)).FirstOrDefault();
                        if (seikyuDetail?.Seikyu_ID > 0)
                        {
                            var commitSeikyu = SalesData.CommitSeikyu.Where(w => (w.Seikyu_ID == seikyuDetail.Seikyu_ID)).FirstOrDefault();
                            if (commitSeikyu?.Seikyu_Commit_ID > 0)
                            {
                                uriageUnchin.IsCreditSlip = true;
                                uriageUnchin.Shime_Datetime = commitSeikyu.Shime_Datetime;
                            }
                        }
                    }
                }
                if (SalesData.CommitShitabarai?.Count > 0)
				{
                    foreach (T_Uriage_Shitabarai_Local uriageShitabarai in SalesData.UriageShitabarai)
                    {
                        var commitShitabarai = SalesData.CommitShitabarai.Where(w => (w.Customer_Branch_ID == uriageShitabarai.Yosya_Branch_ID) && (w.Shime_Datetime >= uriageShitabarai.Shiharai_Date)).FirstOrDefault();
                        if (commitShitabarai?.Shitabarai_Commit_ID > 0)
                        {
                            uriageShitabarai.IsCreditSlip = true;
                            uriageShitabarai.Shime_Datetime = commitShitabarai.Shime_Datetime;
                        }
                    }
                }
                if (SalesData.CommitUnsyu?.Count > 0)
				{
                    foreach (T_Uriage_Unsyu_Local uriageUnsyu in SalesData.UriageUnsyu)
                    {
                        var commitUnsyu = SalesData.CommitUnsyu.Where(w => (w.Uriage_Unsyu_ID == uriageUnsyu.Uriage_Unsyu_ID)).FirstOrDefault();
                        if (commitUnsyu?.Commit_Unsyu_ID > 0)
                        {
                            uriageUnsyu.IsCreditSlip = true;
                            uriageUnsyu.Shime_Datetime = commitUnsyu.Shime_Datetime;
                        }
                    }
                }
                #endregion

                SalesData.RowCount = 1;
                
                if(data.Search != null)
                {
                    SalesData.Search = data.Search;
                }
                else
                {
                    SalesData.Search = new SalesPaymentModel.SearchModelForSalesPaymentList()
                    {
                        SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
                        SelectEndDay = DateTime.Now.ToString("yyyy/MM/dd"),
                    };
                }

                SalesData.Search.BackMenuAction = "DataList";

                return View("SalesEntry", SalesData);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 指定された条件に基づいて請求情報の部分ビューを取得します。
        /// </summary>
        /// <param name="rowCount">行数</param>
        /// <param name="model">課税区分モデルのJSON文字列</param>
        /// <param name="unit">ユニットのJSON文字列</param>
        /// <param name="SelectedUnitId">選択されたユニットID</param>
        /// <param name="Uriage_Kubun">売上区分</param>
        /// <param name="isCreditSlip">クレジットスリップかどうかを示すフラグ</param>
        /// <returns>請求情報の部分ビュー</returns>
        public async Task<IActionResult> GetBillingInfoPartial(BillingInfoRequestDto request)
        {
            try
            {
                // 売上モデルのインスタンスを作成し、プロパティを設定
                Dto.SalesModel salesModel = new Dto.SalesModel
                {
                    RowCount = request.RowCount,
                    IsSeikyuCommitted = request.IsCreditSlip,
                    Uriage_Unchin_Kubun = request.UriageKubun,
                    ProcessingCategory = request.ProcessingCategory,
                    ShimeDay = request.ShimeDay
                };

                salesModel.IsDeletable = salesModel.RowCount > 1;
                // kazeiKubunをオブジェクトに変換
                List<CodeDataDto> kazeiKubunList = JsonConvert.DeserializeObject<List<CodeDataDto>>(request.Model);

                // JSON形式の単位データをデシリアライズしてリストに変換
                List<CodeDataDto> Unit = JsonConvert.DeserializeObject<List<CodeDataDto>>(request.Unit);
                if (!string.IsNullOrEmpty(request.CustomerUriageCalcData))
                    salesModel.CustomerUriageCalcData = JsonConvert.DeserializeObject<M_Customer_Uriage_Calc_Local>(request.CustomerUriageCalcData);

                // KazeiKubunDto の設定
                salesModel.KazeiKubunDto = kazeiKubunList.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // UnitDto を設定
                salesModel.UnitDto = Unit.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // 選択された単位IDを設定
                salesModel.SelectedUnitId = request.SelectedUnitId;
                // TatekaekinTotal を初期化
                salesModel.TatekaekinTotal = 0;
                salesModel.AnkenPoint = new()
                {
                    new() { Address3 = request.Tsumi },
                    new() { Address3 = request.Oroshi }
                };

                if (!string.IsNullOrEmpty(request.CustomerCode))
                    salesModel.CustomerSeikyusaki = "自社";

                if (request.ProcessingCategory == "直接入力")
                {
                    API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                    Dto.M_Customer_Branch_Local result = await api.GetCustomer(request.CustomerID);
                    if (result.Customer_ID > 0)
                    {
                        // 顧客選択情報
                        request.CustomerID = result.Customer_Branch_ID;
                        request.CustomerCode = result.Customer_Branch_Code;
                        request.CustomerName = result.Customer_Branch_Name;
                    }

                    if (result.Seikyu_Customer_Branch_ID != 0)
                    {
                        Dto.M_Customer_Branch_Local result2 = await api.GetCustomer(result.Seikyu_Customer_Branch_ID);
                        if (result2.Customer_ID > 0)
						{
                            // 顧客請求先情報[Seikyu_Customer_Branch_ID]
                            request.CustomerID = result2.Customer_Branch_ID;
                            request.CustomerCode = result2.Customer_Branch_Code;
                            request.CustomerName = result2.Customer_Branch_Name;
                        }
                    }
                }

                salesModel.HaisyaData = new V_HaisyaDataList_Local
                {
                    KokyakuId = request.CustomerID,
                    KokyakuCode = request.CustomerCode,
                    Customer_Name_Abbr = request.CustomerName,
                };

                if (request.IsFirstItem)
                {
                    salesModel.Uriage = new();
                }

                if (request.SeikyuDate != null)
                    salesModel.EndDateTime = request.SeikyuDate;
                return await PartialViewAsJson("BillingInfo", salesModel, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 指定された条件に基づいて支払情報の部分ビューを取得します。
        /// </summary>
        /// <param name="rowCount">行数</param>
        /// <param name="model">課税区分モデルのJSON文字列</param>
        /// <param name="unit">ユニットのJSON文字列</param>
        /// <param name="SelectedUnitId">選択されたユニットID</param>
        /// <returns>支払情報の部分ビュー</returns>
        public async Task<IActionResult> GetUnderPaymentPartial(UnderPaymentInfoRequestDto request)
        {
            try
            {
                Dto.SalesModel salesModel = new Dto.SalesModel
                {
                    RowCount = request.RowCount,
                    IsShitabaraiCommitted = request.IsCreditSlip,
                    Uriage_Unchin_Kubun = request.UriageKubun,
                    ProcessingCategory = request.ProcessingCategory,
                    ShimeDay = request.ShimeDay
                };

                salesModel.IsDeletable = salesModel.RowCount > 1;
                // kazeiKubunをオブジェクトに変換
                List<CodeDataDto> kazeiKubunList = JsonConvert.DeserializeObject<List<CodeDataDto>>(request.Model);

                List<CodeDataDto> Unit = JsonConvert.DeserializeObject<List<CodeDataDto>>(request.Unit);

                // KazeiKubunDto の設定
                salesModel.KazeiKubunDto = kazeiKubunList.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                salesModel.UnitDto = Unit.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();

                salesModel.AnkenPoint = new List<T_Anken_Point_Local> {
                    new T_Anken_Point_Local { Address3 = request.Tsumi },
                    new T_Anken_Point_Local { Address3 = request.Oroshi }};

                salesModel.SelectedUnitId = request.SelectedUnitId;

                if (request.ProcessingCategory == "直接入力")
                {
                    API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                    Dto.M_Customer_Branch_Local result = await api.GetCustomer(request.CustomerID);
                    if (result.Customer_ID > 0)
                    {
                        // 顧客選択情報
                        request.CustomerID = result.Customer_Branch_ID;
                        request.CustomerCode = result.Customer_Branch_Code;
                        request.CustomerName = result.Customer_Branch_Name;
                    }

                    if (result.Shiharai_Customer_Branch_ID != 0)
                    {
                        Dto.M_Customer_Branch_Local result2 = await api.GetCustomer(result.Shiharai_Customer_Branch_ID);
                        if (result2.Customer_ID > 0)
                        {
                            // 顧客下払先情報[Shiharai_Customer_Branch_ID]
                            request.CustomerID = result2.Customer_Branch_ID;
                            request.CustomerCode = result2.Customer_Branch_Code;
                            request.CustomerName = result2.Customer_Branch_Name;

                        }
                    }
                }

                salesModel.HaisyaData = new V_HaisyaDataList_Local
                {
                    KokyakuId = request.CustomerID,
                    KokyakuCode = request.CustomerCode,
                    Customer_Name_Abbr = request.CustomerName,
                    Haisya_Kubun = request.HaisyaKubun
                };

                if (request.ShiharaiDate != null && request.ShiharaiDate != DateTime.MinValue)
                {
                    salesModel.EndDateTime = request.ShiharaiDate;
                }

                if (!string.IsNullOrEmpty(request.Baggage))
                {
                    salesModel.BaggageGroupDto = new List<BaggageGroupDto_Local>()
                    {
                        JsonConvert.DeserializeObject<BaggageGroupDto_Local>(request.Baggage)
                    };
                }

                if (request.HaisyaKubun == 2)
                {
                    salesModel.ShimeDay = request.YosyaShiharaiShimeday ?? 0;
                    salesModel.Yosya = new M_Yosya_Branch_Local
                    {
                        Yosya_Branch_ID = request.YosyaBranchId ?? 0,
                        Customer_Branch_Code = request.YosyaBranchCode,
                        Customer_Branch_Name_Abbr = request.YosyaBranchNameAbbr
                    };
                }

                return await PartialViewAsJson("UnderPayment", salesModel, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 支払情報の閉じられた部分ビューを取得します。
        /// </summary>
        /// <returns>支払情報の閉じられた部分ビュー</returns>
        public async Task<IActionResult> GetUnderPaymentClosedPartial()
        {
            try
            {
                Dto.SalesModel salesModel = new Dto.SalesModel
                {
                };

                return await PartialViewAsJson("UnderPaymentClosed", salesModel, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 下払い赤伝黒伝登録時の下払情報の表示
        /// </summary>
        /// <param name=""></param>
        /// <returns>salesModel</returns>
        public async Task<IActionResult> GetUnderPaymentAkaKurodenPartial(int Uriage_Kubun, int rowCount)
        {
            try
            {
                Dto.SalesModel salesModel = new Dto.SalesModel
                {
                    Uriage_Shiharai_Kubun = Uriage_Kubun,
                    RowCount = rowCount
                };

                return await PartialViewAsJson("UnderPaymentAkaKuroden", salesModel, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 会社負担登録画面（モーダル）を開く
        /// </summary>
        /// <param name="SyaryoID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SelectBurdenModal(Dto.SalesModel param)
        {
            try
            {
                SelectBurdenDto model = new()
                {
                    AnkenLuggageList = new(),
                    BurdenGroupList = new(),
                    BurdenList = new(),
                    // CompanyID = param.Company_ID,
                    // UserID = param.UserID,
                    // AnkenID = param.Anken_ID,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                model.BurdenList = await masterDataApi.GetBurdenList(1);
                model.BurdenGroupList = await masterDataApi.GetBurdenGroupList(1);
                model.AnkenLuggageList = param.UriageFutanList;

                if (model.AnkenLuggageList == null)
                {
                    Dto.T_Uriage_Futan_Local luggage = new()
                    {
                        // Anken_ID = param.Anken_ID,
                        Sort = 0,
                        futan_Kubun = 0,
                    };
                    model.AnkenLuggageList = new();
                    model.AnkenLuggageList.Add(luggage);
                }
                return await PartialViewAsJson("SelectBurdenModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>result</returns>
        public async Task<IActionResult> GetCustomerSeikyusaki(int Customer_ID)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);

                Dto.M_Customer_Branch_Local result = await api.GetCustomer(Customer_ID);

                if (result.Seikyu_Customer_Branch_ID != 0)
                {
                    Dto.M_Customer_Branch_Local result2 = await api.GetCustomer(result.Seikyu_Customer_Branch_ID);

                    var res = new
                    {
                        result.Customer_Branch_ID,
                        result.Customer_ID,
                        result.SortOrder,
                        result.Oya_Branch_ID,
                        result.Customer_Branch_Code,
                        result.Customer_Branch_Name,
                        result.Customer_Branch_Name_Kana,
                        result.Customer_Branch_Name_Abbr,
                        result.Customer_Branch_Post,
                        result.Customer_Branch_Address1,
                        result.Customer_Branch_Address2,
                        result.Customer_Branch_Address3,
                        result.Customer_Branch_Phone1,
                        result.Customer_Branch_Phone2,
                        result.Customer_Branch_Fax1,
                        result.Customer_Branch_Fax2,
                        result.Mail_Title,
                        result.Mail_Address1,
                        result.Mail_Address2,
                        result.AnkenRemarks,
                        result.SeikyuRemarks,
                        result.SeikyuTantouID,
                        result.Seikyu_Kubun,
                        result.SeikyuDate_Kubun,
                        result.Toll_Kubun,
                        result.Shime_Day,
                        result.Seikyu_Address1,
                        result.Seikyu_Address2,
                        result.Seikuy_PostCode,
                        result.Seikyu_Customer_Branch_ID,
                        result.Shiharai_TantouID,
                        result.Shiharai_Shime_Day,
                        result.Shiharai_Remarks,
                        result.Shiharai_Customer_Branch_ID,
                        result.Del_Flg,
                        result.Insert_Datetime,
                        result.Insert_User,
                        result.Update_Datetime,
                        result.Update_User,
                        CustomerSeikyusaki = result2.Customer_Branch_Code + " " + result2.Customer_Branch_Name_Abbr
                    };

                    return Json(res);
                }
                else
                {
                    var res = new
                    {
                        result.Customer_Branch_ID,
                        result.Customer_ID,
                        result.SortOrder,
                        result.Oya_Branch_ID,
                        result.Customer_Branch_Code,
                        result.Customer_Branch_Name,
                        result.Customer_Branch_Name_Kana,
                        result.Customer_Branch_Name_Abbr,
                        result.Customer_Branch_Post,
                        result.Customer_Branch_Address1,
                        result.Customer_Branch_Address2,
                        result.Customer_Branch_Address3,
                        result.Customer_Branch_Phone1,
                        result.Customer_Branch_Phone2,
                        result.Customer_Branch_Fax1,
                        result.Customer_Branch_Fax2,
                        result.Mail_Title,
                        result.Mail_Address1,
                        result.Mail_Address2,
                        result.AnkenRemarks,
                        result.SeikyuRemarks,
                        result.SeikyuTantouID,
                        result.Seikyu_Kubun,
                        result.SeikyuDate_Kubun,
                        result.Toll_Kubun,
                        result.Shime_Day,
                        result.Seikyu_Address1,
                        result.Seikyu_Address2,
                        result.Seikuy_PostCode,
                        result.Seikyu_Customer_Branch_ID,
                        result.Shiharai_TantouID,
                        result.Shiharai_Shime_Day,
                        result.Shiharai_Remarks,
                        result.Shiharai_Customer_Branch_ID,
                        result.Del_Flg,
                        result.Insert_Datetime,
                        result.Insert_User,
                        result.Update_Datetime,
                        result.Update_User,
                        CustomerSeikyusaki = "自社",
                    };
                    return Json(res);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 請求先のShimeDatetimeを取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>result</returns>
        public async Task<IActionResult> GetCommitSeikyuShimeDatetime(int Customer_ID)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                DateTime? shimeDatetime = await api.GetCommitSeikyuShimeTime(Customer_ID);
                return Json(new
                {
                    shimeDatetime = shimeDatetime?.ToString("yyyy-MM-dd"),
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 下払先のShimeDatetimeを取得
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>result</returns>
        public async Task<IActionResult> GetCommitShitabaraiShimeDatetime(int Customer_ID)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                DateTime? shimeDatetime = await api.GetCommitShitabaraiShimeTime(Customer_ID);
                return Json(new
                {
                    shimeDatetime = shimeDatetime?.ToString("yyyy-MM-dd"),
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 荷物情報マスタへの追加登録
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonAddBurdenMaster(int paramGroupID, string paramLuggageText, string paramUnit)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // パラメータのバリデーション
                if (paramGroupID == 0) { throw new Exception("パラメーターエラー：GroupIDが正しくありません"); }
                if (paramLuggageText == null || paramLuggageText.Length == 0) { throw new Exception("パラメーターエラー：LuggageTextが正しくありません"); }
                // 荷物データを作成
                Dto.M_Burden_Local luggage = new()
                {
                    Burden_Group_ID = paramGroupID,
                    Company_ID = loguinUser.Company_ID,
                    Burden_Name = paramLuggageText,
                    Unit_Name = paramUnit,
                    SortOrder = 0,
                };
                // マスターデータAPIのインスタンスを作成
                MasterDataApi masterDataApi = new(_mapApiSettiong);
                // 荷物データをデータベースに挿入または更新
                MsterDataCommonResultValDto_Local result = await masterDataApi.InsertUpdateBurdenMasterData(luggage);
                // 更新後の荷物リストを取得
                List<Dto.M_Burden_Local> luggageList = await masterDataApi.GetBurdenList(loguinUser.Company_ID);
                // 登録した荷物データをリストから検索
                M_Burden_Local target = luggageList.FirstOrDefault(m => m.Burden_Group_ID == paramGroupID && m.Burden_Name == paramLuggageText)
                    ?? throw new Exception("データ登録エラー：一度画面を閉じて再度登録し直してください。");
                // 成功した場合、荷物IDを返却
                return Json(new { resultVal = target.Burden_ID.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { resultVal = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 配車データリストを取得します。
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="iCcompanyID"></param>
        /// <param name="iCustomerID"></param>
        /// <param name="iBranchID"></param>
        /// <returns>配車データのリストを非同期に返します。</returns>
        private async Task<IEnumerable<Dto.V_HaisyaDataList_Local>> GetHaisyaDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID)
        {
            using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
            return await api.GetHaisyaDataList(iCcompanyID, targetDate, targetDateFrom, targetDateTo, iCustomerID);
        }

        /// <summary>
        /// 売上情報の登録（月額専属）
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostUriageData([FromBody] PostUriageDataModel combinedData)
        {
            try
            {
                using SalesDataApi api = new(_mapApiSettiong);
                var result = await api.PostUriageData(combinedData);

                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// 売上情報の登録
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostSales([FromBody] Dto.SalesModel combinedData)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostSales(combinedData);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 確定売上情報の登録（月額専属）
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostKakuteiUriageData(List<int> uriageIds)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostKakuteiUriageData(uriageIds);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// 売上情報の削除
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteSales([FromBody] Dto.SalesModel combinedData)
        {
            try
            {
                using API.WebApp.SalesDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.DeleteSales(combinedData);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }
    }
}
