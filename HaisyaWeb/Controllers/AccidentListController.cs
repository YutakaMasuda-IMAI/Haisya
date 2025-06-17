using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.AccidentListModel;

namespace HaisyaWeb.Controllers
{
    public class AccidentListController : BaseController
    {
        private readonly ILogger<AccidentListController> _logger;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public AccidentListController(ILogger<AccidentListController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 変更承認処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(SearchModelForAccidentList param)
        {
            try
            {
                DataListModel model = await CreateModel(param);

                return View("../AccidentList/Index", model);
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

            SearchModelForAccidentList search = new();

            // M_Code：18に該当するデータを表示するようにお願いします
            List<M_Code_Data_Local> accidentTypes = await GetMCodes(18);

            search.AccidentTypes = accidentTypes;

            DataListModel model = new()
            {
                Search = search
            };
            return model;
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel(SearchModelForAccidentList param)
        {
            SearchModelForAccidentList search = new()
            {
                AccidentReason = param.AccidentReason,
                AccidentName = param.AccidentName,
                DriverName = param.DriverName,
                CarNumber = param.CarNumber,
                FromDate = string.IsNullOrEmpty(param.FromDate) ? DateTime.Now.ToString("yyyy/MM/dd") : param.FromDate,
                ToDate = string.IsNullOrEmpty(param.ToDate) ? DateTime.Now.ToString("yyyy/MM/dd") : param.ToDate,
                WfRadioButtons = param.WfRadioButtons,
                IsBack =param.IsBack
            };

            // M_Code：18に該当するデータを表示
            List<M_Code_Data_Local> accidentTypes = await GetMCodes(18);

            search.AccidentTypes = accidentTypes;

            DataListModel model = new()
            {
                Search = search
            };
            return model;
        }

        /// <summary>
        /// リストのDataListModelの作成＆返却
        /// </summary>
        /// <returns></returns>
        private async Task<DataListModel> CreateListModel(SearchModelForAccidentList param)
        {
            // DataListModelのインスタンスを初期化
            DataListModel model = new() { };

            // 検索パラメータがnullの場合は処理を終了してnullを返す
            if (param == null) { return null; }
            // デフォルト値として使用するDateTimeを定義
            DateTime defaultValue = default; // Default value if parsing fails

            // param.FromDateの値をDateTimeに変換。失敗した場合はdefaultValueを使用
            if (DateTime.TryParse(param.FromDate, out DateTime parsedFromDate))
            {
                // 変換成功時の処理
            }
            else
            {
                // 変換に失敗した場合、デフォルト値を使用
                parsedFromDate = defaultValue;
            }
            // param.ToDateの値をDateTimeに変換。失敗した場合はdefaultValueを使用
            if (DateTime.TryParse(param.ToDate, out DateTime parsedToDate))
            {
                // 変換成功時の処理
            }
            else
            {
                // 変換に失敗した場合、デフォルト値を使用
                parsedToDate = defaultValue;
            }
            // デフォルトではログインユーザーでフィルタリングを有効にする
            bool loginUserFilter = true;
            // WfRadioButtonsが1の場合、ログインユーザーフィルタリングを無効にする
            if (param.WfRadioButtons == 1)
            {
                loginUserFilter = false;
            }
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            // GetAccidentsメソッドを呼び出して、事故リストを取得
            List<AccidentListItem_Local> listData = await GetAccidents(loguinUser.Company_ID, loguinUser.User_ID, param.AccidentReason, loginUserFilter, param.AccidentName, parsedFromDate, parsedToDate, param.DriverName, param.CarInt.ToString());

            model.JikoDataLists = listData;

            return model;
        }

        /// <summary>
        /// JSON：事故一覧のリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForAccidentList(SearchModelForAccidentList param, DataListSortModel sortParam)
        {
            try
            {
                DataListModel model = await CreateListModel(param);
                model.SortParam = sortParam;

                return await PartialViewAsJson("DataListForNone", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 指定された条件に基づいて事故リストを非同期に取得します。
        /// </summary>
        /// <param name="companyId">会社のID。</param>
        /// <param name="userId">ユーザーのID。</param>
        /// <param name="jikoKubun">事故区分。</param>
        /// <param name="loginUser">ログインユーザーかどうかを示すブール値。</param>
        /// <param name="jikoDisplay">事故の表示名（オプション）。</param>
        /// <param name="fromDate">検索開始日（オプション）。</param>
        /// <param name="toDate">検索終了日（オプション）。</param>
        /// <param name="displayName">表示名（オプション）。</param>
        /// <param name="syabanNumber">車輛番号（オプション）。</param>
        /// <returns>指定された条件に基づいて取得した <see cref="List{AccidentListItem_Local}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、事故リストAPIを使用して事故データを取得します.
        /// 指定されたパラメータを使用して事故データをフィルタリングし、条件に一致する事故情報をリストとして返します.
        /// </remarks>
        private async Task<List<AccidentListItem_Local>> GetAccidents(   
            int companyId,
            int userId,
            int jikoKubun,
            bool loginUser,
            string jikoDisplay = "",
            DateTime fromDate = default,
            DateTime toDate = default,
            string displayName = "",
            string syabanNumber = "")
        {
            using API.WebApp.AccidentListApi api = new(_mapApiSettiong);
            return await api.GetAccidentList(companyId, userId, jikoKubun, loginUser, jikoDisplay, fromDate, toDate, displayName, syabanNumber);
        }

        /// <summary>
        /// 指定されたコードIDに基づいてM_Codeデータリストを非同期に取得します。
        /// </summary>
        /// <param name="codeId">取得するコードのID。</param>
        /// <returns>指定されたコードIDに基づいて取得した <see cref="List{M_Code_Data_Local}"/> のリスト。</returns>
        /// <remarks>
        /// このメソッドは、M_Code_APIを使用して指定されたコードIDに関連するデータリストを取得します.
        /// 指定されたIDに関連する M_Code のデータリストを返します.
        /// </remarks>
        private async Task<List<M_Code_Data_Local>> GetMCodes(int codeId)
        {
            using API.WebApp.AccidentListApi api = new(_mapApiSettiong);
            return await api.GetMCodeList(codeId);
        }

        /// <summary>
        /// 事故一覧のモーダルを開き、必要なデータを含むHTMLを返します。
        /// </summary>
        /// <returns>事故一覧のモーダルビューを含む <see cref="IActionResult"/>。</returns>
        /// <remarks>
        /// このメソッドは、事故一覧のモーダルを表示するためのデータを準備し、JSON形式で返します.
        /// </remarks>
        public async Task<IActionResult> OpenAccidentListModal()
        {
            try
            {
                DataListModel model = await CreateModel();

                 // 事故一覧のモーダルバージョンを開く
                return await PartialViewAsJson("AccidentListModal", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：事故一覧のモーダルのリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ModalJsonGetDataListForAccidentList(SearchModelForAccidentList param)
        {
            try
            {
                param.WfRadioButtons = 1;
                DataListModel model = await CreateListModel(param);

                return await PartialViewAsJson("ModalDataListForNone", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }
    }
}
