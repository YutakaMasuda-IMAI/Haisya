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
using static HaisyaWeb.Models.ShitabaraiInquiryModifyListModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 下払問合せ変更承認一覧コントローラー
    /// </summary>
    public class ShitabaraiInquiryModifyListController : BaseController
    {
        private readonly ILogger<ShitabaraiInquiryModifyListController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="signInManager">サインインマネージャー</param>
        /// <param name="viewRenderService">ビューのレンダリングサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public ShitabaraiInquiryModifyListController(ILogger<ShitabaraiInquiryModifyListController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 下払問合せ変更承認一覧
        /// [売上]-[下払い問合せ]-[問合せ変更承認登録]
        /// </summary>
        /// <param name="Search">検索条件</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> Index(string Search)
        {
            try
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    SearchDataList search = JsonConvert.DeserializeObject<SearchDataList>(Search);

                    DataListModel mo = await CreateModel(search);
                    mo.Search.BackMenuAction = "DataList";
                    return View(mo);
                }
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 初期表示データ作成
        /// </summary>
        /// <param name="Search">検索条件</param>
        /// <returns>データリストモデル</returns>
        private async Task<DataListModel> CreateModel(SearchDataList Search = null)
        {
            Dto.V_LoginUser_Local loginUser = await GetLoginUser();

            SearchDataList param = Search ?? new()
            {
                SelectDay = DateOnly.FromDateTime(DateTime.Now),
                SelectZeiKubun = 0,
                SelectHakkouKubun = 1,
                SelectKingakuHenkou = 1,
                // TODO:GetUserGroupSelectは下払の情報を取るには、「UserGroupLists.Eigyo:3」を渡す
                SelectShitabaraiTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loginUser.Company_ID,
                                                UserGroupLists.Eigyo, loginUser.User_ID) ?? "ALL",
            };

            DataListModel m = new()
            {
                // ログインユーザー取得
                Company_ID = loginUser.Company_ID,
                Search = new()
                {
                    SelectDay = param.SelectDay,
                    // TODO:GetUserGroupSelectは下払の情報を取るには、「UserGroupLists.Eigyo:3」を渡す
                    TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loginUser.Company_ID, UserGroupLists.Eigyo, false),
                    SelectShimebi = param.SelectShimebi,
                    SelectShitabaraiTantou = param.SelectShitabaraiTantou,
                    SelectZeiKubun = param.SelectZeiKubun,
                    SelectHakkouKubun = param.SelectHakkouKubun,
                    SelectKingakuHenkou = param.SelectKingakuHenkou,
                    Customer1 = param.Customer1,
                    Customer_name1 = param.Customer_name1,
                    Customer2 = param.Customer2,
                    Customer_name2 = param.Customer_name2,
                    RangeSearch = param.RangeSearch,
                }
            };

            return m;
        }

        /// <summary>
        /// 傭車先選択のモーダル
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="ModalType">モーダルタイプ（FROMまたはTO）</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> IndexModal(int CompanyID, string ModalType)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.YosyaModel.YosyaDataListModel model = new()
                {
                    CompanyID = CompanyID,
                    ModalType = ModalType,
                };

                return await PartialViewAsJson("../Yosya/YousyaModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// JSON：下払問合せ変更承認一覧データHTMLの返却
        /// </summary>
        /// <param name="param">検索データリスト</param>
        /// <param name="sortParam">ソートパラメータ</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> JsonGetDataListForShitabaraiInquiryModifyList(SearchDataList param, DataListSortModel sortParam)
        {
            try
            {
                int shimebi = int.TryParse(param.SelectShimebi, out int result) ? result : 0;

                if (param != null)
                {
                    if (param.Customer2 == null)
                    {
                        if (param.Customer1 != null)
                            param.Customer2 = param.Customer1;
                        else
                            param.Customer2 = 99999999;
                    }

                    if (param.Customer1 == null)
                        param.Customer1 = 0;
                }

                //ログイン情報取得
                Dto.V_LoginUser_Local loginUser = await GetLoginUser();

                DataListModel model = new() 
                {
                    SortParam = sortParam
                };

                // 月初
                string firstDay = null;
                if (param.SelectDay != null)
                    firstDay = param.SelectDay?.ToString("yyyy/MM/01");

                // データの取得（APIコール）
                IEnumerable<Dto.V_ShitabaraiCheckDataList_Local> list = await GetShitabaraiCheckDataList(
                    loginUser.Company_ID,
                    firstDay,
                    shimebi,
                    param.SelectShitabaraiTantou,
                    param.SelectZeiKubun,
                    param.SelectHakkouKubun,
                    param.SelectKingakuHenkou,
                    param.Customer1.ToString(),
                    param.Customer2.ToString()
                );

                // 発行区分、(1：WEB、2：帳票)で再度絞込み
                list = list.Where(x => x.Inquiry_Status == param.SelectHakkouKubun).ToList();
                // TODO:金額変更の戻り値無しの為絞れない
                //list = list.Where(x => x. == param.SelectKingakuHenkou).ToList();

                // Group
                var shitabaraiCheckDataGropuList = list.OrderBy(m => m.Yosya_Branch_ID).GroupBy(x => new
                {
                    Shime_Day = x.Shime_Day
                    ,
                    Tax_category = x.Zei_Kubun
                    ,
                    Customer_Branch_ID = x.Yosya_Branch_ID
                });

                List<Dto.V_ShitabaraiCheckDataList_Local> invoiceCheckDataList_Local = new List<Dto.V_ShitabaraiCheckDataList_Local>();
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
                        Dto.V_ShitabaraiCheckDataList_Local groupList = new Dto.V_ShitabaraiCheckDataList_Local();
                        groupList = invoiceCheckData;
                        groupList.Uriage_Shiharai_ID_LIST = String.Join(",", Uriage_Shiharai_ID_List);

                        invoiceCheckDataList_Local.Add(groupList);
                        break;
                    }
                }
                list = invoiceCheckDataList_Local;

                List<ResultDataList> listData = new();
                foreach (var data in list)
                {
                    listData.Add(new ResultDataList(data));
                }

                model.Result = listData;

                return await PartialViewAsJson("../ShitabaraiInquiryModifyList/DataList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// JSON：一覧データの返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払年月</param>
        /// <param name="shimeDay">締日</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">問合せステータス</param>
        /// <param name="shiharaiChanged">支払変更</param>
        /// <param name="yosyasakiFrom">傭車先FROM</param>
        /// <param name="yosyasakiTo">傭車先TO</param>
        /// <returns>支払チェックデータリスト</returns>
        private async Task<IEnumerable<Dto.V_ShitabaraiCheckDataList_Local>> GetShitabaraiCheckDataList(int CompanyID, string shiharaiNengetsu, int shimeDay, string shiharaiTantou, int zeiKubun, int inquiryStatus, int shiharaiChanged, string yosyasakiFrom, string yosyasakiTo)
        {
            using API.WebApp.ShitabaraiInquiryModifyListApi api = new(_mapApiSettiong);
            return await api.GetShitabaraiCheckDataList(CompanyID, shiharaiNengetsu, shimeDay, shiharaiTantou, zeiKubun, inquiryStatus, shiharaiChanged, yosyasakiFrom, yosyasakiTo, 0);
        }
    }
}
