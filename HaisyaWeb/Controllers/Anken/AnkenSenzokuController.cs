using HaisyaWeb.API.WebApp;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
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

namespace HaisyaWeb.Controllers.Anken
{
    [Authorize]
    public class AnkenSenzokuController : BaseController
    {
        private readonly ILogger<AnkenSenzokuController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public AnkenSenzokuController(ILogger<AnkenSenzokuController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 専属ドライバー案件登録一覧画面へ遷移
        /// </summary>
        /// <param name="param">SenzokuBaseModel</param>
        /// <returns></returns>
        public async Task<IActionResult> SenzokuRegIndex(SenzokuBaseModel param)
        {
            try
            {
                if (param.SenzokuID == 0) { throw new Exception("パラメーターエラー：SenzokuIDが正しくありません"); }
                if (param.SenzokuDriverID == 0) { throw new Exception("パラメーターエラー：SenzokuDriverIDが正しくありません"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                SenzokuRegsterListModel model = new()
                {
                    SelectDay = param.SelectDay,
                    SelectTantou = param.SelectTantou,
                    SelectSyasyu = param.SelectSyasyu,
                    SelectKata = param.SelectKata,
                    SelectGroup = param.SelectGroup,
                    SelectMonth = param.SelectMonth,
                    BackMenuAction = "SenzokuRegIndex",
                    BackMenuAction2 = "SenzokuRegIndex",
                    SenzokuID = param.SenzokuID,
                    SenzokuDriverID = param.SenzokuDriverID,
                    TantouID = param.TantouID,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                model.SenzokuDriverData = await masterDataApi.GetSenzokuDriverViewData(loguinUser.Company_ID, null, model.SenzokuDriverID);

                return View("SenzokuAnkenList", model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 一覧データの返却（Json）
        /// </summary>
        /// <param name="SelectMonth"></param>
        /// <param name="SenzokuID"></param>
        /// <param name="SenzokuDriverID"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetSenzokuAnkenList(DateTime SelectMonth, int SenzokuID, int SenzokuDriverID)
        {

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                SenzokuRegsterListModel model = new()
                {
                    SelectMonth = SelectMonth,
                    SenzokuID = SenzokuID,
                    SenzokuDriverID = SenzokuDriverID,
                };

                MasterDataApi masterDataApi = new(_mapApiSettiong);
                model.SenzokuData = await masterDataApi.GetSenzokuViewData(loguinUser.Company_ID, SenzokuID);
                model.SenzokuDriverData = await masterDataApi.GetSenzokuDriverViewData(loguinUser.Company_ID, SelectMonth, SenzokuDriverID);
                //model.CompanyDriverData = await masterDataApi.GetCompanyDriverList(loguinUser.Company_ID);

                using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                IEnumerable<Dto.V_AnkenDataList_Local> ankenDataList = await api.GetAnkenDataList(null,
                                                    SelectMonth.ToString("yyyy/MM/01"), SelectMonth.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"),
                                                    loguinUser.Company_ID, 0, 0, SenzokuID);
                ankenDataList = ankenDataList.Where(m => m.Senzoku_Driver_ID == SenzokuDriverID).ToList();
                model.AnkenDataList = ankenDataList;

                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
                model.haisyaList = await apiH.GetHaisyaDataList(loguinUser.Company_ID, null,
                                                    SelectMonth.ToString("yyyy/MM/01"), SelectMonth.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd"),
                                                    0, 0, SenzokuID);

                return await PartialViewAsJson("SenzokuAnkenDataList", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        ///　専属ドライバー一覧の選択画面
        /// </summary>
        /// <param name="param">SearchModelForAnkenList</param>
        /// <returns></returns>
        public async Task<IActionResult> SelectSenzokuDriver(SearchModelForAnkenList param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                DateTime dt = DateTime.Now;

                SenzokuSelectDriverModel model = new()
                {
                    SelectMonth = DateTime.Parse(dt.ToString("yyyy/MM/01")),
                    BackMenuAction = "Home",
                };

                if (param != null && param.SelectDay != null )
                {
                    if (param.SelectMonth.Year > 2000) model.SelectMonth = param.SelectMonth;
                    if (param.SelectMonth.Year <= 2000) model.SelectMonth = DateTime.Parse(DateTime.Parse(param.SelectDay).ToString("yyyy/MM/01"));
                    model.SelectDay = param.SelectDay;
                    model.SelectTantou = param.SelectTantou;
                    model.SelectSyasyu = param.SelectSyasyu;
                    model.SelectKata = param.SelectKata;
                    model.SelectGroup = param.SelectGroup;
                    model.BackMenuAction = param.BackMenuAction;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 一覧データの返却（Json）
        /// </summary>
        /// <param name="selectMonth"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonGetSenzokuDriverList(DateTime selectMonth)
        {
            SenzokuSelectDriverModel model = new();

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                MasterDataApi masterDataApi = new(_mapApiSettiong);

                model.SenzokuDriverList = await masterDataApi.GetSenzokuDriverViewList(loguinUser.Company_ID, selectMonth);
                model.CompanyUserGroupList = await masterDataApi.GetCompanyUserGroupList(loguinUser.Company_ID, UserGroupLists.Haisya);

                return await PartialViewAsJson("SelectSenzokuDriverList", model);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 案件登録画面に遷移
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IActionResult> AnkenRegisterIndex(SenzokuBaseModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                AnkenBaseModel model = new();

                CopyProperty(model, param);
                model.Company_ID = loguinUser.Company_ID;

                return param.AnkenId > 0
                    ? RedirectToAction("Index", "AnkenRegister", model)
                    : RedirectToAction("RegisterIndex", "AnkenRegister", model);
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}
