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
using static HaisyaWeb.Models.CompletedPaymentsModel;

namespace HaisyaWeb.Controllers
{
    public class CompletedPaymentsController : BaseController
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public CompletedPaymentsController(ILogger<FixAmountApprovalController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 変更承認処理
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<IActionResult> CompletedPaymentsIndex(SearchModelForFixAmountApprovalList searchParams)
        {
            try
            {
                // CreateModelメソッドを呼び出して、検索パラメータに基づいたDataListModelを作成
                DataListModel model = await CreateModel(searchParams);
                model.Search.BackMenuAction = "DataList";

                return View("../CompletedPayments/Index", model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<DataListModel> CreateModel(SearchModelForFixAmountApprovalList param)
        {
            // パラメータがnullの場合、新しいインスタンスを作成
            param ??= new SearchModelForFixAmountApprovalList();

            // 各項目ごとにnullチェックを行い、nullの場合は初期値を設定
            param.BackMenuAction ??= "DataList";
            param.SelectMonth = param.SelectMonth.Year > 2000 ? param.SelectMonth : DateTime.Parse(DateTime.Now.ToString("yyyy/MM/01"));
            param.SelectZeiKubun = param.SelectZeiKubun > 0 ? param.SelectZeiKubun : 0;
            param.SelectHakkouKubun = param.SelectHakkouKubun > 0 ? param.SelectHakkouKubun : 0;
            param.SelectKingakuHenkou = param.SelectKingakuHenkou > 0 ? param.SelectKingakuHenkou : 0;

            if (param.SelectTantou == null) { param.SelectTantou = "ALL"; }

            SearchModelForFixAmountApprovalList search = new();

            // ログインユーザー取得
            V_LoginUser_Local loguinUser = await GetLoginUser();

            DataListModel model = new()
            {
                Company_ID = loguinUser.Company_ID,
            };

            search = new()
            {
                SelectTantou = param.SelectTantou,
                SelectMonth = param.SelectMonth,
                SelectZeiKubun = param.SelectZeiKubun,
                SelectHakkouKubun = param.SelectHakkouKubun,
                SelectKingakuHenkou = param.SelectKingakuHenkou,
                SelectShimebi = param.SelectShimebi,
                SelectTokuisakiIDTo = param.SelectTokuisakiIDTo,
                SelectTokuisakiNameTo = param.SelectTokuisakiNameTo,
                SelectTokuisakiID = param.SelectTokuisakiID,
                SelectTokuisakiName = param.SelectTokuisakiName,
                RangeSearch = param.RangeSearch,
                TokuisakiName = param.TokuisakiName,
            };

            model.Search = search;
            return model;
        }

        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertTNyukin2([FromBody] DataListModel.SeikyuNyukinModel model)
        {
            string errorMessage = null;
            try
            {
                // リクエストモデルやその中のリストがnullでないかを確認
                if (model == null || model.NyukinLists == null || model.SeikyuDataLists == null)
                {
                    return BadRequest("リクエストボディが欠落しているか、無効です");
                }
                // 入金データを格納するリストを初期化
                List<T_Nyukin_Local> nyukinlist = new List<T_Nyukin_Local>();

                // 入金リストと請求データリストをループして、Seikyu_IDが一致するものを探す
                foreach (var nyukin in model.NyukinLists)
                {
                    foreach (var seikyu in model.SeikyuDataLists)
                    {
                        int? id = seikyu.Seikyu_ID;
                        // 入金のSeikyu_IDと請求データのSeikyu_IDが一致したらリストに追加
                        if (nyukin.Seikyu_ID == id)
                        {
                            nyukinlist.Add(nyukin);
                            break; // 一度一致したら、次の入金リストの項目に進む
                        }
                    }
                }
                // 一致するデータが見つからなかった場合、エラーレスポンスを返す
                if (nyukinlist == null || nyukinlist.Count == 0)
                {
                    return BadRequest("CSVのSeikyu_IDと表示されているリストのSeikyu_IDが一致していなかった");
                }

                // return ValidationProblem();
                // 入金データをAPI経由で挿入処理
                using API.WebApp.CompletedPaymentsApi api = new(_mapApiSettiong);
                await api.InsertTNyukin(nyukinlist);
                // 正常に処理が完了したら、成功レスポンスを返す
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                return Json(new { partialView = "", errorMessage });
            }
        }

        /// <summary>
        /// TNyukinリストのデータの追加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<IActionResult> InsertNyukin([FromBody] Dto.PostPaymentInputDataModel model)
        {
            string errorMessage = null;
            try
            {
                // リクエストモデルやその中のリストがnullでないかを確認
                if (model == null || model == null)
                {
                    return BadRequest("リクエストボディが欠落しているか、無効です");
                }
                // 入金データを格納するリストを初期化
                List<T_Nyukin_Local> nyukinlist = new List<T_Nyukin_Local>();
                Dto.V_LoginUser_Local loginUser = await GetLoginUser();

                // 入金リストと請求データリストをループして、Seikyu_IDが一致するものを探す
                foreach (var nyukin in model.PaymentNyukinList)
                {
                    nyukin.Seikyu_ID = model.Seikyu_ID;
                    nyukin.Insert_User = loginUser.User_ID;
                    nyukin.Update_User = loginUser.User_ID;
                    nyukinlist.Add(nyukin);
                }
                // 一致するデータが見つからなかった場合、エラーレスポンスを返す
                if (nyukinlist == null || !nyukinlist.Any())
                {
                    return BadRequest("CSVのSeikyu_IDと表示されているリストのSeikyu_IDが一致していなかった");
                }

                // return ValidationProblem();
                // 入金データをAPI経由で挿入処理
                using API.WebApp.CompletedPaymentsApi api = new(_mapApiSettiong);
                await api.InsertOrUpdateTNyukin(nyukinlist);
                // 正常に処理が完了したら、成功レスポンスを返す
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                return Json(new { partialView = "", errorMessage });
            }
        }

        /// <summary>
        /// JSON：一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <param name="sortParam"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForCompletedPayments(SearchModelForFixAmountApprovalList param, DataListSortModel sortParam)
        {
            // パラメータがnullの場合、処理を中止してnullを返す
            if (param == null) { return null; }

            DataListModel model = new() 
            {
                SortParam = sortParam,
            };

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 指定されたパラメータを基に請求データリストを取得
                IEnumerable<Dto.V_SeikyuZumiDataList_Local> list = await GetSeikyuZumiDataList(loguinUser.Company_ID, param.SelectMonth.ToString("yyyy/MM/dd"), param.SelectTokuisakiID, param.SelectTokuisakiIDTo, param.TokuisakiName, param.SelectShimebi);
                // 税区分でフィルタリング
                list = list.Where(m => m.Zei_Kubun == param.SelectZeiKubun).ToList();
                // 発行区分でさらにフィルタリング
                list = list.Where(m => m.Inquiry_Status == param.SelectHakkouKubun).ToList();

                model.SeikyuDataLists = list;

                return await PartialViewAsJson("DataListForNone", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 入金入力画面の返却
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> PaymentInputIndex(PaymentInputViewModel model)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 指定された検索パラメータに基づいて請求データリストを取得します。
                // 取得には、ユーザーの会社ID、選択された日付、取引先ID、取引先ID範囲、請求担当、締め日が使用されます
                IEnumerable<Dto.V_SeikyuZumiDataList_Local> list = await GetSeikyuZumiDataList(loguinUser.Company_ID, model.SelectMonth.ToString("yyyy/MM/dd"), model.SelectTokuisakiID, model.SelectTokuisakiIDTo, model.TokuisakiName, model.SelectShimebi);
                // 取得した請求データの中から、モデルに指定された請求IDと一致するデータを取得します。
                model.SeikyuData = list.FirstOrDefault(item => item.Seikyu_ID == model.SeikyuId);

                return View("../CompletedPayments/PaymentInputIndex", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }


        /// <summary>
        /// 入金情報と返金情報と請求入金履歴と請求情報の返却
        /// </summary>
        /// <param name="Seikyu_ID"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPaymentInputDataList(int Seikyu_ID)
        {
            try
            {
                using API.WebApp.CompletedPaymentsApi api = new(_mapApiSettiong);
                Dto.PaymentInputDataListModel paymentInputDataList = await api.GetPaymentInputDataList(Seikyu_ID);

                return await PartialViewAsJson("../CompletedPayments/PaymentInputDataList", paymentInputDataList, true);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorModel = new();
                errorModel.RequestId = ex.HelpLink ?? GetErrorRequestId();
                errorModel.Message = ex.Message;
                return await PartialViewAsJson("Error", errorModel, true);
            }
        }

        /// <summary>
        /// JSON：請求データの返却
        /// </summary>
        /// <param name="iCompanyID"></param>
        /// <param name="targetMonth"></param>
        /// <param name="targetTokuisaki"></param>
        /// <param name="toTokuisaki"></param>
        /// <param name="tokuisakiName"></param>
        /// <param name="shimeDay"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Dto.V_SeikyuZumiDataList_Local>> GetSeikyuZumiDataList(int iCompanyID, string targetMonth, string targetTokuisaki, string toTokuisaki, string tokuisakiName, string shimeDay)
        {
            using API.WebApp.SeikyuDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.V_SeikyuZumiDataList_Local> seikyuCheckDataList = await api.GetSeikyuZumiDataList(iCompanyID, targetMonth, targetTokuisaki, toTokuisaki, tokuisakiName, shimeDay);
            return seikyuCheckDataList;
        }

        public async Task<IEnumerable<Dto.M_CompanyUser_Group_Local>> GetCompanyUserGroupListFromUserID(int User_ID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_CompanyUser_Group_Local> group_LocalList = await api.GetCompanyUserGroupListFromUserID(User_ID);
            return group_LocalList;
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
        public async Task<IEnumerable<Dto.M_Customer_Local>> GetTokuisakiDataList(string targetDate, string targetDateFrom, string targetDateTo, int iCcompanyID, int iBranchID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Customer_Local> dataList = await api.GetCustomerList(iCcompanyID, null, null, null);
            return dataList;
        }

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostPaymentInputData([FromBody] Dto.PostPaymentInputDataModel combinedData)
        {
            string errorMessage = null;
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                combinedData.User_ID = loguinUser.User_ID;

                using API.WebApp.CompletedPaymentsApi api = new(_mapApiSettiong);
                var result = await api.PostPaymentInputData(combinedData);
                return Json(new { data = result, errorMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                return Json(new { partialView = "", errorMessage });
            }
        }

    }
}
