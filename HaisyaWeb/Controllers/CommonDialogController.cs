using HaisyaWeb.API.WebApp;
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

namespace HaisyaWeb.Controllers
{
    public class CommonDialogController : BaseController
    {
        private readonly ILogger<CommonDialogController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public CommonDialogController(ILogger<CommonDialogController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        #region 共通得意先親選択
        /// <summary>
        /// 得意先選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectCustomerOya(int companyId, int selectedId)
        {

            string errorMessage = null;

            try
            {

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerOyaModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerList = await apiM.GetCustomerList(loguinUser.Company_ID);

                return await PartialViewAsJson("SelectCustomerOyaModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (errorMessage == null)
                {
                    errorMessage = e.Message;
                }

                return Json(new { partialView = "", message = errorMessage });
            }
            finally
            {

            }
        }



        #endregion 共通得意先親選択


        #region 共通得意先選択
        /// <summary>
        /// 得意先選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectCustomer(int companyId, int selectedId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                };

                return await PartialViewAsJson("SelectCustomerModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_CustomerデータリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerDataList(string code = null, string key = null, string phone = null)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CustomerBranchList = new(),
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerBranchList = await apiM.GetCustomerBranchList(loguinUser.Company_ID, 0, 0, code, key, phone);

                return await PartialViewAsJson("CustomerDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_Customer使用率TOP30リスト
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerListForUtilizationRate()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                const int SelectTopCount = 30;

                CommonDialogModel.SelectCustomerModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRitsu",
                    CustomerBranchList = new(),
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用率 Top" + SelectTopCount.ToString(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Customer_Branch_Local> listData = await api.GetCustomerBranchListForUtilizationRate(loguinUser.Company_ID, loguinUser.User_ID, model.SelectTopCount)
                    ?? throw new Exception("データの取得に失敗しました");
                model.CustomerBranchList = listData;

                return await PartialViewAsJson("CustomerDataList2", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_Customer使用履歴TOP30リスト
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerListForRireki()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                const int SelectTopCount = 30;

                Models.CommonDialogModel.SelectCustomerModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRireki",
                    CustomerBranchList = new(),
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用履歴 Top" + SelectTopCount.ToString(),
                };


                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Customer_Branch_Local> listData = await api.GetCustomerBranchListForRireki(loguinUser.Company_ID, loguinUser.User_ID, model.SelectTopCount)
                    ?? throw new Exception("データの取得に失敗しました");
                model.CustomerBranchList = listData;
                return await PartialViewAsJson("CustomerDataList2", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }
        #endregion 共通得意先検索

        #region 共通得意先担当選択
        /// <summary>
        /// 得意先担当選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="customerId"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectCustomerTantou(int companyId, int customerId, int selectedId)
        {
            try
            {
                if (customerId == 0) { throw new Exception("パラメーターエラー：CustomerId"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerTantouModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                    CustomerID = customerId,
                };
                return await PartialViewAsJson("SelectCustomerTantouModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_CustomerTantouデータリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerTantouDataList(int CustomerID = 0)
        {
            try
            {
                if (CustomerID == 0) { throw new Exception("パラメーターエラー：CustomerID"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerTantouModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CustomerTantouList = new(),
                    CustomerID = CustomerID,
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerTantouList = await apiM.GetCustomerTantouList(loguinUser.Company_ID, CustomerID);

                return await PartialViewAsJson("CustomerTantouDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        #endregion 共通得意先担当検索

        #region 共通車輌選択
        /// <summary>
        /// 車輌選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectSyaryoManagement(int companyId, int selectedId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectSyaryoManagementModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                    MyTantouOnlyFlg = 1,
                    TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                    SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                };

                return await PartialViewAsJson("SelectSyaryoManagementModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_SyaryoManagementデータリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetSyaryoManagementDataList(Models.CommonDialogModel.SelectSyaryoManagementModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectSyaryoManagementModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SyaryoManagementList = new(),
                    CompanyDriverList = new(),
                    CompanyDriverSyaryoList = new(),
                };

                int paramUserID = 0;
                if (param.MyTantouOnlyFlg == 1) { paramUserID = loguinUser.User_ID; }

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                Dto.M_Syaryo_Local m_Syaryo = new();
                if (param.SelectSyasyuID > 0) m_Syaryo = await apiM.GetSyaryoData(loguinUser.Company_ID, param.SelectSyasyuID);


                model.SyaryoManagementList = await apiM.GetSyaryoManagementList(loguinUser.Company_ID, param.Syaban, paramUserID, m_Syaryo.SYASYU, m_Syaryo.KATA);
                model.CompanyDriverList = await apiM.GetCompanyDriverList(loguinUser.Company_ID);
                model.CompanyDriverSyaryoList = await apiM.GetCompanyDriverSyaryoList(loguinUser.Company_ID);

                return await PartialViewAsJson("SyaryoManagementDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }
        #endregion 共通車輌選択

        #region 共通仕入れ業者選択
        /// <summary>
        /// 仕入れ業者選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectVender(int companyId, int selectedId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectVenderModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                };
                return await PartialViewAsJson("SelectVenderModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_VenderデータリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetVenderDataList(string code = null, string key = null, string phone = null)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectVenderModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    VenderList = new(),
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.VenderList = await apiM.GetVenderList(loguinUser.Company_ID, code, key, phone);

                return await PartialViewAsJson("VenderDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_Vender使用率TOP30リスト
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetVenderListForUtilizationRate()
        {
            const int SelectTopCount = 20;
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectVenderModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRitsu",
                    VenderList = new(),
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用率 Top" + SelectTopCount.ToString(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Vender_Local> listData = await api.GetVenderListForUtilizationRate(loguinUser.Company_ID, loguinUser.User_ID, model.SelectTopCount)
                    ?? throw new Exception("データの取得に失敗しました");
                model.VenderList = listData;

                return await PartialViewAsJson("VenderDataList2", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_Vender使用履歴TOP30リスト
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetVenderListForRireki()
        {
            const int SelectTopCount = 20;
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectVenderModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRireki",
                    VenderList = new(),
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用履歴 Top" + SelectTopCount.ToString(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                List<Dto.M_Vender_Local> listData = await api.GetVenderListForRireki(loguinUser.Company_ID, loguinUser.User_ID, model.SelectTopCount)
                    ?? throw new Exception("データの取得に失敗しました");
                model.VenderList = listData;
                return await PartialViewAsJson("VenderDataList2", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }
        #endregion 共通仕入れ業者検索

        #region 共通乗務員選択
        /// <summary>
        /// 乗務員選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectCompanyDriver(int companyId, int selectedId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCompanyDriverModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                    MyTantouOnlyFlg = true,
                    NowUsingSyaryoFlg = true,
                    TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                    SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                };
                return await PartialViewAsJson("SelectCompanyDriverModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_CompanyDriverデータリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCompanyDriverDataList(Models.CommonDialogModel.SelectCompanyDriverModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCompanyDriverModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CompanyDriverList = new(),
                    SelectSyasyuID = param.SelectSyasyuID,
                    Syaban = param.Syaban,
                    NowUsingSyaryoFlg = param.NowUsingSyaryoFlg,
                    MyTantouOnlyFlg = param.MyTantouOnlyFlg,
                };

                int paramUserID = 0;
                if (param.MyTantouOnlyFlg == true) { paramUserID = loguinUser.User_ID; }

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CompanyDriverList = await apiM.GetVCompanyDriverList(loguinUser.Company_ID, 1, paramUserID);

                if (model.NowUsingSyaryoFlg)
                {
                    DateTime dateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
                    model.CompanyDriverList = model.CompanyDriverList.Where(m => m.End_Date == null || m.End_Date >= dateTime).ToList();
                    model.CompanyDriverList = model.CompanyDriverList.Where(m => m.Taisyoku_Date == null || m.Taisyoku_Date >= dateTime).ToList();
                }

                if (model.Syaban != null)
                {
                    model.CompanyDriverList = model.CompanyDriverList.Where(m => m.Syaban_Number == model.Syaban).ToList();
                }

                return await PartialViewAsJson("CompanyDriverDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }
        #endregion 共通乗務員選択

        #region 共通傭車選択
        /// <summary>
        /// 傭車選択ダイアログを開く
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="selecttedId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectCustomerDriver(int companyId, int selectedId, int customerBranchID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                CommonDialogModel.SelectCustomerDriverModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                    CustomerBranchID = customerBranchID,
                    TantouSelectList = await SearchCommonService.GetTantouSelect(_mapApiSettiong, loguinUser.Company_ID, TantouLists.Tantou),
                    SyasyuSelectList = await SearchCommonService.GetSyasyuSelect(_mapApiSettiong, loguinUser.Company_ID),
                };

                return await PartialViewAsJson("SelectCustomerDriverModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// Json：M_CustomerDriverデータリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerBranchDataList(Models.CommonDialogModel.SelectCustomerDriverModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectCustomerDriverModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableCustomerBranch",
                    CustomerBranchList = new(),
                    SelectSyasyuID = param.SelectSyasyuID,
                    Syaban = param.Syaban,
                    code = param.code,
                    key = param.key,
                    phone = param.phone,
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerBranchList = await apiM.GetCustomerBranchList(loguinUser.Company_ID, 0, 1, model.code, model.key, model.phone);

                return await PartialViewAsJson("CustomerBranchDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }

        /// <summary>
        /// Json：M_CustomerDriverデータリストのHTMLの返却
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerSyaryoDataList(CommonDialogModel.SelectCustomerDriverModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                CommonDialogModel.SelectCustomerDriverModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CustomerSyaryoList = new(),
                    SelectSyasyuID = param.SelectSyasyuID,
                    Syaban = param.Syaban,
                    CustomerBranchID = param.CustomerBranchID,
                };

                using MasterDataApi apiM = new(_mapApiSettiong);
                Dto.M_Syaryo_Local m_Syaryo = new();
                if (param.SelectSyasyuID > 0) m_Syaryo = await apiM.GetSyaryoData(loguinUser.Company_ID, param.SelectSyasyuID);

                model.CustomerSyaryoList = await apiM.GetCustomerSyaryoList(loguinUser.Company_ID, param.CustomerBranchID);

                //model.CustomerDriverSyaryoList = await apiM.GetCustomerDriverSyaryoList(loguinUser.Company_ID, param.SelectSyasyuID);
                //model.CustomerDriverList = await apiM.GetCustomerDriverList(loguinUser.Company_ID, param.CustomerBranchID);
                model.CustomerBranchList = await apiM.GetCustomerBranchList(loguinUser.Company_ID);

                return await PartialViewAsJson("CustomerSyaryoDataList", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
        }
        #endregion 共通傭車選択

        #region 共通住所選択
        /// <summary>
        /// 住所選択ダイアログを開く
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="selecttedId"></param>
        /// <param name="mapDisplayKubun"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OpenDialogForSelectAddress(int companyId, int selectedId, int mapDisplayKubun)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CommonDialogModel.SelectAddressModel model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectedID = selectedId,
                    AreaList = new(),
                    AreaKenList = new(),
                    MapApiSettings = _mapApiSettiong,
                    MapDisplayKubun = mapDisplayKubun,
                };

                string url = _mapApiSettiong.WebUri.JavaScriptAPI + "/auth/jsapi/loader.htm";
                url += GetMapApiUrlPram();
                model.MapsApiForJSUrl = url;
                model.WebViewFlg = GetWebViewFlg();

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.AreaList = await apiM.GetAreaList(model.CompanyID);
                model.AreaKenList = await apiM.GetAreaKenList(model.CompanyID);

                return await PartialViewAsJson("SelectAddressModal", model, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", message = e.Message });
            }
        }

        /// <summary>
        /// 市区町住所リストを取得して返却する
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAddressShiku(int area)
        {
            try
            {
                Context.AddressList addressList = HttpContext.Session.GetObject<Context.AddressList>(SessionKeyAddress);

                addressList ??= await SetAddressList();

                IEnumerable<Dto.M_PostCode_Local> listData = null;

                switch (area)
                {
                    case 1:
                        listData = addressList.HokkaidoAddressItem;
                        break;
                    case 2:
                        listData = addressList.TohokuAddressItem;
                        break;
                    case 3:
                        listData = addressList.HokurikuAddressItem;
                        break;
                    case 4:
                        listData = addressList.ChubuAddressItem;
                        break;
                    case 5:
                        listData = addressList.KantoAddressItem;
                        break;
                    case 6:
                        listData = addressList.KinkiAddressItem;
                        break;
                    case 7:
                        listData = addressList.ChugokuAddressItem;
                        break;
                    case 8:
                        listData = addressList.ShikokuAddressItem;
                        break;
                    case 9:
                        listData = addressList.KyusyuAddressItem;
                        break;
                    case 10:
                        listData = addressList.OkinawaAddressItem;
                        break;
                    default:
                        return Json(new { result = string.Empty, message = "引数が不正です。" });
                }

                //listData = listData.Where(m => m.POSTAL_CODE.Substring(3, 4) == "0000").ToList();
                listData = listData.Where(m => m.CHO_IKI == "以下に掲載がない場合").ToList();

                string jsonString = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Dto.M_PostCode_Local>>(listData);

                HttpContext.Session.SetObject(SessionKeyAddress, addressList);

                using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);

                return Json(new { result = jsonString });
            }
            catch (Exception x)
            {
                return Json(new { partialView = "", message = x.Message });
            }
        }

        ///
        /// <summary>
        /// 町域住所リストを取得して返却する
        /// </summary>
        /// <param name="area"></param>
        /// <param name="ken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAddressChoiki(int area, string ken)
        {
            try
            {
                Context.AddressList addressList = HttpContext.Session.GetObject<Context.AddressList>(SessionKeyAddress);

                addressList ??= await SetAddressList();

                IEnumerable<Dto.M_PostCode_Local> listData = null;

                switch (area)
                {
                    case 1:
                        listData = addressList.HokkaidoAddressItem;
                        break;
                    case 2:
                        listData = addressList.TohokuAddressItem;
                        break;
                    case 3:
                        listData = addressList.HokurikuAddressItem;
                        break;
                    case 4:
                        listData = addressList.ChubuAddressItem;
                        break;
                    case 5:
                        listData = addressList.KantoAddressItem;
                        break;
                    case 6:
                        listData = addressList.KinkiAddressItem;
                        break;
                    case 7:
                        listData = addressList.ChugokuAddressItem;
                        break;
                    case 8:
                        listData = addressList.ShikokuAddressItem;
                        break;
                    case 9:
                        listData = addressList.KyusyuAddressItem;
                        break;
                    case 10:
                        listData = addressList.OkinawaAddressItem;
                        break;
                    default:
                        return Json(new { result = string.Empty, message = "引数が不正です。" });
                }

                listData = listData.Where(m => m.KEN == ken && m.CHO_IKI != "以下に掲載がない場合").ToList();
                //listData = listData.Where(m => m.KEN == ken && m.CHO_IKI != "以下に掲載がない場合" && m.POSTAL_CODE.Substring(3, 2) == "00").ToList();

                string jsonString = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Dto.M_PostCode_Local>>(listData);

                HttpContext.Session.SetObject<Context.AddressList>(SessionKeyAddress, addressList);

                using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);

                return Json(new { result = jsonString });
            }
            catch (Exception x)
            {
                return Json(new { partialView = "", message = x.Message });
            }
        }

        /// <summary>
        /// M_Address使用率TOP30リスト
        /// </summary>
        /// <param name="returnKubun"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetAddressListForUtilizationRate(int returnKubun = 0)
        {
            string exceptionMessage = null;
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                const int SelectTopCount = 30;

                Models.CommonDialogModel.SelectAddressModel model = new()
                {
                    UserID = loguinUser.User_ID,
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRitsu",
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用率 Top" + SelectTopCount.ToString(),
                };

                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                List<Dto.T_Anken_Point_Local> listData = await api.GetAnkenPointForUtilizationRate(model.CompanyID, model.UserID, 30)
                    ?? throw new Exception("データの取得に失敗しました");
                model.AnkenPointList = listData;

                if (returnKubun == 0)
                {
                    return await PartialViewAsJson("AddressDataList2", model, true);
                }
                else
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize<List<Dto.T_Anken_Point_Local>>(listData);
                    using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);
                    return Json(new { result = jsonString, message = exceptionMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }

        /// <summary>
        /// M_Address使用履歴TOP30リスト
        /// </summary>
        /// <param name="returnKubun"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetAddressListForRireki(int returnKubun = 0)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                const int SelectTopCount = 30;

                Models.CommonDialogModel.SelectAddressModel model = new()
                {
                    UserID = loguinUser.User_ID,
                    CompanyID = loguinUser.Company_ID,
                    ListIdName = "tableShiyoRireki",
                    SelectTopCount = SelectTopCount,
                    TheadTitle = "使用履歴 Top" + SelectTopCount.ToString(),
                };


                API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                List<Dto.T_Anken_Point_Local> listData = await api.GetAnkenPointForRireki(model.CompanyID, model.UserID, 30)
                    ?? throw new Exception("データの取得に失敗しました");
                model.AnkenPointList = listData;

                if (returnKubun == 0)
                {
                    return await PartialViewAsJson("AddressDataList2", model, true);
                }
                else
                {
                    string jsonString = System.Text.Json.JsonSerializer.Serialize<List<Dto.T_Anken_Point_Local>>(listData);
                    using System.Net.Http.StringContent content = new(jsonString, System.Text.Encoding.UTF8);
                    return Json(new { result = jsonString });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "", message = ex.Message });
            }
        }

        #endregion 共通住所検索
    }
}
