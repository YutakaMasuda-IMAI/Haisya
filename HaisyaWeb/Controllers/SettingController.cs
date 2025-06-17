using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using HaisyaWeb.Dto;
using static HaisyaWeb.Models.SettingModel;
using static HaisyaWeb.Common.SystemConstants;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// SettingController クラスは、設定画面に関連する操作を管理します。
    /// ユーザー権限の確認、データの取得・保存、および画面の表示処理を行います。
    /// </summary>
    [Authorize]
    public class SettingController : BaseController
    {
        private readonly ILogger<SettingController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public SettingController(ILogger<SettingController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 設定画面のトップページを表示します。
        /// </summary>
        /// <returns>設定画面のビュー。</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return await CompanyMenu();
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        #region 会社情報マスタ
        #region M_Company
        /*****************************************************************************
          M_Company M_CompanyBranchマスタ
          *****************************************************************************/
        /// <summary>
        /// M_Company M_CompanyBranchマスタ画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CompanyMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyMenu");

                SettingModel.CompanyDto model = new()
                {
                    M_Company = new(),
                    M_CompanyBranchList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_Company = await api.GetCompanyData(loguinUser.Company_ID);
                model.M_CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return View("CompanyMenu", model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MasterData1Menu);
            }
        }

        /// <summary>
        /// M_CompanyBranchマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> CompanyBranchModal(int BranchID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoListModal");

                SettingModel.CompanyBranchModalDto model = new()
                {
                    M_Company = new(),
                    M_CompanyBranch = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                if (0 == BranchID)
                {
                    model.M_CompanyBranch.Company_ID = loguinUser.Company_ID;
                    model.M_CompanyBranch.Branch_ID = 0;
                    model.M_CompanyBranch.Oya_Branch_ID = 0;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    model.M_Company = await api.GetCompanyData(loguinUser.Company_ID);
                    model.M_CompanyBranch = await api.GetCompanyBranchData(BranchID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("CompanyBranchModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_Companyマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyRegExec(SettingModel.CompanyDto param)
        {
            try
            {
                if (CompanyVaridationCheck(param.M_Company, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCompanyData(param.M_Company);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_Companyマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mSyaryo"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CompanyVaridationCheck(Dto.M_Company_Local mSyaryo, out string errorMessage)
        {
            if (mSyaryo.Company_Name == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            if (mSyaryo.Company_Name_Abbr == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }
            errorMessage = "";
            return false;
        }
        #endregion M_Company

        #region M_CompanyBranch 
        /// <summary>
        /// M_CompanyBranchマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyBranchRegExec(SettingModel.CompanyBranchModalDto param)
        {
            try
            {
                if (CompanyBranchVaridationCheck(param.M_CompanyBranch, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateForCompanyBranch(param.M_CompanyBranch);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyBranchマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mSyaryo"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CompanyBranchVaridationCheck(Dto.M_CompanyBranch_Local mSyaryo, out string errorMessage)
        {
            if (mSyaryo.Branch_Name == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            if (mSyaryo.Branch_Name_Abbr == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }
            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_CompanyBranchマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyBranchListSortCommit(SettingModel.CompanyDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateSortOrderForCompanyBranch(param.M_CompanyBranchList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
        #endregion M_CompanyBranch
        #endregion 会社情報マスタ

        #region 従業員マスタ
        /// <summary>
        /// 従業員情報メニューを表示します。
        /// ログインユーザーの権限を確認し、該当企業の従業員情報および支店情報を取得してモデルに設定します。
        /// </summary>
        /// <returns>従業員情報メニューのビュー。</returns>
        public async Task<IActionResult> EmployeeMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "EmployeeMenu");

                EmployeeMenuDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyUserList = new(),
                    M_CompanyBranchList = new(),
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.CompanyUserList = await api.GetCompanyUserList(loguinUser.Company_ID, "all");
                model.M_CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MasterData1Menu);
            }
        }

        #region M_CompanyUser
        /*****************************************************************************
         M_CompanyUserマスタ
         *****************************************************************************/
        /// <summary>
        /// 従業員情報のモーダル画面を表示します。
        /// 指定されたユーザーIDに基づいて、従業員情報を新規登録または編集用に取得します。
        /// </summary>
        /// <param name="UserID">編集対象のユーザーID。新規登録の場合は0。</param>
        /// <returns>従業員情報のモーダルビューをJSON形式で返します。</returns>
        public async Task<IActionResult> CompanyUserModal(int UserID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "EmployeeMenu");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { return null; }

                SettingModel.CompanyUserModalDto dto = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyUser = new(),
                    M_CompanyBranchList = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                dto.M_CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);

                if (UserID == 0)
                {
                    dto.CompanyUser.Company_ID = dto.CompanyID;
                    dto.Title = "新規登録";
                    dto.BtnCaption = "設定の保存";
                }
                else
                {
                    dto.CompanyUser = await api.GetCompanyUserData(UserID);
                    dto.EmployeeNumber = dto.CompanyUser.Employee_Number.ToString();
                    dto.Title = "修正";
                    dto.BtnCaption = "設定の保存";
                }
                
                return await PartialViewAsJson("CompanyUserModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyUserマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyUserRegExec(SettingModel.CompanyUserModalDto param)
        {
            try
            {
                if (CompanyUserVaridationCheck(param, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                if (param.EmployeeNumber != null)
                {
                    param.CompanyUser.Employee_Number = int.Parse(param.EmployeeNumber);
                }
                
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCompanyUserMasterData(param.CompanyUser);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyUserマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mCompanyUser"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CompanyUserVaridationCheck(SettingModel.CompanyUserModalDto dto, out string errorMessage)
        {
            if (dto.CompanyUser.Branch_ID == 0) { errorMessage = "入力エラー。支店/営業所/事業所が未選択です。"; return true; }
            if (dto.CompanyUser.Last_Name == null) { errorMessage = "入力エラー。性が空白です。"; return true; }
            if (dto.CompanyUser.Display_Name == null) { errorMessage = "入力エラー。表示名が空白です。"; return true; }
            if (dto.EmployeeNumber != null && int.TryParse(dto.EmployeeNumber, out int result) == false) { errorMessage = "従業員CDは数値のみです。"; return true; }
            errorMessage = "";
            return false;
        }

        #endregion M_CompanyUser

        #endregion 従業員マスタ

        #region ログインユーザーマスタ
        /*****************************************************************************
          M_CompanyUserマスタ
          *****************************************************************************/
        /// <summary>
        /// ログインユーザー情報のメニューを表示します。
        /// ユーザーの権限を確認し、ログインユーザーとそのロール情報を取得してモデルに設定します。
        /// </summary>
        /// <returns>ログインユーザーメニューのビュー。</returns>
        public async Task<IActionResult> LoginUserMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "LoginUserMenu");

                SettingModel.LoginUserMenuDto model = new()
                {
                    CompanyID = loguinUser.Branch_ID,
                    LoginUserList = new(),
                    LoginUserRoleList = new(),
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.LoginUserRoleList = await api.GetLoginUserRoleList(model.CompanyID);
                model.LoginUserList = await api.GetV_LoginUserList(model.CompanyID);
                model.LoginUserList = model.LoginUserList.Where(m => m.CompanyUser_Del_Flg == false).ToList();
  
                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.MasterData1Menu);
            }
        }


        #region M_LoginUser_Role
        /*****************************************************************************
         M_LoginUser_Roleマスタ
         *****************************************************************************/
        /// <summary>
        /// ログインユーザーのロール情報を管理する画面を表示します。
        /// ユーザー権限を確認し、対象企業のロール情報を取得してモデルに設定します。
        /// </summary>
        /// <returns>ログインユーザーロールの管理画面のビュー。</returns>
        public async Task<IActionResult> LoginUserRole()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "LoginUserRole");

                SettingModel.LoginUserRoleDto model = new()
                {
                    CompanyID = loguinUser.Branch_ID,
                    LoginUserRoleList = new(),
                };
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.LoginUserRoleList = await api.GetLoginUserRoleList(model.CompanyID);

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// ログインユーザーロールのモーダル画面を表示します。
        /// 指定されたロールIDに基づいて、新規登録または編集用のデータを取得します。
        /// </summary>
        /// <param name="Role">編集対象のロールID。新規登録の場合は0。</param>
        /// <returns>ログインユーザーロールのモーダルビューをJSON形式で返します。</returns>
        public async Task<IActionResult> LoginUserRoleModal(int Role)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "LoginUserRole");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { return null; }

                SettingModel.LoginUserRoleModalDto dto = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    LoginUserRole = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                if (Role == 0)
                {
                    dto.LoginUserRole.Company_ID = dto.CompanyID;
                    dto.Title = "新規登録";
                    dto.BtnCaption = "設定の保存";
                }
                else
                {
                    dto.LoginUserRole = await api.GetLoginUserRoleData(dto.CompanyID, Role);
                    if (dto.LoginUserRole == null) { throw new Exception("対象データが取得出来ませんでした。"); }
                    dto.Title = "修正";
                    dto.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("LoginUserRoleModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_LoginUser_Roleマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginUserRoleRegExec(SettingModel.LoginUserRoleModalDto param)
        {
            try
            {
                if (LoginUserRoleVaridationCheck(param, out var errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateLoginUserRoleMasterData(param.LoginUserRole);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_LoginUser_Roleマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mLoginUserRole"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool LoginUserRoleVaridationCheck(SettingModel.LoginUserRoleModalDto dto, out string errorMessage)
        {
            if (dto.LoginUserRole.Company_ID == 0) { errorMessage = "入力エラー。会社IDが空白です。"; return true; }
            if (dto.LoginUserRole.RoleName == null) { errorMessage = "入力エラー。ロール名が空白です。"; return true; }
            errorMessage = "";
            return false;
        }
        #endregion M_LoginUser_Role

        #region M_LoginUser
        /*****************************************************************************
         M_LoginUserマスタ
         *****************************************************************************/
        /// <summary>
        /// ログインユーザー情報のモーダル画面を表示します。
        /// 指定されたログインユーザーIDに基づいて、新規登録または編集用のデータを取得します。
        /// </summary>
        /// <param name="LoginUserID">編集対象のログインユーザーID。新規登録の場合は0。</param>
        /// <returns>ログインユーザー情報のモーダルビューをJSON形式で返します。</returns>
        public async Task<IActionResult> LoginUserModal(int LoginUserID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "LoginUserMenu");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { return null; }

                SettingModel.LoginUserModalDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    LoginUser = new(),
                    LoginUserRoleList = new(),
                    AreaList = new(),
                    CompanyUserList = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.LoginUserRoleList = await api.GetLoginUserRoleList(model.CompanyID);
                model.AreaList = await api.GetAreaList(model.CompanyID);
                model.CompanyUserList = await api.GetCompanyUserList(model.CompanyID, "all", 1);
                model.SyasyuSelectList = await Service.SearchCommonService.GetSyasyuSelect(_mapApiSettiong, model.CompanyID);
                model.KataSelectList = await Service.SearchCommonService.GetKataSelect(_mapApiSettiong, model.CompanyID);

                if (LoginUserID == 0)
                {
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    model.LoginUser = await api.GetLoginUserData(LoginUserID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("LoginUserModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, message = ex.Message });
            }
        }

        /// <summary>
        /// M_LoginUserマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginUserRegExec(SettingModel.LoginUserModalDto param)
        {
            try
            {
                if (LoginUserVaridationCheck(param, out var errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateLoginUserMasterData(param.LoginUser);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// 指定されたパスワードがセキュリティ要件を満たしているかを検証します。
        /// 要件: 
        /// - 8～15文字の長さ
        /// - 少なくとも1つの小文字、大文字、数字、および特殊文字を含む(特殊文字は削除)
        /// </summary>
        /// <param name="password">検証対象のパスワード文字列。</param>
        /// <param name="ErrorMessage">要件に違反した場合のエラーメッセージ。</param>
        /// <returns>要件を満たしている場合は true、それ以外の場合は false。</returns>
        /// <exception cref="Exception">パスワードが空の場合にスローされます。</exception>
        private static bool ValidatePassword(string password, out string ErrorMessage)
        {
            string input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMiniMaxChars = new Regex(@".{8,15}");
            Regex hasLowerChar = new Regex(@"[a-z]+");
            //Regex hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            //else if (!hasSymbols.IsMatch(input))
            //{
            //    ErrorMessage = "Password should contain At least one special case characters";
            //    return false;
            //}
            else
            {
                return true;
            }
        }

        /// <summary>
        /// M_LoginUserマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mLoginUser"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool LoginUserVaridationCheck(SettingModel.LoginUserModalDto dto, out string errorMessage)
        {
            if (dto.LoginUser.LoginID == null) { errorMessage = "入力エラー。ログインIDが空白です。"; return true; }
            if (dto.LoginUser.User_ID == 0) { errorMessage = "入力エラー。ユーザが未選択です。"; return true; }
            if (dto.LoginUser.Role == 0) { errorMessage = "入力エラー。権限設定が未選択です。"; return true; }
            if (dto.LoginUser.DefaultArea == null) { errorMessage = "入力エラー。初期エリアが未選択です。"; return true; }
            errorMessage = "";
            return false;
        }
        #endregion M_LoginUser


        #endregion ログインユーザーマスタ

        #region 配車マスタ
        /// <summary>
        /// 配車メニュー画面を表示します。
        /// ログインユーザー情報を取得し、画面を描画します。
        /// </summary>
        /// <returns>配車メニュー画面のビュー。</returns>
        public async Task<IActionResult> HaisyaMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return View();
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        #region M_CompanyUser_Group M_CompanyUser_GroupUser 配車グループ
        /*****************************************************************************
         M_CompanyUser_Groupマスタ
         *****************************************************************************/
        /// <summary>
        /// M_CompanyUser_Groupマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CompanyUserGroup(int GroupKubun)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyUserGroup");

                Service.UserGroupLists sEnum = (Service.UserGroupLists)Enum.ToObject(typeof(Service.UserGroupLists), GroupKubun);

                string title = sEnum switch { Service.UserGroupLists.Haisya => "配車グループ", Service.UserGroupLists.Seikyu => "請求グループ", _ => "不明" };

                SettingModel.CompanyUserGroupDto model = new()
                {
                    CompanyUserGroupList = new(),
                    CompanyID = loguinUser.Company_ID,
                    GroupKubun = (int)sEnum,
                    Title = title,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_CompanyUser_Group_Local> syasyoList = await api.GetCompanyUserGroupList(loguinUser.Company_ID, sEnum);

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                foreach (var target in syasyoList)
                {
                    SettingModel.M_CompanyUserGroupDto data = new();
                    CopyProperty(data, target);
                    model.CompanyUserGroupList.Add(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> CompanyUserGroupModal(int GroupID, int GroupKubun)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CompanyUserGroup");

                Service.UserGroupLists sEnum = (Service.UserGroupLists)Enum.ToObject(typeof(Service.UserGroupLists), GroupKubun);

                SettingModel.CompanyUserGroupModalDto model = new()
                {
                    CompanyUserGroupData = new(),
                    CompanyUserGroupUserList = new(),
                    CompanyID = loguinUser.Company_ID,
                    GroupID = GroupID,
                    GroupKubun = (int)sEnum
                };
                model.TollSyasyuSelectList = await Service.SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 2);
                model.DetailSyasyuSelectList = await Service.SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 1);
                model.CompanyUserGroupSizeSelectList = await Service.SearchCommonService.GetSyasyuSizeSelect(_mapApiSettiong, loguinUser.Company_ID);

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                TantouLists tantou = sEnum switch
                {
                    UserGroupLists.Haisya => TantouLists.Tantou,
                    UserGroupLists.Eigyo => TantouLists.Eigyo,
                    UserGroupLists.Seikyu => TantouLists.Seikyu,
                    _ => TantouLists.ALL
                };

                model.CompanyUserList = await Service.SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, tantou);
                model.CompanyUserList = model.CompanyUserList.Where(m => m.Disabled == false);

                if (GroupID == 0)
                {
                    model.CompanyUserGroupData.Group_ID = 0;
                    model.CompanyUserGroupData.Company_ID = loguinUser.Company_ID;
                    model.CompanyUserGroupData.Group_Kubun = (int)UserGroupLists.Haisya;
                    model.CompanyUserGroupData.SortOrder = 0;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    model.CompanyUserGroupData = await api.GetCompanyUserGroupData(loguinUser.Company_ID, GroupID);
                    model.CompanyUserGroupUserList = await api.GetCompanyUserGroupUserList(GroupID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                if (model.CompanyUserGroupUserList.Count == 0)
                {
                    Dto.M_CompanyUser_GroupUser_Local data = new()
                    {
                        Group_ID = GroupID,
                        User_ID = 0,
                        Del_Flg = false,
                    };
                    model.CompanyUserGroupUserList.Add(data);
                }

                return await PartialViewAsJson("CompanyUserGroupModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param">CompanyUserGroupModalDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyUserGroupRegExec(SettingModel.CompanyUserGroupModalDto param)
        {
            try
            {
                if (CompanyUserGroupVaridationCheck(param, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCompanyUserGroupMasterData(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="param">CompanyUserGroupModalDto</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CompanyUserGroupVaridationCheck(SettingModel.CompanyUserGroupModalDto param, out string errorMessage)
        {

            if (param.CompanyUserGroupData.Group_Name == null) { errorMessage = "入力エラー。グループ名が空白です。"; return true; }
            if (param.CompanyUserGroupData.Display_Name == null) { errorMessage = "入力エラー。表示名が空白です。"; return true; }

            foreach (var item in param.CompanyUserGroupUserList)
            {
                if (item.User_ID == 0) { errorMessage = "担当者が未設定です。"; return true; }
            }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyUserGroupListSortCommit(SettingModel.CompanyUserGroupDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateCompanyUserGroupListSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CompanyUser_Groupマスタ画面の削除処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanyUserGroupDelExec(SettingModel.CompanyUserGroupModalDto param)
        {
            try
            {
                if (CompanyUserGroupVaridationCheck(param, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.DeleteCompanyUserGroupMasterData(param.CompanyUserGroupData);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
        #endregion M_CompanyUser_Group M_CompanyUser_GroupUser 配車グループ

        #region 専属マスタ
        /// <summary>
        /// 専属マスタ登録画面Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SenzokuIndex()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SenzokuMaster");

                SenzokuListModel model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.SenzokuList = await api.GetSenzokuViewList(loguinUser.Company_ID);
                model.SenzokuDriverList = await api.GetSenzokuDriverViewList(loguinUser.Company_ID, null);

                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_Senzoku登録画面
        /// </summary>
        /// <param name="senzokuId"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonSenzokuRegModal(int senzokuId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SenzokuMaster");

                SenzokuModalModel model = new()
                {
                    UserID = loguinUser.User_ID,
                    CompanyID = loguinUser.Company_ID,
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                    BtnCaption = "設定の保存",
                    Senzoku = new(),
                };
                model.GroupList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);

                model.SeikyuKubunList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "0", Text = "案件毎" },
                        new SelectListItem { Value = "1", Text = "月額" },
                    };

                model.CalcKubunList = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "0", Text = "月額から計算" },
                        new SelectListItem { Value = "1", Text = "日額から計算" },
                    };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                if (senzokuId == 0)
                {
                    model.Title = "新規登録";
                    model.Senzoku.Company_ID = loguinUser.Company_ID;

                } else
                {
                    model.Title = "修正";
                    List<Dto.M_Senzoku_Local> senzoku = await api.GetSenzokuList(loguinUser.Company_ID);
                    model.Senzoku = senzoku.FirstOrDefault(m => m.SenzokuID == senzokuId);

                    Dto.V_Senzoku_Local v = await api.GetSenzokuViewData(loguinUser.Company_ID, senzokuId);
                    model.KokyakuName = v.Senzoku_Name_Abbr;
                    model.KokyakuTantouName = v.Tantou_Name_Abbr;
                }

                return await PartialViewAsJson("SenzokuRegModal", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 専属データの登録または更新を実行します。
        /// </summary>
        /// <param name="param">専属データの入力モデル。</param>
        /// <returns>登録または更新の結果をJSON形式で返却。</returns>
        /// <remarks>
        /// - APIを呼び出して専属データを登録または更新します。
        /// - 処理結果として、成功/失敗フラグとエラーメッセージを返却します。
        /// - 例外発生時には、失敗フラグとエラーメッセージを含むJSONを返却します。
        /// </remarks>
        public async Task<IActionResult> SenzokuRegExec(SenzokuModalModel param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSenzokuData(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion 専属マスタ

        #region 専属乗務員マスタ
        /// <summary>
        /// M_SenzokuDriver登録画面
        /// </summary>
        /// <param name="senzokuDriverId"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonSenzokuDriverRegModal(int senzokuId, int senzokuDriverId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SenzokuMaster");

                SenzokuDriverModalModel model = new()
                {
                    UserID = loguinUser.User_ID,
                    CompanyID = loguinUser.Company_ID,
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                    BtnCaption = "設定の保存",
                    SenzokuDriver = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                if (senzokuDriverId == 0)
                {
                    model.Title = "新規登録";
                    model.SenzokuDriver.Company_ID = loguinUser.Company_ID;
                    model.SenzokuDriver.SenzokuID = senzokuId;
                }
                else
                {
                    model.Title = "修正";
                    List<Dto.M_Senzoku_Driver_Local> driver = await api.GetSenzokuDriverList(loguinUser.Company_ID, null);
                    model.SenzokuDriver = driver.FirstOrDefault(m => m.Senzoku_Driver_ID == senzokuDriverId);

                    V_Senzoku_Driver_Local vdriver = await api.GetSenzokuDriverViewData(loguinUser.Company_ID, null, senzokuDriverId);
                    model.DriverDisplay = vdriver.Display_Name;
                    model.DriverSyaryoDisplay = "車種：  " + vdriver.Syasyu + vdriver.Kata + " 車番：  " + vdriver.Syaban_Number;
                }

                return await PartialViewAsJson("SenzokuDriverRegModal", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// 専属乗務員の登録・更新処理を実行します。
        /// 入力された専属乗務員データをAPIを介して保存します。
        /// </summary>
        /// <param name="param">専属乗務員データを含むモデル。</param>
        /// <returns>処理結果をJSON形式で返します。</returns>
        public async Task<IActionResult> SenzokuDriverRegExec(SenzokuDriverModalModel param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSenzokuDriverData(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
        #endregion 専属乗務員マスタ

        #endregion 配車マスタ

        #region 案件マスタ
        /// <summary>
        /// 案件メニュー画面を表示します。
        /// ログインユーザー情報を取得し、案件メニュー画面を描画します。
        /// </summary>
        /// <returns>案件メニュー画面のビュー。</returns>
        public async Task<IActionResult> AnkenMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return View("AnkenMenu");
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        #region M_Syaryo
        /*****************************************************************************
         M_Syaryoマスタ
         *****************************************************************************/
        /// <summary>
        /// M_Syaryoマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Syaryo()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoModal");

                SettingModel.SyaryoDto model = new()
                {
                    M_SyaryoList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Syaryo_Local> syasyoList = await api.GetSyaryoList(loguinUser.Company_ID);
                List<M_Code_Data_Local> TollSyasyuCodeList = await api.M_Code_DataList(2);
                List<M_Code_Data_Local> DetailSyasyuCodeList = await api.M_Code_DataList(1);

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                foreach (var target in syasyoList)
                {
                    SettingModel.M_SyaryoDto data = new();
                    CopyProperty(data, target);
                    data.CARDETAILINFO_Disp = DetailSyasyuCodeList.FirstOrDefault(m => m.Code_Data == data.CARDETAILINFO).Code_Name;
                    data.TOLL_TYPE_Disp = TollSyasyuCodeList.FirstOrDefault(m => m.Code_Data == data.TOLL_TYPE).Code_Name;
                    model.M_SyaryoList.Add(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_Syaryoマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> SyaryoModal(int SyaryoID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoListModal");

                SettingModel.SyaryoModalDto model = new()
                {
                    M_Syaryo = new(),
                };
                model.TollSyasyuSelectList = await Service.SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 2);
                model.DetailSyasyuSelectList = await Service.SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 1);
                model.SyaryoSizeSelectList = await Service.SearchCommonService.GetSyasyuSizeSelect(_mapApiSettiong, loguinUser.Company_ID);
                model.KataSelectList = await Service.SearchCommonService.GetKataMasterSelect(_mapApiSettiong, loguinUser.Company_ID);

                if (SyaryoID == 0)
                {
                    model.M_Syaryo.Syaryo_ID = 0;
                    model.M_Syaryo.Company_ID = loguinUser.Company_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    model.M_Syaryo = await api.GetSyaryoData(loguinUser.Company_ID, SyaryoID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("SyaryoModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_Syaryoマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoRegExec(SettingModel.SyaryoModalDto param)
        {
            try
            {
                if (SyaryoVaridationCheck(param.M_Syaryo, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyaryoMasterData(param.M_Syaryo);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_Syaryoマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mSyaryo"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool SyaryoVaridationCheck(Dto.M_Syaryo_Local mSyaryo, out string errorMessage)
        {
            if (mSyaryo.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            if (mSyaryo.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_Syaryoマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoListSortCommit(SettingModel.SyaryoDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateSyaryoListSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_Syaryoマスタ画面の削除処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoDelExec(SettingModel.SyaryoModalDto param)
        {
            try
            {
                if (SyaryoVaridationCheck(param.M_Syaryo, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyaryoMasterData(param.M_Syaryo);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
        #endregion M_Syaryo

        #region M_SyaryoSize
        /*****************************************************************************
         M_SyaryoSizeマスタ
         *****************************************************************************/
        /// <summary>
        /// M_SyaryoSizeマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SyaryoSize()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoSizeModal");

                SettingModel.SyaryoSizeDto model = new()
                {
                    M_SyaryoSizeList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_SyaryoSizeList = await api.GetSyaryoSizeList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_SyaryoSizeマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> SyaryoSizeModal(string SyasyuSize)
        {
            try
            {
                SettingModel.SyaryoSizeModalDto model = new()
                {
                    M_SyaryoSize = new(),
                };

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoSizeModal");

                if (SyasyuSize == "新規")
                {
                    model.M_SyaryoSize.Company_ID = loguinUser.Company_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    model.M_SyaryoSize = await api.GetSyaryoSizeData(loguinUser.Company_ID, SyasyuSize);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("SyaryoSizeModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_SyaryoSizeマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoSizeRegExec(SettingModel.SyaryoSizeModalDto param)
        {
            try
            {
                if (SyaryoSizeVaridationCheck(param.M_SyaryoSize, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyaryoSizeMasterData(param.M_SyaryoSize);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_SyaryoSizeマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mSyaryoSize"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool SyaryoSizeVaridationCheck(Dto.M_SyaryoSize_Local mSyaryoSize, out string errorMessage)
        {
            if (mSyaryoSize.SIZE == null) { errorMessage = "入力エラー。車種サイズ情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_SyaryoSizeマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoSizeSortCommit(SettingModel.SyaryoSizeDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateSyaryoSizeSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }
        #endregion M_SyaryoSize

        #region M_SyaryoCost
        /*****************************************************************************
        M_SyaryoCostマスタ
        *****************************************************************************/
        /// <summary>
        /// M_SyaryoCostマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SyaryoCost()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoCostModal");

                SettingModel.SyaryoCostDto model = new()
                {
                    M_SyaryoCostList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_SyaryoList = await api.GetSyaryoList(loguinUser.Company_ID);
                model.M_SyaryoCostList = await api.GetSyaryoCostList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_SyaryoCostマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> SyaryoCostModal(int CompanyID, int SyaryoID)
        {
            try
            {
                SettingModel.SyaryoCostModalDto model = new()
                {
                    M_Syaryo = new(),
                    M_SyaryoCost = new(),
                };

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoCostModal");

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_Syaryo = await api.GetSyaryoData(loguinUser.Company_ID, SyaryoID);
                model.M_SyaryoCost = await api.GetSyaryoCostList(loguinUser.Company_ID, SyaryoID);
                model.CompanyID = loguinUser.Company_ID;
                model.Title = "登録・修正";
                model.BtnCaption = "設定の保存";
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("SyaryoCostModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_SyaryoCostマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyaryoCostRegExec(SettingModel.SyaryoCostModalDto param)
        {
            try
            {
                if (SyaryoCostVaridationCheck(param.M_SyaryoCost, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyaryoCostData(param.M_SyaryoCost);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_SyaryoCostマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mSyaryoCost"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool SyaryoCostVaridationCheck(List<Dto.M_SyaryoCost_Local> mSyaryoCost, out string errorMessage)
        {

            //if (mSyaryoCost.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            //if (mSyaryoCost.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion M_SyaryoCost

        #region M_DefaultMoney
        /*****************************************************************************
        M_DefaultMoneyマスタ
        *****************************************************************************/
        /// <summary>
        /// M_DefaultMoneyマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DefaultMoney()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "DefaultMoneyModal");

                SettingModel.DefaultMoneyDto model = new()
                {
                    M_SyaryoSizeList = new(),
                    M_DefaultMoneyList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_AreaList = await api.GetAreaList(model.CompanyID);
                model.M_SyaryoSizeList = await api.GetSyaryoSizeList(loguinUser.Company_ID);
                model.M_DefaultMoneyList = await api.GetDefaultMoneyList(null, null);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_DefaultMoneyマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> DefaultMoneyModal(string Area, string SyasyuSize)
        {
            try
            {
                SettingModel.DefaultMoneyModalDto model = new()
                {
                    M_SyaryoSize = new(),
                    M_DefaultMoneyList = new(),
                };

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "DefaultMoneyModal");

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_SyaryoSize = await api.GetSyaryoSizeData(loguinUser.Company_ID, SyasyuSize);
                model.M_DefaultMoneyList = await api.GetDefaultMoneyList(Area, SyasyuSize);
                model.Area = Area;
                model.CompanyID = loguinUser.Company_ID;
                model.Title = "登録・修正";
                model.BtnCaption = "設定の保存";
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("DefaultMoneyModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_DefaultMoneyマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DefaultMoneyRegExec(SettingModel.DefaultMoneyModalDto param)
        {
            try
            {
                if (DefaultMoneyVaridationCheck(param.M_DefaultMoneyList, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateDefaultMoneyData(param.M_DefaultMoneyList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_DefaultMoneyマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mDefaultMoney"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool DefaultMoneyVaridationCheck(List<Dto.M_DefaultMoney_Local> mDefaultMoney, out string errorMessage)
        {

            //if (mDefaultMoney.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            //if (mDefaultMoney.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion M_DefaultMoney

        #region M_DefaultMoney_WaitTimeForArea 
        /*****************************************************************************
          M_DefaultMoney_WaitTimeForArea マスタ
          *****************************************************************************/
        /// <summary>
        /// M_DefaultMoney_WaitTimeForArea マスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DefaultMoneyWaitTimeCalc()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "DefaultMoneyWaitTimeForArea");

                SettingModel.DefaultMoneyWaitTimeForAreaDto model = new()
                {
                    M_AreaList = new(),
                    DefaultMoneyWaitTimeList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_AreaList = await api.GetAreaList(model.CompanyID);
                model.M_SyaryoSizeList = await api.GetSyaryoSizeList(loguinUser.Company_ID);
                model.DefaultMoneyWaitTimeList = await api.GetDefaultMoneyWaitTimeForAreaList(null, null);
                model.DefaultMoneyWaitTimeForCompanyList = await api.GetDefaultMoneyWaitTimeForCompanyList(loguinUser.Company_ID, null, null);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;
                model.EditEnabledUnyu = roleList.Count != 0 && roleList.First().Enabled && (roleList.First().Method == "ALL");

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> DefaultMoneyWaitTimeCalcModal(string SyasyuSize, string Area)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "DefaultMoneyWaitTimeForArea");

                SettingModel.DefaultMoneyWaitTimeForAreaModalDto model = new()
                {
                    M_DefaultMoneyWaitTimeList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_SyaryoSize = await api.GetSyaryoSizeData(loguinUser.Company_ID, SyasyuSize);
                model.M_DefaultMoneyWaitTimeList = await api.GetDefaultMoneyWaitTimeForAreaList(SyasyuSize, Area);
                model.Area = Area;
                model.CompanyID = loguinUser.Company_ID;
                model.Title = "登録・修正";
                model.BtnCaption = "設定の保存";
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("DefaultMoneyWaitTimeCalcModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DefaultMoneyWaitTimeForAreaRegExec(SettingModel.DefaultMoneyWaitTimeForAreaDto param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (DefaultMoneyWaitTimeForAreaVaridationCheck(param.DefaultMoneyWaitTimeList, out var errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateDefaultMoneyWaitTimeForAreaData(param.DefaultMoneyWaitTimeList);
                if (!result.RetrunFlg){ return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage }); }
                Dto.MsterDataCommonResultValDto_Local result2 = await api.InsertUpdateDefaultMoneyWaitTimeForCompanyData(loguinUser.Company_ID, param.DefaultMoneyWaitTimeForCompanyList);
                return Json(new { retrunFlg = result2.RetrunFlg, errorMessage = result2.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_DefaultMoney_WaitTimeForAreaマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mDefaultMoneyWaitTimeForArea"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool DefaultMoneyWaitTimeForAreaVaridationCheck(List<Dto.M_DefaultMoney_WaitTimeForArea_Local> mDefaultMoneyWaitTimeForArea, out string errorMessage)
        {
            //if (mDefaultMoneyWaitTimeForArea.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            //if (mDefaultMoneyWaitTimeForArea.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }

        #endregion M_DefaultMoney_WaitTimeForArea

        #region M_PersonnelExpenses
        /*****************************************************************************
          M_PersonnelExpensesマスタ
          *****************************************************************************/
        /// <summary>
        /// M_PersonnelExpensesマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PersonnelExpenses()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "PersonnelExpenses");

                SettingModel.PersonnelExpensesDto model = new()
                {
                    M_SyaryoList = new(),
                    M_PersonnelExpenseList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_SyaryoList = await api.GetSyaryoList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                model.M_SyaryoList = model.M_SyaryoList.Where(m => m.DEL_FLG == false).OrderBy(m => m.SortOrder).ToList();

                List<Dto.M_PersonnelExpense_Local> personList = await api.GetPersonnelExpenseList(loguinUser.Company_ID);

                foreach (var data in model.M_SyaryoList) 
                {
                    Dto.M_PersonnelExpense_Local target = new();

                    Dto.M_PersonnelExpense_Local person = personList.FirstOrDefault(m => m.Syasyu == data.SYASYU && m.Kata == data.KATA);
                    if (person == null)
                    {
                        target.Company_ID = loguinUser.Company_ID;
                        target.Syasyu = data.SYASYU;
                        target.Kata = data.KATA;
                        target.Base = 0;
                        target.Day = 0;
                        target.Midnight = 0;
                        target.Holiday = 0;
                        target.HolidayMidnight = 0;
                        target.BenefitsCosts = 0;
                        target.IndirectCosts = 0;
                        model.M_PersonnelExpenseList.Add(target);
                    } else
                    {
                        model.M_PersonnelExpenseList.Add(person);
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_PersonnelExpenseマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PersonnelExpenseRegExec(SettingModel.PersonnelExpensesDto param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (PersonnelExpenseVaridationCheck(param.M_PersonnelExpenseList, out string errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdatePersonnelExpenseData(loguinUser.Company_ID, param.M_PersonnelExpenseList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_PersonnelExpenseマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mPersonnelExpense"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool PersonnelExpenseVaridationCheck(List<Dto.M_PersonnelExpense_Local> mPersonnelExpense, out string errorMessage)
        {

            //if (mPersonnelExpense.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            //if (mPersonnelExpense.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion M_PersonnelExpenses

        #region M_FuelCosts
        /*****************************************************************************
          M_FuelCostsマスタ
          *****************************************************************************/
        /// <summary>
        /// M_FuelCostsマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FuelCosts()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "FuelCosts");

                SettingModel.FuelCostsDto model = new()
                {
                    M_FuelCostList = new(),
                    CompanyID = loguinUser.Company_ID,
                    BranchList = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.M_FuelCostList = await api.GetFuelCostList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                List<M_CompanyBranch_Local> list = await api.GetCompanyBranchList(loguinUser.Company_ID);

                model.BranchList.Add(new SelectListItem() { Value = "0", Text = "全支店/全営業所/全事業所" });
                foreach(var target in list.OrderBy(m => m.SortOrder))
                {
                    model.BranchList.Add(new SelectListItem() { Value = target.Branch_ID.ToString(), Text = target.Branch_Name_Abbr });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }
        }

        /// <summary>
        /// M_FuelCostマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FuelCostRegExec(SettingModel.FuelCostsDto param)
        {
            string errorMessage = "";

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (FuelCostVaridationCheck(param.M_FuelCostList, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateFuelCostData(loguinUser.Company_ID, param.M_FuelCostList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_FuelCostマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mFuelCost"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool FuelCostVaridationCheck(List<Dto.M_FuelCost_Local> mFuelCost, out string errorMessage)
        {

            //if (mFuelCost.SYASYU == null) { errorMessage = "入力エラー。車種情報が空白です。"; return true; }
            //if (mFuelCost.KATA == null) { errorMessage = "入力エラー。型情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion M_FuelCosts

        #region M_Anken_Excharge
        /*****************************************************************************
         M_Anken_Exchargeマスタ
         *****************************************************************************/
        /// <summary>
        /// M_Anken_Exchargeマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AnkenExcharge()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenExcharge");

                SettingModel.AnkenExchargeDto model = new()
                {
                    M_AnkenExchargeList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Anken_Excharge_Local> dataList = await api.M_Anken_ExchargeList(loguinUser.Company_ID, null);

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                foreach (var target in dataList)
                {
                    Dto.M_Anken_Excharge_Local data = new();
                    CopyProperty(data, target);
                    model.M_AnkenExchargeList.Add(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }

        }

        /// <summary>
        /// M_Anken_Exchargeマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> AnkenExchargeModal(string SyasyuSize, string KomokuKey)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenExcharge");

                SettingModel.AnkenExchargeModalDto model = new()
                {
                    M_Anken_Excharge = new(),
                };
                model.SyaryoSizeSelectList = await Service.SearchCommonService.GetSyasyuSizeSelect(_mapApiSettiong, loguinUser.Company_ID);

                if ("新規".Equals(SyasyuSize))
                {
                    model.M_Anken_Excharge.Company_ID = loguinUser.Company_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    model.M_SyaryoSize = await api.GetSyaryoSizeData(loguinUser.Company_ID, SyasyuSize);
                    List<Dto.M_Anken_Excharge_Local> dataList = await api.M_Anken_ExchargeList(loguinUser.Company_ID, SyasyuSize);
                    model.M_Anken_Excharge = dataList.FirstOrDefault(m => m.Komoku_Key == KomokuKey);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("AnkenExchargeModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_Anken_Exchargeマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenExchargeRegExec(SettingModel.AnkenExchargeModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (AnkenExchargeVaridationCheck(param.M_Anken_Excharge, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateAnkenExchargeMasterData(param.M_Anken_Excharge);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_Anken_Exchargeマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mAnkenExcharge"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool AnkenExchargeVaridationCheck(Dto.M_Anken_Excharge_Local mAnkenExcharge, out string errorMessage)
        {
            if (mAnkenExcharge.SIZE == null) { errorMessage = "入力エラー。車種サイズが空白です。"; return true; }
            if (mAnkenExcharge.Komoku_Key == null) { errorMessage = "入力エラー。項目キーが空白です。"; return true; }
            if (mAnkenExcharge.Komoku_Name == null) { errorMessage = "入力エラー。項目表示名が空白です。"; return true; }
            if (mAnkenExcharge.Komoku_Name_abbr == null) { errorMessage = "入力エラー。略称が空白です。"; return true; }
            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_Anken_Exchargeマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenExchargeSortCommit(SettingModel.AnkenExchargeDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateAnkenExchargeSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion M_Anken_Excharge

        #region M_Luggage_Group
        /*****************************************************************************
         M_Luggage_Groupマスタ
         *****************************************************************************/
        /// <summary>
        /// M_Luggage_Groupマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AnkenLuggageGroup()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenLuggage");

                SettingModel.AnkenLuggageGroupDto model = new()
                {
                    LuggageGroupList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Luggage_Group_Local> dataList = await api.GetLuggageGroupList(loguinUser.Company_ID);

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                foreach (var target in dataList)
                {
                    Dto.M_Luggage_Group_Local data = new();
                    CopyProperty(data, target);
                    model.LuggageGroupList.Add(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }

        }

        /// <summary>
        /// M_Luggage_Groupマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> AnkenLuggageGroupModal(int id)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenLuggage");

                SettingModel.AnkenLuggageGroupModalDto model = new()
                {
                    LuggageGroup = new(),
                    CompanyID = loguinUser.Company_ID,
                    GroupID = id,
                };

                if (id == 0)
                {
                    //新規
                    model.LuggageGroup.Company_ID = loguinUser.Company_ID;
                    model.LuggageGroup.SortOrder = 0;
                    model.LuggageGroup.Luggage_Group_ID = 0;
                    model.LuggageGroup.Insert_User = loguinUser.User_ID;
                    model.LuggageGroup.Update_User = loguinUser.User_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    List<Dto.M_Luggage_Group_Local> dataList = await api.GetLuggageGroupList(loguinUser.Company_ID);
                    model.LuggageGroup = dataList.FirstOrDefault(m => m.Luggage_Group_ID == id);
                    model.LuggageGroup.Update_User = loguinUser.User_ID;
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("AnkenLuggageGroupModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_Luggage_Groupマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenLuggageGroupRegExec(SettingModel.AnkenLuggageGroupModalDto param)
        {
            string errorMessage = "";
            try
            {
                if (AnkenLuggageGroupVaridationCheck(param.LuggageGroup, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateLuggageGroupMasterData(param.LuggageGroup);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_Luggage_Groupマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mAnkenLuggage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool AnkenLuggageGroupVaridationCheck(Dto.M_Luggage_Group_Local mAnkenLuggage, out string errorMessage)
        {
            if (mAnkenLuggage.Luggage_GroupName == null) { errorMessage = "入力エラー。グループ名が空白です。"; return true; }
            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_Luggage_Groupマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenLuggageGroupSortCommit(SettingModel.AnkenLuggageGroupDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateLuggageGroupSortOrder(param.LuggageGroupList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion M_Luggage_Group

        #region M_Equipment_Group
        /*****************************************************************************
         M_Equipment_Groupマスタ
         *****************************************************************************/
        /// <summary>
        /// M_Equipment_Groupマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AnkenEquipmentGroup()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenEquipment");

                SettingModel.AnkenEquipmentGroupDto model = new()
                {
                    EquipmentGroupList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Equipment_Group_Local> dataList = await api.GetEquipmentGroupList(loguinUser.Company_ID);

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                foreach (var target in dataList)
                {
                    Dto.M_Equipment_Group_Local data = new();
                    CopyProperty(data, target);
                    model.EquipmentGroupList.Add(data);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }

        }

        /// <summary>
        /// M_Equipment_Groupマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> AnkenEquipmentGroupModal(int id)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "AnkenEquipment");

                SettingModel.AnkenEquipmentGroupModalDto model = new()
                {
                    EquipmentGroup = new(),
                    CompanyID = loguinUser.Company_ID,
                    GroupID = id,
                };

                if (id == 0)
                {
                    //新規
                    model.EquipmentGroup.Company_ID = loguinUser.Company_ID;
                    model.EquipmentGroup.SortOrder = 0;
                    model.EquipmentGroup.Equipment_Group_ID = 0;
                    model.EquipmentGroup.Insert_User = loguinUser.User_ID;
                    model.EquipmentGroup.Update_User = loguinUser.User_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    List<Dto.M_Equipment_Group_Local> dataList = await api.GetEquipmentGroupList(loguinUser.Company_ID);
                    model.EquipmentGroup = dataList.FirstOrDefault(m => m.Equipment_Group_ID == id);
                    model.EquipmentGroup.Update_User = loguinUser.User_ID;
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("AnkenEquipmentGroupModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });

            }

        }

        /// <summary>
        /// M_Equipment_Groupマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenEquipmentGroupRegExec(SettingModel.AnkenEquipmentGroupModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (AnkenEquipmentGroupVaridationCheck(param.EquipmentGroup, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateEquipmentGroupMasterData(param.EquipmentGroup);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_Equipment_Groupマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mAnkenEquipment"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool AnkenEquipmentGroupVaridationCheck(Dto.M_Equipment_Group_Local mAnkenEquipment, out string errorMessage)
        {
            if (mAnkenEquipment.Equipment_GroupName == null) { errorMessage = "入力エラー。グループ名が空白です。"; return true; }
            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_Equipment_Groupマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnkenEquipmentGroupSortCommit(SettingModel.AnkenEquipmentGroupDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateEquipmentGroupSortOrder(param.EquipmentGroupList);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion M_Equipment_Group

        #region M_SyasyuKubun 車種区分
        /*****************************************************************************
         M_SyasyuKubunマスタ
         *****************************************************************************/
        /// <summary>
        /// M_SyasyuKubunマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SyasyuKubun()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyasyuKubun");

                SettingModel.SyasyuKubunDto model = new()
                {
                    SyasyuKubunList = new(),
                    SyaryoSizeList = new(),
                    KataList = new(),
                    SyaryoList = new(),
                    CompanyID = loguinUser.Company_ID,
                };
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.KataList = await api.GetKataList(loguinUser.Company_ID);
                model.SyaryoSizeList = await api.GetSyaryoSizeList(loguinUser.Company_ID);
                model.SyasyuKubunList = await api.GetSyasyuKubunList(loguinUser.Company_ID);

                List<Dto.M_Syaryo_Local> list = new();
                IEnumerable<Dto.M_Syaryo_Local> dataList = await api.GetSyaryoList(loguinUser.Company_ID);
                var queryKata = dataList.OrderBy(x => x.SortOrder).GroupBy(x => new { Size = x.SIZE, kata = x.Kata_ID });
                int i = 1;
                foreach (var item in queryKata)
                {
                    list.Add(new Dto.M_Syaryo_Local() { SIZE = item.Key.Size, Kata_ID = item.Key.kata, SortOrder = i });
                    i++;
                }
                model.SyaryoList = list;


                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }

        }

        /// <summary>
        /// M_SyasyuKubunマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> SyasyuKubunModal(int SyasyuKubunID, string Size, string KataID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyasyuKubun");

                SettingModel.SyasyuKubunModalDto model = new()
                {
                    SyasyuKubunData = new(),
                    SyasyuKubunList = new(),
                    CompanyID = loguinUser.Company_ID,
                    SyasyuKubunID = SyasyuKubunID,
                };
                model.KataSelectList = await Service.SearchCommonService.GetKataMasterSelect(_mapApiSettiong, loguinUser.Company_ID);

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                if (SyasyuKubunID == 0)
                {
                    model.SyasyuKubunData.SyasyuKubun_ID = 0;
                    model.SyasyuKubunData.Company_ID = loguinUser.Company_ID;
                    model.SyasyuKubunData.SortOrder = 0;
                    model.SyasyuKubunData.SIZE = Size;
                    model.SyasyuKubunData.Kata_ID = KataID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    model.SyasyuKubunData = await api.GetSyasyuKubunData(loguinUser.Company_ID, SyasyuKubunID);
                    //model.SyasyuKubunList = await api.GetSyasyuKubunList(loguinUser.Company_ID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("SyasyuKubunModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// M_SyasyuKubunマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param">SyasyuKubunModalDto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyasyuKubunRegExec(SettingModel.SyasyuKubunModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (SyasyuKubunVaridationCheck(param, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyasyuKubunMasterData(param.SyasyuKubunData);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_SyasyuKubunマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="param">SyasyuKubunModalDto</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool SyasyuKubunVaridationCheck(SettingModel.SyasyuKubunModalDto param, out string errorMessage)
        {
            if (param.SyasyuKubunData.SIZE == null) { errorMessage = "車種サイズが未入力です。"; return true; }
            if (param.SyasyuKubunData.Kata_ID == null) { errorMessage = "型が未選択です。"; return true; }
            if (param.SyasyuKubunData.KUBUN == null) { errorMessage = "車種区分が未入力です。"; return true; }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_SyasyuKubunマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyasyuKubunListSortCommit(SettingModel.SyasyuKubunDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateSyasyuKubunListSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_SyasyuKubunマスタ画面の削除処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SyasyuKubunDelExec(SettingModel.SyasyuKubunModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (SyasyuKubunVaridationCheck(param, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.DeleteSyasyuKubunMasterData(param.SyasyuKubunData);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion M_SyasyuKubun 車種区分

        #region M_Kata 型マスタ
        /*****************************************************************************
         M_Kataマスタ
         *****************************************************************************/
        /// <summary>
        /// M_Kataマスタ一覧画面のIndex
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Kata()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Kata");

                SettingModel.KataDto model = new()
                {
                    KataList = new(),
                    CompanyID = loguinUser.Company_ID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.KataList = await api.GetKataList(loguinUser.Company_ID);
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return View(model);
            }
            catch (Exception ex)
            {
                return Error(ex, Layout.LayoutForSettingWindow);
            }

        }

        /// <summary>
        /// M_Kataマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="Syasyu"></param>
        /// <param name="Kata"></param>
        /// <returns></returns>
        public async Task<IActionResult> KataModal(string KataID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Kata");

                SettingModel.KataModalDto model = new()
                {
                    KataData = new(),
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                };

                if (KataID == "新規")
                {
                    model.KataData.Company_ID = loguinUser.Company_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                }
                else
                {
                    API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                    model.KataData = await api.GetKataData(loguinUser.Company_ID, KataID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("KataModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });

            }

        }

        /// <summary>
        /// M_Kataマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> KataRegExec(SettingModel.KataModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (KataVaridationCheck(param.KataData, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateKataMasterData(param.KataData);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_Kataマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mKata"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool KataVaridationCheck(Dto.M_Kata_Local mKata, out string errorMessage)
        {

            if (mKata.Kata_ID == null) { errorMessage = "入力エラー。型のキー情報が空白です。"; return true; }
            if (mKata.Kata_Display == null) { errorMessage = "入力エラー。型表示情報が空白です。"; return true; }

            errorMessage = "";
            return false;
        }

        /// <summary>
        /// M_Kataマスタ画面の並び順変更処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> KataSortCommit(SettingModel.KataDto param)
        {
            try
            {
                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.UpdateKataSortOrder(param);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }
        #endregion M_Kata　型マスタ

        #endregion 案件マスタ

        #region 車輌管理マスタ
        /// <summary>
        /// 車両管理画面を表示します。
        /// </summary>
        /// <returns>車両管理画面のビュー。</returns>
        public async Task<IActionResult> SyaryoManagement()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoManagement");

                SettingModel.SyaryoManagementDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyDriverSyaryoList = new(),
                    CompanyDriverList = new(),
                    SyaryoManagementList = new(),
                    CompanyBranchList = new(),
                };
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                model.CompanyDriverSyaryoList = await api.GetCompanyDriverSyaryoList(loguinUser.Company_ID);
                model.CompanyDriverList = await api.GetCompanyDriverList(loguinUser.Company_ID);
                model.SyaryoManagementList = await api.GetSyaryoManagementList(loguinUser.Company_ID);
                model.CompanyBranchList = await api.GetCompanyBranchList(loguinUser.Company_ID);
                model.SyaryoList = await api.GetSyaryoList(loguinUser.Company_ID);
                model.GroupSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);

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
                return Error(ex, Layout.MainLayout);
            }
        }

        #region M_CompanyDriver_SyaryoManagemen
        /*****************************************************************************
         M_CompanyDriver_SyaryoManagemenマスタ
         *****************************************************************************/
        /// <summary>
        /// 車両管理モーダルを表示します。
        /// 車両管理IDに基づき、既存の車両データを取得するか、
        /// 新規登録用のデータを初期化して返却します。
        /// </summary>
        /// <param name="CompanyID">対象会社のID。</param>
        /// <param name="SyaryoManagementID">対象車両管理のID。新規の場合は0。</param>
        /// <returns>車両管理モーダルの部分ビュー。</returns>
        public async Task<IActionResult> CompanySyaryoManagemenModal(int CompanyID, int SyaryoManagementID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "SyaryoManagement");
                if (!(roleList.Count != 0 && roleList.First().Enabled)) { return null; }

                SettingModel.SyaryoManagementModalDto dto = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SyaryoManagement = new(),
                    CompanyBranchList = new(),

                };
                dto.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                dto.GroupList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);
                dto.SyaryoList = await SearchCommonService.GetUSyaryoSelect(_mapApiSettiong, loguinUser.Company_ID, null, null, true);
                dto.CompanyBranchList = await api.GetCompanyBranchList(CompanyID);


                if (SyaryoManagementID == 0)
                {
                    dto.Title = "新規登録";
                    dto.BtnCaption = "設定の保存";
                    Dto.M_SyaryoManagement_Local data = new()
                    {
                        SyaryoManagement_ID = 0,
                        Company_ID = CompanyID,
                        Branch_ID = 0,
                        Tntou_ID = 0,
                    };
                    dto.SyaryoManagement = data;
                }
                else
                {
                    dto.SyaryoManagement = await api.GetSyaryoManagementData(SyaryoManagementID);
                    dto.Title = "修正";
                    dto.BtnCaption = "設定の保存";
                }

                return await PartialViewAsJson("SyaryoManagementModal", dto, true);
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, message = ex.Message });

            }
        }

        /// <summary>
        /// M_SyaryoManagementマスタ画面の登録更新処理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CompanySyaryoManagementRegExec(SettingModel.SyaryoManagementModalDto param)
        {
            string errorMessage = "";

            try
            {
                if (CompanySyaryoManagementVaridationCheck(param, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateSyaryoManagementMasterData(param.SyaryoManagement);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }

        }

        /// <summary>
        /// M_CompanyDriver_SyaryoManagemenマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="mCompanyDriver_SyaryoManagemen"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool CompanySyaryoManagementVaridationCheck(SettingModel.SyaryoManagementModalDto dto, out string errorMessage)
        {
            M_SyaryoManagement_Local target = dto.SyaryoManagement;
            //if (target.SyaryoManagemen_ID == 0) { errorMessage = "入力エラー。車輌が未選択です。"; return true; }
            //if (target.Start_Date.Year == 1900) { errorMessage = "入力エラー。運用開始日が空白です。"; return true; }
            //if (target.Group_ID == 0) { errorMessage = "入力エラー。担当配車が空白です。"; return true; }
 
            errorMessage = "";
            return false;
        }

        #endregion M_CompanyDriver_SyaryoManagemen

        #endregion 車輌管理マスタ

        #region 日報マスタ
        /// <summary>
        /// 日報メニュー画面を表示します。
        /// </summary>
        /// <returns>日報メニュー画面のビュー。</returns>
        public async Task<IActionResult> NippouMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return View("NippouMenu");
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }
        #endregion 日報マスタ

        #region 経理・請求マスタ
        /// <summary>
        /// 経理メニュー画面を表示します。
        /// </summary>
        /// <returns>経理メニュー画面のビュー。</returns>
        public async Task<IActionResult> KeiriMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return View("KeiriMenu");
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        #endregion 経理・請求マスタ

        #region 車輌管理マスタ
        /// <summary>
        /// 車両メニュー画面を表示します。
        /// </summary>
        /// <returns>車両メニュー画面のビュー。</returns>
        public async Task<IActionResult> SyaryoMenu()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                return View("SyaryoMenu");
            }
            catch (Exception x)
            {
                return Error(x);
            }
        }

        #endregion 車輌管理マスタ

    }
}
