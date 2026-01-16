using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.SalesPaymentModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 売上・下払一覧コントローラー
    /// </summary>
    public class SalesPaymentController : BaseController
    {
        private readonly ILogger<SalesPaymentController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public SalesPaymentController(ILogger<SalesPaymentController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 日報連携結果処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(DataListModel s)
        {
            try
            {
                DataListModel model = await CreateModel(s?.Search);
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
        /// InputIndexの処理
        /// </summary>
        /// <returns></returns> 
        public async Task<IActionResult> InputIndex(int num_of_inq, int num_of_filter)
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.SelectFilter = num_of_filter;
                model.Search.SelectGroup = num_of_inq;
                // model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;
                return View("InputIndex", model);
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
        /// DailyReportRegistrationIndexの処理
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<IActionResult> DailyReportRegistrationIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;
                return View("DailyReportRegistration", model);
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
        /// `DataListModel` を作成し、初期化したデータを含むモデルを返却します。
        /// </summary>
        /// <returns>初期化された `DataListModel` インスタンス</returns>
        private async Task<DataListModel> CreateModel(SearchModelForSalesPaymentList param = null)
        {

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            // デフォルトの検索パラメーターを設定
            param ??= new();
            param.SelectDay ??= DateOnly.FromDateTime(DateTime.Now);
            param.SelectEndDay ??= DateOnly.FromDateTime(DateTime.Now);
            param.SelectTantou ??= await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Haisya, loguinUser.User_ID) ?? "ALL";
            param.SelectStatus ??= new int[] { 0, 1, 2, 3, };
            param.SelectSeikyuTantou ??= await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Seikyu, loguinUser.User_ID) ?? "ALL";

            DataListModel model = new()
            {
                Company_ID = loguinUser.Company_ID
            };

            SearchModelForSalesPaymentList search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, false),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SelectSeikyuTantouList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu, false),

                RangeSearch = param.RangeSearch,
                SelectDay = param.SelectDay,
                SelectEndDay = param.SelectEndDay,
                ExpandSearch = param.ExpandSearch,
                SelectFilter = param.SelectFilter,
                SelectGroup = param.SelectGroup,
                SelectTokuisakiID = param.SelectTokuisakiID,
                SelectTokuisakiName = param.SelectTokuisakiName,
                SelectTantou = param.SelectTantou,
                SelectSeikyuTantou = param.SelectSeikyuTantou,
                JyoumuinID = param.JyoumuinID,
                JyoumuinName = param.JyoumuinName,
                SelectStatus = param.SelectStatus,
            };

            model.Search = search;
            return model;
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForSalesPaymentList param, DataListSortModel sortParam)
        {
            if (param == null) { return null; }
            DataListModel model = new()
            {
                SortParam = sortParam
            };
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 'SelectFilter' に基づいて 'uriageKubun' を設定
                // 'SelectFilter' に 1 を加えた値を 'uriageKubun' に設定
                int uriageKubun = param.SelectFilter + 1;

                // 'SelectGroup' に基づいて 'tourokuKubun' を設定
                // 'SelectGroup' に 1 を加えた値を 'tourokuKubun' に設定
                int tourokuKubun = param.SelectGroup + 1;

                model.UriageKubun = uriageKubun;

                // 'JyoumuinID' が空でない場合、'driverId' にその値を整数として設定
                int driverId = 0;

                if (!String.IsNullOrEmpty(param.JyoumuinID))
                {
                    driverId = int.Parse(param.JyoumuinID);
                }

                int haisyaTantou = 0;
                if (!String.IsNullOrEmpty(param.SelectTantou) && param.SelectTantou != "ALL")
                {
                    haisyaTantou = int.Parse(param.SelectTantou);
                }

                int seikyuTantou = 0;
                if (!String.IsNullOrEmpty(param.SelectSeikyuTantou) && param.SelectSeikyuTantou != "ALL")
                {
                    seikyuTantou = int.Parse(param.SelectSeikyuTantou);
                }

                DateOnly? date2 = null;
                // param.SelectEndDayの値を確認
                if (param.SelectEndDay != null) { date2 = param.SelectEndDay; }

                IEnumerable<Dto.V_UriageDataList> list = null;

                string tokuisakiID = "";

                if (!string.IsNullOrEmpty(param.SelectTokuisakiID))
                {
                    tokuisakiID = param.SelectTokuisakiID;
                }

                list = await GetUriageDataList(loguinUser.Company_ID, uriageKubun, tourokuKubun, param.SelectDay?.ToString("yyyy/MM/dd"), param.SelectEndDay?.ToString("yyyy/MM/dd"), tokuisakiID, tokuisakiID, "", "", 0, "1900/01/01", seikyuTantou, haisyaTantou, driverId);

                List<Dto.T_Nippou_Local> tNippous = await GetTNippous();

                List<HaisyaDataList> listData = new();

                if (list != null)
                {
                    // 各データ項目に基づいて 'HaisyaDataList' オブジェクトを作成し、'listData' に追加
                    foreach (var data in list)
                    {
                        HaisyaDataList haisyaDataList = new HaisyaDataList(data);
                        // 'tNippous' リストから、'Anken_ID' が一致するデータを探し、関連付け
                        foreach (var nippou in tNippous)
                        {
                            if (nippou.Anken_ID == data.Anken_ID)
                            {
                                haisyaDataList.TNippou = nippou;
                            }
                        }
                        // 'listData' に作成した 'HaisyaDataList' オブジェクトを追加
                        listData.Add(haisyaDataList);
                    }

                    if (param.SelectTantou != null && !"ALL".Equals(param.SelectTantou))
                    {
                        // listData = listData.Where(m => m.HasiyaTantouID == int.Parse(param.SelectTantou)).ToList();
                    }

                    if (param.SelectSeikyuTantou != null && !"ALL".Equals(param.SelectSeikyuTantou))
                    {
                        // listData = listData.Where(m => m.KokyakuTantouId == int.Parse(param.SelectSeikyuTantou)).ToList();
                    }

                    if (param.SelectSyasyu != null)
                    {
                        // listData = listData.Where(m => m.SyasyuDisplay.Contains(param.SelectSyasyu)).ToList();
                    }

                    if (param.SelectKata != null)
                    {
                        // listData = listData.Where(m => m.Kata.Contains(param.SelectKata)).ToList();
                    }

                    if (param.SelectStatus == null)
                    {
                        param.SelectStatus = new int[] { };
                    }

                    switch (uriageKubun)
                    {
                        case 2:
                            // 売上区分：専属月額
                            break;
                        case 3: 
                            // 売上区分：直接

                            // 売上リストを取得する
                            List<int> uriageIds = list.Where(r => r.Uriage_ID != null).Select(r => r.Uriage_ID ?? 0).ToList();
                            IEnumerable<T_Uriage_Local> uriages = await GetUriageByIds(uriageIds);
                            
                            Dictionary<int, T_Uriage_Local> uriageDict = new Dictionary<int, T_Uriage_Local>();
                            foreach(var uriage in uriages)
                            {
                                uriageDict.Add(uriage.Uriage_ID, uriage);
                            }

                            // [暫定]が外れた
                            if (!param.SelectStatus.Contains(1))
                            {
                                listData = listData.Where(r => uriageDict.TryGetValue(r.Uriage_ID ?? 0, out T_Uriage_Local uriage) && uriage.Reg_Status != 1).ToList();
                            }
                            // [確定]が外れた
                            if (!param.SelectStatus.Contains(2))
                            {
                                listData = listData.Where(r => uriageDict.TryGetValue(r.Uriage_ID ?? 0, out T_Uriage_Local uriage) && uriage.Reg_Status != 2).ToList();
                            }
                            // [仮]が外れた
                            if (!param.SelectStatus.Contains(3))
                            {
                                listData = listData.Where(r => uriageDict.TryGetValue(r.Uriage_ID ?? 0, out T_Uriage_Local uriage) && uriage.Reg_Status != 3).ToList();
                            }
                            // [未]が外れた
                            if (!param.SelectStatus.Contains(0))
                            {
                                listData = listData.Where(r => uriageDict.TryGetValue(r.Uriage_ID ?? 0, out T_Uriage_Local uriage) && uriage.Reg_Status != 0).ToList();
                            }
                            break;
                        default:
                            // 売上区分：案件
                            List<int?> allRegStatus = new List<int?>() { 0, 1, 2, 3, null };
                            
                            // ＜絞込み条件＞
                            if (tourokuKubun == 1)
                            {
                                List<HaisyaDataList> listFilterAnken = listData.Where(r => allRegStatus.Contains(r.Reg_Status)).ToList();

                                // [暫定]が外れた
                                if (!param.SelectStatus.Contains(1))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status != 1).ToList();
                                }
                                // [確定]が外れた
                                if (!param.SelectStatus.Contains(2))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status != 2).ToList();
                                }
                                // [仮]が外れた
                                if (!param.SelectStatus.Contains(3))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status != 3).ToList();
                                }
                                // [未]が外れた
                                if (!param.SelectStatus.Contains(0))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status != null).ToList();
                                }
                                listData = listFilterAnken;
                            }

                            // 登録区分：下払
                            if (tourokuKubun == 2)
                            {                          
                                List<HaisyaDataList> listFilterAnken = listData.Where(r => allRegStatus.Contains(r.Reg_Status_Shitabarai)).ToList();

                                // [暫定]が外れた
                                if (!param.SelectStatus.Contains(1))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status_Shitabarai != 1).ToList();
                                }
                                // [確定]が外れた
                                if (!param.SelectStatus.Contains(2))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status_Shitabarai != 2).ToList();
                                }
                                // [仮]が外れた
                                if (!param.SelectStatus.Contains(3))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status_Shitabarai != 3).ToList();
                                }
                                // [未]が外れた
                                if (!param.SelectStatus.Contains(0))
                                {
                                    listFilterAnken = listFilterAnken.Where(r => r.Reg_Status_Shitabarai != null).ToList();
                                }
                                listData = listFilterAnken;

                                // Haisya_Kubun(1:自車/2:傭車/3:専属傭車/4:自車専属/5:自車専任)
                                listData = listData.Where(r => r.Haisya_Kubun == 2 || r.Haisya_Kubun == 3).ToList();
                            }
                            break;
                    }
                }

                model.DataDataLists = listData;

                return await PartialViewAsJson("DataListForNone", model, true);
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
        private async Task<IEnumerable<Dto.V_UriageDataList>> GetUriageDataList(int companyId, int uriageKubun, int tourokuKubun, string fromDate, string toDate, string fromTokuisaki, string toTokuisaki, string fromYosya, string toYosya, int shimeDay, string seikyudateTo, int seikyuTantou, int haisyaTantou, int driverId)
        {
            using API.WebApp.UriageDataApi api2 = new(_mapApiSettiong);
            return await api2.GetUriageDataList(companyId, uriageKubun, tourokuKubun, fromDate, toDate, fromTokuisaki, toTokuisaki, fromYosya, toYosya, shimeDay, seikyudateTo, seikyuTantou, haisyaTantou, driverId);
        }

        /// <summary>
        /// 売上リストを取得する
        /// </summary>
        /// <param name="ids">売上のidリスト</param>
        /// <returns>売上リスト</returns>
        private async Task<IEnumerable<Dto.T_Uriage_Local>> GetUriageByIds(List<int> ids)
        {
            using API.WebApp.UriageDataApi api2 = new(_mapApiSettiong);
            return await api2.GetUriageByIds(ids);
        }

        private async Task<List<Dto.T_Nippou_Local>> GetTNippous()
        {
            using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
            return await api.GetTNippous();
        }

        /// <summary>
        /// 乗務員データを取得し、指定されたページ名の部分ビューをJSON形式で返します。
        /// </summary>
        /// <param name="driverId">乗務員ID。</param>
        /// <param name="pageName">返される部分ビューの名前。デフォルトは "CustomerDataList"。</param>
        /// <returns>部分ビューを含むJSON形式の結果を非同期に返します。エラーが発生した場合、エラーメッセージを含むJSONを返します。</returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListJyoumuin(string driverId, string pageName = "CustomerDataList")
        {
            try
            {
                int driverid = int.Parse(driverId);
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.DriverModel.DriverMenuDto model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    // CustomerList = new(),
                };

                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                Dto.M_CompanyDriver_Local driverData = await apiM.GetCompanyDriverData(driverid);
                model.CompanyDriverList = new List<Dto.M_CompanyDriver_Local>
                {
                    driverData
                };

                return await PartialViewAsJson(pageName, model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 全ての乗務員データリストを取得し、指定されたページ名の部分ビューをJSON形式で返します。
        /// </summary>
        /// <param name="pageName">返される部分ビューの名前。デフォルトは "CustomerDataList"。</param>
        /// <returns>部分ビューを含むJSON形式の結果を非同期に返します。エラーが発生した場合、エラーメッセージを含むJSONを返します。</returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListJyoumuinFull(string pageName = "CustomerDataList")
        {
            try
            {                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DriverModel.DriverMenuDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    // CustomerList = new(),
                };

                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                // model.CompanyDriverList = await apiM.GetCompanyDriverList(CompanyID);
                List<Dto.M_CompanyDriver_Local> driverData = await apiM.GetCompanyDriverList(loguinUser.Company_ID);
                model.CompanyDriverList = driverData;

                return await PartialViewAsJson(pageName, model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }
    }
}
