using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
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
using static HaisyaWeb.Models.DailyReportModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 受領書処理一覧コントローラー
    /// </summary>
    public class ReceiptController : BaseController
    {
        private readonly ILogger<ReceiptController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public ReceiptController(ILogger<ReceiptController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 受領書処理一覧
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.Search.SelectFilter = 1;
                return View(model);
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
                    model.Search.SelectEndDay = param.SelectEndDay;
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
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            SearchModelForDailyReportList param = new()
            {
                SelectGroup = 0,
                SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
                SelectEndDay = DateTime.Now.ToString("yyyy/MM/dd"),
                SelectTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Haisya, loguinUser.User_ID) ?? "ALL",
                SelectSeikyuTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Seikyu, loguinUser.User_ID) ?? "ALL",
            };

            SearchModelForDailyReportList search = new();

            DataListModel model = new()
            {
                Company_ID = loguinUser.Company_ID,
            };

            search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, false),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SelectTantou = param.SelectTantou,
                SelectTab = param.SelectTab,
                SelectDay = param.SelectDay,
                SelectEndDay = param.SelectEndDay,
                SelectGroup = param.SelectGroup,
                SelectSeikyuTantouList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu, false),
                SelectSeikyuTantou = param.SelectSeikyuTantou,
            };

            model.Search = search;
            return model;
        }

        /// <summary>
        /// JSON形式で案件一覧データのHTMLを返却するメソッド
        /// </summary>
        /// <param name="param">検索条件を含むSearchModelForDailyReportListオブジェクト</param>
        /// <returns>JSON形式のアクション結果</returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForDailyReportList param, DataListSortModel sortParam)
        {
            // パラメータがnullの場合はnullを返却
            if (param == null) { return null; }

            sortParam.SortOrder ??= "asc";
            sortParam.SortItemParam ??= nameof(HaisyaDataList.Anken_ID);
            DataListModel model = new()
            {
                SortParam = sortParam,
            };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            try
            {
                int customerID = 0;
                string html = "DataListForNone";
                if (param.SelectTokuisakiID != null)
                {
                    Dto.M_Customer_Branch_Local CustomerBranch = await api.GetCustomerBranchData(int.Parse(param.SelectTokuisakiID));
                    customerID = CustomerBranch.Customer_ID;
                }
                // 対象日付をパースしてフォーマットを整える
                DateTime date1 = DateTime.Parse(param.SelectDay);
                DateTime? date2 = null;
                // param.SelectEndDayの値を確認
                if (DateTime.TryParse(param.SelectEndDay, out DateTime parsedFromDate))
                {
                    date2 = DateTime.Parse(param.SelectEndDay);
                }

                IEnumerable<Dto.V_HaisyaDataList_Local> list = null;

                list = await GetHaisyaDataList(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, customerID, 0);

                if (date2 != null)
                {
                    list = await GetHaisyaDataList(null, DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), DateTime.Parse(param.SelectEndDay).ToString("yyyy/MM/dd"), loguinUser.Company_ID, customerID, 0);
                }

                List<HaisyaDataList> listData = new();
                foreach (var data in list)
                {
                    listData.Add(new HaisyaDataList(data));
                }
                List<int> ankenIds = listData.Select(n => n.Anken_ID).ToList();

                // AnkenIDのリストからAnkenDisplayデータリストを取得
                List<Dto.T_Anken_Display_Local> ankenDisplays = await GetAnkenDisplayAsync(ankenIds);

                List<Dto.T_Nippou_Local> nippous = new List<Dto.T_Nippou_Local>();

                // 案件IDごとに Nippou オブジェクトを作成
                foreach (var ankenid in ankenIds)
                {
                    Dto.T_Nippou_Local nippou = new Dto.T_Nippou_Local
                    {
                        // Nippou_ID = 2,
                        AnkenDisplay_ID = ankenDisplays.Where(m => m.Anken_ID == ankenid).Select(s => s.AnkenDisplay_ID).FirstOrDefault(),
                        Anken_ID = ankenid,
                        // Receipt = 2,
                        // Commnet = "AnotherComment",
                        // ApprovalStatus = 2,
                        // RenkeiStatus = 1,
                        // Insert_Datetime = DateTime.Parse("2024-05-23T00:00:00"),
                        // Insert_User = 2,
                        // Update_Datetime = DateTime.Parse("2024-05-23T00:00:00"),
                        // Update_User = 3
                    };
                    nippous.Add(nippou);
                }
                // Nippou データを取得
                List<Dto.T_Nippou_Local> nippouList = await SearchTNippouAsync(nippous);

                // listData の各データに対して Nippou データを設定
                foreach (var data in listData)
                {
                    foreach (var nippou in nippouList)
                    {
                        if (data.Anken_ID == nippou.Anken_ID)
                        {
                            data.TNippou = nippou;
                            if (nippou.Receipt > 0)
                            {
                                data.SetReciptFlg = true;
                            }
                        }
                    }
                }

                if (!"ALL".Equals(param.SelectTantou) && param.SelectTantou != null)
                {
                    listData = listData.Where(m => m.TantouID == int.Parse(param.SelectTantou)).ToList();
                }

                if (!"ALL".Equals(param.SelectSeikyuTantou) && param.SelectSeikyuTantou != null)
                {
                    listData = listData.Where(m => m.TantouID == int.Parse(param.SelectSeikyuTantou)).ToList();
                }

                if (param.SelectSyasyu != null)
                {
                    listData = listData.Where(m => m.SyasyuDisplay.Contains(param.SelectSyasyu)).ToList();
                }

                if (param.SelectKata != null)
                {
                    listData = listData.Where(m => m.Kata.Contains(param.SelectKata)).ToList();
                }

                if (param.SelectFilter == 1)
                {
                    listData = listData.Where(m => !m.SetReciptFlg).ToList();
                }

                if (param.SelectFilter == 2)
                {
                    listData = listData.Where(m => m.SetReciptFlg).ToList();
                }

                model.DataDataLists = listData;

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
        /// JSON：案件一覧データの返却
        /// </summary>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="CcompanyID"></param>
        /// <param name="branchID"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_HaisyaDataList_Local>> GetHaisyaDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID)
        {
            using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
            return await api.GetHaisyaDataList(iCcompanyID, targetDate, targetDateFrom, targetDateTo, iCustomerID);
        }

        /// <summary>
        /// AnkenIDリストに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="Anken_IDs">Anken_IDのリスト</param>
        /// <returns>T_Anken_Displayのオブジェクトのリスト</returns>
        private async Task<List<Dto.T_Anken_Display_Local>> GetAnkenDisplayAsync(List<int> Anken_IDs)
        {
            using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
            return await api.GetAnkenDisplayAsync(Anken_IDs);
        }

        /// <summary>
        /// T_Nippouテーブルにコメントを挿入します。
        /// ログインユーザーのIDを使用して挿入ユーザーを設定します。
        /// </summary>
        /// <param name="t_Nippou">挿入するコメントが含まれるT_Nippouオブジェクト。</param>
        /// <returns>非同期操作を表すTask。</returns>
        [HttpPost]
        public async Task<IActionResult> InsertCommentAsync2([FromBody] Dto.T_Nippou_Local t_Nippou)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                t_Nippou.Insert_User = loguinUser.User_ID;
                using API.WebApp.NippouDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertCommentAsync(t_Nippou);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// T_Nippouテーブルに領収書情報を挿入します。
        /// ログインユーザーのIDを使用して挿入ユーザーを設定します。
        /// </summary>
        /// <param name="t_Nippou">挿入する領収書情報が含まれるT_Nippouオブジェクト。</param>
        /// <returns>非同期操作を表すTask。</returns>
        [HttpPost]
        public async Task<IActionResult> InsertReceiptAsync2([FromBody] Dto.T_Nippou_Local t_Nippou)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                t_Nippou.Insert_User = loguinUser.User_ID;

                // ankenDisplay_IDがなければT_Nippouに初期値設定
                if (t_Nippou.Receipt == 1)
                {
                    t_Nippou.Receipt_Date = DateTime.Today;
                    t_Nippou.Commnet = null;
                    t_Nippou.ApprovalStatus = 0;
                    t_Nippou.RenkeiStatus = 0;
                }
                else
                {
                    //null にできないので仮
                    t_Nippou.Receipt_Date = new DateTime(1900, 1, 1);
                }

                using API.WebApp.NippouDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertReceiptAsync(t_Nippou);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// T_Nippouテーブルに基づいて検索を行い、結果を返します。
        /// </summary>
        /// <param name="t_Nippous">検索条件が含まれるT_Nippouオブジェクトのリスト。</param>
        /// <returns>検索結果が含まれるT_Nippouオブジェクトのリストを返します。</returns>
        [HttpPost]
        public async Task<List<Dto.T_Nippou_Local>> SearchTNippouAsync2([FromBody] List<Dto.T_Nippou_Local> t_Nippous)
        {
            try
            {
                using API.WebApp.NippouDataApi api = new(_mapApiSettiong);
                return await api.SearchTNippouAsync(t_Nippous);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
        }

        /// <summary>
        /// T_Nippouテーブルに基づいて検索を行い、結果を返します。
        /// </summary>
        /// <param name="t_Nippous">検索条件が含まれるT_Nippouオブジェクトのリスト。</param>
        /// <returns>検索結果が含まれるT_Nippouオブジェクトのリストを返します。</returns>
        private async Task<List<Dto.T_Nippou_Local>> SearchTNippouAsync(List<Dto.T_Nippou_Local> t_Nippous)
        {
            using API.WebApp.NippouDataApi api = new(_mapApiSettiong);
            return await api.SearchTNippouAsync(t_Nippous);
        }
    }
}