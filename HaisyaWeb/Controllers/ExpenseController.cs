using HaisyaWeb.API.WebApp;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.ExpenseModel;

namespace HaisyaWeb.Controllers
{
    public class ExpenseController : BaseController
    {
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(
            ILogger<ExpenseController> logger,
            SignInManager<ApplicationUser> signInManager,
            IOptions<MapApiSettings> mapApiSetting,
            IViewRenderService viewRenderService) => (_logger, _signInManager, _mapApiSettiong, _viewRenderService) = (logger, signInManager, mapApiSetting.Value, viewRenderService);

        /// <summary>
        /// 経費データのインデックスページを表示します。
        /// </summary>
        /// <returns>インデックスページのビュー</returns>
        public async Task<IActionResult> Index(DataListModel m)
        {
            try
            {
                return View(await CreateModel(m?.Search));
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 経費データの検索結果を表示します。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(DataListModel m, DataListSortModel sortParam)
        {
            m.SortParam = sortParam;
            try
            {
                return await PartialViewAsJson("_DataList", await SearchData(m));
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// モデルを作成します。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel(SearchModelForExpenseList s)
        {
            s ??= new();
            if (s.CompanyID == 0)
            {
                s.CompanyID = (await GetLoginUser()).Company_ID;
            }
            s.YearSelectList ??= SearchCommonService.GetYearSelectListV2(10);
            return new()
            {
                Search = s,
            };
        }

        /// <summary>
        /// 経費データを検索します。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private async Task<DataListModel> SearchData(DataListModel m)
        {
            // 検索パラメータを取得
            SearchModelForExpenseList s = m.Search;
            // 会社IDが0の場合、検索せずそのままモデルを返す
            if (s.CompanyID == 0)
                return m;
            // APIクラスのインスタンスを生成し、経費データを取得するために使用
            using ExpenseDataApi api = new(_mapApiSettiong);
            // API呼び出しによって経費データを非同期で取得し、結果をモデルに設定
            m.ExpenseDataLists = await api.GetExpenseDataList(
                s.CompanyID,
                s.Expense_category,
                string.IsNullOrWhiteSpace(s.Vehicle_number) ? null : Convert.ToInt32(s.Vehicle_number), // 車両番号が空でない場合に数値に変換して渡す
                string.IsNullOrWhiteSpace(s.Driver_code) ? null : s.Driver_code, // 運転手コードが空でない場合に渡す
                string.IsNullOrWhiteSpace(s.Driver_name) ? null : s.Driver_name, // 運転手名が空でない場合に渡す
                s.Accident_year);

            return m;
        }
    }
}
