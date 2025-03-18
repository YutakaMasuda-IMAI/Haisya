using HaisyaWeb.API.Map;
using HaisyaWeb.API.WebApp;
using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;
using static HaisyaWeb.Models.AnkenModel;
using static HaisyaWeb.Models.MapApiModel;

namespace HaisyaWeb.Controllers.Anken
{
    [Authorize]
    public class AnkenRegisterController : BaseController
    {
        private readonly ILogger<AnkenRegisterController> _logger;

        private const string SessionCopyAnken = "_objCopyAnkenInfo";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public AnkenRegisterController(ILogger<AnkenRegisterController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Index処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(AnkenBaseModel param, int ankenId = 0)
        {
            try
            {
                AnkenRegisterModel model = new();

                if (ankenId > 0)
                {
                    model = await CreateModel();
                    await GetEditData(ankenId, model);
                    if (param.SenzokuID > 0)
                    {
                        model.SenzokuID = param.SenzokuID;
                        model.SenzokuDriverID = param.SenzokuDriverID;
                        model.TargetDate = param.TargetDate;
                        model.SelectMonth = param.SelectMonth;
                        model.TantouID = param.TantouID;
                        model.HaisyaPlanKubun = 4;
                    }
                }

                if (param.BackMenuAction != null && param.BackMenuAction.Length > 0)
                {
                    model.SelectDay = param.SelectDay;
                    model.SelectTantou = param.SelectTantou;
                    model.SelectSyasyu = param.SelectSyasyu;
                    model.SelectKata = param.SelectKata;
                    model.SelectGroup = param.SelectGroup;
                    model.BackMenuAction = param.BackMenuAction;
                    model.BackMenuAction2 = param.BackMenuAction;
                }

                if (ankenId > 0)
                {
                    if (model.Anken_Kubun == 1)
                    {
                        return View("RiyoUnsoIndex", model);
                    }
                    else if (model.Anken_Kubun == 2)
                    {
                        return View("SenzokuIndex", model);
                    }
                    else
                    {
                        return View("RegisterIndex", model);
                    }
                }
                else
                {
                    return View(param);
                }
            }
            catch (Exception ex)
            {
                 return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 案件コピー処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecCopyAnkenForKako(AnkenRegisterModel param)
        {
            try
            {
                if (param.Anken_ID_KakoSelect == 0) { throw new Exception("パラメーターエラー：CopyAnkenID:" + param.Anken_ID_KakoSelect.ToString()); }

                AnkenRegisterModel model = new();
                model = await CreateModel();
                await GetEditData(param.Anken_ID_KakoSelect, model);
                model.SelectDay = param.SelectDay;
                if (DateTime.TryParse(model.SelectDay, out DateTime date))
                {
                    foreach (var target in model.PointList)
                    {
                        target.PointDate = date;
                        target.PointDateTime = DateTime.Parse((DateTime.Parse(model.SelectDay)).ToString("yyyy/MM/dd") + ' ' + target.PointTime);
                    }
                }

                if (param.SenzokuID > 0)
                {
                    model.SenzokuID = param.SenzokuID;
                    model.SenzokuDriverID = param.SenzokuDriverID;
                    model.TargetDate = param.TargetDate;
                    model.SelectMonth = param.SelectMonth;
                    model.HaisyaPlanKubun = 4;
                }

                HttpContext.Session.Remove(SessionCopyAnken);
                HttpContext.Session.SetObject(SessionCopyAnken, model);
                return Redirect("CopyAnkenIndex");
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 案件コピー処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExecCopyAnken(AnkenRegisterModel param)
        {
            try
            {
                HttpContext.Session.Remove(SessionCopyAnken);
                HttpContext.Session.SetObject(SessionCopyAnken, param);
                return Redirect("CopyAnkenIndex");
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// コピー案件新規画面にリダイレクト
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CopyAnkenIndex()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                //セッション情報からデータの取得
                AnkenRegisterModel param = HttpContext.Session.GetObject<AnkenRegisterModel>(SessionCopyAnken)
                    ?? throw new Exception("セッション情報が不正です。");
                AnkenRegisterModel model = new();

                model = await CreateModel(param);
                model.Anken_ID = 0;
                model.Anken_No = null;
                model.Anken_Status = 0;
                model.Anken_Latest_Order = 0;
                model.PageType = "CopyAnken";

                HttpContext.Session.Remove(SessionCopyAnken);

                return View("RegisterIndex", model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 案件登録画面を開く
        /// </summary>
        /// <param name="param">AnkenBaseModel</param>
        /// <param name="ankenId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RegisterIndex(AnkenBaseModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                AnkenRegisterModel model = await CreateModel();
                model.Company_ID = loguinUser.Company_ID;
                model.BackMenuAction = "Home";
                model.BackMenuAction2 = "Home";
                model.HaisyaPlanKubun = 1; //配車予定：自車

                if (param.SelectDay != null)
                {
                    model.SelectDay = param.SelectDay;
                    model.SelectTantou = param.SelectTantou;
                    model.SelectSyasyu = param.SelectSyasyu;
                    model.SelectKata = param.SelectKata;
                    model.SelectGroup = param.SelectGroup;
                    model.BackMenuAction = param.BackMenuAction;
                    model.BackMenuAction2 = param.BackMenuAction;

                    for (int i = 0; i < model.PointList.Count; i++)
                    {
                        model.PointList[i].PointDateTime = DateTime.Parse(param.SelectDay + " 00:00");
                        model.PointList[i].PointDate = DateTime.Parse(param.SelectDay);
                        model.PointList[i].PointTime = "00:00";
                    }
                }

                if (param.SenzokuID > 0)
                {
                    model.SenzokuID = param.SenzokuID;
                    model.SenzokuDriverID = param.SenzokuDriverID;
                    model.TargetDate = param.TargetDate;
                    model.SelectMonth = param.SelectMonth;
                    model.BackMenuAction = "SenzokuRegIndex";
                    model.BackMenuAction2 = "SenzokuRegIndex";
                    model.HaisyaPlanKubun = 4;

                    MasterDataApi masterDataApi = new(_mapApiSettiong);
                    Dto.V_Senzoku_Driver_Local SenzokuDriverData = await masterDataApi.GetSenzokuDriverViewData(loguinUser.Company_ID, model.SelectMonth, model.SenzokuDriverID);
                    model.KokyakuId = (int)SenzokuDriverData.Customer_Branch_ID;
                    model.KokyakuCode = SenzokuDriverData.Customer_Branch_Code;
                    model.KokyakuName = SenzokuDriverData.Customer_Branch_Name_Abbr;
                    model.KokyakuTantouId = (int)SenzokuDriverData.KokyakuTantouId;
                    model.KokyakuTantouName = SenzokuDriverData.Tantou_Name_Abbr;
                    model.KokyakuTantouPhone = SenzokuDriverData.Tantou_Phone1;
                    model.NumberCommLimitKubun = 1;  //車番連絡不要
                    model.SyasyuID = (int)SenzokuDriverData.Syaryo_ID;
                    model.Syasyu = SenzokuDriverData.Syasyu;
                    model.SyasyuDisplay = SenzokuDriverData.SyasyuDisplay;
                    model.SyasyuID = (int)SenzokuDriverData.Syaryo_ID;
                    for (int i = 0; i < model.PointList.Count; i++)
                    {
                        model.PointList[i].PointDateTime = param.TargetDate;
                        model.PointList[i].PointDate = param.TargetDate;
                        model.PointList[i].PointTime = "00:00";
                    }

                    model.Title = "専属庸車案件登録　顧客：（" + SenzokuDriverData.Customer_Branch_Code + "）" + SenzokuDriverData.Customer_Branch_Name_Abbr + "　　　　乗務員：" + SenzokuDriverData.Display_Name + "　(" + SenzokuDriverData.SYABAN + ")　　　配車日：" + param.TargetDate.ToString("yyyy/MM/dd(ddd)");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 前の画面に戻る
        /// </summary>
        /// <param name="param">AnkenBaseModel</param>
        /// <returns></returns>
        public async Task<IActionResult> BackMenu(AnkenBaseModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if ("Home".Equals(param.BackMenuAction))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if ("SenzokuRegIndex".Equals(param.BackMenuAction))
                {
                    return RedirectToAction("SenzokuRegIndex", "AnkenSenzoku", param);
                }
                else if ("AnkenList".Equals(param.BackMenuAction))
                {
                    return RedirectToAction("Index", "AnkenList", param);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// モデルを作成
        /// </summary>
        /// <param name="model">AnkenRegisterModel</param>
        /// <returns></returns>
        private async Task<AnkenRegisterModel> CreateModel(AnkenRegisterModel model = null)
        {

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            model ??= new()
            {
                SeikyuKubun = 0,
                AnkenStatus = 0,
                SelectPointTab = 0,
                NumberCommLimitKubun = 0,
                EigyoshoModori = false,
                CheckOroshiSpace = false,
                EdnGoBackEigyosyo = false,
                Daisuu = 1,
                Ferry = "false",
                TimeRestriction = "true",
                Twouturn = "true",
                PointList = new()
                {
                    new() { PointId = 0, Kubun = 1, TitleDisplay = "積み", Point_Order = 1, PointStatusKubun = 1, PointTimeKubun = 1, PointRoadTypeKubun = "all", },
                    new() { PointId = 1, Kubun = 9, TitleDisplay = "卸し", Point_Order = 2, PointStatusKubun = 1, PointTimeKubun = 1, PointRoadTypeKubun = "all", }
                },
                Anken_ID = 0,
                Anken_Status = 0,
                Anken_Latest_Order = 0,
                PublishGroup = 0,
                PublishFlg = false,
                HaisyaPlanKubun = 0,
                TantouID = loguinUser.DefaultGroup,
                TsumiTaskTime = "00:30",
                OroshiTaskTime = "00:30",
                Area = (int)loguinUser.Area_ID,
            };

            model.UserID = loguinUser.User_ID;
            model.Company_ID = loguinUser.Company_ID;
            model.Branch_ID = loguinUser.Branch_ID;

            model.TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);
            model.EigyoSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Eigyo);
            model.SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID);
            model.AreaSelectList = await SearchCommonService.GetAreaSelectListItem(_mapApiSettiong, loguinUser.Company_ID);
            model.PublishGroupSelectList = await SearchCommonService.GetPublishGroupSelectListItem(_mapApiSettiong, loguinUser.Company_ID, loguinUser.Branch_ID, loguinUser.User_ID);
            model.DriverGrossCalcSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 20);

            model.FerrySelectList = new SelectListItemEx[] {
                new() { Value="false", Text="利用しない" },
                new() { Value="true", Text="利用しない" },
            };
            model.TimeRestrictionSelectList = new SelectListItemEx[] {
                new() { Value="true", Text="利用する" },
                new() { Value="false", Text="利用しない" },
            };
            model.TwouturnSelectList = new SelectListItemEx[] {
                new() { Value="true", Text="する" },
                new() { Value="false", Text="しない" },
            };
            model.DaisuuSelectList = new SelectListItemEx[] {
                new() { Value="1", Text="1" },
                new() { Value="2", Text="2" },
                new() { Value="3", Text="3" },
                new() { Value="4", Text="5" },
                new() { Value="5", Text="5" },
                new() { Value="6", Text="6" },
            };
            model.PointComboBoxItemsStatusKubun = new SelectListItemEx[] {
                new() { Value="1", Text="確定" },
                new() { Value="2", Text="暫定" },
            };
            model.PointComboBoxItemsTimeKubun = new SelectListItemEx[] {
                new() { Value="1", Text="頃" },
                new() { Value="2", Text="まで" },
                new() { Value="3", Text="厳守" },
            };

            //積み、卸し等のポイント区分リスト
            model.PointSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 3);

            model.PointSelectRoadTypeList = new SelectListItemEx[] {
                new() { Value="2", Text="全道路" },
                new() { Value="0", Text="一般優先" },
                new() { Value="1", Text="高速優先" },
            };

            model.MapApiSettings = _mapApiSettiong;

            string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
            url += GetMapApiUrlPram();
            model.MapsApiForJSUrl = url;
            model.WebViewFlg = GetWebViewFlg();

            return model;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegExec2Async(AnkenRegisterModel param)
        {
            string responseBody = "";
            string errorMessage = null;

            try
            {

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if ("DriveRoot".Equals(param.ExecType))
                {
                    ////ドライブルート検索結果画面のモデル作成
                    DriveRouteListModel model = new()
                    {
                        DriveRouteListData = new(),
                        ExtraChargeList = new(),
                    };
                    // ドライブルート検索実施
                    model = await GetDriveRootAsync(param);

                    return await PartialViewAsJson("DriveListModal", model, true, true);


                }
                else if ("AddNew".Equals(param.ExecType))
                {
                    AnkenDataModelDto_Local ankenDataModelDto = new()
                    {
                        T_Anken = new(),
                        T_Anken_Detail = new(),
                        T_Anken_Publish = new(),
                        T_Anken_PointList = new(),
                        T_Anken_DisplayList = new(),
                        V_LoginUser = loguinUser,
                    };

                    await SetDto(ankenDataModelDto, param, loguinUser);
                    // データ登録
                    AnkenDataApi ankenDataApi = new(_mapApiSettiong);
                    ankenDataModelDto.T_Anken = await ankenDataApi.AddNewAnkenData(ankenDataModelDto);

                    return Json(new { t_Anken = ankenDataModelDto.T_Anken, errorMessage = errorMessage });
                }
                else if ("Update".Equals(param.ExecType))
                {
                    if (param.Anken_ID == 0)
                    {
                        return Json(new { t_Anken = "", errorMessage = "AnkenIDが不正" });
                    }

                    int loginUserID = int.Parse(_signInManager.Context.User.Claims.ToList()[0].Value);

                    AnkenDataModelDto_Local ankenDataModelDto = new()
                    {
                        T_Anken = new(),
                        T_Anken_Detail = new(),
                        T_Anken_Publish = new(),
                        T_Anken_PointList = new(),
                        T_Anken_DisplayList = new(),
                    };

                    await SetDto(ankenDataModelDto, param, loguinUser);
                    // データ登録
                    AnkenDataApi ankenDataApi = new(_mapApiSettiong);
                    UpdateAnkenDataDto updateAnkenDataDto = await ankenDataApi.UpdateAnkenData(ankenDataModelDto);
                    if (updateAnkenDataDto.ErrrMessage != null && updateAnkenDataDto.ErrrMessage.Length > 0)
                    {
                        errorMessage = updateAnkenDataDto.ErrrMessage;
                    }
                    else
                    {
                        ankenDataModelDto.T_Anken = updateAnkenDataDto.Anken;
                    }
                    return Json(new { t_Anken = ankenDataModelDto.T_Anken, errorMessage });
                }
                else
                {
                    errorMessage = "パラメーターエラー";
                    return Json(new { t_Anken = "", errorMessage });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(responseBody);
                errorMessage ??= e.Message;
                return Json(new { t_Anken = "", errorMessage });
            }
        }

        /// <summary>
        /// 案件履歴画面（モーダル）を返却する
        /// </summary>
        /// <param name="ankenID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAnkenRirekiModal(int ankenID)
        {
            try
            {
                if (ankenID == 0) { throw new Exception("パラメーターエラー：ankenID"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                AnkenRirekiModel model = new();

                using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                model.AnkenList = await api.GetAnkenDataList(null, null, null,
                                                    loguinUser.Company_ID, 0, 0, 0, 0, 1, ankenID);
                model.AnkenPointList = await api.GetAnkenPointList(ankenID);


                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CodeList = await apiM.M_Code_DataList(0);
                model.CompanyUserList = await apiM.GetCompanyUserList(loguinUser.Company_ID);
                model.CompanyUserGroupList = await apiM.GetCompanyUserGroupList(loguinUser.Company_ID, Service.UserGroupLists.ALL);

                return await PartialViewAsJson("AnkenRirekiModal", model);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// ドライブルート検索
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        private async Task<DriveRouteListModel> GetDriveRootAsync(AnkenRegisterModel param)
        {
            ////ドライブルート検索結果画面のモデル作成
            DriveRouteListModel returnVal = new()
            {
                DriveRouteListData = new(),
                ExtraChargeList = new(),
            };

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            AnkenDataApi ankenDataApi = new(_mapApiSettiong);
            MasterDataApi masterDataApi = new(_mapApiSettiong);

            string waypoint = "";//経由地点
            string from = "";//出発地点
            string to = "";//到着地点
            string waypointtype = ""; ;//経由地点
            string fromstype = "";//出発地点
            string totype = "";//到着地点

            /////////////パラメーター
            ////地点設定
            List<PointDto_Local> Pointlist = param.PointList.OrderBy(m => m.Point_Order).ToList();
            for (int i = 0; i < Pointlist.Count; i++)
            {
                if (i == 0)
                {
                    ///出発地点
                    from = Pointlist[i].Lng + "," + Pointlist[i].Lat;
                    fromstype = Pointlist[i].PointRoadTypeKubun;
                }
                else if (i == (Pointlist.Count - 1))
                {
                    ///到着地点
                    to = Pointlist[i].Lng + "," + Pointlist[i].Lat;
                    totype = Pointlist[i].PointRoadTypeKubun;
                }
                else
                {
                    ///経由地点
                    waypoint += "," + Pointlist[i].Lng + "," + Pointlist[i].Lat;
                    waypointtype += "," + Pointlist[i].PointRoadTypeKubun;
                }
            }
            if (waypoint != null && waypoint != "") { waypoint = waypoint[1..]; }
            if (waypointtype != null && waypointtype != "") { waypointtype = waypointtype[1..]; }

            //検索挙動変更（1：ルート所要時間に最適化した値を使用します。（デフォルト値 ：0) に比べ平均的にルート所要時間が短縮されます。）
            int searchparam = 1;

            //出発時刻指定
            string departuretime = null;
            //////車種設定
            Dto.M_Syaryo_Local syaryo = await masterDataApi.GetSyaryoData(param.Company_ID, param.SyasyuID);
            if (syaryo == null) { throw new Exception("対象の車種マスタが存在しません。ルート検索を中止します。"); }
            //車種
            param.Syasyu = syaryo.SYASYU;
            //型
            param.Kata = syaryo.KATA;
            //車種サイズ
            param.SyasyuSize = syaryo.SIZE;
            //詳細車種
            string cardetailinfo = syaryo.RegulationType;
            //料金車種
            string tolltype = syaryo.TOLL_TYPE;

            param.Height = (double)syaryo.HEIGHT;
            param.Width = (double)syaryo.WIDTH;
            param.Weight = (double)syaryo.CAR_GROSS_WEIGHT;
            param.Nenpi = (double)syaryo.AVG_FUEL_COSTS;

            //スマートIC利用指定
            string smartic = "true";
            //規制考慮
            string regulation = param.TimeRestriction;
            if ("none".Equals(regulation, StringComparison.Ordinal)) { regulation = ""; }
            //2段階Uターン回避指定
            string twouturn = param.Twouturn;
            //フェリー考慮指定]
            string ferry = param.Ferry;

            //追加料金
            List<AnkenExchargeDto_Local> AnkenExchargeList = new();
            // 検索ルート検索処理
            DriveRouteListDto_Local driveListtEx = await ankenDataApi.GetDriveRouteListExAsync(
                                                                            param.Area,
                                                                            loguinUser.Company_ID,
                                                                            from, to, waypoint, param.Syasyu, param.Kata,
                                                                            param.Height, param.Width, param.Weight, param.Nenpi,
                                                                            fromstype, totype, waypointtype,
                                                                            param.SyasyuSize,
                                                                            searchparam, departuretime, cardetailinfo,
                                                                            tolltype, smartic, regulation, twouturn, ferry,
                                                                            param.TsumiTaskTime, param.OroshiTaskTime,
                                                                            param.DriverGrossCalc,
                                                                            AnkenExchargeList);
            if (driveListtEx == null) { throw new Exception("ネットワーク接続エラー"); }

            if (driveListtEx.ErrrMessage != null && driveListtEx.ErrrMessage.Length > 0) { throw new Exception(driveListtEx.ErrrMessage); }

            if (driveListtEx.DriveRouteListDisplayList.Count == 1) { throw new Exception("ルート検索出来ませんでした"); }


            //ドライブルート結果
            foreach (DriveRouteListDisplay_Local data in driveListtEx.DriveRouteListDisplayList)
            {
                if (data.TotalDistance > 0) { data.TotalDistance = Math.Round(data.TotalDistance, 1); }
                returnVal.DriveRouteListData.Add(data);
            };

            //追加費用リスト
            foreach (ExtraChargeDto_Local data in driveListtEx.ExchargeDataList)
            {
                returnVal.ExtraChargeList.Add(data);
            };

            // セッションに文字列を書き込む
            HttpContext.Session.SetString("SyasyuID", param.SyasyuID.ToString());
            HttpContext.Session.SetString("TsumiTaskTime", param.TsumiTaskTime);
            HttpContext.Session.SetString("OroshiTaskTime", param.OroshiTaskTime);
            HttpContext.Session.SetString("Area", param.Area.ToString());

            return returnVal;

        }

        /// <summary>
        /// モデルに値を設定
        /// </summary>
        /// <param name="ankenDataModelDto">AnkenDataModelDto</param>
        /// <param name="param">AnkenRegisterModel</param>
        /// <param name="loguinUser">V_LoginUser</param>
        /// <returns></returns>
        private async Task SetDto(AnkenModel.AnkenDataModelDto_Local ankenDataModelDto, AnkenRegisterModel param,
                                            Dto.V_LoginUser_Local loguinUser)
        {
            try
            {
                int Branch_ID = int.Parse(_signInManager.Context.User.Claims.ToList()[0].Value);

                /////////////////////////T_Anken//////////////////////////////
                ankenDataModelDto.T_Anken.Anken_ID = param.Anken_ID;
                ankenDataModelDto.T_Anken.Anken_Latest_Order = param.Anken_Latest_Order;
                ankenDataModelDto.T_Anken.Anken_No = param.Anken_No;
                ankenDataModelDto.T_Anken.Anken_Status = param.AnkenStatus;
                ankenDataModelDto.T_Anken.Anken_Kubun = param.Anken_Kubun;
                ankenDataModelDto.T_Anken.SenzokuID = param.SenzokuID;
                ankenDataModelDto.T_Anken.Senzoku_Driver_ID = param.SenzokuDriverID;

                if (ankenDataModelDto.T_Anken.Anken_ID == 0)
                {
                    ankenDataModelDto.T_Anken.Company_ID = loguinUser.Company_ID;
                    ankenDataModelDto.T_Anken.Branch_ID = loguinUser.Branch_ID;
                }

                /////////////////////////T_Anken_Publish//////////////////////////////
                ankenDataModelDto.T_Anken_Publish.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Publish.PublishGroup_ID = param.PublishGroup;
                ankenDataModelDto.T_Anken_Publish.Publish_Flg = param.PublishFlg;
                ankenDataModelDto.T_Anken_Publish.Publish_FromDatetime = param.PublishFromDatetime;
                ankenDataModelDto.T_Anken_Publish.Publish_ToDatetime = param.PublishToDatetime;

                /////////////////////////T_Anken_Detail//////////////////////////////
                ankenDataModelDto.T_Anken_Detail.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                ankenDataModelDto.T_Anken_Detail.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                ankenDataModelDto.T_Anken_Detail.TantouID = param.TantouID;
                ankenDataModelDto.T_Anken_Detail.EigyoID = param.EigyoID;
                ankenDataModelDto.T_Anken_Detail.HaisyaPlanKubun = param.HaisyaPlanKubun;
                ankenDataModelDto.T_Anken_Detail.HaisyaDriverID = param.HaisyaDriverId;
                ankenDataModelDto.T_Anken_Detail.HaisyaDriverSyaryoID = param.HaisyaDriverSyaryoId;
                ankenDataModelDto.T_Anken_Detail.HaisyaDriverDisplay = param.HaisyaDriverDisplay;
                ankenDataModelDto.T_Anken_Detail.Work_Name = param.WorkName;
                ankenDataModelDto.T_Anken_Detail.Reg_Kubun = param.RegKubun;

                ////請求関連;
                ankenDataModelDto.T_Anken_Detail.SeikyuKubun = param.SeikyuKubun;
                ////顧客情報;
                ankenDataModelDto.T_Anken_Detail.KokyakuId = param.KokyakuId;
                ankenDataModelDto.T_Anken_Detail.KokyakuCode = param.KokyakuCode;
                ankenDataModelDto.T_Anken_Detail.KokyakuName = param.KokyakuName;
                ankenDataModelDto.T_Anken_Detail.KokyakuTantouId = param.KokyakuTantouId;
                ankenDataModelDto.T_Anken_Detail.KokyakuTantouName = param.KokyakuTantouName;
                ankenDataModelDto.T_Anken_Detail.KokyakuTantouPhone = param.KokyakuTantouPhone;
                ////車種情報;
                ankenDataModelDto.T_Anken_Detail.Syaryo_ID = param.SyasyuID;
                ankenDataModelDto.T_Anken_Detail.Syasyu = param.Syasyu;
                ankenDataModelDto.T_Anken_Detail.SyasyuSize = param.SyasyuSize;
                ankenDataModelDto.T_Anken_Detail.SyasyuDisplay = param.SyasyuDisplay;
                ankenDataModelDto.T_Anken_Detail.Kata = param.Kata;
                ////台数;
                ankenDataModelDto.T_Anken_Detail.Daisuu = param.Daisuu;
                ////ルート検索条件;
                ankenDataModelDto.T_Anken_Detail.Root_Ferry = param.Ferry;
                ankenDataModelDto.T_Anken_Detail.Root_Regulation = param.TimeRestriction;
                ankenDataModelDto.T_Anken_Detail.Root_Twouturn = param.Twouturn;
                ankenDataModelDto.T_Anken_Detail.Root_EigyoshoModori = param.EigyoshoModori;
                ///積み降ろし時間
                ankenDataModelDto.T_Anken_Detail.TsumiTaskTime = param.TsumiTaskTime;
                ankenDataModelDto.T_Anken_Detail.OroshiTaskTime = param.OroshiTaskTime;

                ankenDataModelDto.T_Anken_Detail.CheckOroshiSpace = param.CheckOroshiSpace;
                ankenDataModelDto.T_Anken_Detail.EdnGoBackEigyosyo = param.EdnGoBackEigyosyo;

                ankenDataModelDto.T_Anken_Detail.Area = param.Area;

                ankenDataModelDto.T_Anken_Detail.DriverGrossCalc = param.DriverGrossCalc;

                ankenDataModelDto.T_Anken_Detail.SyabanRenraku_Remarks = param.SyabanRenrakuRemarks;
                ankenDataModelDto.T_Anken_Detail.Notice = param.Notice;


                ankenDataModelDto.T_Anken_Detail.Toll_Kubun = param.TollKubun;
                ankenDataModelDto.T_Anken_Detail.Toll_Money = (decimal)param.TollMoney;
                ankenDataModelDto.T_Anken_Detail.Toll_Remarks = param.TollRemarks;


                ////車番連絡;
                ankenDataModelDto.T_Anken_Detail.NumberCommLimitKubun = param.NumberCommLimitKubun;
                if (param.NumberCommLimitDateTime != null)
                {
                    ankenDataModelDto.T_Anken_Detail.NumberCommLimitDateTime = param.NumberCommLimitDateTime;
                }

                MasterDataApi dataApi = new(_mapApiSettiong);
                Dto.M_Syaryo_Local syaryo = await dataApi.GetSyaryoData(loguinUser.Company_ID, param.SyasyuID);
                if (syaryo != null)
                {
                    ankenDataModelDto.T_Anken_Detail.Height = (double)syaryo.HEIGHT;
                    ankenDataModelDto.T_Anken_Detail.Width = (double)syaryo.WIDTH;
                    ankenDataModelDto.T_Anken_Detail.Weight = (double)syaryo.CAR_GROSS_WEIGHT;
                    ankenDataModelDto.T_Anken_Detail.Nenpi = (double)syaryo.AVG_FUEL_COSTS;

                    ankenDataModelDto.T_Anken_Detail.Syasyu = syaryo.SYASYU;
                    ankenDataModelDto.T_Anken_Detail.Kata = syaryo.KATA;
                    ankenDataModelDto.T_Anken_Detail.SyasyuDisplay = syaryo.SyasyuDisplay;
                }

                ////選択ルート;
                if (param.SelectedDriveRouteDisplay != null && param.SelectedDriveRouteDisplay.routeID != null)
                {
                    CopyProperty(ankenDataModelDto.T_Anken_Detail, param.SelectedDriveRouteDisplay);

                    ankenDataModelDto.T_Anken_Detail.RouteID = param.SelectedDriveRouteDisplay.routeID;
                    ankenDataModelDto.T_Anken_Detail.RouteType = int.Parse(param.SelectedDriveRouteDisplay.routeType);
                    ankenDataModelDto.T_Anken_Detail.RouteTypeDisplay = param.SelectedDriveRouteDisplay.RouteTypeDisplay;
                    ankenDataModelDto.T_Anken_Detail.Route_TotalTime = param.SelectedDriveRouteDisplay.TotalTime;
                    ankenDataModelDto.T_Anken_Detail.Route_BreakTime = param.SelectedDriveRouteDisplay.BreakTime;
                    ankenDataModelDto.T_Anken_Detail.Route_RestTime = param.SelectedDriveRouteDisplay.RestTime;

                    ankenDataModelDto.T_Anken_Detail.Route_TotalDistance = param.SelectedDriveRouteDisplay.TotalDistance;
                    ankenDataModelDto.T_Anken_Detail.Route_FuelConsume = (decimal?)param.SelectedDriveRouteDisplay.FuelConsume;
                    ankenDataModelDto.T_Anken_Detail.Route_Totaltoll = (decimal?)param.SelectedDriveRouteDisplay.Totaltoll;
                    ankenDataModelDto.T_Anken_Detail.Route_RestTimeDisplay = param.SelectedDriveRouteDisplay.RestTimeDisplay;

                    ankenDataModelDto.T_Anken_Detail.Route_StdFreight = (decimal?)param.SelectedDriveRouteDisplay.StdFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_StdALLFreight = (decimal?)param.SelectedDriveRouteDisplay.StdALLFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_StdExcharge = (decimal?)param.SelectedDriveRouteDisplay.StdExcharge;
                    ankenDataModelDto.T_Anken_Detail.Route_StdTotalFreight = (decimal?)param.SelectedDriveRouteDisplay.StdTotalFreight;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForLaborCost = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountForLaborCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForFuelCost = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountForFuelCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForSyaryoCost = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountForSyaryoCost;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForLuggage = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountForLuggage;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountForExcharge = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountForExcharge;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmount = (decimal?)param.SelectedDriveRouteDisplay.GrossAmount;
                    ankenDataModelDto.T_Anken_Detail.Route_GrossAmountTotal = (decimal?)param.SelectedDriveRouteDisplay.GrossAmountTotal;
                    ankenDataModelDto.T_Anken_Detail.Route_TotalDays = param.SelectedDriveRouteDisplay.TotalDays;
                }

                ////請求情報;
                ankenDataModelDto.T_Anken_Detail.BaseFee = (decimal?)param.BaseFee;
                ankenDataModelDto.T_Anken_Detail.ExtraCharge = (decimal?)param.ExtraCharge;
                ankenDataModelDto.T_Anken_Detail.Toll = (decimal?)param.Toll;
                ankenDataModelDto.T_Anken_Detail.Discount = (decimal?)param.Discount;
                ankenDataModelDto.T_Anken_Detail.GrossAmount = (decimal?)param.GrossAmount;

                ankenDataModelDto.T_Anken_Detail.Luggage_Weight = param.LuggageWeight;
                ankenDataModelDto.T_Anken_Detail.LuggageDisplay = param.LuggageDisplay;
                ankenDataModelDto.T_Anken_Detail.EquipmentDisplay = param.EquipmentDisplay;


                ankenDataModelDto.T_Anken_Detail.Insert_Datetime = DateTime.Now;
                ankenDataModelDto.T_Anken_Detail.Insert_User = loguinUser.User_ID;


                if (ankenDataModelDto.T_Anken.Anken_ID > 0)
                {
                    ankenDataModelDto.T_Anken_Detail.Update_Datetime = DateTime.Now;
                    ankenDataModelDto.T_Anken_Detail.Update_User = loguinUser.User_ID;
                }

                ////////////////////////////////T_Anken_PointList///////////////////////////////////
                List<PointDto_Local> pointlist = param.PointList.OrderBy(m => m.Point_Order).ToList();
                int pointOrder = 1;
                foreach (PointDto_Local dto in pointlist)
                {
                    if (dto.PointDateTime != null)
                    {
                        dto.PointTime = ((DateTime)dto.PointDateTime).ToString("HH:mm");
                        dto.PointDate = DateTime.Parse(((DateTime)dto.PointDateTime).ToString("yyyy/MM/d"));
                    }
                    Dto.T_Anken_Point_Local t_Anken_Point_Local = new();
                    CopyProperty(t_Anken_Point_Local, dto);
                    t_Anken_Point_Local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                    t_Anken_Point_Local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                    t_Anken_Point_Local.RoadType = dto.PointRoadTypeKubun;
                    t_Anken_Point_Local.Kubun = dto.Kubun;
                    t_Anken_Point_Local.Point_Order = pointOrder;
                    t_Anken_Point_Local.Insert_Datetime = DateTime.Now;
                    t_Anken_Point_Local.Insert_User = loguinUser.User_ID;
                    if (ankenDataModelDto.T_Anken.Anken_ID > 0)
                    {
                        t_Anken_Point_Local.Update_Datetime = DateTime.Now;
                        t_Anken_Point_Local.Update_User = loguinUser.User_ID;
                    }
                    t_Anken_Point_Local.SEKubun = t_Anken_Point_Local.Kubun switch { 1 => "S", 9 => "E", _ => "" };
                    pointOrder += 1;
                    ankenDataModelDto.T_Anken_PointList.Add(t_Anken_Point_Local);
                }

                /////////////////////////////////T_Anken_OyaKokyakuList///////////////////////////////////
                if (param.OyaKokyakuListData != null && param.OyaKokyakuListData.Count > 1)
                {
                    ankenDataModelDto.T_Anken_OyaKokyakuList = new();
                    int i = 1;
                    foreach (Dto.T_Anken_OyaKokyaku_Local data in param.OyaKokyakuListData)
                    {
                        Dto.T_Anken_OyaKokyaku_Local t_Anken_OyaKokyaku = new();
                        CopyProperty(t_Anken_OyaKokyaku, data);
                        t_Anken_OyaKokyaku.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                        t_Anken_OyaKokyaku.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                        t_Anken_OyaKokyaku.Kokyaku_Order = i;
                        i += 1;
                        ankenDataModelDto.T_Anken_OyaKokyakuList.Add(t_Anken_OyaKokyaku);
                    }
                }

                /////////////////////////////////T_Anken_Luggage///////////////////////////////////
                if (param.AnkenLuggageList != null && param.AnkenLuggageList.Count > 1)
                {
                    ankenDataModelDto.T_Anken_LuggageList = new();
                    int i = 1;
                    foreach (Dto.T_Anken_Luggage_Local data in param.AnkenLuggageList)
                    {
                        Dto.T_Anken_Luggage_Local local = new();
                        CopyProperty(local, data);
                        local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                        local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                        local.Insert_Datetime = DateTime.Now;
                        local.Insert_User = loguinUser.User_ID;

                        if (ankenDataModelDto.T_Anken.Anken_ID > 0)
                        {
                            local.Update_Datetime = DateTime.Now;
                            local.Update_User = loguinUser.User_ID;
                        }
                        i += 1;
                        ankenDataModelDto.T_Anken_LuggageList.Add(local);
                    }
                }

                /////////////////////////////////T_Anken_Equipment///////////////////////////////////
                if (param.AnkenEquipmentList != null && param.AnkenEquipmentList.Count > 1)
                {
                    ankenDataModelDto.T_Anken_EquipmentList = new();
                    int i = 1;
                    foreach (Dto.T_Anken_Equipment_Local data in param.AnkenEquipmentList)
                    {
                        Dto.T_Anken_Equipment_Local local = new();
                        CopyProperty(local, data);
                        local.Anken_ID = ankenDataModelDto.T_Anken.Anken_ID;
                        local.Anken_Order = ankenDataModelDto.T_Anken.Anken_Latest_Order;
                        local.Insert_Datetime = DateTime.Now;
                        local.Insert_User = loguinUser.User_ID;

                        if (ankenDataModelDto.T_Anken.Anken_ID > 0)
                        {
                            local.Update_Datetime = DateTime.Now;
                            local.Update_User = loguinUser.User_ID;
                        }
                        i += 1;
                        ankenDataModelDto.T_Anken_EquipmentList.Add(local);
                    }
                }

                /////////////////////////////////T_Anken_Display///////////////////////////////////////////
                /// 台数分データを作成する
                int iKey = 1;
                for (int i = 1; i <= param.Daisuu; i++)
                {
                    Dto.T_Anken_Point_Local startP = null;
                    Dto.T_Anken_Point_Local startE = null;
                    Dto.T_Anken_Display_Local target = null;

                    List<Dto.T_Anken_Point_Local> plist = ankenDataModelDto.T_Anken_PointList.Where(m => m.Kubun != 2).OrderBy(m => m.Point_Order).ToList();
                    for (int m = 0; m < plist.Count; m++)
                    {
                        if (startP == null) startP = plist[m];
                        if (plist[m].Kubun.Equals(3) || plist[m].Kubun.Equals(4))
                        {
                            startE = plist[m];
                            target = SetAnkenDisplay(loguinUser, ankenDataModelDto.T_Anken.Anken_ID, startP, startE,
                                    "(" + ankenDataModelDto.T_Anken_Detail.KokyakuCode + ")" + ankenDataModelDto.T_Anken_Detail.KokyakuName);
                            target.Daisuu_Sort = i;
                            target.Display_Kubun = 2;
                            target.Anken_Key = iKey;
                            ankenDataModelDto.T_Anken_DisplayList.Add(target);
                            startP = startE;
                            iKey++;
                        }
                        if (m == (plist.Count - 1))
                        {
                            startE = plist[m];
                            target = SetAnkenDisplay(loguinUser, ankenDataModelDto.T_Anken.Anken_ID, startP, startE,
                                    "(" + ankenDataModelDto.T_Anken_Detail.KokyakuCode + ")" + ankenDataModelDto.T_Anken_Detail.KokyakuName);
                            target.Daisuu_Sort = i;
                            target.Display_Kubun = 2;
                            target.Anken_Key = iKey;

                            if ((target.StartDatetime.Hour > 0 && target.EndDatetime.Hour == 0) && (target.StartDatetime.Day == target.EndDatetime.Day))
                            {
                                if (param.SelectedDriveRouteDisplay.TotalTime != null)
                                {
                                    DateTime aa = DateTime.Parse("1900/01/01 " + param.SelectedDriveRouteDisplay.TotalTime.Replace("時", ":").Replace("分", ""));
                                    DateTime end = target.StartDatetime.AddHours(aa.Hour).AddMinutes(aa.Minute);
                                    target.EndDatetime = end;
                                }
                            }
                            ankenDataModelDto.T_Anken_DisplayList.Add(target);
                            iKey = 1;
                        }
                    }
                }

                if (ankenDataModelDto.T_Anken.Anken_ID > 0)
                {
                    ankenDataModelDto.T_Anken_Detail.Update_User = loguinUser.User_ID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 引数からT_Anken_Display_Localを作成する
        /// </summary>
        /// <param name="loguinUser">V_LoginUser</param>
        /// <param name="AnkenID"></param>
        /// <param name="startP">T_Anken_Point</param>
        /// <param name="startE">T_Anken_Point</param>
        /// <param name="Display1"></param>
        /// <returns></returns>
        private Dto.T_Anken_Display_Local SetAnkenDisplay(Dto.V_LoginUser_Local loguinUser, int AnkenID, Dto.T_Anken_Point_Local startP,
                    Dto.T_Anken_Point_Local startE, string Display1)
        {
            Dto.T_Anken_Display_Local data = new();

            data.Anken_ID = AnkenID;
            data.Day = (DateTime)startP.PointDate;
            data.Display_Kubun = 2;
            data.StartDatetime = DateTime.Parse(((DateTime)startP.PointDate).ToString("yyyy/MM/dd") + ' ' + startP.PointTime);
            data.EndDatetime = DateTime.Parse(((DateTime)startE.PointDate).ToString("yyyy/MM/dd") + ' ' + startE.PointTime);

            data.Start_Point_Kubun = startP.Kubun;
            data.Start_BuildingName = startP.BuildingName;
            data.Start_BuildingZid = startP.BuildingZid;
            data.Start_BuildingZid_Attr = startP.BuildingZid_Attr;
            data.Start_BuildingNameRead = startP.BuildingNameRead;
            data.Start_Point_KoumokuTitle = startP.Point_KoumokuTitle;
            data.Start_Point_Type = startP.Point_Type;
            data.Start_PointName = startP.PointName;
            data.Start_Lat = startP.Lat;
            data.Start_Lng = startP.Lng;
            data.Start_Post_code = startP.Post_code;
            data.Start_Address = startP.Address;
            data.Start_Address2 = startP.Address2;
            data.Start_Address3 = startP.Address3;
            data.Start_Address4 = startP.Address4;

            data.End_Point_Kubun = startE.Kubun;
            data.End_BuildingName = startE.BuildingName;
            data.End_BuildingZid = startE.BuildingZid;
            data.End_BuildingZid_Attr = startE.BuildingZid_Attr;
            data.End_BuildingNameRead = startE.BuildingNameRead;
            data.End_Point_KoumokuTitle = startE.Point_KoumokuTitle;
            data.End_Point_Type = startE.Point_Type;
            data.End_PointName = startE.PointName;
            data.End_Lat = startE.Lat;
            data.End_Lng = startE.Lng;
            data.End_Post_code = startE.Post_code;
            data.End_Address = startE.Address;
            data.End_Address2 = startE.Address2;
            data.End_Address3 = startE.Address3;
            data.End_Address4 = startE.Address4;


            data.Display1 = Display1;
            data.Display2 = startP.Address2 + startP.Address3 + "　⇒　" + startE.Address2 + startE.Address3;

            data.Insert_Datetime = DateTime.Now;
            data.Insert_User = (int?)loguinUser.User_ID;

            if (AnkenID > 0)
            {
                data.Update_Datetime = DateTime.Now;
            }

            return data;
        }

        /// <summary>
        /// データベースからデータの取得してプロパティに設定
        /// 更新用
        /// </summary>
        /// <param name="paramAnkenId">AnkenId</param>
        /// <param name="model">AnkenRegisterModel</param>
        /// <returns></returns>
        private async Task GetEditData(int paramAnkenId, AnkenRegisterModel model)
        {
            AnkenDataModelDto_Local dto = new()
            {
                T_Anken = new(),
                T_Anken_Detail = new(),
                T_Anken_Publish = new(),
                T_Anken_PointList = new(),
                T_Anken_Riyounso = new(),
                T_Anken_Riyounso_PointList = new(),
                T_Anken_LuggageList = new(),
            };

            try
            {
                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                dto = await api.GetAnkenData(paramAnkenId);

                /////////////////////////T_Anken//////////////////////////////
                model.Anken_ID = dto.T_Anken.Anken_ID;
                model.Anken_Latest_Order = dto.T_Anken.Anken_Latest_Order;
                model.Anken_No = dto.T_Anken.Anken_No;
                model.Anken_Status = dto.T_Anken.Anken_Status;
                model.Company_ID = dto.T_Anken.Company_ID;
                model.Branch_ID = dto.T_Anken.Branch_ID;
                model.Anken_Kubun = dto.T_Anken.Anken_Kubun;


                /////////////////////////T_Anken_Publish//////////////////////////////
                if (dto.T_Anken_Publish != null)
                {
                    model.PublishGroup = dto.T_Anken_Publish.PublishGroup_ID;
                    model.PublishFlg = dto.T_Anken_Publish.Publish_Flg;
                    if (dto.T_Anken_Publish.Publish_FromDatetime != null) { model.PublishFromDatetime = (DateTime)dto.T_Anken_Publish.Publish_FromDatetime; }
                    if (dto.T_Anken_Publish.Publish_ToDatetime != null) { model.PublishToDatetime = (DateTime)dto.T_Anken_Publish.Publish_ToDatetime; }
                }

                /////////////////////////T_Anken_Detail//////////////////////////////
                #region T_Anken_Detail
                model.TantouID = dto.T_Anken_Detail.TantouID;
                model.EigyoID = dto.T_Anken_Detail.EigyoID;
                model.HaisyaPlanKubun = dto.T_Anken_Detail.HaisyaPlanKubun;
                model.HaisyaDriverId = dto.T_Anken_Detail.HaisyaDriverID;
                model.HaisyaDriverSyaryoId = dto.T_Anken_Detail.HaisyaDriverSyaryoID;
                model.HaisyaDriverDisplay = dto.T_Anken_Detail.HaisyaDriverDisplay;
                model.WorkName = dto.T_Anken_Detail.Work_Name;
                model.RegKubun = dto.T_Anken_Detail.Reg_Kubun;

                ////請求関連;
                model.SeikyuKubun = (int)dto.T_Anken_Detail.SeikyuKubun;
                ////顧客情報;
                model.KokyakuId = dto.T_Anken_Detail.KokyakuId;
                model.KokyakuCode = dto.T_Anken_Detail.KokyakuCode;
                model.KokyakuName = dto.T_Anken_Detail.KokyakuName;
                model.KokyakuTantouId = dto.T_Anken_Detail.KokyakuTantouId;
                model.KokyakuTantouName = dto.T_Anken_Detail.KokyakuTantouName;
                model.KokyakuTantouPhone = dto.T_Anken_Detail.KokyakuTantouPhone;
                ////車種情報;
                model.SyasyuID = dto.T_Anken_Detail.Syaryo_ID;
                model.Syasyu = dto.T_Anken_Detail.Syasyu;
                model.SyasyuSize = dto.T_Anken_Detail.SyasyuSize;
                model.SyasyuDisplay = dto.T_Anken_Detail.SyasyuDisplay;
                model.Kata = dto.T_Anken_Detail.Kata;
                ////台数;
                model.Daisuu = (int)dto.T_Anken_Detail.Daisuu;
                ////ルート検索条件;
                model.Ferry = dto.T_Anken_Detail.Root_Ferry;
                model.TimeRestriction = dto.T_Anken_Detail.Root_Regulation;
                model.Twouturn = dto.T_Anken_Detail.Root_Twouturn;
                model.EigyoshoModori = dto.T_Anken_Detail.Root_EigyoshoModori;
                ///積み降ろし時間
                model.TsumiTaskTime = dto.T_Anken_Detail.TsumiTaskTime;
                model.OroshiTaskTime = dto.T_Anken_Detail.OroshiTaskTime;

                model.CheckOroshiSpace = dto.T_Anken_Detail.CheckOroshiSpace;
                model.EdnGoBackEigyosyo = dto.T_Anken_Detail.EdnGoBackEigyosyo;

                model.Area = dto.T_Anken_Detail.Area;

                model.DriverGrossCalc = dto.T_Anken_Detail.DriverGrossCalc;

                model.TollKubun = dto.T_Anken_Detail.Toll_Kubun;
                model.TollMoney = (double)dto.T_Anken_Detail.Toll_Money;
                model.TollRemarks = dto.T_Anken_Detail.Toll_Remarks;

                ////車番連絡;
                model.NumberCommLimitKubun = (int)dto.T_Anken_Detail.NumberCommLimitKubun;
                if (dto.T_Anken_Detail.NumberCommLimitDateTime != null)
                {
                    model.NumberCommLimitDateTime = dto.T_Anken_Detail.NumberCommLimitDateTime;
                }

                ////選択ルート;
                if (dto.T_Anken_Detail.RouteTypeDisplay != null)
                {
                    model.SelectedDriveRouteDisplay = new()
                    {
                        routeID = dto.T_Anken_Detail.RouteID,
                        Syasyu = dto.T_Anken_Detail.Syasyu,
                        SyasyuSize = dto.T_Anken_Detail.SyasyuSize,
                        Kata = dto.T_Anken_Detail.Kata,
                        routeType = dto.T_Anken_Detail.RouteType.ToString(),
                        RouteTypeDisplay = dto.T_Anken_Detail.RouteTypeDisplay,
                        TotalTime = dto.T_Anken_Detail.Route_TotalTime,
                        BreakTime = (int)dto.T_Anken_Detail.Route_BreakTime,
                        RestTime = (int)dto.T_Anken_Detail.Route_RestTime,

                        TotalDistance = (double)dto.T_Anken_Detail.Route_TotalDistance,
                        FuelConsume = (double)dto.T_Anken_Detail.Route_FuelConsume,
                        Totaltoll = (double)dto.T_Anken_Detail.Route_Totaltoll,
                        RestTimeDisplay = dto.T_Anken_Detail.Route_RestTimeDisplay,

                        StdFreight = (double)dto.T_Anken_Detail.Route_StdFreight,
                        StdALLFreight = (double)dto.T_Anken_Detail.Route_StdALLFreight,
                        StdExcharge = (double)dto.T_Anken_Detail.Route_StdExcharge,
                        StdTotalFreight = (double)dto.T_Anken_Detail.Route_StdTotalFreight,
                        GrossAmountForLaborCost = (double)dto.T_Anken_Detail.Route_GrossAmountForLaborCost,
                        GrossAmountForFuelCost = (double)dto.T_Anken_Detail.Route_GrossAmountForFuelCost,
                        GrossAmountForSyaryoCost = (double)dto.T_Anken_Detail.Route_GrossAmountForSyaryoCost,
                        GrossAmountForLuggage = (double)dto.T_Anken_Detail.Route_GrossAmountForLuggage,
                        GrossAmountForExcharge = (double)dto.T_Anken_Detail.Route_GrossAmountForExcharge,
                        GrossAmount = (double)dto.T_Anken_Detail.Route_GrossAmount,
                        GrossAmountTotal = (double)dto.T_Anken_Detail.Route_GrossAmountTotal,
                        TotalDays = (int)dto.T_Anken_Detail.Route_TotalDays
                    };
                }

                ////請求情報;
                model.BaseFee = (double)dto.T_Anken_Detail.BaseFee;
                model.ExtraCharge = (double)dto.T_Anken_Detail.ExtraCharge;
                model.Toll = (double)dto.T_Anken_Detail.Toll;
                model.Discount = (double)dto.T_Anken_Detail.Discount;
                model.GrossAmount = (double)dto.T_Anken_Detail.GrossAmount;

                model.Height = (double)dto.T_Anken_Detail.Height;
                model.Width = (double)dto.T_Anken_Detail.Width;
                model.Weight = (double)dto.T_Anken_Detail.Weight;
                model.Nenpi = (double)dto.T_Anken_Detail.Nenpi;

                model.LuggageWeight = dto.T_Anken_Detail.Luggage_Weight;
                model.LuggageDisplay = dto.T_Anken_Detail.LuggageDisplay;
                model.EquipmentDisplay = dto.T_Anken_Detail.EquipmentDisplay;

                model.Notice = dto.T_Anken_Detail.Notice;
                model.SyabanRenrakuRemarks = dto.T_Anken_Detail.SyabanRenraku_Remarks;

                #endregion T_Anken_Detail

                ////////////////////////////////T_Anken_PointList///////////////////////////////////
                #region T_Anken_PointList
                List<T_Anken_Point_Local> pointlist = dto.T_Anken_PointList.OrderBy(m => m.Point_Order).ToList();
                model.PointList = new();
                foreach (Dto.T_Anken_Point_Local data in pointlist)
                {
                    PointDto_Local point = new();
                    CopyProperty(point, data);
                    point.PointId = data.Point_Order;
                    if (point.PointDate != null)
                    {
                        point.PointDateTime = DateTime.Parse(((DateTime)point.PointDate).ToString("yyyy/MM/dd") + ' ' + point.PointTime);
                    }
                    point.TitleDisplay = data.Kubun switch { 1 => "積み", 2 => "経由", 9 => "卸し", _ => "" };
                    point.PointRoadTypeKubun = data.RoadType;
                    model.PointList.Add(point);
                }
                #endregion T_Anken_PointList

                /////////////////////////////////T_Anken_OyaKokyakuList///////////////////////////////////
                if (dto.T_Anken_OyaKokyakuList != null && dto.T_Anken_OyaKokyakuList.Count > 0)
                {
                    model.OyaKokyakuListData = new();
                    foreach (Dto.T_Anken_OyaKokyaku_Local data in dto.T_Anken_OyaKokyakuList)
                    {
                        Dto.T_Anken_OyaKokyaku_Local local = new();
                        CopyProperty(local, data);
                        model.OyaKokyakuListData.Add(local);
                    }
                }

                /////////////////////////////////T_Anken_Luggage///////////////////////////////////
                if (dto.T_Anken_LuggageList != null && dto.T_Anken_LuggageList.Count > 0)
                {
                    model.AnkenLuggageList = new();
                    foreach (Dto.T_Anken_Luggage_Local data in dto.T_Anken_LuggageList)
                    {
                        Dto.T_Anken_Luggage_Local local = new();
                        CopyProperty(local, data);
                        model.AnkenLuggageList.Add(local);
                    }
                }

                /////////////////////////////////T_Anken_Equipment///////////////////////////////////
                if (dto.T_Anken_EquipmentList != null && dto.T_Anken_EquipmentList.Count > 0)
                {
                    model.AnkenEquipmentList = new();
                    foreach (Dto.T_Anken_Equipment_Local data in dto.T_Anken_EquipmentList)
                    {
                        Dto.T_Anken_Equipment_Local local = new();
                        CopyProperty(local, data);
                        model.AnkenEquipmentList.Add(local);
                    }
                }

                /////////////////////////////////M_Customer_Tantou///////////////////////////////////
                if (dto.M_Customer_Tantou != null)
                {
                    model.KokyakuTantouCellPhone = dto.M_Customer_Tantou.Cell_Phone;
                    model.KokyakuTantouPhone1 = dto.M_Customer_Tantou.Phone1;
                    model.KokyakuTantouFax1 = dto.M_Customer_Tantou.Fax1;
                }

                /////////////////////////////////M_Customer_Branch///////////////////////////////////
                model.KokyakuInfoDisplay = dto.AnkenRemarks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 別タブでドライブルート詳細画面（モーダル）を開く
        /// </summary>
        /// <param name="routeID"></param>
        /// <param name="routeType"></param>
        /// <returns></returns>
        public async Task<IActionResult> DriveRouteDetail(string routeID, string routeType)
        {

            try
            {
                // セッションから文字列を読み込む
                string sasyuID = HttpContext.Session.GetString("SyasyuID");
                string tsumiTaskTime = HttpContext.Session.GetString("TsumiTaskTime");
                string oroshiTaskTime = HttpContext.Session.GetString("OroshiTaskTime");
                string area = HttpContext.Session.GetString("Area");

                // セッション情報のエラーチェック
                if (!(sasyuID != null && sasyuID.Length > 0)) { throw new Exception("セッションエラー：ルート候補一覧画面を閉じて再度ルート検索を実施してください。"); }
                if (!(tsumiTaskTime != null && tsumiTaskTime.Length > 0)) { throw new Exception("セッションエラー：ルート候補一覧画面を閉じて再度ルート検索を実施してください。"); }
                if (!(oroshiTaskTime != null && oroshiTaskTime.Length > 0)) { throw new Exception("セッションエラー：ルート候補一覧画面を閉じて再度ルート検索を実施してください。"); }
                if (!(area != null && area.Length > 0)) { throw new Exception("セッションエラー：ルート候補一覧画面を閉じて再度ルート検索を実施してください。"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                //////車種設定
                MasterDataApi masterDataApi = new(_mapApiSettiong);
                Dto.M_Syaryo_Local syaryo = await masterDataApi.GetSyaryoData(loguinUser.Company_ID, int.Parse(sasyuID));
                if (syaryo == null) { throw new Exception("対象の車種マスタが存在しません。ルート検索を中止します。"); }
                //車種
                string Syasyu = syaryo.SYASYU;
                //型
                string Kata = syaryo.KATA;
                //サイズ
                string SyasyuSize = syaryo.SIZE;

                int.TryParse(area, out int areaID);

                // 検索ルート検索処理
                AnkenDataApi ankenDataApi = new(_mapApiSettiong);
                DriveRouteListDto_Local driveListtEx = await ankenDataApi.GetDriveDetailAsync(loguinUser.Company_ID, routeID, routeType, areaID,
                                                                                            Syasyu, Kata, SyasyuSize,
                                                                                            tsumiTaskTime, oroshiTaskTime);

                string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
                url += GetMapApiUrlPram();

                RouteDetailModel model = new()
                {
                    driveList = driveListtEx.DriveRouteListDisplayList[0],
                    MapApiSettings = _mapApiSettiong,
                    MapsApiForJSUrl = url,
                    CenterLatlon = new(),
                };

                // 中心マーカーの算出
                int i = model.driveList.link.Count / 2;
                model.CenterLatlon.lat = model.driveList.link[i].line[0].lat;
                model.CenterLatlon.lng = model.driveList.link[i].line[0].lng;


                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForRouteDetail);
            }
        }

        /// <summary>
        /// 住所検索画面（モーダル）を開く
        /// </summary>
        /// <param name="data">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SelectAddressModal(AnkenRegisterModel data)
        {
            try
            {
                SelectAddressModalDto model = new()
                {
                    CompanyID = data.Company_ID,
                    UserID = data.UserID,
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.AreaList = await apiM.GetAreaList(model.CompanyID);

                return await PartialViewAsJson("SelectAddressModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 名称検索タブの名称入力後の抽出処理
        /// </summary>
        /// <param name="key"></param>
        [HttpPost]
        public async Task<IActionResult> JsonGetAddressListForKey(string key)
        {
            try
            {
                string exceptionMessage = null;

                Context.AddressList addressList = HttpContext.Session.GetObject<Context.AddressList>(SessionKeyAddress);

                addressList ??= await SetAddressList();

                SearchAddressListDto model = new()
                {
                    SearchPostCodeList = new(),
                    SearchTatemonoList = new(),
                };

                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.ChugokuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.KinkiAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.KyusyuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.ShikokuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.KantoAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.ChubuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.TohokuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.HokurikuAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.HokkaidoAddressItem);
                AddSearchAddressList(model.SearchPostCodeList, key, (List<Dto.M_PostCode_Local>)addressList.OkinawaAddressItem);

                AddressApi api = new(_mapApiSettiong);
                Map_Building_Name_Local dataList = await api.GetBuildingNameAsync(null, key);
                if (dataList.ErrrMessage == null)
                {
                    foreach (var item in dataList.item)
                    {
                        Map_Building_NameItem_Local data = new() { position = new(), };
                        CopyProperty(data, item);
                        model.SearchTatemonoList.Add(data);
                    }
                }

                string jsonString = System.Text.Json.JsonSerializer.Serialize(model);
                using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);
                return Json(new { result = jsonString, message = exceptionMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 引数の住所リストを「SearchAddressList」に追加する
        /// </summary>
        /// <param name="listdata">M_PostCodeリスト</param>
        /// <param name="key"></param>
        /// <param name="target">コピー元のM_PostCodeリスト</param>
        private void AddSearchAddressList(List<Dto.M_PostCode_Local> listdata, string key, List<Dto.M_PostCode_Local> target)
        {
            List<Dto.M_PostCode_Local> list = target.Where(m => m.SHI_KU_CHO.Contains(key) || m.CHO_IKI.Contains(key)).ToList();

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    Dto.M_PostCode_Local data = new();
                    CopyProperty(data, item);
                    listdata.Add(data);
                }
            }
        }

        /// <summary>
        /// 地図登録ポイントリストの返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetSavePointList(int CompanyID, int UserID)
        {
            string exceptionMessage = null;
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                List<Dto.T_Point_Local> PointList = new();
                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                List<Dto.T_Point_Local> dataList = await api.GetPointListFromUserId(loguinUser.User_ID, 0);
                foreach (Dto.T_Point_Local item in dataList)
                {
                    Dto.T_Point_Local data = new();
                    CopyProperty(data, item);
                    PointList.Add(data);
                }

                string jsonString = System.Text.Json.JsonSerializer.Serialize(PointList);
                using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);
                return Json(new { result = jsonString, message = exceptionMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// ポイント登録画面（モーダル）を開く
        /// </summary>
        /// <param name="data">PointRegisterDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PointRegisterModal(PointRegisterDto data)
        {
            try
            {
                PointRegisterDto model = new()
                {
                    CompanyID = data.CompanyID,
                    UserID = data.UserID,
                    PointData = new(),
                    SelectPointKubun = 1,
                    SelectGroupID = 0,
                };

                CopyProperty(model.PointData, data.PointData);
                model.PointData.User_ID = data.UserID;
                model.GroupSelectList = await Service.SearchCommonService.GetUserGroupSelect(_mapApiSettiong, model.CompanyID, UserGroupLists.Haisya);

                return await PartialViewAsJson("PointRegisterModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 登録ポイントに対する重複チェックの結果を返却
        /// </summary>
        /// <param name="data">PointRegisterDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PointRegForCheck(PointRegisterDto data)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                string exceptionMessage = null;
                bool flgRet = false;

                int SarchUserID = 0;
                int SarchGroupID = 0;
                if (data.SelectPointKubun == 1) SarchUserID = data.UserID;
                if (data.SelectPointKubun == 2) SarchGroupID = data.PointData.Group_ID;
                data.PointData.Insert_User = loguinUser.User_ID;

                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                Dto.T_Point_Local point = await api.GetPointDataToZip(data.PointData.Address_Code, SarchUserID, SarchGroupID);
                if (point != null) { flgRet = true; }
                return Json(new { result = flgRet.ToString(), message = exceptionMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// ポイント登録画面（モーダル）の登録処理
        /// </summary>
        /// <param name="data">PointRegisterDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PointRegExec(PointRegisterDto data)
        {
            string errorMessage = null;

            try
            {

                if (PointVaridationCheck(data, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                if (data.SelectPointKubun == 1)
                {
                    data.PointData.Group_ID = 0;
                }

                if (data.SelectPointKubun == 2)
                {
                    data.PointData.User_ID = 0;
                    data.PointData.Group_ID = data.SelectGroupID;
                }

                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdatePointData(data.PointData);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// T_Point登録画面の入力チェック画面
        /// </summary>
        /// <param name="dto">PointRegisterDto</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool PointVaridationCheck(PointRegisterDto dto, out string errorMessage)
        {
            if (dto.SelectPointKubun == 2)
            {
                if (dto.PointData.Group_ID == 0) { errorMessage = "入力エラー。配車グループがが未選択です。"; return true; }
            }

            if (dto.PointData.BuildingName == null) { errorMessage = "入力エラー。ポイント名が空白です。"; return true; }
            if (dto.PointData.Address == null) { errorMessage = "入力エラー。型表示情報が空白です。"; return true; }
            if (dto.PointData.Lng == null) { errorMessage = "入力エラー。緯度情報が空白です。"; return true; }
            if (dto.PointData.Lat == null) { errorMessage = "入力エラー。経度情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// 荷物情報登録画面（モーダル）を開く
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SelectLuggageModal(AnkenRegisterModel param)
        {
            try
            {
                SelectLuggageDto model = new()
                {
                    AnkenLuggageList = new(),
                    LuggageGroupList = new(),
                    LuggageList = new(),
                    CompanyID = param.Company_ID,
                    UserID = param.UserID,
                    AnkenID = param.Anken_ID,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                model.LuggageList = await masterDataApi.GetLuggageList(param.Company_ID);
                model.LuggageList = model.LuggageList.Where(m => m.Del_Flg == false).ToList();
                model.LuggageGroupList = await masterDataApi.GetLuggageGroupList(param.Company_ID);
                model.LuggageGroupList = model.LuggageGroupList.Where(m => m.Del_Flg == false).ToList();
                model.AnkenLuggageList = param.AnkenLuggageList;

                model.AnkenLuggageList ??= new()
                {
                    new()
                    {
                        Anken_ID = param.Anken_ID,
                        Anken_Order = param.Anken_Latest_Order,
                    }
                };

                return await PartialViewAsJson("SelectLuggageModal", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 荷物情報マスタへの追加登録
        /// </summary>
        /// <param name="paramGroupID"></param>
        /// <param name="paramLuggageText"></param>
        /// <param name="paramUnit"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonAddLuggageMaster(int paramGroupID, string paramLuggageText, string paramUnit)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (paramGroupID == 0) { throw new Exception("パラメーターエラー：GroupIDが正しくありません"); }
                if (paramLuggageText == null || paramLuggageText.Length == 0) { throw new Exception("パラメーターエラー：LuggageTextが正しくありません"); }

                Dto.M_Luggage_Local luggage = new()
                {
                    Luggage_Group_ID = paramGroupID,
                    Company_ID = loguinUser.Company_ID,
                    Luggage_Name = paramLuggageText,
                    Unit_Name = paramUnit,
                    SortOrder = 0,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                var result = await masterDataApi.InsertUpdateLuggageMasterData(luggage);

                List<Dto.M_Luggage_Local> luggageList = await masterDataApi.GetLuggageList(loguinUser.Company_ID);

                M_Luggage_Local target = luggageList.FirstOrDefault(m => m.Luggage_Group_ID == paramGroupID && m.Luggage_Name == paramLuggageText)
                    ?? throw new Exception("データ登録エラー：一度画面を閉じて再度登録し直してください。");
                return Json(new { resultVal = target.Luggage_ID.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { resultVal = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 装備品情報登録画面（モーダル）を開く
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SelectEquipmentModal(AnkenRegisterModel param)
        {
            try
            {
                SelectEquipmentDto model = new()
                {
                    AnkenEquipmentList = new(),
                    EquipmentGroupList = new(),
                    EquipmentList = new(),
                    CompanyID = param.Company_ID,
                    UserID = param.UserID,
                    AnkenID = param.Anken_ID,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                model.EquipmentList = await masterDataApi.GetEquipmentList(param.Company_ID);
                model.EquipmentList = model.EquipmentList.Where(m => m.Del_Flg == false).ToList();
                model.EquipmentGroupList = await masterDataApi.GetEquipmentGroupList(param.Company_ID);
                model.EquipmentGroupList = model.EquipmentGroupList.Where(m => m.Del_Flg == false).ToList();
                model.AnkenEquipmentList = param.AnkenEquipmentList;

                model.AnkenEquipmentList ??= new()
                {
                    new()
                    {
                        Anken_ID = param.Anken_ID,
                        Anken_Order = param.Anken_Latest_Order,
                    }
                };

                return await PartialViewAsJson("SelectEquipmentModal", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 装備品情報マスタへの追加登録
        /// </summary>
        /// <param name="paramGroupID"></param>
        /// <param name="paramEquipmentText"></param>
        /// <param name="paramUnit"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonAddEquipmentMaster(int paramGroupID, string paramEquipmentText, string paramUnit)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (paramGroupID == 0) { throw new Exception("パラメーターエラー：GroupIDが正しくありません"); }
                if (paramEquipmentText == null || paramEquipmentText.Length == 0) { throw new Exception("パラメーターエラー：EquipmentTextが正しくありません"); }

                Dto.M_Equipment_Local luggage = new()
                {
                    Equipment_Group_ID = paramGroupID,
                    Company_ID = loguinUser.Company_ID,
                    Equipment_Name = paramEquipmentText,
                    Unit_Name = paramUnit,
                    SortOrder = 0,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                var result = await masterDataApi.InsertUpdateEquipmentMasterData(luggage);

                List<Dto.M_Equipment_Local> luggageList = await masterDataApi.GetEquipmentList(loguinUser.Company_ID);

                M_Equipment_Local target = luggageList.FirstOrDefault(m => m.Equipment_Group_ID == paramGroupID && m.Equipment_Name == paramEquipmentText)
                    ?? throw new Exception("データ登録エラー：一度画面を閉じて再度登録し直してください。");
                return Json(new { resultVal = target.Equipment_ID.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { resultVal = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 過去案件検索一覧画面に遷移
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SelectKakoAnkenList(AnkenRegisterModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DateTime dt = DateTime.Now;

                SelectKakoAnkenListDto model = new()
                {
                    Company_ID = loguinUser.Company_ID,
                    SelectMonth_From = DateTime.Parse(dt.AddYears(-2).ToString("yyyy/MM/01")),
                    SelectMonth_To = DateTime.Parse(dt.ToString("yyyy/MM/01")),
                };

                HttpContext.Session.SetObject("AnkenRegisterModel", param);

                SearchModelForAnkenList search = new()
                {
                    TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                    SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                    KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                    SelectTantou = param.SelectTantou,
                    SelectTab = param.SelectTab,
                    SelectDay = param.SelectDay,
                    SelectGroup = param.SelectGroup,
                };

                model.Search = search;

                return View("SelectKakoAnkenList", model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        /// <summary>
        /// 過去案件検索リストの返却
        /// </summary>
        /// <param name="param">AnkenRegisterModel</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetKakoAnkenList(AnkenRegisterModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                SelectKakoAnkenListDto model = new()
                {
                };

                if (param.KokyakuId == 0) { return await PartialViewAsJson("SelectKakoAnkenList", model, true); }

                using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                IEnumerable<Dto.V_AnkenDataList_Local> ankenDataList = await api.GetAnkenDataList(null, null, null, loguinUser.Company_ID, 0, param.KokyakuId, 0);

                List<PointDto_Local> pointlist = param.PointList.OrderBy(m => m.Point_Order).ToList();
                if (pointlist.Count > 0)
                {
                    PointDto_Local point = pointlist[0];
                    if (point.Address2 != null)
                    {
                        ankenDataList = ankenDataList.Where(m => m.START_Address2 == point.Address2).ToList();
                    }
                    if (point.Address3 != null)
                    {
                        ankenDataList = ankenDataList.Where(m => m.START_Address3 == point.Address3).ToList();
                    }
                }

                /////////////////////////////////車種///////////////////////////////////
                if (param.SyasyuID > 0)
                {
                    ankenDataList = ankenDataList.Where(m => m.Syaryo_ID == param.SyasyuID).ToList();
                }

                /////////////////////////////////T_Anken_Luggage///////////////////////////////////
                if (param.AnkenLuggageList != null && param.AnkenLuggageList.Count > 1)
                {
                    MasterDataApi masterDataApi = new(_mapApiSettiong);
                    List<Dto.M_Luggage_Local> luggageList = await masterDataApi.GetLuggageList(loguinUser.Company_ID);

                    List<string> target = new();

                    foreach (Dto.T_Anken_Luggage_Local data in param.AnkenLuggageList)
                    {
                        Dto.M_Luggage_Local lug = luggageList.FirstOrDefault(m => m.Luggage_ID == data.Luggage_ID);
                        if (lug != null)
                        {
                            target.Add(lug.Luggage_Name);
                        }
                    }

                    ankenDataList = ankenDataList.Where(m => m.Luggage != null).ToList();
                    ankenDataList = ankenDataList.Where(m => target.Any(p => m.Luggage.Contains(p))).ToList();
                }

                model.AnkenDataLists = ankenDataList;

                return await PartialViewAsJson("SelectKakoAnkenList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }
    }
}
