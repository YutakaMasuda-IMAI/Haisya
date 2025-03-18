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
using static HaisyaWeb.Models.FixAmountApprovalModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 変更承認一覧のコントローラークラス
    /// </summary>
    public class FixAmountApprovalController : BaseController
    {
        private readonly ILogger<FixAmountApprovalController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public FixAmountApprovalController(ILogger<FixAmountApprovalController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 変更承認処理の初期画面を表示します。
        /// 検索条件に基づいてデータを取得し、変更承認画面のモデルを構築します。
        /// </summary>
        /// <param name="searchParams">変更承認リストの検索条件。</param>
        /// <returns>承認画面を表示するためのIActionResult。</returns>
        public async Task<IActionResult> FixAmountApprovalIndex(SearchModelForFixAmountApprovalList searchParams)
        {
            try
            {
                DataListModel model = await CreateModel(searchParams);
                model.Search.SelectZeiKubun = 0;
                if (model.Search.SelectHakkouKubun == 0)
                    model.Search.SelectHakkouKubun = 1;
                if (model.Search.SelectKingakuHenkou == null)
                    model.Search.SelectKingakuHenkou = 1;

                return View("../FixAmountApproval/Index", model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <param name="param">SearchModelForFixAmountApprovalList</param> 
        /// <returns>model</returns>
        private async Task<DataListModel> CreateModel(SearchModelForFixAmountApprovalList param)
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            // パラメータがnullの場合、新しいインスタンスを作成
            param ??= new SearchModelForFixAmountApprovalList();

            // 各項目ごとにnullチェックを行い、nullの場合は初期値を設定
            param.BackMenuAction ??= "DataList";
            param.SelectDay ??= DateTime.Now.ToString("yyyy/MM/dd");
            param.SelectSeikyuTantou ??= await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                UserGroupLists.Seikyu, loguinUser.User_ID) ?? "ALL";

            SearchModelForFixAmountApprovalList search = new();

            DataListModel model = new()
            {
                Company_ID = loguinUser.Company_ID,
            };

            search = new()
            {
                TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                SelectSeikyuTantou = param.SelectSeikyuTantou,
                SelectDay = param.SelectDay,
                SelectZeiKubun = param.SelectZeiKubun,
                SelectHakkouKubun = param.SelectHakkouKubun,
                SelectKingakuHenkou = param.SelectKingakuHenkou,
                SelectShimebi = param.SelectShimebi,
                SelectSeikyuTantouList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu, false),
                SelectTokuisakiID = param.SelectTokuisakiID,
                SelectTokuisakiName = param.SelectTokuisakiName,
                SelectTokuisakiIDTo = param.SelectTokuisakiIDTo,
                SelectTokuisakiNameTo = param.SelectTokuisakiNameTo,
                IsSelectTokuisaki = param.IsSelectTokuisaki
            };

            model.Search = search;
            return model;
        }

        /// <summary>
        /// JSON形式で案件一覧データのHTMLを返却します。
        /// 指定された検索条件に基づいてデータをフィルタリングし、結果をJSON形式で返します。
        /// </summary>
        /// <param name="param">案件一覧データの検索条件を含むパラメーター。</param>
        /// <returns>JSON形式で案件一覧データを返却するIActionResult。</returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForFixApproval(SearchModelForFixAmountApprovalList param, DataListSortModel sortParam)
        {
            // パラメーターが null の場合、適切な応答を返す
            if (param == null) { return null; }

            DataListModel model = new() { };

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 月初
                string firstDay = null;
                if (DateTime.TryParse(param.SelectDay, out DateTime dfirstDay))
                    firstDay = dfirstDay.ToString("yyyy/MM/01");
                string tokuisakiID = "0";
                string tokuisakiIDTo = "9999999999999";
                if (param.SelectTokuisakiID != null)
                {
                    tokuisakiID = tokuisakiIDTo = param.SelectTokuisakiID;
                }
                if (param.SelectTokuisakiIDTo != null)
                {
                    tokuisakiIDTo = param.SelectTokuisakiIDTo;
                }

                IEnumerable<Dto.V_SeikyuCheckDataList_Local> list = await GetSeikyuCheckDataList(loguinUser.Company_ID, firstDay, tokuisakiID, tokuisakiIDTo);

                List<SeikyuCheckDataList> listData = new();

                // データリストが取得できた場合、フィルタリング処理を実施
                if (list != null)
                {
                    // V_SeikyuCheckDataList_Local から SeikyuCheckDataList への変換
                    foreach (var data in list)
                    {
                        listData.Add(new SeikyuCheckDataList(data));
                    }
                    // 各検索条件に基づいてデータをフィルタリング
                    if (param.SelectShimebi != null)
                    {
                        // 締日が指定されている場合、その締日と一致するアイテムのみをリストに残す
                        listData = listData.Where(m => m.Shime_Day.ToString() == param.SelectShimebi).ToList();
                    }

                    if (param.SelectSeikyuTantou != null && !"ALL".Equals(param.SelectSeikyuTantou))
                    {
                        // 請求担当が指定されており、"ALL" でない場合、その請求担当IDと一致するアイテムのみをリストに残す
                        listData = listData.Where(m => m.Seikyu_TantouID == int.Parse(param.SelectSeikyuTantou)).ToList();
                    }
                    // 税区分が指定されている場合、その税区分と一致するアイテムのみをリストに残す
                    //listData = listData.Where(m => m.Zei_Kubun == param.SelectZeiKubun).ToList();
                    // 発行区分が指定されている場合、その発行区分と一致するアイテムのみをリストに残す
                    listData = listData.Where(m => m.Inquiry_Status == param.SelectHakkouKubun).ToList();
                    // 請求金額変更が指定されている場合、その請求金額変更と一致するアイテムのみをリストに残す
                    listData = listData.Where(m => m.Seikyu_Changed == param.SelectKingakuHenkou).ToList();

                    //TODO SelectTokuisakiIDはコードの為、プロシージャの引数変更が必要
                    //if (param.SelectTokuisakiID != null && param.SelectTokuisakiIDTo != null)
                    //{
                    //    // 得意先IDの範囲が指定されている場合、その範囲に含まれる得意先IDを持つアイテムのみをリストに残す
                    //    listData = listData.Where(m => m.Customer_Branch_ID >= int.Parse(param.SelectTokuisakiID)
                    //    && m.Customer_Branch_ID <= int.Parse(param.SelectTokuisakiIDTo)).ToList();
                    //}
                    //else if (param.SelectTokuisakiID != null && param.SelectTokuisakiIDTo == null)
                    //{
                    //    // 得意先IDの下限が指定されている場合、その下限以上の得意先IDを持つアイテムのみをリストに残す
                    //    listData = listData.Where(m => m.Customer_Branch_ID >= int.Parse(param.SelectTokuisakiID)).ToList();
                    //}


                    // Group
                    var seikyuCheckDataGropuList = listData.OrderBy(m => m.Customer_Branch_ID).GroupBy(x => new
                    {
                        Shime_Day = x.Shime_Day
                        ,
                        Tax_category = x.Zei_Kubun
                        ,
                        Customer_Branch_ID = x.Customer_Branch_ID
                    });

                    List<SeikyuCheckDataList> seikyuCheckDataList_Local = new List<SeikyuCheckDataList>();
                    foreach (var target in seikyuCheckDataGropuList)
                    {
                        // 付随する運賃IDリスト
                        List<int?> Uriage_Unchin_ID_List = new List<int?>();
                        foreach (var invoiceData in target)
                        {
                            if (Uriage_Unchin_ID_List.Contains(invoiceData.Uriage_Unchin_ID))
                                continue;

                            Uriage_Unchin_ID_List.Add(invoiceData.Uriage_Unchin_ID);
                        }

                        var key = target.Key;
                        foreach (var invoiceData in target)
                        {
                            SeikyuCheckDataList groupList = new SeikyuCheckDataList();
                            groupList = invoiceData;
                            //groupList.Uriage_Unchin_ID_LIST = String.Join(",", Uriage_Unchin_ID_List);
                            seikyuCheckDataList_Local.Add(groupList);
                            break;
                        }
                    }
                    listData = seikyuCheckDataList_Local;
                }

                model.SeikyuCheckDataLists = listData.OrderBy(m => m.Customer_Branch_ID).ToList();
                model.SortParam = sortParam;

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
        /// <param name="iCompanyID"></param>
        /// <param name="targetMonthDate"></param>
        /// <param name="tokuisakiID"></param>
        /// <param name="tokuisakiIDTo"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Dto.V_SeikyuCheckDataList_Local>> GetSeikyuCheckDataList(int iCompanyID, string targetDate, string tokuisakiID, string tokuisakiIDTo)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            return await api.GetSeikyuCheckDataList(iCompanyID, targetDate, tokuisakiID, tokuisakiIDTo);
        }
    }
}
