using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Http;
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
using static HaisyaWeb.Models.AnkenModel;
using static HaisyaWeb.Models.DailyReportModel;

namespace HaisyaWeb.Controllers
{
    public class DailyReportController : BaseController
    {
        const string SessionKeyDaileReport = "_objKeyDaileReport";

        // private readonly ILogger<DailyReportController> _logger; // 使用していないので一旦コメントアウト

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public DailyReportController(ILogger<DailyReportController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            //_logger = logger; // 使用していないので一旦コメントアウト
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 日報連携結果処理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(AnkenModel.SearchModelForAnkenList param)
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
                    model.Search.SingleSwitch = param.SingleSwitch;
                    model.Search.SelectEndDay = param.SelectEndDay;
                    model.Search.KokyakuId = param.KokyakuId;
                    model.Search.SelectTokuisakiName = param.KokyakuName;
                    model.Search.SelectSeikyuTantou = param.SelectSeikyuTantou;
                    model.Search.SelectFilter = param.SelectFilter;
                    model.Search.IsCollapse = param.IsCollapse;
                    model.Search.SelectTokuisakiID = param.SelectTokuisakiID;
                }

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
        /// 運転日報登録・修正/認証
        /// </summary>
        /// <param name="param">SearchModelForDailyReportList 日報検索リストモデル</param>
        /// <returns></returns>
        public async Task<IActionResult> DailyReportRegistration(SearchModelForDailyReportList param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DailyReportRegistrationModel model = new()
                {
                    HaisyaDataList = new(),
                };

                if (param == null) { throw new Exception("パラメーターエラー：SearchModelForDailyReportList"); }
                if (param.SelectDay == null) { throw new Exception("パラメーターエラー：SelectDay"); }

                string targetDate = DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd");

                model.Search = new SearchModelForDailyReportList
                {
                    SelectDay = param.SelectDay,
                    SelectEndDay = param.SelectEndDay,
                    SingleSwitch = param.SingleSwitch,
                    KokyakuId = param.KokyakuId,
                    KokyakuName = param.KokyakuName,
                    SelectTantou = param.SelectTantou,
                    SelectSeikyuTantou = param.SelectSeikyuTantou,
                    SelectFilter = param.SelectFilter,
                    SelectGroup = param.SelectGroup,
                    IsCollapse = param.IsCollapse,
                    SelectTokuisakiID = param.SelectTokuisakiID
                };

                if (!string.IsNullOrEmpty(param.SelectTokuisakiID) && int.TryParse(param.SelectTokuisakiID, out int parsedCustomerID))
                {
                    model.Search.SelectTokuisakiID = param.SelectTokuisakiID;
                }

                model.HaisyaDataList = await GetHaisyaDataListFromSearch(param);

                model.HaisyaDataData = model.HaisyaDataList.FirstOrDefault(m => m.AnkenDisplay_ID == param.AnkenDisplay_ID);

                //ラジオボタンの初期値
                model.NumOfFilter = 0;

                model.Anken_ID = param.Anken_ID;
                model.AnkenDisplay_ID = param.AnkenDisplay_ID;

                return View("DailyReportRegistration", model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 日報（案件）の詳細画面を作成して返却
        /// </summary>
        /// <param name="Anken_ID"></param>
        /// <param name="SelectDay"></param>
        /// <param name="SelectEndDay"></param>
        /// <param name="SelectTokuisakiID"></param>
        /// <returns></returns>
        public async Task<IActionResult> DailyReportRegistrationDetail(SearchModelForDailyReportList param)
        {
            try
            {
                // ログインユーザー取得
                V_LoginUser_Local loguinUser = await GetLoginUser();

                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
                IEnumerable<Dto.V_HaisyaDataList_Local> listData = await GetHaisyaDataListFromSearch(param);

                V_HaisyaDataList_Local filteredList = listData.Where(item => item.AnkenDisplay_ID == param.AnkenDisplay_ID).FirstOrDefault();

                using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
                DailyReportRegistrationDetailModel DailyReportDetail = await api.GetDailyReportAnken(param.Anken_ID,
                                    param.AnkenDisplay_ID, param.DriverCd, param.Syaban,
                                    filteredList?.KokyakuId, filteredList?.Driver_ID, filteredList?.Haisya_Kubun,
                                    filteredList?.SyaryoManagement_ID, filteredList?.Haisya_ID,
                                    filteredList?.StartDatetime ?? DateTime.MinValue,
                                    filteredList?.EndDatetime ?? DateTime.MinValue);

                DailyReportRegistrationDetailModel model = new()
                {
                    DriveRouteListData = new(),
                    PointList = new(),
                    SelectedDriveRouteDisplay = new(),
                    Search = param
                };

                model.Anken_ID = param.Anken_ID;
                model.AnkenDisplay_ID = param.AnkenDisplay_ID;
                model.SelectDay = param.SelectDay;
                model.AnkenDetail = DailyReportDetail.AnkenDetail;
                model.AnkenDisplay = DailyReportDetail.AnkenDisplay;
                model.DegitakoData = DailyReportDetail.DegitakoData;
                model.Company_ID = loguinUser.Company_ID;
                model.Area = loguinUser.Area_ID?.ToString() ?? string.Empty;
                model.HaisyaDataList = filteredList;
                model.Ferry = DailyReportDetail.AnkenDetail.Root_Ferry;
                model.Regulation = DailyReportDetail.AnkenDetail.Root_Regulation;
                model.Twouturn = DailyReportDetail.AnkenDetail.Root_Twouturn;
                model.TsumiTaskTime = "01:00";
                model.OroshiTaskTime = "01:00";
                model.Height = DailyReportDetail.AnkenDetail.Height;
                model.Width = DailyReportDetail.AnkenDetail.Width;
                model.Weight = DailyReportDetail.AnkenDetail.Weight;
                model.Nenpi = DailyReportDetail.AnkenDetail.Nenpi;
                model.SyasyuID = DailyReportDetail.AnkenDetail.Syaryo_ID;
                model.SyasyuSize = DailyReportDetail.AnkenDetail.SyasyuSize;
                model.Syasyu = DailyReportDetail.AnkenDetail.Syasyu;
                model.Kata = DailyReportDetail.AnkenDetail.Kata;
                model.TollSeikyuKubun = DailyReportDetail.TollSeikyuKubun;
                model.KUDGSIRIdList = DailyReportDetail.KUDGSIRIdList;
                model.PageType = "Nippou";
                model.SeikyuRemarks = DailyReportDetail.SeikyuRemarks;
                model.DisplayName = DailyReportDetail.DisplayName;
                model.SyabanNumber = DailyReportDetail.SyabanNumber;
                model.Nippou_ID = DailyReportDetail.Nippou_ID;
                model.Nippou = DailyReportDetail.Nippou;

                ////////////////////////////////T_Anken_PointList///////////////////////////////////
                #region T_Anken_PointList
                List<T_Anken_Point_Local> pointlist = DailyReportDetail.PointLists.OrderBy(m => m.Point_Order).ToList();
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

                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.PointKubunCode = await apiM.M_Code_DataList(3);

                model.PointSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 3);

                HttpContext.Session.Remove(SessionKeyDaileReport);
                HttpContext.Session.SetObject(SessionKeyDaileReport, model);

                return await PartialViewAsJson("DailyReportRegistrationDetail", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 日報の詳細画面を作成して返却
        /// </summary>
        /// <param name="Anken_ID"></param>
        /// <param name="SelectDay"></param>
        /// <param name="SelectEndDay"></param>
        /// <param name="SelectTokuisakiID"></param>
        /// <returns></returns>
        public async Task<IActionResult> DailyReportRegistrationDetailToNippou(SearchModelForDailyReportList param)
        {
            try
            {
                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
                IEnumerable<Dto.V_HaisyaDataList_Local> listData = await GetHaisyaDataListFromSearch(param);

                V_HaisyaDataList_Local filteredList = listData.Where(item => item.AnkenDisplay_ID == param.AnkenDisplay_ID).FirstOrDefault();

                using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
                DailyReportRegistrationDetailModel DailyReportDetail = await api.GetDailyReportDetail(param.Anken_ID,
                                    param.AnkenDisplay_ID, param.DriverCd, param.Syaban,
                                    filteredList?.KokyakuId, filteredList?.Driver_ID, filteredList?.Haisya_Kubun,
                                    filteredList?.SyaryoManagement_ID, filteredList?.Haisya_ID,
                                    filteredList?.StartDatetime ?? DateTime.MinValue,
                                    filteredList?.EndDatetime ?? DateTime.MinValue);

                DailyReportRegistrationDetailModel model = new()
                {
                    DriveRouteListData = new List<DriveRouteListDisplay_Local>(),
                    PointList = new List<PointDto_Local>(),
                    SelectedDriveRouteDisplay = new DriveRouteListDisplay_Local()
                };

                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                model.Anken_ID = param.Anken_ID;
                model.AnkenDisplay_ID = param.AnkenDisplay_ID;
                model.SelectDay = param.SelectDay;
                model.AnkenDetail = DailyReportDetail.AnkenDetail;
                model.AnkenDisplay = DailyReportDetail.AnkenDisplay;
                model.DegitakoData = DailyReportDetail.DegitakoData;
                model.NippouTollOther = DailyReportDetail.NippouTollOther;
                model.InitialDisplayNippouTollOther = DailyReportDetail.NippouTollOther;
                model.NippouToll = DailyReportDetail.NippouToll;
                model.InitialDisplayNippouToll = DailyReportDetail.NippouToll;
                model.Nippou = DailyReportDetail.Nippou;
                model.HighwayData = DailyReportDetail.HighwayData;
                model.Company_ID = loguinUser.Company_ID;
                model.Area = loguinUser.Area_ID?.ToString() ?? string.Empty;
                model.HaisyaDataList = filteredList;
                //model.Ferry = DailyReportDetail.AnkenDetail.Root_Ferry;
                //model.Regulation = DailyReportDetail.AnkenDetail.Root_Regulation;
                //model.Twouturn = DailyReportDetail.AnkenDetail.Root_Twouturn;
                //model.Height = DailyReportDetail.AnkenDetail.Height;
                //model.Width = DailyReportDetail.AnkenDetail.Width;
                //model.Weight = DailyReportDetail.AnkenDetail.Weight;
                //model.Nenpi = DailyReportDetail.AnkenDetail.Nenpi;
                //model.SyasyuID = DailyReportDetail.AnkenDetail.Syaryo_ID;
                //model.SyasyuSize = DailyReportDetail.AnkenDetail.SyasyuSize;
                //model.Syasyu = DailyReportDetail.AnkenDetail.Syasyu;
                //model.Kata = DailyReportDetail.AnkenDetail.Kata;
                model.Nippou_ID = DailyReportDetail.Nippou_ID;
                model.Nippou_Stay = DailyReportDetail.Nippou_Stay;
                model.Nippou_Kaiso = DailyReportDetail.Nippou_Kaiso;
                model.Nippou_Stay_Degitako = DailyReportDetail.Nippou_Stay_Degitako;
                model.Nippou_Kaiso_Degitako = DailyReportDetail.Nippou_Kaiso_Degitako;
                model.NippouKaisoDegitakoIdList = DailyReportDetail.NippouKaisoDegitakoIdList;
                model.NippouStayDegitakoIdList = DailyReportDetail.NippouStayDegitakoIdList;
                model.NippouApproval = DailyReportDetail.NippouApproval;
                model.TollSeikyuKubun = DailyReportDetail.TollSeikyuKubun;
                model.KUDGSIRIdList = DailyReportDetail.KUDGSIRIdList;
                model.PageType = "Nippou";
                model.SeikyuRemarks = DailyReportDetail.SeikyuRemarks;
                model.DisplayName = DailyReportDetail.DisplayName;
                model.SyabanNumber = DailyReportDetail.SyabanNumber;
                model.NippouTollDegitako = new();

                if (model.HaisyaDataList?.Haisya_Kubun != 2)
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "1", Text = "個人負担" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }
                else
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }
                if (model.Nippou == null)
                {
                    model.Nippou = new();
                    model.Nippou_Stay = new();
                    model.Nippou_Kaiso = new();
                    model.Nippou_Stay_Degitako = new();
                    model.Nippou_Kaiso_Degitako = new();
                    model.NippouKaisoDegitakoIdList = new List<int>();
                    model.NippouStayDegitakoIdList = new List<int>();
                    model.NippouApproval = new();
                    model.NippouToll = new();
                    model.NippouTollDegitako = new();
                }
                else
                {
                    foreach (var d in model.NippouToll)
                    {
                        model.NippouTollDegitako.Add(d);
                    }
                    foreach (var d in model.HighwayData)
                    {
                        if (model.NippouTollDegitako.Where(w => w.開始日時 == d.開始日時 && w.終了日時 == d.終了日時 && w.開始道路番号 == d.開始道路番号 && w.開始道路名 == d.開始道路名 && w.開始ETC番号 == d.開始ETC番号 && w.開始IC名 == d.開始IC名 && w.終了IC名 == d.終了IC名).FirstOrDefault() != null)
                        {
                            continue;
                        }
                        model.NippouTollDegitako.Add(new T_Nippou_Toll_Local()
                        {
                            ID = d.ID,
                            読取日 = d.読取日,
                            事業所CD = d.事業所CD,
                            運行日 = d.運行日?.Date ?? DateTime.MinValue,
                            事業所名 = d.事業所名,
                            車輌CD = d.車輌CD,
                            車輌名 = d.車輌名,
                            乗務員CD = d.乗務員CD,
                            乗務員名 = d.乗務員名,
                            対象乗務員区分 = d.対象乗務員区分,
                            開始日時 = d.開始日時,
                            終了日時 = d.終了日時,
                            開始道路番号 = d.開始道路番号,
                            開始道路名 = d.開始道路名,
                            開始ETC番号 = d.開始ETC番号,
                            開始IC名 = d.開始IC名,
                            終了道路番号 = d.終了道路番号,
                            終了道路名 = d.終了道路名,
                            終了ETC番号 = d.終了ETC番号,
                            終了IC名 = d.終了IC名,
                            精算区分 = d.精算区分,
                            精算区分名 = d.精算区分名,
                            料金 = d.料金,
                            走行距離 = d.走行距離,
                            読取NO = d.読取NO
                        });
                    }
                }
                model.NippouTollDegitako = model.NippouTollDegitako.OrderBy(o => o.開始日時).ToList();
                for (int i = 0; i < model.NippouTollDegitako.Count; i++)
                {
                    model.NippouTollDegitako[i].Sort = i;
                }

                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.PointKubunCode = await apiM.M_Code_DataList(3);

                model.GroupUserSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 9);
                model.PointSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 3);

                model.OtherPaidDataList = new();

                return await PartialViewAsJson("DailyReportRegistrationDetail_Nippou", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 検索内容からV_HaisyaDataListを返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<List<Dto.V_HaisyaDataList_Local>> GetHaisyaDataListFromSearch(SearchModelForDailyReportList param)
        {

            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            List<Dto.V_HaisyaDataList_Local> list = null;

            // 検索日付が範囲指定の場合
            DateTime? dateEnd = null;
            // param.SelectEndDayの値を確認
            if (DateTime.TryParse(param.SelectEndDay, out DateTime parsedFromDate))
            {
                dateEnd = DateTime.Parse(param.SelectEndDay);
            }

            int customerID = 0;
            if (!string.IsNullOrEmpty(param.SelectTokuisakiID) && int.TryParse(param.SelectTokuisakiID, out int parsedCustomerID))
            {
                customerID = parsedCustomerID;
            }

            using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
            if (dateEnd != null)
            {
                list = await apiH.GetHaisyaDataList(loguinUser.Company_ID,
                                null, DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), ((DateTime)dateEnd).ToString("yyyy/MM/dd"),
                                customerID);
            }
            else
            {
                list = await apiH.GetHaisyaDataList(loguinUser.Company_ID,
                                DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, customerID);
            }

            return list;
        }

        /// <summary>
        /// モーダルを開く
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CertificationRequestModal(Dto.CertificationRequestModel param)
        {
            try
            {
                using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
                Dto.CertificationRequestModel model = await api.GetCertificationRequestModel(param.Nippou_Approval_ID);

                model.GroupUserSelectList = model.GroupUserList.Select(item => new SelectListItem
                {
                    Value = item.Group_ID.ToString(),
                    Text = item.Display_Name
                }).ToList();

                if (model.NippouApprovalData != null)
                {
                    DateTime limitDate = DateTime.Parse(param.FormattedLimitDate);
                    if (limitDate != DateTime.MinValue)
                    {
                        model.FormattedLimitDate = limitDate.ToString("yyyy-MM-dd");
                    }
                    else if (model.NippouApprovalData.Limit_DateTime == DateTime.MinValue)
                    {
                        // システム日付
                        model.FormattedLimitDate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        model.FormattedLimitDate = model.NippouApprovalData.Limit_DateTime.ToString("yyyy-MM-dd");
                    }
                }
                model.Comment = param.Comment ?? model.NippouApprovalData.Order_Memo;
                model.Nippou_ID = param.Nippou_ID;
                model.Approval_Group_ID = param.Approval_Group_ID == 0 ? model.NippouApprovalData.Approval_Group_ID : param.Approval_Group_ID;

                return await PartialViewAsJson("CertificationRequestModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 承認依頼モーダル画面を返信する（Json）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostCertificationRequest(Dto.CertificationRequestModel data)
        {
            try
            {
                using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
                var result = await api.PostCertificationRequest(data);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// 宿泊料金登録用モーダル画面を開く
        /// </summary>
        /// <param name="OvernightModalData"></param>
        /// <returns></returns>
        public async Task<IActionResult> OvernightModal(string OvernightModalData)
        {
            try
            {
                DailyReportRegistrationDetailModel model = new()
                {
                    Nippou_Stay = new(),
                };

                //セッション情報からデータの取得
                model = HttpContext.Session.GetObject<DailyReportRegistrationDetailModel>(SessionKeyDaileReport);

                if (!string.IsNullOrEmpty(OvernightModalData))
                {
                    DailyReportRegistrationDetailModel model2 = JsonConvert.DeserializeObject<DailyReportRegistrationDetailModel>(OvernightModalData);

                    model.Nippou_Stay ??= new();
                    model.NippouStayDegitakoIdList ??= new();

                    model.Nippou_Stay.Start_Datetime = model2.Nippou_Stay.Start_Datetime;
                    model.Nippou_Stay.End_Datetime = model2.Nippou_Stay.End_Datetime;
                    model.Nippou_Stay.Start_ShikuName = model2.Nippou_Stay.Start_ShikuName;
                    model.Nippou_Stay.End_ShikuName = model2.Nippou_Stay.End_ShikuName;
                    model.Nippou_Stay.Interval_Time = model2.Nippou_Stay.Interval_Time;
                    model.Nippou_Stay.Dllowance = model2.Nippou_Stay.Dllowance;
                    model.NippouStayDegitakoIdList = model2.NippouStayDegitakoIdList;
                }

                if (model.Nippou_Stay != null && model.Nippou_Stay.Interval_Time.HasValue)
                {
                    int totalMinutes = model.Nippou_Stay.Interval_Time.Value;
                    int hours = totalMinutes / 60;
                    int minutes = totalMinutes % 60;
                    model.FormattedIntervalTime = $"{hours:D2}:{minutes:D2}";
                }
                else
                {
                    model.FormattedIntervalTime = "0";
                }

                return await PartialViewAsJson("OvernightModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 空車回送料金登録用モーダル画面を開く        
        /// </summary>
        /// <param name="EmptyCarModalData"></param>
        /// <returns></returns>        
        public async Task<IActionResult> EmptyCarModal(string EmptyCarModalData)
        {
            try
            {
                DailyReportRegistrationDetailModel model = new();

                //セッション情報からデータの取得
                model = HttpContext.Session.GetObject<DailyReportRegistrationDetailModel>(SessionKeyDaileReport);

                if (!string.IsNullOrEmpty(EmptyCarModalData))
                {
                    DailyReportRegistrationDetailModel model2 = JsonConvert.DeserializeObject<DailyReportRegistrationDetailModel>(EmptyCarModalData);

                    model.Nippou_Kaiso ??= new();
                    model.NippouKaisoDegitakoIdList ??= new();

                    model.Nippou_Kaiso.Start_Datetime = model2.Nippou_Kaiso.Start_Datetime;
                    model.Nippou_Kaiso.End_Datetime = model2.Nippou_Kaiso.End_Datetime;
                    model.Nippou_Kaiso.Start_ShikuName = model2.Nippou_Kaiso.Start_ShikuName;
                    model.Nippou_Kaiso.End_ShikuName = model2.Nippou_Kaiso.End_ShikuName;
                    model.Nippou_Kaiso.Distance = model2.Nippou_Kaiso.Distance;
                    model.Nippou_Kaiso.Dllowance = model2.Nippou_Kaiso.Dllowance;
                    model.Nippou_Kaiso.Commnet = model2.Nippou_Kaiso.Commnet;
                    model.NippouKaisoDegitakoIdList = model2.NippouKaisoDegitakoIdList;
                }
                return await PartialViewAsJson("EmptyCarModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// OtherPaidModalモーダルを開く        
        /// </summary>
        /// <param name="NippouTollOther"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns> 
        public async Task<IActionResult> OtherPaidModal(string NippouTollOther, int rowIndex)
        {
            try
            {
                T_Nippou_Toll_Other model;

                if (string.IsNullOrEmpty(NippouTollOther))
                {
                    // highwayDataがnullまたは空文字列の場合の処理
                    model = new T_Nippou_Toll_Other(); // 空のモデルを作成するか、適切な初期値を設定する
                }
                else
                {
                    model = JsonConvert.DeserializeObject<T_Nippou_Toll_Other>(NippouTollOther);
                }

                model.GroupUserSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 9, true);

                model.Index = rowIndex;

                return await PartialViewAsJson("OtherPaidModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// HighwayDataModalモーダルを開く    
        /// </summary>
        /// <param name="nippouToll"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns> 
        public async Task<IActionResult> HighwayDataModal(string nippouToll, int rowIndex)
        {
            try
            {
                T_Nippou_Toll model;

                if (string.IsNullOrEmpty(nippouToll))
                {
                    // highwayDataがnullまたは空文字列の場合の処理
                    model = new T_Nippou_Toll(); // 空のモデルを作成するか、適切な初期値を設定する
                }
                else
                {
                    model = JsonConvert.DeserializeObject<T_Nippou_Toll>(nippouToll);
                }
                model.Index = rowIndex;
                model.FormattedStartDate = model.開始日時?.ToString("yyyy-MM-ddThh:mm") ?? string.Empty;
                model.FormattedEndDate = model.終了日時?.ToString("yyyy-MM-ddThh:mm") ?? string.Empty;

                return await PartialViewAsJson("HighwayDataModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// UpdateOtherPaidDataのリストを返す
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> UpdateOtherPaidData(DailyReportRegistrationDetailModel param)
        {
            try
            {
                DailyReportRegistrationDetailModel model = new()
                {
                    HaisyaDataList = param.HaisyaDataList
                };
                if (model.HaisyaDataList.Haisya_Kubun != 2)
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "1", Text = "個人負担" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }
                else
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }

                model.GroupUserSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 9, false);

                model.NippouTollOther = param.NippouTollOther;
                // 更新後のViewModelを返す
                return await PartialViewAsJson("OthderPaidList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// UpdateHighwayDataリストを返す
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns> 
        [HttpPost]
        public async Task<IActionResult> UpdateHighwayData(string paramStr)
        {
            try
            {
                var param = JsonConvert.DeserializeObject<DailyReportRegistrationDetailModel>(paramStr);
                DailyReportRegistrationDetailModel model = new()
                {
                    NippouToll = param.NippouToll == null || !param.NippouToll.Any()
                        ? new()
                        : param.NippouToll,
                    HaisyaDataList = param.HaisyaDataList,
                    NippouTollDegitako = param.NippouToll == null || !param.NippouToll.Any()
                        ? new()
                        : param.NippouToll,
                    HighwayData = param.HighwayData == null || !param.HighwayData.Any()
                        ? new()
                        : param.HighwayData,
                };
                if (model.HaisyaDataList.Haisya_Kubun != 2)
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "1", Text = "個人負担" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }
                else
                {
                    model.FutanKubunList = new List<SelectListItem>
                    {
                        new() { Value = "0", Text = "" },
                        new() { Value = "2", Text = "会社負担" },
                        new() { Value = "3", Text = "荷主負担" }
                    };
                }

                model.Nippou = param.Nippou;
                model.NippouTollDegitako = param.NippouToll;
                foreach (var d in model.HighwayData)
                {
                    if (model.NippouTollDegitako.Where(w => w.開始日時 == d.開始日時 && w.終了日時 == d.終了日時 && w.開始道路番号 == d.開始道路番号 && w.開始道路名 == d.開始道路名 && w.開始ETC番号 == d.開始ETC番号 && w.開始IC名 == d.開始IC名 && w.終了IC名 == d.終了IC名).FirstOrDefault() != null)
                    {
                        continue;
                    }
                    else
                    {
                        model.NippouTollDegitako.Add(new T_Nippou_Toll_Local()
                        {
                            ID = d.ID,
                            読取日 = d.読取日,
                            事業所CD = d.事業所CD,
                            運行日 = d.運行日 ?? new DateTime(),
                            事業所名 = d.事業所名,
                            車輌CD = d.車輌CD,
                            車輌名 = d.車輌名,
                            乗務員CD = d.乗務員CD,
                            乗務員名 = d.乗務員名,
                            対象乗務員区分 = d.対象乗務員区分,
                            開始日時 = d.開始日時,
                            終了日時 = d.終了日時,
                            開始道路番号 = d.開始道路番号,
                            開始道路名 = d.開始道路名,
                            開始ETC番号 = d.開始ETC番号,
                            開始IC名 = d.開始IC名,
                            終了道路番号 = d.終了道路番号,
                            終了道路名 = d.終了道路名,
                            終了ETC番号 = d.終了ETC番号,
                            終了IC名 = d.終了IC名,
                            精算区分 = d.精算区分,
                            精算区分名 = d.精算区分名,
                            料金 = d.料金,
                            走行距離 = d.走行距離,
                            読取NO = d.読取NO,
                            Futan_Kubun = 0
                        });
                    }
                }

                model.NippouTollDegitako = model.NippouTollDegitako.OrderBy(o => o.開始日時).ToList();
                model.InitialDisplayNippouToll = param.InitialDisplayNippouToll == null || !param.InitialDisplayNippouToll.Any()
                    ? new()
                    : param.InitialDisplayNippouToll;

                // 更新後のViewModelを返す
                return await PartialViewAsJson("HighwayList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// ProvisionalRegistrationの処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ProvisionalRegistration(string data)
        {
            try
            {
                DailyReportRegistrationDetailModel model;

                if (string.IsNullOrEmpty(data))
                {
                    // highwayDataがnullまたは空文字列の場合の処理
                    model = new DailyReportRegistrationDetailModel(); // 空のモデルを作成するか、適切な初期値を設定する
                }
                else
                {
                    model = JsonConvert.DeserializeObject<DailyReportRegistrationDetailModel>(data);
                    //デジタコ連携のデータをT_Nippou_Tollに追加
                    foreach (var d in model.NippouTollDegitako)
                    {
                        var existingNippouToll = model.NippouToll.Where(w => w.開始日時 == d.開始日時 && w.終了日時 == d.終了日時 && w.開始道路番号 == d.開始道路番号 && w.開始道路名 == d.開始道路名 && w.開始ETC番号 == d.開始ETC番号 && w.開始IC名 == d.開始IC名 && w.終了IC名 == d.終了IC名).FirstOrDefault();
                        if (existingNippouToll != null)
                        {
                            existingNippouToll.Sort = d.Sort;
                            existingNippouToll.Futan_Kubun = d.Futan_Kubun;
                            continue;
                        }
                        model.NippouToll.Add(new T_Nippou_Toll_Local()
                        {
                            Futan_Kubun = d.Futan_Kubun,
                            Sort = d.Sort,
                            読取日 = d.読取日,
                            事業所CD = d.事業所CD,
                            運行日 = d.運行日,
                            事業所名 = d.事業所名,
                            車輌CD = d.車輌CD,
                            車輌名 = d.車輌名,
                            乗務員CD = d.乗務員CD,
                            乗務員名 = d.乗務員名,
                            対象乗務員区分 = d.対象乗務員区分,
                            開始日時 = d.開始日時,
                            終了日時 = d.終了日時,
                            開始道路番号 = d.開始道路番号,
                            開始道路名 = d.開始道路名,
                            開始ETC番号 = d.開始ETC番号,
                            開始IC名 = d.開始IC名,
                            終了道路番号 = d.終了道路番号,
                            終了道路名 = d.終了道路名,
                            終了ETC番号 = d.終了ETC番号,
                            終了IC名 = d.終了IC名,
                            精算区分 = d.精算区分,
                            精算区分名 = d.精算区分名,
                            料金 = d.料金,
                            走行距離 = d.走行距離,
                        });
                    }
                
                }

                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                model.User_ID = loguinUser.User_ID;

                using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
                var result = await api.ProvisionalRegistration(model);
                return Json(new { data = result });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
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

            DataListModel model = new() { };

            model.Company_ID = loguinUser.Company_ID;

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
        /// JSON：案件一覧データHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetDataList(SearchModelForDailyReportList param, DataListSortModel sortParam)
        {
            sortParam.SortOrder ??= "asc";
            sortParam.SortItemParam ??= nameof(HaisyaDataList.Anken_ID);
            DataListModel model = new()
            {
                SortParam = sortParam,
            };

            using API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            try
            {
                int customerID = 0;
                string html = "DataListForNone";
                if (param == null) { return null; }
                if (!string.IsNullOrEmpty(param.SelectTokuisakiID))
                {
                    Dto.M_Customer_Branch_Local CustomerBranch = await api.GetCustomerBranchData(int.Parse(param.SelectTokuisakiID));
                    customerID = (CustomerBranch != null) ? CustomerBranch.Customer_ID : 0;
                }
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                // 日付範囲指定の場合
                DateTime? dateEnd = null;
                // param.SelectEndDayの値を確認
                if (DateTime.TryParse(param.SelectEndDay, out DateTime parsedFromDate))
                {
                    dateEnd = parsedFromDate;
                }

                IEnumerable<Dto.V_HaisyaDataList_Local> list = null;

                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);

                if (dateEnd != null)
                {
                    list = await apiH.GetHaisyaDataList(loguinUser.Company_ID,
                                    null, DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), ((DateTime)dateEnd).ToString("yyyy/MM/dd"), customerID);
                }
                else
                {
                    list = await apiH.GetHaisyaDataList(loguinUser.Company_ID,
                                    DateTime.Parse(param.SelectDay).ToString("yyyy/MM/dd"), null, null, customerID);
                }

                List<T_Nippou_Local> tNippous = await GetTNippous();

                List<HaisyaDataList> listData = new();

                if (list != null)
                {
                    foreach (var data in list)
                    {
                        HaisyaDataList haisyaDataList = new HaisyaDataList(data);
                        foreach (var nippou in tNippous)
                        {
                            if (nippou.Anken_ID == data.Anken_ID)
                            {
                                haisyaDataList.TNippou = nippou;
                            }
                        }

                        listData.Add(haisyaDataList);
                    }

                    if (param.SelectTantou != null && !"ALL".Equals(param.SelectTantou))
                    {
                        listData = listData.Where(m => m.TantouID == int.Parse(param.SelectTantou)).ToList();
                    }

                    if (param.SelectSeikyuTantou != null && !"ALL".Equals(param.SelectSeikyuTantou))
                    {
                        listData = listData.Where(m => m.KokyakuTantouId == int.Parse(param.SelectSeikyuTantou)).ToList();
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
                        listData = listData.Where(m => m?.TNippou?.Receipt == 0).ToList();
                    }

                    if (param.SelectFilter == 2)
                    {
                        listData = listData.Where(m => m?.TNippou?.Receipt == 1).ToList();
                    }

                    if (param.SelectGroup == 1)
                    {
                        listData = listData.Where(m => m?.TNippou?.ApprovalStatus == 0).ToList();
                    }

                    if (param.SelectGroup == 2)
                    {
                        listData = listData.Where(m => m?.TNippou?.ApprovalStatus == 1).ToList();
                    }
                }

                model.DataDataLists = listData;

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 日報一覧取得
        /// </summary>
        /// <returns></returns>
        private async Task<List<Dto.T_Nippou_Local>> GetTNippous()
        {
            using API.WebApp.DailyReportDataApi api = new(_mapApiSettiong);
            return await api.GetTNippous();
        }

        public async Task<IActionResult> AnkenPointRegExec(DailyReportRegistrationDetailModel param)
        {
            try
            {
                AnkenDataModelDto_Local model = new()
                {
                    T_Anken = new(),
                    T_Anken_PointList = new(),
                };

                model.T_Anken.Anken_ID = param.Anken_ID;
                foreach (var target in param.PointList)
                {
                    Dto.T_Anken_Point_Local point = new();
                    CopyProperty(point, target);
                    model.T_Anken_PointList.Add(point);
                }

                using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                UpdateAnkenDataDto returnVal = await api.UpdateAnkenDataForPoint(model);

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
    }
}
