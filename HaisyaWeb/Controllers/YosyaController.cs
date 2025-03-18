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
using static HaisyaWeb.Models.YosyaModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 傭車コントローラー
    /// </summary>
    public class YosyaController : BaseController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger">ロガー</param>
        /// <param name="signInManager">サインインマネージャー</param>
        /// <param name="viewRenderService">ビューのレンダリングサービス</param>
        /// <param name="mapApiSetting">マップAPI設定</param>
        public YosyaController(ILogger<YosyaController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }

        /// <summary>
        /// 傭車一覧Index
        /// </summary>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                YosyaDataListModel model = new()
                {
                    CompanyID = loguinUser.User_ID,
                };

                return View(model);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MasterDataMenu);
            }
        }

        /// <summary>
        /// 傭車選択一覧Index
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="code">コード</param>
        /// <param name="key">キー</param>
        /// <param name="phone">電話番号</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> JsonGetDataList(int CompanyID, string code = null, string key = null, string phone = null)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "YosyaModal");

                YosyaDataListModel model = new() {
                    CompanyID = loguinUser.User_ID,
                    CustomerBranchList = new(),
                };

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerBranchList = await apiM.GetCustomerBranchList(CompanyID, 0, 1, code, key, phone);

                model.CustomerSyaryoList = await apiM.GetCustomerSyaryoList(loguinUser.Company_ID, 0);

                model.CustomerDriverList = await apiM.GetCustomerDriverList(loguinUser.Company_ID, 0);
                model.CustomerDriverSyaryoList = await apiM.GetCustomerDriverSyaryoList(loguinUser.Company_ID, 0);

                return await PartialViewAsJson("DataListForNone", model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// M_Customerマスタ画面のモーダル登録画面
        /// </summary>
        /// <param name="CustomerBranchID">顧客支店ID</param>
        /// <param name="CustomerDriverID">顧客ドライバーID</param>
        /// <param name="CustomerDriverSyaryoID">顧客ドライバー車両ID</param>
        /// <returns>ビュー</returns>
        public async Task<IActionResult> YosyaModal(int CustomerBranchID, int CustomerDriverID, int CustomerDriverSyaryoID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "YosyaModal");

                YosyaModalDto model = new()
                {
                    CustomerDriverData = new(),
                    CustomerDriverSyaryoData = new(),
                    CustomerDataTantouList = new(),
                    CustomerDataTantouHaisyaGroupList = new(),
                    CompanyID = loguinUser.Company_ID,
                    CustomerDriverSyaryoID = CustomerDriverSyaryoID,
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.M_Customer_Branch_Local branchData = await api.GetCustomerBranchData(CustomerBranchID);

                model.CompanyUserGroupList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, false, "全グループ", "0");
                model.GroupList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya, true);

                if (CustomerDriverSyaryoID.Equals(0) && CustomerDriverID.Equals(0))
                {
                    model.Title = "新規登録";
                    model.TitleForDriver = "乗務員情報新規登録";
                    model.TitleForSyaryo = "車輌情報新規登録";
                    model.BtnCaption = "設定の保存";
                    model.CustomerDriverData.Customer_ID = branchData.Customer_ID;
                    model.CustomerDriverData.Customer_Branch_ID = branchData.Customer_Branch_ID;
                    model.CustomerDriverSyaryoData.Customer_ID = branchData.Customer_ID;
                    model.CustomerDriverSyaryoData.Customer_Branch_ID = branchData.Customer_Branch_ID;
                    model.CustomerDriverData.From_Date = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                    model.CustomerDriverSyaryoData.Start_Date = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                }
                else if(CustomerDriverSyaryoID.Equals(0) && CustomerDriverID > 0)
                {
                    model.Title = "車輌新規登録";
                    model.TitleForDriver = "乗務員情報更新";
                    model.TitleForSyaryo = "車輌情報新規登録";
                    model.BtnCaption = "設定の保存";

                    List<Dto.M_Customer_Driver_Local> driverlist = await api.GetCustomerDriverList(loguinUser.Company_ID, CustomerBranchID);
                    model.CustomerDriverData = driverlist.FirstOrDefault(m => m.Customer_Driver_ID == CustomerDriverID);

                    model.CustomerDriverSyaryoData.Customer_ID = branchData.Customer_ID;
                    model.CustomerDriverSyaryoData.Customer_Branch_ID = branchData.Customer_Branch_ID;
                    model.CustomerDriverSyaryoData.Start_Date = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                }
                else
                {
                    model.Title = "修正";
                    model.TitleForDriver = "乗務員情報更新";
                    model.TitleForSyaryo = "車輌情報更新";
                    model.BtnCaption = "設定の保存";
                    List<Dto.M_Customer_Driver_Local> driverlist = await api.GetCustomerDriverList(loguinUser.Company_ID, CustomerBranchID);
                    model.CustomerDriverData = driverlist.FirstOrDefault(m => m.Customer_Driver_ID == CustomerDriverID);

                    List<Dto.M_Customer_Driver_Syaryo_Local> syaryolist = await api.GetCustomerDriverSyaryoList(loguinUser.Company_ID, CustomerBranchID);
                    model.CustomerDriverSyaryoData = syaryolist.FirstOrDefault(m => m.Customer_DriverSyaryo_ID == CustomerDriverSyaryoID);

                    model.CustomerDataTantouList = await api.GetCustomerTantouList(loguinUser.Company_ID, CustomerBranchID);
                    model.CustomerDataTantouHaisyaGroupList = await api.GetCustomerTantouHaisyaGroupList(model.CustomerDriverData.Customer_ID);
                    
                }
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                if (model.CustomerDataTantouList.Count == 0)
                {
                    Dto.M_Customer_Tantou_Local data = new()
                    {
                        Customer_ID = branchData.Customer_ID,
                        Customer_Branch_ID = branchData.Customer_Branch_ID,
                        Company_ID = loguinUser.Company_ID,
                    };
                    model.CustomerDataTantouList.Add(data);
                }

                if (model.CustomerDataTantouHaisyaGroupList.Count == 0)
                {
                    Dto.M_Customer_TantouHaisyaGroup_Local data = new()
                    {
                        Customer_ID = branchData.Customer_ID,
                    };
                    model.CustomerDataTantouHaisyaGroupList.Add(data);
                }

                return await PartialViewAsJson("Register", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 傭車選登録Index
        /// </summary>
        /// <param name="dto">傭車モーダルDTO</param>
        /// <returns>JSON結果</returns>
        public async Task<IActionResult> YosyaRegExec(YosyaModalDto dto)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (RegisterVaridationCheck(dto.CustomerDriverData, dto.CustomerDriverSyaryoData, out var errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = new();
                result = await api.InsertUpdateYosyaData(dto);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_Customerマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="dto">顧客ドライバーデータ</param>
        /// <param name="dtoS">顧客ドライバー車両データ</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>バリデーション結果</returns>
        private bool RegisterVaridationCheck(Dto.M_Customer_Driver_Local dto, Dto.M_Customer_Driver_Syaryo_Local dtoS, out string errorMessage)
        {
            //if (dto.Yosya_Name == null) { errorMessage = "入力エラー。傭車名情報が空白です。"; return true; }
            //if (dto.Yosya_Name_Kana == null) { errorMessage = "入力エラー。カナ情報が空白です。"; return true; }
            //if (dto.Yosya_Name_Abbr == null) { errorMessage = "入力エラー。略名情報が空白です。"; return true; }
            //if (dto.Company_ID == 0) { errorMessage = "登録エラー。一度システムを終了して再度登録してください。"; return true; }

            errorMessage = "";
            return false;
        }
    }
}
