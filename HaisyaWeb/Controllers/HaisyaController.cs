using HaisyaWeb.Common;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
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
using static HaisyaWeb.Models.HaisyaModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 配車コントローラー
    /// </summary>
    [Authorize]
    public class HaisyaController : BaseController
    {
        private readonly ILogger<HaisyaController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="signInManager">サインインマネージャー</param>
        /// <param name="viewRenderService">ビューのレンダリングサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public HaisyaController(ILogger<HaisyaController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _logger = logger;
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 配車表Index
        /// </summary>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 配車台帳Index
        /// </summary>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> LedgerIndex()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 車番連絡Index
        /// </summary>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> CarNumberList()
        {
            try
            {
                DataListModel model = await CreateModel();
                model.Search.BackMenuAction = "DataList";
                model.SyoriKubun = 1;
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// JSON：車番連絡一覧データHTMLの返却
        /// </summary>
        /// <param name="param">検索モデル</param>
        /// <param name="sortParam">ソートパラメータ</param>
        /// <returns>ビュー</returns>
        [HttpGet]
        public async Task<IActionResult> JsonGetCarNumberDataList(SearchModelForHaisya param, DataListSortModel sortParam)
        {
            try
            {
                DataListModel model = new()
                {
                    SortParam = sortParam,
                };

                V_LoginUser_Local loguinUser = await GetLoginUser();

                if (param == null) { return null; }

                // param.SelectTantouが"ALL"の場合は0に、それ以外の場合はintに変換する
                int.TryParse(param.SelectTantou, out int tantouId);

                API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                List<SyabanRenrakuModel> renrakuModel = await api.GetSyabanRenraku(loguinUser.Company_ID, tantouId, DateTime.Parse(param.SelectDay), DateTime.Parse(param.SelectEndDay), param.SelectFilter);
                model.SyabanRenrakuLists = renrakuModel;

                string html = "../Haisya/DataListForNone";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 配車データを取得する
        /// </summary>
        /// <param name="Haisya_ID">配車のId</param>
        /// <returns>配車データ</returns>
        [HttpGet]
        public async Task<IActionResult> GetHaisyaData(int Haisya_ID)
        {
            try
            {
                API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                T_Haisya_Local haisya = await api.GetHaisyaData(Haisya_ID);
                T_Haisya_Yosya_Local haisyaYosya = await api.GetHaisyaYosyaData(Haisya_ID);

                var dataResult = new
                {
                    Haisya = haisya,
                    HaisyaYosya = haisyaYosya
                };

                return Json(new { data = dataResult, errorMessage = "" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 配車表の乗務員リスト表示画面を返却
        /// </summary>
        /// <param name="param">検索モデル</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForHaisya param)
        {
            try
            {
                //ログイン情報取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Haisya");

                //初期値の設定
                DataListModel model = await CreateModel(param);

                model.Search.SelectStatus = param.SelectStatus;


                string strDateFrom = DateTime.Parse(param.SelectDay).AddDays(-5).ToString("yyyy/MM/dd");
                string strDateTo = DateTime.Parse(param.SelectDay).AddDays(model.spaceDay).AddSeconds(-1).ToString("yyyy/MM/dd HH:mm:ss");
                //乗務員一覧の取得
                List<Dto.V_CompanyDriver_Local> drivers = await GetDriverDataLists(loguinUser.Company_ID, DateTime.Parse(param.SelectDay), model.Search.SelectTantou);
                drivers = drivers.Where(h => h.Driver_ID != 0).ToList();

                model.HaisyaDataLists = new();
                //案件配車リストの取得
                List<Dto.V_HaisyaDataList_Local> haisyaList = await GetHaisyaDataLists(loguinUser.Company_ID, null, strDateFrom, strDateTo);

                haisyaList.ForEach(item =>
                {
                    V_HaisyaDataListDto dto = new();
                    CopyProperty(dto, item);
                    // 更新フラグ　日報処理を開始したデータは変更不可。
                    dto.EditFlg = item.Renraku_Kubun == 0 ? true : false;
                    model.HaisyaDataLists.Add(dto);
                });

                //配車日
                model.SelectDay = DateTime.Parse(param.SelectDay);
                //総労働時間取得
                model.KintaiTotalWorkingTimeLists = await GetKintaiTotalWorkingTimeList(loguinUser.Company_ID, DateTime.Parse(strDateFrom).ToString("yyyy/MM/01"));
                //勤怠確定情報取得
                model.KintaiCommitLists = await GetKintaiCommitLists(loguinUser.Company_ID, strDateFrom, DateTime.Parse(strDateTo).AddSeconds(1).ToString("yyyy/MM/dd"));
                // 休暇マスタ
                model.LeaveList = await GetLeaveLists(loguinUser.Company_ID);
                //休日情報の取得
                List<M_Holiday_Local> holidayList = await GetHolidayLists(loguinUser.Company_ID, strDateFrom, strDateTo);
                //残有休休暇日数の取得
                List<T_Leave_Summary_Local> kintaiLeaveSumList = await GetKintaiLeaveLists(loguinUser.Company_ID, 0);
                //勤怠情報取得
                List<T_Kintai_Local> kintai = await GetKintaiLists(strDateFrom, 0);

                //var beforeDay = DateTime.Parse(param.SelectDay).AddDays(-1).ToString("yyyy/MM/dd");
                //List<Dto.M_CompanyUser_Group_Local> listGroupOfUser = await GetCompanyUserGroupList(loguinUser.User_ID);

                //乗務員一覧のV_DriverListDtoをリストに設定
                drivers.ForEach(item =>
                {
                    Dto.V_DriverListDto dto = new() { V_Ankens = new(), };
                    CopyProperty(dto, item);


                    //TODO:いずれDriver_IDに変更
                    //残有休日数
                    T_Leave_Summary_Local leave = kintaiLeaveSumList.Find(h => h.乗務員CD == item.Employee_Number);
                    dto.Leave_Days = leave != null ? leave.Leave_Days : 0;
                    //TODO:いずれDriver_IDに変更
                    //総労働時間
                    V_TOTAL_WORKING_TIME_Local total = model.KintaiTotalWorkingTimeLists.FirstOrDefault(h => h.WORKER_CD == item.Employee_Number);
                    if (total != null) dto.Restraint_Time = total.TOTAL_TIME_DISPLAY;
                    //更新権限　
                    dto.EditFlg = roleList.Count != 0 && roleList.First().Enabled;       ///// && listGroupOfUser.Any(g => g.Group_ID == item.Group_ID)

                    T_Kintai_Local kintaiInfo = kintai.FirstOrDefault(m => m.Driver_ID == item.Driver_ID);
                    if (kintaiInfo != null)
                    {
                        dto.RenzokuWorkDays = kintaiInfo.Renzoku_Days;
                        dto.KousokuTime = kintaiInfo.Work_Time;
                    }

                    //？？？
                    //item.IsReported =　？？？

                    //確定配車件数
                    dto.countHaisyaConfirm = model.HaisyaDataLists.Count(h => h.Driver_ID == item.Driver_ID
                                                                    && h.DriverSyaryo_ID == item.DriverSyaryo_ID
                                                                    && h.Haisya_Kubun == item.JISYA_YOSYA_KUBUN
                                                                    && h.Display_Kubun == 2
                                                                    && h.Haisya_Status == 1);
                    //配車件数
                    dto.countHaisya = model.HaisyaDataLists.Count(h => h.Driver_ID == item.Driver_ID
                                                                    && h.DriverSyaryo_ID == item.DriverSyaryo_ID
                                                                    && h.Haisya_Kubun == item.JISYA_YOSYA_KUBUN
                                                                    && h.Display_Kubun == 2);
                    //
                    dto.V_Ankens = model.HaisyaDataLists.Where(h => h.Driver_ID == item.Driver_ID
                                                                    && h.DriverSyaryo_ID == item.DriverSyaryo_ID
                                                                     && h.Day == model.SelectDay).ToList();

                    model.DriverDataLists.Add(dto);
                });

                //List<Dto.M_CompanyUser_Group_Local> listGroupSearch = (model.Search.SelectTantou != "ALL" && model.Search.SelectTantou != null)
                //    ? await GetCompanyUserGroupList(int.Parse(model.Search.SelectTantou))
                //    : new();

                //案件情報を画面の抽出条件に合わせてフィルター
                model.HaisyaDataLists = model.HaisyaDataLists.Where(a =>
                {
                    if (model.Search.SelectTantou != "ALL" && model.Search.SelectTantou != null && a.TantouID != int.Parse(model.Search.SelectTantou)) return false;    /*&& !listGroupSearch.Any(g => g.Group_ID == a.TantouID)*/
                    if (param.SelectTokuisakiID != null && a.KokyakuId != int.Parse(param.SelectTokuisakiID)) return false;
                    if (param.SelectSyasyu != null && a.Syaryo_ID.ToString() != param.SelectSyasyu) return false;
                    if (param.SelectKata != null && a.Kata != param.SelectKata) return false;
                    //if (param.SelectStatus == null || param.SelectStatus.Count() == 0) return false;
                    //if (!param.SelectStatus.Contains(a.Anken_Status)) return false;
                    return true;
                }).ToList();

                //乗務員一覧を画面の抽出条件に合わせてフィルター
                model.DriverDataLists = model.DriverDataLists.Where(d =>
                {
                    if (model.Search.SelectTantou != "ALL" && model.Search.SelectTantou != null && d.Group_ID != int.Parse(model.Search.SelectTantou)) return false;       /* && !listGroupSearch.Any(g => g.Group_ID == d.Group_ID)*/
                    if (param.SelectSyasyu != null && d.Syasyu.ToString() != param.SelectSyasyu) return false;
                    if (param.SelectKata != null && d.Kata != param.SelectKata) return false;

                    if (param.SelectFilter == 1 && d.countHaisyaConfirm == 0) return false;
                    if (param.SelectFilter == 2 && d.countHaisya - d.countHaisyaConfirm == 0) return false;
                    if (param.SelectFilter == 3 && d.countHaisya != 0) return false;
                    return true;
                }).ToList();

                if (param.SortOrder != 0)
                {
                    model.HaisyaDataLists = param.SortOrder switch
                    {
                        2 => model.HaisyaDataLists.OrderBy(a => a.HaisyaKubunDisplay).ToList(),
                        3 => model.HaisyaDataLists.OrderBy(a => a.Start_Address).ToList(),
                        4 => model.HaisyaDataLists.OrderBy(a => a.Start_Post_code).ToList(),
                        5 => model.HaisyaDataLists.OrderBy(a => a.KokyakuName).ToList(),
                        _ => model.HaisyaDataLists.OrderBy(a => a.Insert_Datetime).ToList(),
                    };
                }

                return await PartialViewAsJson("HaisyaList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 案件新規割当て処理
        /// </summary>
        /// <param name="param">配車データ</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> AddNewHaisyaData(Dto.T_Haisya_Local param)
        {
            try
            {
                if (param.Anken_ID == 0 || param.AnkenDisplay_ID == 0 || param.DriverSyaryo_ID == 0 || param.Driver_ID == 0)
                {
                    throw new Exception("案件新規割当て処理：パラメーターエラー");
                }

                //ログイン情報取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                HaisyaDataModelDto dto = new()
                {
                    Haisya = param,
                    Haisya_Detail = new(),
                    Haisya_Yosya = new(),
                };

                //専属傭車の場合
                if (param.Haisya_Kubun != 1)
                {
                    Dto.T_Haisya_Yosya_Local yosya = new()
                    {
                        Yosya_Branch_ID = param.Driver_ID,
                        YosyaDriverSyaryo_ID = param.DriverSyaryo_ID,
                        Yosya_Sort = 0,
                        Yosya_Tantou_ID = 0,
                    };
                    dto.Haisya_Yosya.Add(yosya);
                }

                //今後の拡張の為のテーブルです。
                Dto.T_Haisya_Detail_Local detail = new()
                {
                    Haisya_ID = 0,
                    Haisya_Detail_ID = 0,
                };
                dto.Haisya_Detail = detail;

                dto.Haisya.Company_ID = loguinUser.Company_ID;
                dto.Haisya.Insert_User = loguinUser.LoginUser_ID;
                dto.Haisya.Update_User = loguinUser.LoginUser_ID;

                // データ登録
                API.WebApp.HaisyaDataApi haisyaDataApi = new(_mapApiSettiong);
                var result = await haisyaDataApi.AddNewHaisyaData(dto);

                return Json(new { data = result, errorMessage = result.ErrrMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 配車割当て解除処理
        /// </summary>
        /// <param name="Haisya_ID">配車ID</param>
        /// <param name="Anken_ID">案件ID</param>
        /// <param name="AnkenDisplay_ID">案件表示ID</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> UnassignHaisyaData(int Haisya_ID, int Anken_ID, int AnkenDisplay_ID)
        {
            try
            {
                if (Haisya_ID == 0 || Anken_ID == 0 || AnkenDisplay_ID == 0) { throw new Exception("割当て解除処理：パラメーターエラー"); }

                Dto.T_Haisya_Local haisya = new()
                {
                    Haisya_ID = Haisya_ID,
                    Anken_ID = Anken_ID,
                    AnkenDisplay_ID = AnkenDisplay_ID,
                };

                // データ登録
                API.WebApp.HaisyaDataApi haisyaDataApi = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await haisyaDataApi.UnassignHaisyaData(haisya);

                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 案件移動処理
        /// </summary>
        /// <param name="param">移動パラメータ</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> MoveHaisyaData(Dto.T_Haisya_Move_Param param)
        {
            try
            {
                //ログイン情報取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                // データ登録
                API.WebApp.HaisyaDataApi haisyaDataApi = new(_mapApiSettiong);

                HaisyaModel.HaisyaDataModelDto dto = new()
                {
                    //AnkenDataList = new(),
                    Haisya = new(),
                    Haisya_Detail = new(),
                    Haisya_Del = new(),
                };

                Dto.T_Haisya_Local haisya = new()
                {
                    Anken_ID = param.Anken_ID,
                    AnkenDisplay_ID = param.AnkenDisplay_ID,
                    Driver_ID = param.Driver_ID,
                    DriverSyaryo_ID = param.DriverSyaryo_ID,
                    SyaryoManagement_ID = param.SyaryoManagement_ID,
                    SyaryoManagement_ID1 = param.SyaryoManagement_ID1,
                    Insert_User = loguinUser.LoginUser_ID,
                    Update_User = loguinUser.LoginUser_ID,
                };
                dto.Haisya = haisya;

                Dto.T_Haisya_Local haisyadel = new()
                {
                    Haisya_ID = param.Haisya_ID_From,
                };
                dto.Haisya_Del = haisyadel;

                Dto.MsterDataCommonResultValDto_Local result = null;
                result = await haisyaDataApi.MoveHaisyaData(dto);

                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 配車ステータス更新処理
        /// </summary>
        /// <param name="Haisya_ID">配車ID</param>
        /// <param name="Status">ステータス</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateStatusHaisya(int Haisya_ID, int Status)
        {
            try
            {
                // データ登録
                API.WebApp.HaisyaDataApi haisyaDataApi = new(_mapApiSettiong);

                MsterDataCommonResultValDto_Local result = await haisyaDataApi.UpdateStatusHaisya(Haisya_ID, Status);

                return Json(new { data = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 指定AnkenIDの配車済み判定を返却
        /// </summary>
        /// <param name="Anken_ID">案件ID</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> CheckEnableContact(int Anken_ID)
        {
            try
            {
                // データ登録
                API.WebApp.HaisyaDataApi haisyaDataApi = new(_mapApiSettiong);

                ContactAbility result = await haisyaDataApi.CheckEnableContact(Anken_ID);

                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 配車台帳の荷物情報と乗務員の一覧画面を返却
        /// </summary>
        /// <param name="param">検索モデル</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> JsonGetLedgerDataList(SearchModelForHaisya param)
        {
            try
            {
                //ログイン情報取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Haisya");

                DataListModel model = await CreateModel(param);
                string strDateFrom = param.SelectDay;
                string strDateTo = DateTime.Parse(param.SelectDay).AddDays(1).AddSeconds(-1).ToString("yyyy/MM/dd HH:mm:ss");
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;
                model.Search.SelectTantou = param.SelectTantou;

                API.WebApp.MasterDataApi masterApi = new(_mapApiSettiong);
                API.WebApp.AnkenDataApi ankenApi = new(_mapApiSettiong);

                List<Dto.V_CompanyDriver_Local> drivers = await GetDriverDataLists(loguinUser.Company_ID, DateTime.Parse(param.SelectDay), model.Search.SelectTantou);
                drivers = drivers.Where(h => h.Driver_ID != 0).ToList();

                //配車日
                model.SelectDay = DateTime.Parse(param.SelectDay);
                model.HaisyaDataLists = new();

                List<Dto.V_HaisyaDataList_Local> haisyaList = await GetHaisyaDataLists(loguinUser.Company_ID, null, strDateFrom, strDateTo);
                //指定日以前開始の案件データを
                List<Dto.V_HaisyaDataList_Local> haisyaUnderList =  await GetHaisyaDataLists(loguinUser.Company_ID, null, null, null, strDateFrom);

                //勤怠データ
                List<Dto.T_Kintai_Commit_Local> kintaiDataList = await GetKintaiCommitLists(loguinUser.Company_ID, strDateFrom, strDateTo);
                //備考データ
                List<Dto.T_Haisya_Driver_Day_Remark_Local> driverRemarks = await GetHaisyaDriverDayRemarks(strDateFrom, 0);

                //乗務員一覧のV_DriverListDtoをリストに設定
                drivers.ForEach(item =>
                {
                    Dto.V_DriverListDto dto = new() { V_Ankens = new(), };
                    CopyProperty(dto, item);
                    //案件情報の設定
                    dto.V_Ankens = haisyaList.Where(h => h.Driver_ID == item.Driver_ID
                                                && h.DriverSyaryo_ID == item.DriverSyaryo_ID
                                                 && h.Day == model.SelectDay).ToList();
                    //出勤情報の設定
                    T_Kintai_Commit_Local kintai = kintaiDataList?.Find(k => k.乗務員CD == item.Employee_Number && k.勤怠日 == model.SelectDay);
                    if (kintai != null)
                    {
                        dto.HolidayKubun = kintai?.勤務区分 == 2 ? "休" : "出";
                    }
                    // 備考の設定
                    T_Haisya_Driver_Day_Remark_Local remark = driverRemarks.FirstOrDefault(m => m.Driver_ID == item.Driver_ID);
                    if (remark != null) dto.Remark = remark.Remarks;

                    Dto.V_HaisyaDataList_Local under = haisyaUnderList.FirstOrDefault(m => m.Driver_ID == item.Driver_ID);
                    if (under != null) dto.HaisyaUnderData = under;


                    model.DriverDataLists.Add(dto);
                });

                // ログイン者の権限のある配車グループを設定
                List<Dto.M_CompanyUser_Group_Local> groupUserList = await GetCompanyUserGroupList(loguinUser.User_ID);
                model.GroupUserList = groupUserList.Where(m => m.Group_Kubun == 1).ToList();

                // Filter Anken
                // "→配車済み(確定)、配車済み(暫定)はT_Haisya.Haisya_Statusから取得になります。
                // →未配車はT_Haisyaにデータがない荷物情報になります。"
                model.HaisyaDataLists = model.HaisyaDataLists?.Where(item =>
                {
                    //var driver = item.V_Drivers.Count > 0 ? item.V_Drivers[0] : null;
                    //item.haisyaKubunDisplay = item.Anken_Kubun == 1 ? "利用" : driver == null ? "" : driver?.DriverSyaryo_ID != 0 ? "自車" : driver?.YosyaKubun == 1 ? "専属" : "庸車";

                    //if (param.SelectTantou != null && param.SelectTantou != "ALL" && !groupUserList.Any(g => g.Group_ID == item.TantouID)) return false;
                    if (param.SelectTantou != null && param.SelectTantou != "ALL" && int.Parse(param.SelectTantou) != item.TantouID) return false;

                    /*Dto.M_Syaryo_Local syasyu = syasyuList.Find(item => param.SelectSyasyu == item.Syaryo_ID.ToString());
                    if (syasyu != null && (syasyu.SIZE != item.SyasyuSize || syasyu.KATA != item.Kata || syasyu.SYASYU != item.Syasyu)) return false;*/

                    if (param.SelectSyasyu != null && item.Syaryo_ID.ToString() != param.SelectSyasyu) return false;
                    if (param.SelectKata != null && param.SelectKata != item.Kata) return false;
                    //if ((param.SelectFilter == 1 || param.SelectFilter == 2) && item.V_Drivers.Count() == 0) return false;
                    // if (param.SelectFilter == 3 && item.Daisuu <= 0) return false;
                    if (param.SelectTokuisakiID != null && param.SelectTokuisakiID != item.KokyakuId.ToString()) return false;
                    if (param.SelectStatus == null || param.SelectStatus.Count() == 0) return false;
                    if (!param.SelectStatus.Contains(item.Anken_Status)) return false;
                    if (param.SelectFilter == 1 && item.Haisya_Status != 1) return false;
                    if (param.SelectFilter == 2 && item.Haisya_Status != 0) return false;
                    if (param.SelectFilter == 3 && item.Haisya_Status != null) return false;
                    return true;
                }).ToList();


                // Filter Driver
                model.DriverDataLists = model.DriverDataLists?.Where(item =>
                {
                    //if (param.SelectTantou != null && param.SelectTantou != "ALL" && !groupUserList.Any(g => g.Group_ID == item.Group_ID)) return false;
                    if (param.SelectTantou != null && param.SelectTantou != "ALL" && int.Parse(param.SelectTantou) != item.Group_ID) return false;
                    if (param.SelectSyasyu != null && param.SelectSyasyu != item.Syaryo_ID.ToString()) return false;
                    if (param.SelectKata != null && param.SelectKata != item.Kata) return false;
                    //if ((param.SelectFilter == 1 || param.SelectFilter == 2) && item.V_Drivers.Count() == 0) return false;
                    //if ((param.SelectFilter == 3) && item.V_Drivers.Count() != 0) return false;
                    return true;
                }).ToList();

                //Sort
                model.HaisyaDataLists = model.HaisyaDataLists?.OrderBy(item =>
                {
                    return param.SelectSort switch
                    {
                        1 => item.Anken_ID.ToString(),
                        //case 2: return item.haisyaKubunDisplay.ToString();
                        3 => item.Start_Address,
                        4 => item.Start_Post_code,
                        5 => item.KokyakuName,
                        _ => item.Anken_ID.ToString(),
                    };
                }).ToList();

                // Check dragable
                List<Dto.M_CompanyUser_Group_Local> groupUsers = await GetCompanyUserGroupList(loguinUser.User_ID);

                //model.AnkenLists?.ForEach(a => a.EditFlg = model.EditEnabled && a.Anken_Status != 2 && groupUsers.Any(g => g.Group_ID == a.TantouID) && a.Anken_Kubun == 0);
                model.DriverDataLists?.ForEach(d => d.EditFlg = model.EditEnabled && d.HolidayKubun != "休" && groupUsers.Any(g => g.Group_ID == d.Group_ID));

                ViewData.Model = model;
                string viewAnken = await _viewRenderService.RenderToStringAsync(this, "LedgerAnkenList", model, false);
                string viewDriver = await _viewRenderService.RenderToStringAsync(this, "LedgerDriverList", model, false);
                return Json(new[] { new { partialView = viewAnken }, new { partialView = viewDriver } });
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// DataListModelの作成＆返却
        /// </summary>
        /// <param name="param">検索モデル</param>
        /// <returns>DataListModel</returns>
        private async Task<DataListModel> CreateModel(SearchModelForHaisya param = null)
        {
            // ログインユーザー取得
            V_LoginUser_Local loguinUser = await GetLoginUser();
            // 権限データ
            List<M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Haisya");


            param ??= new()
            {
                SelectGroup = 3,
                SelectDay = DateTime.Now.ToString("yyyy/MM/dd"),
                SelectEndDay = DateTime.Now.ToString("yyyy/MM/dd"),
                SelectTantou = await SearchCommonService.GetUserGroupDefaultVal(_mapApiSettiong, loguinUser.Company_ID,
                                                UserGroupLists.Haisya, loguinUser.User_ID) ?? "ALL",
            };

            DataListModel model = new()
            {
                Company_ID = loguinUser.Company_ID,
                DriverDataLists = new(),
            };

            SearchModelForHaisya search = new()
            {
                //MonthSelectList = SearchCommonService.GetYearMonthSelect(),
                TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, model.Company_ID, UserGroupLists.Haisya),
                SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                SortSelectList = GetSortSelect(),
                SelectTantou = param.SelectTantou,
                SelectTab = param.SelectTab,
                SelectDay = param.SelectDay,
                SelectEndDay = param.SelectEndDay,
                SelectGroup = param.SelectGroup,
                SelectSort = param.SelectSort,
            };

            model.Search = search;
            ///再描画設定
            model.RefreshKubun = 3;

            string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
            url += GetMapApiUrlPram();
            model.MapsApiForJSUrl = url;

            return model;
        }

        /// <summary>
        /// 有休画面に必要な情報の取得
        /// </summary>
        /// <param name="driverId">ドライバーID</param>
        /// <param name="date">日付</param>
        /// <param name="employeeNumber">従業員番号</param>
        /// <returns>AttendanceModalViewModel</returns>
        private async Task<AttendanceModalViewModel> CreateAttendanceModel(int driverId, DateTime date, int employeeNumber)
        {

            //ログイン情報取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
            // 権限データ
            List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Haisya");

            using API.WebApp.KinTaiDataApi api = new(_mapApiSettiong);
            AttendanceModalDto dto = await api.GetSelectUserData(driverId, date, employeeNumber);
            List<Dto.M_LEAVE_Local> leave = await api.GetLeaveList(loguinUser.Company_ID);
            if (leave != null) leave = leave.Where(m => m.LEAVE_FLG == true).ToList();
            List<Dto.M_LEAVE_REASON_Local> liaveReason = await api.GetLeaveReasonList(loguinUser.Company_ID);

            List<SelectListItem> listLists = new();
            leave.ForEach((m) => { listLists.Add(new SelectListItem() { Value = m.LEAVE_CD.ToString(), Text = m.LEAVE_NAME, Selected = false }); });

            List<SelectListItem> liaveReasonLists = new();
            liaveReason.ForEach((m) => { liaveReasonLists.Add(new SelectListItem() { Value = m.REASON_CD.ToString(), Text = m.REASON_NAME, Selected = false }); });

            AttendanceModalViewModel dataModel = new()
            {
                CompanyDriver = dto.CompanyDriver,
                KintaiCommit = dto.KintaiCommit,
                LeaveKubun = dto.LeaveKubun,
                LeaveReason = dto.LeaveReason,
                Remark = dto.Remark,
                Date = dto.Date,
                EditEnabledForKintai = dto.EditEnabledForKintai,
                EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                LeaveKubunList = listLists,
                LeaveReasonList = liaveReasonLists,
            };

            if (dto.KintaiCommit != null && dto.KintaiCommit.勤務区分 == 2)
            {
                dataModel.LeaveKubun = dto.KintaiCommit.休暇区分;
                dataModel.LeaveReason = dto.KintaiCommit.休暇理由;
            }

            return dataModel;
        }

        /// <summary>
        /// 型選択リストの返却
        /// </summary>
        /// <returns>型選択リスト</returns>
        private static List<SelectListItem> GetSortSelect()
        {
            List<SelectListItem> list = new();

            list.Add(new SelectListItem() { Value = "1", Text = "登録順", Selected = false });
            list.Add(new SelectListItem() { Value = "2", Text = "区分", Selected = false });
            list.Add(new SelectListItem() { Value = "3", Text = "住所", Selected = false });
            list.Add(new SelectListItem() { Value = "4", Text = "郵便番号", Selected = false });
            list.Add(new SelectListItem() { Value = "5", Text = "荷主", Selected = false });

            return list;
        }

        /// <summary>
        /// 指定UserIDのM_CompanyUser_Grouデータリストを返却する
        /// </summary>
        /// <param name="UserId">ユーザーID</param>
        /// <returns>M_CompanyUser_Groupリスト</returns>
        private async Task<List<Dto.M_CompanyUser_Group_Local>> GetCompanyUserGroupList(int UserId)
        {
            using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
            List<Dto.M_CompanyUser_Group_Local> list = await apiM.GetCompanyUserGroupListFromUserID(UserId);
            if (list == null) list = new();
            return list.ToList();
        }

        /// <summary>
        /// 配車一覧の返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="targetDate">対象日</param>
        /// <param name="targetDateFrom">対象開始日</param>
        /// <param name="targetDateTo">対象終了日</param>
        /// <param name="targetDateUnderLastest">指定日以前開始で指定日以降終了の最新</param>
        /// <returns>配車データリスト</returns>
        private async Task<List<Dto.V_HaisyaDataList_Local>> GetHaisyaDataLists(int CompanyID, string targetDate, string targetDateFrom, string targetDateTo, string targetDateUnderLastest = null)
        {
            using API.WebApp.HaisyaDataApi apiM = new(_mapApiSettiong);
            List<Dto.V_HaisyaDataList_Local> list = await apiM.GetHaisyaDataList(CompanyID, targetDate, targetDateFrom, targetDateTo, 0, 0, 0, 0, 0, targetDateUnderLastest);
            if (list == null) list = new();
            return list.ToList();
        }

        /// <summary>
        /// V_CompanyDriverを作成して返却
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="targetDate">対象日</param>
        /// <param name="haisyaGroupID">配車グループID</param>
        /// <returns>V_CompanyDriverリスト</returns>
        private async Task<List<Dto.V_CompanyDriver_Local>> GetDriverDataLists(int CompanyID, DateTime targetDate, string haisyaGroupID)
        {
            API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);

            int.TryParse(haisyaGroupID, out int groupID);

            List<Dto.V_CompanyDriver_Local> list = await apiM.GetVCompanyDriverList(CompanyID, 1, groupID);
            //入社日
            list = list.Where(m => m.Nyusya_Date == null || (m.Nyusya_Date != null && m.Nyusya_Date <= targetDate)).ToList();
            //業務開始日
            list = list.Where(m => m.GyomuStart_Date == null || (m.GyomuStart_Date != null && m.GyomuStart_Date <= targetDate)).ToList();
            //退職日
            list = list.Where(m => m.Taisyoku_Date == null || (m.Taisyoku_Date != null && m.Taisyoku_Date >= targetDate)).ToList();
            //車輌
            list = list.Where(m => m.Start_Date == null || (m.Start_Date != null && m.Start_Date <= targetDate)).ToList();
            list = list.Where(m => m.End_Date == null || (m.End_Date != null && m.End_Date >= targetDate)).ToList();

            return list;
        }

        /// <summary>
        /// 案件一覧の返却
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="targetDateFrom">対象開始日</param>
        /// <param name="targetDateTo">対象終了日</param>
        /// <returns>案件表示リスト</returns>
        private async Task<List<Dto.T_Anken_Display_Local>> GetAnkenDisplayList(int companyID, string targetDateFrom, string targetDateTo)
        {
            API.WebApp.AnkenDataApi apiA = new(_mapApiSettiong);
            List<Dto.T_Anken_Display_Local> list = await apiA.GetAnkenDisplayList(targetDateFrom, targetDateTo, companyID);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 確定勤怠情報の返却
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="dateFrom">開始日</param>
        /// <param name="dateTo">終了日</param>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>確定勤怠リスト</returns>
        private async Task<List<Dto.T_Kintai_Commit_Local>> GetKintaiCommitLists(int companyID, string dateFrom, string dateTo, int DriverID = 0)
        {
            API.WebApp.KinTaiDataApi apiM = new(_mapApiSettiong);
            List<Dto.T_Kintai_Commit_Local> list = null;
            list = await apiM.GetKintaiCommit(companyID, dateFrom, dateTo, DriverID);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 勤怠システムの休暇マスタの返却
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <returns>休暇マスタリスト</returns>
        private async Task<List<Dto.M_LEAVE_Local>> GetLeaveLists(int companyID)
        {
            using API.WebApp.KinTaiDataApi api = new(_mapApiSettiong);
            List<Dto.M_LEAVE_Local> list = await api.GetLeaveList(companyID);
            list ??= new();
            return list.ToList();
        }


        /// <summary>
        /// V_TOTAL_WORKING_TIMEの返却
        /// 月次の乗務員毎の走行距離、総労働時間
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="nengetu">年月</param>
        /// <returns>総労働時間リスト</returns>
        private async Task<List<Dto.V_TOTAL_WORKING_TIME_Local>> GetKintaiTotalWorkingTimeList(int companyID, string nengetu)
        {
            using API.WebApp.KinTaiDataApi apiM = new(_mapApiSettiong);
            List<Dto.V_TOTAL_WORKING_TIME_Local> list = null;
            list = await apiM.GetKintaiTotalWorkingTimeList(companyID, nengetu);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 残有休日数リストの返却
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="driverID">ドライバーID</param>
        /// <returns>残有休日数リスト</returns>
        private async Task<List<Dto.T_Leave_Summary_Local>> GetKintaiLeaveLists(int companyID, int driverID = 0)
        {
            using API.WebApp.KinTaiDataApi apiM = new(_mapApiSettiong);
            List<Dto.T_Leave_Summary_Local> list = null;
            list = await apiM.GetKintaiLeaveSummary(companyID, driverID);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// T_Kintaiデータの返却
        /// 連続勤務日数、拘束時間
        /// </summary>
        /// <param name="day">日付</param>
        /// <param name="driverID">ドライバーID</param>
        /// <returns>勤怠リスト</returns>
        private async Task<List<Dto.T_Kintai_Local>> GetKintaiLists(string day = null, int driverID = 0)
        {
            using API.WebApp.KinTaiDataApi apiM = new(_mapApiSettiong);
            List<Dto.T_Kintai_Local> list = null;
            list = await apiM.GetKintaiList(day, driverID);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 休日リストの返却
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="dateFrom">開始日</param>
        /// <param name="dateTo">終了日</param>
        /// <returns>休日リスト</returns>
        private async Task<List<Dto.M_Holiday_Local>> GetHolidayLists(int companyID, string dateFrom, string dateTo)
        {
            using API.WebApp.KinTaiDataApi apiM = new(_mapApiSettiong);
            List<Dto.M_Holiday_Local> list = null;
            list = await apiM.GetHolidayList(companyID, dateFrom, dateTo);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 乗務員配車表備考リストの返却
        /// T_Haisya_Driver_Day_Remark_Local
        /// </summary>
        /// <param name="targetDay">対象日</param>
        /// <param name="DriverId">ドライバーID</param>
        /// <returns>備考リスト</returns>
        private async Task<List<Dto.T_Haisya_Driver_Day_Remark_Local>> GetHaisyaDriverDayRemarks(string targetDay, int DriverId = 0)
        {
            using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
            List<Dto.T_Haisya_Driver_Day_Remark_Local> list = await apiH.GetHaisyaDriverDayRemarks(targetDay, DriverId);
            list ??= new();
            return list.ToList();
        }

        /// <summary>
        /// 個別月次配車表に必要な情報の取得
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="date">日付</param>
        /// <returns>MonthScheduleModelViewModel</returns>
        private async Task<MonthScheduleModelViewModel> CreateMonthScheduleModel(int userId, DateTime date)
        {
            //ログイン情報取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            // カレンダー表示範囲を計算
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
            // ドライバー情報取得
            List<V_CompanyDriver_Local> drivers = await apiM.GetVCompanyDriverList(loguinUser.Company_ID, 0, 0, userId)
                ?? throw new Exception("パラメーターエラー：userId:" + userId.ToString());
            V_CompanyDriver_Local driver = drivers.FirstOrDefault();

            using API.WebApp.KinTaiDataApi api = new(_mapApiSettiong);
            //　確定勤怠情報取得
            List<T_Kintai_Commit_Local> KyukaDataLists = await api.GetKintaiCommit(loguinUser.Company_ID,
                                    startDate.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"), (int)driver.Employee_Number);
            if (KyukaDataLists != null) KyukaDataLists = KyukaDataLists.Where(m => m.勤務区分 == 2).ToList();

            //　乗務外情報取得
            List<T_KINTAI_COMMIT_NON_CREW_Local> NocrewDataLists = await api.GetKintaiCommitNonCrew(loguinUser.Company_ID,
                                    startDate.ToString("yyyy/MM/dd"), endDate.ToString("yyyy/MM/dd"), (int)driver.Employee_Number);
            //　月次勤務情報取得
            List<Dto.V_TOTAL_WORKING_TIME_Local> workingTimeLists = await api.GetKintaiTotalWorkingTimeList(loguinUser.Company_ID,
                                    startDate.ToString("yyyy/MM/dd"), (int)driver.Employee_Number);
            Dto.V_TOTAL_WORKING_TIME_Local workingTime = workingTimeLists.FirstOrDefault();
            workingTime ??= new();
            List<M_LEAVE_Local> leaves = await api.GetLeaveList(loguinUser.Company_ID);


            using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
            //　配車情報取得
            List<V_HaisyaDataList_Local> HaisyaDataLists = await apiH.GetHaisyaDataList(loguinUser.Company_ID, null, startDate.ToString("yyyy/MM/dd"),
                                               endDate.ToString("yyyy/MM/dd"), 0, 0, 0, userId);


            // リストをJSON形式に変換
            string KyukaDatas = "";
            string json = "";
            foreach (var kvp in KyukaDataLists)
            {
                M_LEAVE_Local leavedata = leaves.FirstOrDefault(m => m.LEAVE_CD == kvp.休暇区分);
                string leaveNm = leavedata != null ? leavedata.ABBR_NAME : "";
                json += $"'{kvp.勤怠日.Day}': '{string.Join(",", leaveNm)}',";
            }
            KyukaDatas = "{" + json.TrimEnd(',') + "}";

            string NocrewDatas = "";
            json = "";
            foreach (var kvp in NocrewDataLists)
            {
                json += $"'{kvp.勤怠日.Day}': '{string.Join(",", "乗務外")}',";
            }
            NocrewDatas = "{" + json.TrimEnd(',') + "}";

            var AnkenDatas = HaisyaDataLists.AsEnumerable()
                      .GroupBy(d => d.Day)
                      .ToDictionary(
                          g => g.Key.Day.ToString(), // グループのキー（各日付）を文字列形式で取得します。
                          g => g.Select(d => new
                          {
                              AnkenRange = $"{d.KokyakuName}",  // 開始ビル名と終了ビル名を結合して、範囲を表現します。
                              Length = (d.EndDatetime - d.StartDatetime).Days + 1, // データの長さを取得します。
                              CrossesMultipleMonths = d.StartDatetime < startDate // 複数の月を跨ぐかどうかの情報を取得します。
                          }).ToArray() // 各グループ内の情報を配列形式で取得します。
                       );

            MonthScheduleModelViewModel dataModel = new MonthScheduleModelViewModel
            {
                DriverName = driver.Display_Name,
                EmployeeNumber = driver.Employee_Number ?? 0,
                DriverBranch = driver.Branch_Name_Abbr,
                RemainingMonthlyWorkingTime = null,
                TotalWorkingTime = null,
                TotalLaborTime = workingTime.TOTAL_TIME_DISPLAY ?? "00:00",
                TotalOvertime = workingTime.TOTAL_ZANGYO_TIME_DISPLAY ?? "00:00",
                TotalPublicHolidayWorkingTime = workingTime.TOTAL_KOKYU_TIME_DISPLAY ?? "00:00",
                TotalLegalHolidayWorkingTime = workingTime.TOTAL_HOUTEIKYU_TIME_DISPLAY ?? "00:00",
                TotalLateNightWorkingTime = workingTime.TOTAL_JITSUSHINYA_TIME_DISPLAY ?? "00:00",
                TotalTravelDistance = Math.Round((double)(workingTime.DISTANCE ?? 0), 1),
                AnkenData = JsonConvert.SerializeObject(AnkenDatas),
                KyukaData = KyukaDatas,
                NocrewData = NocrewDatas,
                DriverID = userId,
                Date = startDate,
            };

            return dataModel;

        }

        /// <summary>
        /// 個別月次配車表画面を表示
        /// </summary>
        /// <param name="kubun">区分</param>
        /// <param name="driverID">ドライバーID</param>
        /// <param name="selectedDate">選択日</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> MonthScheduleCalendarModal(string kubun, int driverID, DateTime selectedDate)
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);
                // selectedDateに値が入っていない場合は現在の時刻からカレンダーの情報を取得
                if (selectedDate == DateTime.MinValue)
                {
                    MonthScheduleModelViewModel model = await CreateMonthScheduleModel(driverID, startDate);
                    if ("yosya".Equals(kubun))
                    {
                        return await PartialViewAsJson("../CharterRirekiModal/Index", model, true);
                    }
                    else
                    {
                        return await PartialViewAsJson("../MonthCalendarModal/Index", model, true);
                    }
                }
                else
                {
                    MonthScheduleModelViewModel model = await CreateMonthScheduleModel(driverID, selectedDate);
                    return await PartialViewAsJson("../MonthCalendarModal/Index", model, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 個別月次配車表（モーダル）画面の明細部分の表示
        /// </summary>
        /// <param name="driverID">ドライバーID</param>
        /// <param name="selectedDate">選択日</param>
        /// <param name="employeeNumber">従業員番号</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMonthScheduleCalendar(int driverID, DateTime selectedDate, int employeeNumber)
        {
            try
            {
                MonthScheduleModelViewModel model = await CreateMonthScheduleModel(driverID, selectedDate);
                return await PartialViewAsJson("../MonthCalendarModal/MonthCalendar", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 個別月次配車表（モーダル）画面の表示
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="driverId">ドライバーID</param>
        /// <param name="employeeNumber">従業員番号</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> AttendanceModal(DateTime date, int driverId, int employeeNumber)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 引数で受け取った日付を、データベースで扱うのに適した形式（00:00:00の時刻）に変換
                DateTime databaseCompliantDateTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

                AttendanceModalViewModel model = await CreateAttendanceModel(driverId, databaseCompliantDateTime, employeeNumber);

                return await PartialViewAsJson("../MonthCalendarModal/AttendanceModal", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 休暇情報の登録処理
        /// </summary>
        /// <param name="param">AttendanceModalViewModel</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> RegisterAttendanceModal(AttendanceModalViewModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                param.UpdateUserId = loguinUser.User_ID;

                API.WebApp.KinTaiDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.PostRegisterAttendanceModal(param);

                return Json(new { result = result.RetrunFlg, message = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { retrunFlg = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 車番連絡用のV_HaisyaDataListを返却
        /// </summary>
        /// <param name="syabanRenrakuModel3">車番連絡モデル</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> CarNumberModal([FromBody] SyabanRenrakuModel3 syabanRenrakuModel3)
        {
            try
            {
                // ログインユーザー取得
                V_LoginUser_Local loguinUser = await GetLoginUser();

                List<V_HaisyaDataList_Local> carNumberLists = await GetCarNumberLists(
                    loguinUser.Company_ID,
                    syabanRenrakuModel3.CustomerId,
                    syabanRenrakuModel3.TantouId,
                    syabanRenrakuModel3.TargetDate,
                    syabanRenrakuModel3.GroupID,
                    syabanRenrakuModel3.HaisyaID
                    );

                SyabanRenrakuCustomModel model = new SyabanRenrakuCustomModel
                {
                    SyabanRenrakuModel3 = syabanRenrakuModel3,
                    CarNumberLists = carNumberLists
                };

                return await PartialViewAsJson("../Haisya/CarNumberListModal", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// V_HaisyaDataListの返却
        /// </summary>
        /// <param name="companyId">会社ID</param>
        /// <param name="customerId">顧客ID</param>
        /// <param name="customerTantouId">顧客担当ID</param>
        /// <param name="targetDate">対象日</param>
        /// <param name="groupID">グループID</param>
        /// <param name="Ids">ID</param>
        /// <returns>V_HaisyaDataListリスト</returns>
        private async Task<List<Dto.V_HaisyaDataList_Local>> GetCarNumberLists(int companyId, int customerId, int customerTantouId, string targetDate, int groupID, int Ids)
        {
            API.WebApp.HaisyaDataApi apiM = new(_mapApiSettiong);
            List<Dto.V_HaisyaDataList_Local> list = await apiM.GetSyabanRenrakuList(companyId, customerId, customerTantouId, targetDate, groupID, Ids);
            return list ?? new();
        }

        /// <summary>
        /// 運行指示書の取得
        /// </summary>
        /// <param name="AnkenID">案件ID</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> GetOperationInstructions(int AnkenID)
        {
            try
            {
                API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                OperationInstructionsModel renrakuModel = await api.GetOperationInstructions(AnkenID);
                return await PartialViewAsJson("./OperationInstructionsModal", renrakuModel, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 配車連絡情報登録処理
        /// </summary>
        /// <param name="syabanRenrakuModel2">配車連絡モデルリスト</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> PostSyabanRenraku([FromBody] List<SyabanRenrakuModel2> syabanRenrakuModel2)
        {
            try
            {
                V_LoginUser_Local loginUser = await GetLoginUser();
                // 配車連絡情報を格納するためのリストを初期化
                List<SyabanRenrakuPostModel> syabanRenrakuPostList = new();
                // 受け取った配車連絡情報のモデルリストを処理
                for (int i = 0; i < syabanRenrakuModel2.Count; i++)
                {
                    // T_Haisya_SyabanRenraku_Local のインスタンスを作成
                    T_Haisya_SyabanRenraku_Local syabanRenraku = new()
                    {
                        //ログイン者の会社ID
                        Company_ID = loginUser.Company_ID,
                        PrintDate = syabanRenrakuModel2[i].PrintDate,
                        Group_ID = syabanRenrakuModel2[i].Group_ID,
                        Day = syabanRenrakuModel2[i].Day.TryParseDate(out var dt) ? dt.Value : DateTime.MinValue,
                        // 顧客担当ID
                        Tantou_ID = syabanRenrakuModel2[i].TantouId,
                        Customer_Branch_ID = syabanRenrakuModel2[i].CustomerBranchId,
                        Mail_Address1 = syabanRenrakuModel2[i].MailAddress1,
                        Mail_Address2 = syabanRenrakuModel2[i].MailAddress2,
                        //ログイン車のユーザーID
                        Insert_User = loginUser.User_ID,
                    };
                    // T_Anken_SyabanRenraku_Local のインスタンスを作成
                    T_Anken_SyabanRenraku_Local ankenSyabanRenraku = new()
                    {
                        Anken_ID = syabanRenrakuModel2[i].AnkenId,
                        Renraku_Kubun = syabanRenrakuModel2[i].RenrakuKubun,
                        Remarks = syabanRenrakuModel2[i].Remarks
                    };
                    // T_Haisya_SyabanRenraku_Detail_Local のインスタンスを作成
                    T_Haisya_SyabanRenraku_Detail_Local haisyaSyabanRenrakuDetail = new()
                    {
                        Anken_ID = syabanRenrakuModel2[i].AnkenId,
                        Remarks = syabanRenrakuModel2[i].Remarks
                    };

                    // SyabanRenrakuPostModel のインスタンスを作成
                    SyabanRenrakuPostModel syabanRenrakuPostModel = new()
                    {
                        HaisyaSyabanRenraku = syabanRenraku,
                        AnkenSyabanRenraku = ankenSyabanRenraku,
                        HaisyaSyabanRenrakuDetail = haisyaSyabanRenrakuDetail,
                        // 備考をカンマで分割してリストに変換
                        RemarksList = syabanRenrakuModel2[i].RemarksList?.Split(',').ToList(),
                    };
                    syabanRenrakuPostList.Add(syabanRenrakuPostModel);
                }
                using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                await api.PostSyabanRenraku(syabanRenrakuPostList);
                return Ok();
            }
            catch (Exception x)
            {
                return Json(new { partialView = "", errorMessage = x.Message });
            }
        }
    }
}
