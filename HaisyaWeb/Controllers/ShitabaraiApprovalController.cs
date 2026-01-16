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
using static HaisyaWeb.Models.ShitabaraiApprovalModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 下払い承認コントローラー
    /// </summary>
    public class ShitabaraiApprovalController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="signInManager">サインインマネージャー</param>
        /// <param name="viewRenderService">ビューレンダーサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public ShitabaraiApprovalController(ILogger<ShitabaraiApprovalController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<Models.MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 下払い問い合わせ発行処理一覧
        /// </summary>
        /// <param name="Search">検索条件</param>
        /// <param name="Check_Shitabarai_ID">チェック下払いID</param>
        /// <returns>ビュー</returns>
        [HttpPost]
        public async Task<IActionResult> Index(string Search, int Check_Shitabarai_ID)
        {
            try
            {
                SearchDataList search2 = JsonConvert.DeserializeObject<SearchDataList>(Search);
                DataListModel model = await CreateModel(Check_Shitabarai_ID, search2);

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
        /// <param name="Check_Shitabarai_ID">チェック下払いID</param>
        /// <param name="search2">検索条件</param>
        /// <returns>データリストモデル</returns>
        private async Task<DataListModel> CreateModel(int Check_Shitabarai_ID, SearchDataList search2)
        {
            // 初期パラメータ設定
            SearchModelForShitabaraiCheckDataList param = new()
            {
                SelectDay = DateOnly.FromDateTime(DateTime.Now),
                SelectZeiKubun = 0,
                PublishDay = DateTime.Now.ToString("yyyy/MM/dd"),
            };
            param.SelectShitabaraiTantou ??= "ALL";

            DataListModel model = new()
            {
                Search2 = search2
            };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            model.Company_ID = loguinUser.Company_ID;

            SearchModelForShitabaraiCheckDataList search = new()
            {
                TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                SelectTantou = param.SelectTantou,
                SelectShitabaraiTantou = param.SelectShitabaraiTantou,
                SelectDay = param.SelectDay,
                SelectShimebi = param.SelectShimebi,
                SelectZeiKubun = param.SelectZeiKubun,
                YousyasakiCodeTo = param.YousyasakiCodeTo,
                YousyasakiIDTo = param.YousyasakiIDTo,
                PublishDay = param.PublishDay,
                ShitabaraiDayTo = param.ShitabaraiDayTo,
            };

            model.Search = search;

            T_Check_Shitabarai_Local printCheckShitabarai = await GetTCheckShitabaraiById(Check_Shitabarai_ID);
            if (printCheckShitabarai.Check_Shitabarai_ID == 0)
            {
                System.Diagnostics.Debug.WriteLine("プロシージャ「Proc_V_ShitabaraiCheckDataList」確認 チェック下払ID:[" + Check_Shitabarai_ID.ToString() + "]は[T_Check_Shitabarai]で削除対象となっています。");
                throw new Exception("プロシージャ「Proc_V_ShitabaraiCheckDataList」確認 チェック下払ID:[" + Check_Shitabarai_ID.ToString() + "]は[T_Check_Shitabarai]で削除対象となっています。");
            }

            if (string.IsNullOrEmpty(search2.YousyasakiCodeTo))
            {
                if (!string.IsNullOrEmpty(search2.YousyasakiCode))
                    search2.YousyasakiCodeTo = search2.YousyasakiCode;
                else
                    search2.YousyasakiCodeTo = "99999999";
            }

            if (string.IsNullOrEmpty(search2.YousyasakiCode))
                search2.YousyasakiCode = "0";

            // 月初
            string firstDay = null;
            if (search2.SelectDay != null)
                firstDay = search2.SelectDay?.ToString("yyyy/MM/01");

            IEnumerable<V_ShitabaraiCheckDataList_Local> shitabaraiCheckDataList = await GetShitabaraiCheckDataList(
                    loguinUser.Company_ID,
                    firstDay,
                    0,
                    search2.SelectShitabaraiTantou,
                    search2.SelectZeiKubun,
                    search2.SelectHakkouKubun,
                    search2.SelectKingakuHenkou,
                    search2.YousyasakiCode,
                    search2.YousyasakiCodeTo,
                    Check_Shitabarai_ID);

            // 取得したデータから最初の要素を取得し、ShitabaraiCheckDataに変換
            V_ShitabaraiCheckDataList_Local firstItem = shitabaraiCheckDataList.FirstOrDefault();
            HaisyaWeb.Dto.V_ShitabaraiCheckDataList_Local shitabaraiCheckData = firstItem != null ? new ShitabaraiCheckData(firstItem) : null;

            // 変換後のデータをmodelにセットする
            model.ShitabaraiCheckData = shitabaraiCheckData;

            model.PrintShitabarai = await GetTPrintShiharai(Check_Shitabarai_ID);

            // レイアウトオプション
            model.LayoutOption = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 11);
            return model;
        }

        /// <summary>
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param">検索条件</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForFixApprovalDetail(SearchModelForShitabaraiCheckDataList param)
        {
            try
            {
                // ログインユーザー情報の取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DataListModel model = new() { };
                // 単位リストの取得
                List<M_Unit_Local> unitlist = await GetUnit(loguinUser.Company_ID);
                List<SeikyuMeisai> list = new List<SeikyuMeisai>();

                // チェック下払いIDに基づくチェック下払い情報の取得
                model.CheckShitabarai = await GetTCheckShitabaraiById(param.Check_Shitabarai_ID);
                model.Check_Shitabarai_ID = param.Check_Shitabarai_ID;

                T_Check_Shitabarai_Local checkShitabarai = await GetTCheckShitabaraiById(param.Check_Shitabarai_ID);

                List<T_Check_Shitabarai_Detail_Local> checkShitabaraiDetailList = await GetTCheckShitabaraiDetailListById(checkShitabarai.Check_Shitabarai_ID);

                List<T_Print_Shitabarai_Detail_Local> meisaiUp = new();
                foreach (T_Check_Shitabarai_Detail_Local checkShitabaraiDetail in checkShitabaraiDetailList)
                {
                    T_Uriage_Shitabarai_Local uriageShitabarai = await GetTUriageShitabaraiById(checkShitabaraiDetail.Uriage_Shiharai_ID);
                    string workName = "";
                    string SyasyuKataName = "";
                    if (uriageShitabarai != null)
					{
                        // 案件情報→[T_Uriage_Shitabarai].「Uriage_ID」→[T_Uriage].「Anken_ID」→[T_Anken_Detail].「Work_Name」
                        T_Anken_Detail_Local ankenDetail = await GetTAnkenDetailByUriageID(uriageShitabarai.Uriage_ID);
                        if (ankenDetail != null)
                            workName = ankenDetail.Work_Name;
                        // 車種→[T_Uriage_Shitabarai].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaryo_ID」→[M_Syaryo].「SyasyuDisplay」
                        M_Syaryo_Local syaryo = await GetMSyaryoByUriageID(uriageShitabarai.Uriage_ID);
                        if (syaryo != null)
                            SyasyuKataName = syaryo.SyasyuDisplay;
                    }
                    T_Print_Shitabarai_Detail_Local printShitabaraiDetail = new()
                    {
                        //Print_Shitabarai_ID
                        //Data_Kubun
                        //Data_Sort
                        Uriage_Shiharai_ID = checkShitabaraiDetail.Uriage_Shiharai_ID,
                        Anken_ID = checkShitabaraiDetail.Anken_ID,
                        //Anken_ID_Detail
                        Display_Date = uriageShitabarai?.Shiharai_Date,
                        Syaban = uriageShitabarai?.Syaban,
                        SyasyuKataName = SyasyuKataName,
                        Tsumi = uriageShitabarai?.Tsumi,
                        Oroshi = uriageShitabarai?.Oroshi,
                        Luggage = uriageShitabarai?.Luggage,
                        Work_Name = workName,
                        Qty = (double)(checkShitabaraiDetail.Qty ?? 0),
                        Unit = checkShitabaraiDetail.Unit ?? 0,
                        UnitPrice = checkShitabaraiDetail.UnitPrice ?? 0,
                        CalcPrice = checkShitabaraiDetail.CalcPrice ?? 0,
                        ShiharaiUnchin = checkShitabaraiDetail.ShiharaiUnchin ?? 0,
                        Tatekaekin = checkShitabaraiDetail.Tatekaekin ?? 0,
                        Warimashi1 = checkShitabaraiDetail.Warimashi1 ?? 0,
                        Warimashi2 = checkShitabaraiDetail.Warimashi2 ?? 0,
                        Warimashi3 = checkShitabaraiDetail.Warimashi3 ?? 0,
                        Warimashi4 = checkShitabaraiDetail.Warimashi4 ?? 0,
                        Warimashi5 = checkShitabaraiDetail.Warimashi5 ?? 0,
                        ShiharaiTotal = checkShitabaraiDetail.ShiharaiTotal ?? 0,
                        //Zei_Kubun
                        //YosyaDriver_ID
                        //Yosya_Branch_ID
                        //Yosya_Name
                        //Yosya_Driver_Name
                        //Remarks_ID
                        Remaks = uriageShitabarai?.Remarks,
                        //FROM_DATE
                        //TO_DATE
                    };
                    meisaiUp.Add(printShitabaraiDetail);
                }
                List<T_Check_Shitabarai_Change_Local> meisaiDown = await GetTCheckShitabaraiChange(param.Check_Shitabarai_ID);

                // `meisaiUp` と `meisaiDown` のアイテムを `SeikyuMeisai` に変換し、リストに追加
                for (int i = 0; i < meisaiUp.Count; i++)
                {
                    // `SeikyuMeisai` クラスのインスタンスを作成
                    SeikyuMeisai data = new()
                    {
                        // `meisaiUp` から上部情報を設定
                        Up = meisaiUp[i]
                    };
                    data.Up.UnitData = unitlist.FirstOrDefault(u => u.Unit_ID == meisaiUp[i].Unit)?.Unit_Display;

                    // [T_Print_Seikyu_Detail]と[T_Check_Seikyu_Change]を[Uriage_Unchin_ID]で紐づけ
                    foreach (T_Check_Shitabarai_Change_Local item in meisaiDown.Where(m => (m.Uriage_Shiharai_ID == meisaiUp[i].Uriage_Shiharai_ID)))
                    {
                        data.Down = item;
                        data.Down.UnitData = unitlist.FirstOrDefault(u => u.Unit_ID == item.Unit)?.Unit_Display;
                    }
					data.Down ??= new T_Check_Shitabarai_Change_Local();

                    data.CheckShitabaraiDetail = checkShitabaraiDetailList.FirstOrDefault(x => x.Uriage_Shiharai_ID == meisaiUp[i].Uriage_Shiharai_ID);

                    list.Add(data);
                }

                //model.CheckShitabaraiDetail = checkShitabaraiDetail;
                model.SeikyuMeisaiList = list;


                List<T_YosyaShiharai_Local> yosyaDataList = new List<T_YosyaShiharai_Local>();

				model.YosyaShiharaiList = yosyaDataList;

                return await PartialViewAsJson("ClaimDataList", model, true);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// 指定されたパラメータに基づいて仕払データのリストを取得します。
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="shiharaiNengetsu">支払い年月（YYYYMM形式）</param>
        /// <param name="shimeDay">締め日</param>
        /// <param name="shiharaiTantou">支払い担当者名</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="inquiryStatus">問い合わせステータス</param>
        /// <param name="shiharaiChanged">支払いの変更状態</param>
        /// <param name="yosyasakiFrom">傭車者先の開始コード</param>
        /// <param name="yosyasakiTo">傭車先の終了コード</param>
        /// <param name="checkShitabaraiId">（オプション）チェック支払ID。デフォルト値は0。</param>
        /// <returns>仕払データリストの非同期タスクを返します。</returns>
        private async Task<IEnumerable<Dto.V_ShitabaraiCheckDataList_Local>> GetShitabaraiCheckDataList(int CompanyID, string shiharaiNengetsu, int shimeDay, string shiharaiTantou, int zeiKubun, int inquiryStatus, int shiharaiChanged, string yosyasakiFrom, string yosyasakiTo, int checkShitabaraiId = 0)
        {
            using API.WebApp.ShitabaraiInquiryModifyListApi api = new(_mapApiSettiong);
            IEnumerable<Dto.V_ShitabaraiCheckDataList_Local> dataList = await api.GetShitabaraiCheckDataList(CompanyID, shiharaiNengetsu, shimeDay, shiharaiTantou, zeiKubun, inquiryStatus, shiharaiChanged, yosyasakiFrom, yosyasakiTo, checkShitabaraiId);
            return dataList;
        }

        /// <summary>
        /// 発行処理
        /// </summary>
        /// <param name="dto">下払いチェック問い合わせDTO</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> PublishShitabaraiApproval(ShitabaraiCheckInquiryDto_Local dto)
        {
            string errorMessage = null;
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                dto.companyID = loguinUser.Company_ID;
                dto.loginUserId = loguinUser.User_ID;

                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PublishShitabaraiCheckDataList(dto);
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

        /// <summary>
        /// T_Check_Shitabaraiの取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>チェック下払いデータ</returns>
        private async Task<T_Check_Shitabarai_Local> GetTCheckShitabaraiById(int checkShitabaraiId)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTCheckShitabaraiById(checkShitabaraiId);
        }

        /// <summary>
        /// T_Check_Shitabarai_Detailの取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>チェック下払い詳細データ</returns>
        public async Task<T_Check_Shitabarai_Detail_Local> GetTCheckShitabaraiDetailById(int checkShitabaraiId)
        {
            try
            {
                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTCheckShitabaraiDetailById(checkShitabaraiId);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// T_Check_Shitabarai_Detailを複数取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>チェック下払い詳細データのリスト</returns>
        private async Task<List<T_Check_Shitabarai_Detail_Local>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTCheckShitabaraiDetailListById(checkShitabaraiId);
        }
        
        /// <summary>
        /// T_Uriage_Shitabaraiの取得
        /// </summary>
        /// <param name="uriageShiharaiID">売上支払ID</param>
        /// <returns>売上支払データ</returns>
        private async Task<Dto.T_Uriage_Shitabarai_Local> GetTUriageShitabaraiById(int uriageShiharaiID)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTUriageShitabaraiById(uriageShiharaiID);
        }

        /// <summary>
        /// T_Anken_Detailの取得
        /// 案件情報→[T_Uriage_Shitabarai].「Uriage_ID」→[T_Uriage].「Anken_ID」→[T_Anken_Detail].「Work_Name」
        /// ※T_Print_Shitabaraiが無くなったことによる対応
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>案件詳細データ</returns>
        private async Task<Dto.T_Anken_Detail_Local> GetTAnkenDetailByUriageID(int Uriage_ID)
        {
            try
            {
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTAnkenDetailByUriageID(Uriage_ID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// M_Syaryoの取得
        /// 車種→[T_Uriage_Shitabarai].「Uriage_ID」→[T_Uriage].「Nippou_ID」→[T_Nippou].「AnkenDisplay_ID」→[T_Haisya].「Syaryoumanagement_ID」→[M_Syaryomanagement].「Syaryo_ID」→[M_Syaryo].「SyasyuDisplay」
        /// ※T_Print_Shitabaraiが無くなったことによる対応
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>車両データ</returns>
        private async Task<Dto.M_Syaryo_Local> GetMSyaryoByUriageID(int Uriage_ID)
        {
            try
            {
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetMSyaryoByUriageID(Uriage_ID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// T_Uriageの取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>売上データ</returns>
        public async Task<Dto.T_Uriage_Local> GetTUriageById(int uriageID)
        {
            try
            {
                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTUriageById(uriageID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// 指定されたIDに基づいて下払い詳細情報を取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>下払い詳細情報</returns>
        public async Task<Dto.T_Shitabarai_Detail_Local> GetTShitabaraiDetail(int uriageID)
        {
            try
            {
                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTShitabaraiDetail(uriageID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// 指定されたIDに基づいて下払い情報を取得
        /// </summary>
        /// <param name="shitabaraiId">下払いID</param>
        /// <returns>下払い情報</returns>
        public async Task<Dto.T_Shitabarai_Local> GetTShitabarai(int shitabaraiId)
        {
            try
            {
                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTShitabarai(shitabaraiId);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// 指定されたIDに基づいて予社支払情報を取得
        /// </summary>
        /// <param name="shitabaraiID">下払いID</param>
        /// <returns>予社支払情報</returns>
        public async Task<Dto.T_YosyaShiharai_Local> GetTYosyaShiharai(int shitabaraiID)
        {
            try
            {
                // 下払い問い合わせ発行処理
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                return await api.GetTYosyaShiharai(shitabaraiID);
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(x.Message);
                return default;
            }
        }

        /// <summary>
        /// 指定された下払いチェックIDに基づいて印刷情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <returns>下払い印刷情報</returns>
        private async Task<Dto.T_Print_Shitabarai_Local> GetTPrintShiharai(int checkShitabaraiId)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTPrintShiharai(checkShitabaraiId);
        }

        /// <summary>
        /// 指定された印刷下払いIDに基づいて印刷詳細情報を取得
        /// </summary>
        /// <param name="printShitabaraiId">印刷下払いID</param>
        /// <returns>印刷下払い詳細情報のリスト</returns>
        private async Task<List<Dto.T_Print_Shitabarai_Detail_Local>> GetTPrintShiharaiDetail(int printShitabaraiId)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTPrintShiharaiDetail(printShitabaraiId);
        }

        /// <summary>
        /// 指定された下払いチェックIDと売上支払IDに基づいて下払い変更情報を取得
        /// </summary>
        /// <param name="checkShitabaraiId">下払いチェックID</param>
        /// <param name="uriageShitabaraiId">売上支払ID (オプション)</param>
        /// <returns>下払い変更情報のリスト</returns>
        private async Task<List<Dto.T_Check_Shitabarai_Change_Local>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null)
        {
            // 下払い問い合わせ発行処理
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetTCheckShitabaraiChange(checkShitabaraiId, uriageShitabaraiId);
        }

        /// <summary>
        /// ポップアップ画面
        /// </summary>
        /// <param name="checkShitabaraiID">チェック下払いID</param>
        /// <param name="uriageShiharaiID">売上支払ID</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> IndexModal(int checkShitabaraiID, int uriageShiharaiID)
        {
            try
            {
                int checkCub = 1;

                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                T_Check_Shitabarai_Local checkShitabarai = await GetTCheckShitabaraiById(checkShitabaraiID);
                T_Check_Shitabarai_Detail_Local checkShitabaraiDetail =
                    (await GetTCheckShitabaraiDetailListById(checkShitabarai.Check_Shitabarai_ID))?.FirstOrDefault(d => d.Uriage_Shiharai_ID == uriageShiharaiID);
                List<T_Check_Shitabarai_Change_Local> checkShitabaraiChangeList = await GetTCheckShitabaraiChange(checkShitabaraiID, uriageShiharaiID);
                T_Check_Shitabarai_Change_Local checkShitabaraiChange = checkShitabaraiChangeList.FirstOrDefault(item => item.Check_Shitabarai_ID == checkShitabaraiID && item.Uriage_Shiharai_ID == uriageShiharaiID);

                T_Print_Shitabarai_Local printShitabarai = await GetTPrintShiharai(checkShitabaraiID);
                T_Check_Shitabarai_Done_Local checkShitabaraiDone = await GetCheckShitabaraiDone(checkShitabaraiID);

                T_Uriage_Shitabarai_Local uriageShitabarai = await GetTUriageShitabaraiById(uriageShiharaiID);

                List<T_Print_Shitabarai_Detail_Local> printShitabaraiDetails = await GetTPrintShiharaiDetail(printShitabarai?.Print_Shitabarai_ID ?? 0);

                T_Print_Shitabarai_Detail_Local printShitabaraiDetail = printShitabaraiDetails.FirstOrDefault(x => x.Uriage_Shiharai_ID == uriageShitabarai.Uriage_Shiharai_ID);
                // var checkSeikyu = await GetSeikyu(Check_Seikyu_ID);
                // var uriageUnchin = await GetUriageUnchin(Uriage_Unchin_ID);
                List<M_Unit_Local> unitList = await GetUnit(loguinUser.Company_ID);
                // var customerBranch = await GetCustomerBranch(checkSeikyu.Customer_Branch_ID);

                M_Yosya_Branch_Local yosyaCustomerBranch = await GetMYosyaBranch(uriageShitabarai.Yosya_Branch_ID);

                IndexModalModel model = new();

                // 単位リストを SelectListItem に変換し、モデルに設定
                model.UnitDto = unitList.Select(item => new SelectListItem
                {
                    Value = item.Unit_ID.ToString(),
                    Text = item.Unit_Display
                }).ToList();

                model.Check_Shitabarai_ID = checkShitabaraiID;
                model.Uriage_Shiharai_ID = uriageShiharaiID;
                model.Change_Flg = checkShitabaraiDone.Change_Flg;
                model.Check_Kubun = checkShitabarai.Check_Kubun;

                // // 共通の設定
                model.DisplayDate = uriageShitabarai.Shiharai_Date;

                model.Tsumi = uriageShitabarai.Tsumi;
                model.Oroshi = uriageShitabarai.Oroshi;
                model.Luggage = uriageShitabarai.Luggage;
                model.Syaban = uriageShitabarai.Syaban;
                model.ZeiKubun = uriageShitabarai.Zei_Kubun;
                model.ShimeDay = checkShitabarai.Shime_Day;
                model.CustomerId = yosyaCustomerBranch.Yosya_Branch_ID;
                model.Customer_Code = yosyaCustomerBranch.Customer_Branch_Code;
                model.Customer_Name = yosyaCustomerBranch.Customer_Branch_Name;
                model.Qty = uriageShitabarai.Qty;
                model.Remarks = uriageShitabarai.Remarks;

                // チェック区分に応じた処理
                if (checkShitabarai.Check_Kubun == 1)
                {
                    model.Qty = checkShitabaraiChange.Qty ?? checkShitabaraiDetail?.Qty;
                    model.Unit = checkShitabaraiChange.Unit ?? checkShitabaraiDetail?.Unit;
                    model.UnitPrice = checkShitabaraiChange.UnitPrice ?? checkShitabaraiDetail?.UnitPrice;
                    model.CalcPrice = checkShitabaraiChange.CalcPrice ?? (model.UnitPrice != null && model.Qty != null ? model.UnitPrice.Value * (decimal)model.Qty.Value : null);
                    model.SeikyuUnchin = checkShitabaraiChange.ShiharaiUnchin ?? (model.CalcPrice);
                    model.Tatekaekin = checkShitabaraiChange.Tatekaekin;
                    model.Warimashi1 = checkShitabaraiChange.Warimashi1;
                    model.Warimashi2 = checkShitabaraiChange.Warimashi2;
                    model.Warimashi3 = checkShitabaraiChange.Warimashi3;
                    model.Warimashi4 = checkShitabaraiChange.Warimashi4;
                    model.Warimashi5 = checkShitabaraiChange.Warimashi5;
                    model.SeikyuTotal = checkShitabaraiChange.ShiharaiTotal;

                    // 変更前の情報を設定
                    model.PreviousQty = uriageShitabarai.Qty;
                    model.PreviousUnit = uriageShitabarai.Unit;
                    model.PreviousUnitPrice = uriageShitabarai.UnitPrice;
                    model.ShiharaiPrice = uriageShitabarai.ShiharaiPrice;
                    // model.ShiharaiPrice = checkShitabaraiChange.ShiharaiUnchin;
                    model.PreviousTatekaekin = uriageShitabarai?.Tatekaekin ?? 0;
                    model.PreviousWarimashi1 = uriageShitabarai?.WarimashiPrice ?? 0;
                    model.PreviousWarimashi2 = 0;
                    model.PreviousWarimashi3 = 0;
                    model.PreviousWarimashi4 = 0;
                    model.PreviousWarimashi5 = 0;
                    model.PreviousSeikyuTotal = checkShitabaraiDetail?.ShiharaiTotal ?? 0;
                }
                else if (checkCub == 2)
                {

                    // 変更前と変更後の情報を同じにする（全てT_Uriage_Unchinから取得）
                    model.Qty = model.PreviousQty = uriageShitabarai.Qty;
                    model.Unit = model.PreviousUnit = uriageShitabarai.Unit;
                    model.UnitPrice = model.PreviousUnitPrice = uriageShitabarai.UnitPrice;
                    model.CalcPrice = model.PreviousCalcPrice = uriageShitabarai.CalcPrice;
                    model.SeikyuUnchin = uriageShitabarai.ShiharaiPrice;
                    model.Tatekaekin = model.PreviousTatekaekin = uriageShitabarai.Tatekaekin;
                    model.Warimashi1 = model.PreviousWarimashi1 = uriageShitabarai.WarimashiPrice;
                    model.Warimashi2 = model.PreviousWarimashi2 = 0;
                    model.Warimashi3 = model.PreviousWarimashi3 = 0;
                    model.Warimashi4 = model.PreviousWarimashi4 = 0;
                    model.Warimashi5 = model.PreviousWarimashi5 = 0;
                    model.SeikyuTotal = model.PreviousSeikyuTotal = uriageShitabarai.CalcPrice;
                }

                return await PartialViewAsJson("IndexModal", model, true);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// 指定された予社支店IDに基づいて予社支店情報を取得
        /// </summary>
        /// <param name="yosyaBranchId">予社支店ID</param>
        /// <returns>予社支店情報</returns>
        private async Task<Dto.M_Yosya_Branch_Local> GetMYosyaBranch(int yosyaBranchId)
        {
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            return await api.GetMYosyaBranch(yosyaBranchId);
        }

        /// <summary>
        /// M_Unitの取得
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <returns>単位データのリスト</returns>
        private async Task<List<Dto.M_Unit_Local>> GetUnit(int iCompanyID)
        {
            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            IEnumerable<Dto.M_Unit_Local> unitData = await api.M_UnitList(iCompanyID);
            return unitData.ToList();
        }
        /// <summary>
        /// JSON：集計データの返却
        /// </summary>
        /// <param name="model">集計データモデル</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> ShitabaraiBatchRegistration([FromBody] Dto.ShitabaraiBatchRegistrationModel_Local model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.ShitabaraiPostBatchRegistration(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 下払いバッチキャンセル
        /// </summary>
        /// <param name="model">集計データモデル</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> ShitabaraiBatchCancel([FromBody] Dto.ShitabaraiBatchRegistrationModel_Local model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.ShitabaraiPostBatchCancel(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// ModalApprovalModelの保存
        /// </summary>
        /// <param name="model">モーダル承認モデル</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> PostModalApproval([FromBody] Dto.ShitabaraiModalApprovalModel_Local model)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostModalApproval(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// T_Check_Shitabarai_Doneデータの返却
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック下払いID</param>
        /// <returns>チェック下払い完了データ</returns>
        private async Task<Dto.T_Check_Shitabarai_Done_Local> GetCheckShitabaraiDone(int Check_Shitabarai_ID)
        {
            using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
            Dto.T_Check_Shitabarai_Done_Local seikyuData = await api.GetT_Check_Shitabarai_Done(Check_Shitabarai_ID);
            return seikyuData;
        }

        /// <summary>
        /// ApprovalStatusModelの保存
        /// </summary>
        /// <param name="model">承認ステータスモデル</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> UpdateApprovalStatus([FromBody] Dto.ShitabaraiApprovalStatusModel_Local model)
        {
            try
            {
                using API.WebApp.ShitabaraiDataCheckApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.UpdateApprovalStatus(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }
    }
}
