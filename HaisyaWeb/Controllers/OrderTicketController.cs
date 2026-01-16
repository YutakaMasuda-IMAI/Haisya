using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.OrderTicketModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 受注札選択一覧コントローラー
    /// </summary>
    public class OrderTicketController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public OrderTicketController(ILogger<OrderTicketController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 日報連携結果処理
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
            // 初期値としてnullのSearchModelForDailyReportListを作成
            // paramがnullの場合、新しいSearchModelForDailyReportListを初期化
            SearchModelForDailyReportList param = new()
            {
                // グループ選択を0に設定（デフォルトの設定）
                SelectGroup = 0,
                // 日付選択を現在の日付に設定（フォーマット: yyyy/MM/dd）
                SelectDay = DateOnly.Parse(DateTime.Now.ToString("yyyy/MM/dd")),
            };
            // 担当者がnullの場合、デフォルトで"ALL"を設定
            param.SelectTantou ??= "ALL";

            DataListModel model = new() { };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            // SearchModelForDailyReportListのプロパティを設定
            SearchModelForDailyReportList search = new()
            {
                // 既存の検索条件を設定
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SelectTantou = param.SelectTantou,
                SelectTab = param.SelectTab,
                SelectDay = param.SelectDay,
                SelectGroup = param.SelectGroup,
                // 請求担当者リストを取得
                SelectSeikyuTantouList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu),
            };
            // 作成したSearchModelForDailyReportListをDataListModelのSearchプロパティに設定
            model.Search = search;
            model.Company_ID = loguinUser.Company_ID;
            return model;
        }

        /// <summary>
        /// JSON形式で案件一覧データのHTMLを返却するメソッド
        /// </summary>
        /// <param name="param">検索条件を含むSearchModelForDailyReportListオブジェクト</param>
        /// <param name="sortParam"></param>
        /// <returns>案件一覧データのHTMLを含むIActionResult</returns>
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

            try
            {
                // JSON文字列から選択された請求担当者のIDリストをデシリアライズ
                List<string> selectedValues = JsonConvert.DeserializeObject<List<string>>(param.SelectSeikyuTantou);

                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 指定された日付に基づいて案件データのリストを取得
                IEnumerable<Dto.V_HaisyaDataList_Local> list = await GetHaisyaDataList(param.SelectDay?.ToString("yyyy/MM/dd"), null, null, loguinUser.Company_ID, 0, 0);

                List<HaisyaDataList> listData = new();

                if (list != null)
                {
                    // データリストからHaisyaDataListのインスタンスを作成し追加
                    foreach (var data in list)
                    {
                        listData.Add(new HaisyaDataList(data));
                    }
                    // 選択された請求担当者のIDが指定されている場合、リストをフィルタリング 
                    if (selectedValues != null && selectedValues.Count > 0 && !selectedValues.Contains("ALL"))
                    {
                        List<int> selectedIds = selectedValues.Select(int.Parse).ToList();
                        listData = listData.Where(m => selectedIds.Contains(m.KokyakuTantouId)).ToList();
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
        /// <param name="iCcompanyID"></param>
        /// <param name="iCustomerID"></param>
        /// <param name="iBranchID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Dto.V_HaisyaDataList_Local>> GetHaisyaDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iCustomerID, int iBranchID)
        {
            try
            {
                using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                return await api.GetHaisyaDataList(iCcompanyID, targetDate, null, null, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}