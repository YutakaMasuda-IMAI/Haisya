using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Common.SystemConstants;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// ドライバーコントローラー
    /// </summary>
    public class DriverController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="signInManager"></param>
        /// <param name="viewRenderService"></param>
        public DriverController(ILogger<DriverController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// ドライバーのインデックスページを表示
        /// </summary>
        /// <returns>インデックスビュー</returns>
        public IActionResult Index()
        {
            return View();
        }

        #region M_CompanyDriver
        /*****************************************************************************
         M_CompanyDriverマスタ
         *****************************************************************************/
        /// <summary>
        /// 会社のドライバー情報を表示
        /// </summary>
        /// <returns>会社のドライバービュー</returns>
        public async Task<IActionResult> CompanyDriver()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyDriver");

                DriverModel.DriverMenuDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyDriverList = new(),
                    M_CompanyBranchList = new(),
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.CompanyDriverList = await api.GetCompanyDriverList(loguinUser.Company_ID);
                model.M_CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.DriverMenu);
            }
        }

        /// <summary>
        /// ドライバーモーダルを表示
        /// </summary>
        /// <param name="DriverID">ドライバーID</param>
        /// <returns>ドライバーモーダルビュー</returns>
        public async Task<IActionResult> CompanyDriverModal(int DriverID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyDriver");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { throw new Exception("権限がありません。"); }

                DriverModel.CompanyDriverModalDto dto = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyDriver = new(),
                    M_CompanyBranchList = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                dto.M_CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);

                if (DriverID == 0)
                {
                    dto.CompanyDriver.Company_ID = dto.CompanyID;
                    dto.Title = "新規登録";
                    dto.BtnCaption = "設定の保存";
                }
                else
                {
                    dto.CompanyDriver = await api.GetCompanyDriverData(DriverID);
                    dto.EmployeeNumber = dto.CompanyDriver.Employee_Number.ToString();
                    dto.Title = "修正";
                    dto.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("CompanyDriverModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, message = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyDriverマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param">ドライバーモーダルDTO</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> CompanyDriverRegExec(DriverModel.CompanyDriverModalDto param)
        {
            try
            {
                if (CompanyDriverVaridationCheck(param, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                param.CompanyDriver.Employee_Number = int.Parse(param.EmployeeNumber);

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCompanyDriverMasterData(param.CompanyDriver);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyDriverマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="dto">ドライバーモーダルDTO</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>入力チェック結果</returns>
        private static bool CompanyDriverVaridationCheck(DriverModel.CompanyDriverModalDto dto, out string errorMessage)
        {
            if (dto.CompanyDriver.Branch_ID == 0) { errorMessage = "入力エラー。支店/営業所/事業所が未選択です。"; return true; }
            if (dto.CompanyDriver.Last_Name == null) { errorMessage = "入力エラー。性が空白です。"; return true; }
            if (dto.CompanyDriver.Display_Name == null) { errorMessage = "入力エラー。表示名が空白です。"; return true; }
            if (int.TryParse(dto.EmployeeNumber, out int result) == false) { errorMessage = "乗務員CDは数値のみです。"; return true; }
            errorMessage = "";
            return false;
        }

        #endregion M_CompanyDriver


        #region 乗務員車輌設定マスタ
        #region M_CompanyDriver_Syaryo
        /// <summary>
        /// 会社のドライバー車輌設定を表示
        /// </summary>
        /// <returns>ドライバー車輌設定ビュー</returns>
        public async Task<IActionResult> CompanyDriverSyaryo()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyDriverSyaryo");

                DriverModel.CompanyDriverSyaryoMenuDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyDriverSyaryoList = new(),
                    CompanyDriverList = new(),
                    SyaryoManagementList = new(),
                    CompanyBranchList = new(),
                    SyaryoList = new(),
                    CompanyUserGroupList = new(),
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.CompanyDriverSyaryoList = await api.GetCompanyDriverSyaryoList(loguinUser.Company_ID);
                model.CompanyDriverList = await api.GetCompanyDriverList(loguinUser.Company_ID);
                model.SyaryoManagementList = await api.GetSyaryoManagementList(loguinUser.Company_ID);
                model.CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);
                model.SyaryoList = await api.GetSyaryoList(loguinUser.Company_ID);
                model.CompanyUserGroupList = await api.GetCompanyUserGroupList(loguinUser.Company_ID, Service.UserGroupLists.Haisya);

                CommonModel search = new()
                {
                    TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true),
                    SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                    KataSelectList = await SearchCommonService.GetKataSelect(_mapApiSettiong, loguinUser.Company_ID),
                    SelectTantou = "ALL",
                };
                model.SelectFilter = 0;

                model.Search = search;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.DriverMenu);
            }
        }

        /*****************************************************************************
         M_CompanyDriver_Syaryoマスタ
         *****************************************************************************/
        /// <summary>
        /// ドライバー車輌設定モーダルを表示
        /// </summary>
        /// <param name="DriverID">ドライバーID</param>
        /// <param name="SyaryoID">車輌ID</param>
        /// <returns>ドライバー車輌設定モーダルビュー</returns>
        public async Task<IActionResult> CompanyDriverSyaryoModal(int DriverID, int SyaryoID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyDriverSyaryo");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { return null; }

                DriverModel.CompanyDriverSyaryoModalDto dto = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    DriverID = DriverID,
                    CompanyDriverSyaryoList = new(),
                    CompanyDriver = new(),
                    SyaryoManagementList = new(),
                    SyaryoSelectList = new(),
                    GrouSelectpList = new(),
                    SyaryoList = new(),
                    SyasyuKubunList = new(),
                    SyaryoKubunSelectList = new(),
                };
                dto.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                dto.CompanyDriver = await api.GetCompanyDriverData(DriverID);

                dto.SyaryoManagementList = await api.GetSyaryoManagementList(dto.CompanyDriver.Company_ID);
                dto.CompanyBranch = await api.GetCompanyBranchData(dto.CompanyDriver.Branch_ID);
                dto.SyaryoList = await api.GetSyaryoList(dto.CompanyDriver.Company_ID);
                dto.SyasyuKubunList = await api.GetSyasyuKubunList(dto.CompanyDriver.Company_ID);

                dto.GrouSelectpList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);
                dto.SyaryoSelectList = await SearchCommonService.GetSyaryoManagementSelect(_mapApiSettiong, loguinUser.Company_ID);

                foreach (var itemData in dto.SyasyuKubunList)
                {
                    dto.SyaryoKubunSelectList.Add(new SelectListItem() { Value = itemData.SyasyuKubun_ID.ToString(), Text = itemData.KUBUN });
                }

                if (SyaryoID == 0)
                {
                    dto.Title = "新規登録";
                    dto.BtnCaption = "設定の保存";
                    Dto.M_CompanyDriver_Syaryo_Local data = new()
                    {
                        DriverSyaryo_ID = 0,
                        Driver_ID = dto.CompanyDriver.Driver_ID,
                        Company_ID = dto.CompanyDriver.Company_ID,
                        Start_Date = DateTime.Parse("1900/01/01"),
                        SyaryoManagement_ID = 0,
                        Group_ID = 0,
                    };
                    dto.CompanyDriverSyaryoList.Add(data);
                }
                else
                {
                    dto.CompanyDriverSyaryoList = await api.GetCompanyDriverSyaryoList(dto.CompanyDriver.Company_ID, DriverID);
                    dto.Title = "修正";
                    dto.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("CompanyDriverSyaryoModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, message = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyDriver_Syaryoマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param">ドライバー車輌設定モーダルDTO</param>
        /// <returns>JSON結果</returns>
        [HttpPost]
        public async Task<IActionResult> CompanyDriverSyaryoRegExec(DriverModel.CompanyDriverSyaryoModalDto param)
        {
            string errorMessage = "";
            try
            {
                param.CompanyDriverSyaryoList = param.CompanyDriverSyaryoList.Where(m => m.Driver_ID > 0).ToList();

                if (CompanyDriver_SyaryoVaridationCheck(param, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCompanyDriverSyaryoMasterData(param.CompanyDriverSyaryoList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyDriver_Syaryoマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="dto">ドライバー車輌設定モーダルDTO</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>入力チェック結果</returns>
        private static bool CompanyDriver_SyaryoVaridationCheck(DriverModel.CompanyDriverSyaryoModalDto dto, out string errorMessage)
        {
            foreach (var target in dto.CompanyDriverSyaryoList)
            {
                if (target.SyaryoManagement_ID == 0) { errorMessage = "入力エラー。車輌が未選択です。"; return true; }
                if (target.Start_Date.Year == 1900) { errorMessage = "入力エラー。運用開始日が空白です。"; return true; }
                if (target.Group_ID == 0) { errorMessage = "入力エラー。担当配車が空白です。"; return true; }
            }
            errorMessage = "";
            return false;
        }

        #endregion M_CompanyDriver_Syaryo

        #endregion 乗務員車輌設定マスタ
    }
}