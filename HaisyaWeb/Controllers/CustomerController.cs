using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaisyaWeb.Models;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace HaisyaWeb.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public CustomerController(ILogger<CustomerController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        #region 顧客
        public async Task<IActionResult> Index()
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            Models.CustomerListDto model = new()
            {
                CompanyID = loguinUser.Company_ID,
            };

            return View(model);
        }

        /// <summary>
        /// Json：顧客マスタ一覧データリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerDataList(int CompanyID, string code = null, string key = null, string phone = null, string pageName = "CustomerDataList")
        {
            try
            {
                if (CompanyID == 0) { return null; }
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CustomerListDataDto model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    CustomerList = new(),
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerList = await apiM.GetCustomerList(CompanyID, code, key, phone);

                return await PartialViewAsJson(pageName, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
            
        }

        /// <summary>
        /// 顧客情報の登録モーダル画面
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerRegModal(int customerID)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "Customer");

                Models.CustomerModalDto model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    CustomerID = customerID,
                    CustomerData = new(),
                };

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                if (customerID == 0)
                {
                    model.CustomerData.Customer_ID = 0;
                    model.CustomerData.Customer_ID_Oya = 0;
                    model.CustomerData.Company_ID = loguinUser.Company_ID;
                    model.Title = "新規登録";
                    model.BtnCaption = "設定の保存";
                } else { 
                    model.CustomerData = await api.GetCustomerData(customerID);
                    model.CustomerBranchList = await api.GetCustomerBranchList(loguinUser.Company_ID, customerID);
                    model.Title = "修正";
                    model.BtnCaption = "設定の保存";
                }

                if (model.CustomerData.Customer_ID_Oya > 0)
                {
                    Dto.M_Customer_Local m_Customer = await api.GetCustomerData(model.CustomerData.Customer_ID_Oya);
                    model.Customer_Name_Oya = m_Customer.Customer_Name_Abbr;
                }
              
                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                return await PartialViewAsJson("CustomerModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }

        }

        /// <summary>
        /// M_Customerマスタのデータ登録
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerRegExec(Models.CustomerModalDto dto)
        {

            string errorMessage = "";

            try
            {
                dto.CustomerData.Yosya_Flg = (dto.YosyaFlg == true) ? 1 : 0; 

                if (CustomerVaridationCheck(dto, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCustomerData(dto);

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
        /// <param name="M_Customer"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CustomerVaridationCheck(Models.CustomerModalDto dto, out string errorMessage)
        {
            if (dto.CustomerData.Customer_Name == null) { errorMessage = "入力エラー。顧客名が空白です。"; return true; }
            if (dto.CustomerData.Customer_Name_Abbr == null) { errorMessage = "入力エラー。略名が未選択です。"; return true; }
            if (dto.CustomerData.Customer_Name_Kana == null) { errorMessage = "入力エラー。顧客名カナが未選択です。"; return true; }
            //if (dto.CustomerData.PostCode == null) { errorMessage = "入力エラー。郵便番号が未選択です。"; return true; }
            //if (dto.CustomerData.Address1 == null) { errorMessage = "入力エラー。住所１が未選択です。"; return true; }
            //if (dto.CustomerData.Phone1 == null) { errorMessage = "入力エラー。電話番号１が未選択です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion 顧客

        #region 顧客支店担当
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CustomerBranchIndex()
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            Models.CustomerBranchModalDto model = new()
            {
                CompanyID = loguinUser.Company_ID,
            };

            return View(model);
        }

        /// <summary>
        /// Json：案件一覧データリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetCustomerBranchDataList(int CompanyID, string code = null, string key = null, 
                                                    string phone = null, string pageName = "CustomerBranchDataList")
        {
            try
            {
                if (CompanyID == 0) { return null; }
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CustomerBranchListDataDto model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    CustomerBranchList = new(),
                };

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerBranchList = await apiM.GetCustomerBranchList(CompanyID, 0, 0, code, key, phone);

                return await PartialViewAsJson(pageName, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }

        }

        /// <summary>
        /// M_CustomerBranchマスタ登録画面（modal）を返却
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerBranchModal(int customerBranchID)
        {
            try
            {

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo(GetType().Name, "CustomerBranch");

                Models.CustomerBranchModalDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CustomerBranchID = customerBranchID,
                    CustomerTantouList = new(),
                    CustomerTollSeikyuKubunList_Distance = new(),
                    CustomerTollSeikyuKubunList_Address = new(),
                    CustomerTollSeikyuKubunList_IC = new(),
                };

                


                API.WebApp.MasterDataApi api = new(_mapApiSettiong);

                model.CustomerBranchData = await api.GetCustomerBranchData(customerBranchID);
                model.CustomerID = model.CustomerBranchData.Customer_ID;
                model.CustomerData = await api.GetCustomerData(model.CustomerID);
                model.CustomerTantouList = await api.GetCustomerTantouList(loguinUser.Company_ID, customerBranchID);
                //model.CustomerICSeikyuKubunList = await api.GetCustomerICSeikyuKubunList(customerBranchID);
                model.CustomerTollSeikyuKubunList = await api.GetCustomerTollSeikyuKubunList(customerBranchID);
                model.CustomerUriageCalcData = await api.GetCustomerUriageCalcData(customerBranchID);
                model.CustomerShiharaiCalcData = await api.GetCustomerShiharaiCalcData(customerBranchID);

                model.CompanyUserGroupList = await Service.SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu, true);
                model.CompanyUserGroupList = model.CompanyUserGroupList.Where(m => m.Disabled == false);


                model.CompanyUserGroupListForShiharai = await Service.SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Shiharai, true);
                model.CompanyUserGroupListForShiharai = model.CompanyUserGroupListForShiharai.Where(m => m.Disabled == false);

                model.SeikyuKubunSelectList = await Service.SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 4, true);

                if (model.CustomerBranchData.Seikyu_Customer_Branch_ID > 0)
                {
                    Dto.M_Customer_Branch_Local branch = await api.GetCustomerBranchData(model.CustomerBranchData.Seikyu_Customer_Branch_ID);
                    model.Seikyu_Customer_Branch_Name = branch?.Customer_Branch_Name_Abbr;
                }

                if (model.CustomerBranchData.Shiharai_Customer_Branch_ID > 0)
                {
                    Dto.M_Customer_Branch_Local branch = await api.GetCustomerBranchData(model.CustomerBranchData.Shiharai_Customer_Branch_ID);
                    model.Shiharai_Customer_Branch_Name = branch?.Customer_Branch_Name_Abbr;
                }

                //特記事項
                model.Remarks = model.CustomerBranchData.Remarks;
                model.SeikyuRemarks = model.CustomerBranchData.SeikyuRemarks;
                model.AnkenRemarks = model.CustomerBranchData.AnkenRemarks;

                // 受領書印刷フラグ
                model.ReceiptOutputFlg =  (model.CustomerBranchData.Receipt_Output_Flg == 0) ? false : true;

                //担当者データが無い場合は１行空行を作成する
                if (model.CustomerTantouList == null || model.CustomerTantouList.Count == 0) {
                    Dto.M_Customer_Tantou_Local tanou = new()
                    {
                        Company_ID = loguinUser.Company_ID,
                        Customer_ID = model.CustomerID,
                    };
                    model.CustomerTantouList.Add(tanou);
                }

                model.Title = "修正";
                model.BtnCaption = "設定の保存";

                if (customerBranchID == 0)
                {
                    model.CustomerBranchData.Seikyu_Kubun = 0;
                    model.CustomerBranchData.SeikyuDate_Kubun = 0;
                    model.CustomerBranchData.Tax_Fraction_Kubun = 0;
                    model.CustomerBranchData.Tax_Fraction_Position = 0;
                }

                model.EditEnabled = roleList.Count != 0 && roleList.First().Enabled;

                model.FutanKubunSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="1", Text="自己負担" },
                    new SelectListItem() { Value="2", Text="荷主負担" },
                    new SelectListItem() { Value="3", Text="会社負担" },
                    };

                model.FutanTypeSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="1", Text="距離" },
                    new SelectListItem() { Value="2", Text="住所" },
                    new SelectListItem() { Value="3", Text="IC" },
                    };

                model.SeikyuDateKubunSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="0", Text="配車日(積日)" },
                    new SelectListItem() { Value="1", Text="卸日" },
                    };
                ///０：切り捨て、１：四捨五入、２：切り上げ
                model.SeikyuTakKubunSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="0", Text="切り捨て" },
                    new SelectListItem() { Value="1", Text="四捨五入" },
                    new SelectListItem() { Value="2", Text="切り上げ" },
                    };

                model.CollectionSightSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="0", Text="当月" },
                    new SelectListItem() { Value="1", Text="翌月" },
                    new SelectListItem() { Value="2", Text="翌々月" },
                    new SelectListItem() { Value="3", Text="翌々々月" },
                    new SelectListItem() { Value="4", Text="翌々々々月" },
                    };

                model.ReportOutputNameFlgSelectList = new SelectListItem[] {
                    new SelectListItem() { Value="0", Text="印刷する" },
                    new SelectListItem() { Value="1", Text="印刷しない" },
                    };

                model.CustomerTollSeikyuKubunList_Distance.Add(new());
                model.CustomerTollSeikyuKubunList_Distance.ForEach(m => m.Seikyu_Type = 1);
                model.CustomerTollSeikyuKubunList_Address.Add(new());
                model.CustomerTollSeikyuKubunList_Address.ForEach(m => m.Seikyu_Type = 2);
                model.CustomerTollSeikyuKubunList_IC.Add(new());
                model.CustomerTollSeikyuKubunList_IC.ForEach(m => m.Seikyu_Type = 3);

                if (model.CustomerTollSeikyuKubunList == null || model.CustomerTollSeikyuKubunList.Count == 0) {} else
                {
                    List<Dto.M_Customer_TollSeikyuKubun_Local> seikyuList = model.CustomerTollSeikyuKubunList.Where(m => m.Seikyu_Type == 1).ToList();
                    if (!(seikyuList == null || seikyuList.Count == 0))
                    {
                        model.CustomerTollSeikyuKubunList_Distance = seikyuList;
                    }
                    seikyuList = model.CustomerTollSeikyuKubunList.Where(m => m.Seikyu_Type == 2).ToList();
                    if (!(seikyuList == null || seikyuList.Count == 0))
                    {
                        model.CustomerTollSeikyuKubunList_Address = seikyuList;
                    }
                    seikyuList = model.CustomerTollSeikyuKubunList.Where(m => m.Seikyu_Type == 3).ToList();
                    if (!(seikyuList == null || seikyuList.Count == 0))
                    {
                        model.CustomerTollSeikyuKubunList_IC = seikyuList;
                    }
                }

                
                return await PartialViewAsJson("CustomerBranchModal", model, true);
            }
            catch (Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }

        }

        /// <summary>
        /// M_CustomerBranchマスタのデータ登録
        /// </summary>
        /// <param name="M_CustomerBranch"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerBranchRegExec(Models.CustomerBranchModalDto dto)
        {

            string errorMessage = "";

            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                if (CustomerBranchVaridationCheck(dto, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                dto.CustomerTollSeikyuKubunList = new();
                if (dto.CustomerTollSeikyuKubunList_Distance != null)
                {
                    dto.CustomerTollSeikyuKubunList_Distance.Where(m => m.Distance1 > 0 || m.Distance2 > 0).ToList().ForEach(m => dto.CustomerTollSeikyuKubunList.Add(m));
                }
                if (dto.CustomerTollSeikyuKubunList_Address != null)
                {
                    dto.CustomerTollSeikyuKubunList_Address.Where(m => m.Address1 != null || m.Address2 != null).ToList().ForEach(m => dto.CustomerTollSeikyuKubunList.Add(m));
                }
                if (dto.CustomerTollSeikyuKubunList_IC != null)
                {
                    dto.CustomerTollSeikyuKubunList_IC.Where(m => m.IC1 != null || m.IC2 != null).ToList().ForEach(m => dto.CustomerTollSeikyuKubunList.Add(m));
                }

                dto.CustomerBranchData.Update_User = loguinUser.User_ID;
                if (dto.CustomerTollSeikyuKubunList != null) { dto.CustomerTollSeikyuKubunList.ForEach(m => { m.Update_User = loguinUser.User_ID; m.Insert_User = loguinUser.User_ID;  }) ; }


                dto.CustomerBranchData.Remarks = dto.Remarks;
                dto.CustomerBranchData.SeikyuRemarks = dto.SeikyuRemarks;
                dto.CustomerBranchData.AnkenRemarks = dto.AnkenRemarks;

                dto.CustomerBranchData.Receipt_Output_Flg = (dto.ReceiptOutputFlg == true) ? 1 : 0;

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.M_Customer_Local customer = await api.GetCustomerData(dto.CustomerData.Customer_ID);

                if (customer.Yosya_Flg == 0)
                {
                    // 傭車データの初期化
                    dto.CustomerBranchData.Customer_Branch_Code_Trac_Yosyasaki = null;
                    dto.CustomerBranchData.Shiharai_TantouID = 0;
                    dto.CustomerBranchData.Shiharai_Shime_Day = 0;
                    dto.CustomerBranchData.Shiharai_Customer_Branch_ID = 0;
                    dto.CustomerBranchData.Shiharai_Mail_Address1 = null;
                    dto.CustomerBranchData.Shiharai_Mail_Header = null;
                    dto.CustomerBranchData.Shiharai_Phone1 = null;
                    dto.CustomerBranchData.Shiharai_Fax1 = null;
                    dto.CustomerBranchData.Shiharai_PostCode = null;
                    dto.CustomerBranchData.Shiharai_Atesaki = null;
                    dto.CustomerBranchData.Shiharai_Address1 = null;
                    dto.CustomerBranchData.Shiharai_Address2 = null;
                    dto.CustomerBranchData.Shiharai_Sight = 0;
                    dto.CustomerBranchData.Shiharai_Day = 0;
                }

                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCustomerBranchData(dto);

                return Json(new { retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// M_CustomerBranchマスタ画面の入力チェック画面
        /// </summary>
        /// <param name="M_CustomerBranch"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CustomerBranchVaridationCheck(Models.CustomerBranchModalDto dto, out string errorMessage)
        {
            if (dto.CustomerBranchData.Customer_Branch_Name == null) { errorMessage = "入力エラー。顧客名が空白です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Name_Abbr == null) { errorMessage = "入力エラー。略名が未選択です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Name_Kana == null) { errorMessage = "入力エラー。顧客名カナが未選択です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Post == null) { errorMessage = "入力エラー。郵便番号が未選択です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Address1 == null) { errorMessage = "入力エラー。住所１が未選択です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Phone1 == null) { errorMessage = "入力エラー。電話番号１が未選択です。"; return true; }

            if (dto.CustomerTantouList == null) { errorMessage = "入力エラー。顧客担当者が未設定です。"; return true; }

            foreach (var tantou in dto.CustomerTantouList)
            {
                if (tantou.Tantou_Name == null) { errorMessage = "入力エラー。担当者名が空白です。"; return true; }
                if (tantou.Tantou_Name_Abbr == null) { errorMessage = "入力エラー。担当者名(メールの宛名)が空白です。"; return true; }
                if (tantou.Mail_Address1 != null && tantou.Mail_Title == null) { errorMessage = "入力エラー。メールタイトルが空白です。"; return true; }
            }

            errorMessage = "";
            return false;
        }
        #endregion 顧客支店担当

        #region トラックメイト連携
        /// <summary>
        /// トラックメイト連携一覧Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TracmateLink()
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CustomerTruckmeteListDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    SelectKubun = 0,
                };

                return View(model);
            }
            catch (Common.SessionTimeOutException ex) {
                Console.WriteLine("Exception: " + ex.Message);
                return RedirectToAction("Login", "Account", new { returnUrl = "/HaisyaWeb/Customer/TracmateLink" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ErrorViewModel errorModel = new();
                errorModel.RequestId = "1";
                errorModel.Message = ex.Message;
                return View("Error", errorModel);
            }
        }

        /// <summary>
        /// Json：トラックメイト未連携データリストのHTMLの返却
        /// </summary>
        /// <param name="code"></param>
        /// <param name="syamei"></param>
        /// <param name="kana"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonGetDataListForTrucmete(string code = null, string key = null, string phone = null, int select = 0)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CustomerTruckmeteListDataDto model = new()
                {
                    CompanyID = loguinUser.User_ID,
                    TokuisakiForNotConnectList = new(),
                };

                API.WebApp.IMAIDataApi api = new(_mapApiSettiong);

                if (select == 1)
                {
                    List<Dto.V_YosyasakiForNotConnect_Local> list = await api.GetYosyasakiForNotConnectList(model.CompanyID, code, key, phone);

                    foreach(var target in list)
                    {
                        Dto.V_TokuisakiForNotConnect_Local data = new();
                        CopyProperty(data, target);
                        data.社名 = target.名称;
                        data.住所１ = target.住所1;
                        data.住所２ = target.住所2;
                        model.TokuisakiForNotConnectList.Add(data);
                    }
                    
                    
                } else
                {
                    model.TokuisakiForNotConnectList = await api.GetTokuisakiForNotConnectList(model.CompanyID, code, key, phone);
                }

                
                

                return await PartialViewAsJson("TrucmeteDataList", model, true);
            } catch (Exception ex)
            {
                return await JsonError(ex);
            }
            
        }

        /// <summary>
        /// トラックメイト連携のモーダル画面
        /// </summary>
        /// <param name="code"></param>
        /// <param name="select">0:得意先/1:傭車先</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TracmateLinkModal(string code, int select)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                Models.CustomerTruckmeteModalDto model = new()
                {
                    CompanyID = loguinUser.Company_ID,
                    CustomerData = new(),
                    CustomerBranchData = new(),
                    TokuisakiData = new(),
                    CustomerTantouList = new(),
                    SeikyuTantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Seikyu, true),
                    ShiharaiTantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Shiharai, true),
                };

                List<Dto.V_Tokuisaki_Local> list = null;
                List<Dto.V_YosyasakiForNotConnect_Local> yosyaList = new();

                Dto.V_Tokuisaki_Local v_Tokuisaki = null;
                Dto.V_YosyasakiForNotConnect_Local v_Yosyasaki = null;

                API.WebApp.IMAIDataApi api = new(_mapApiSettiong);
                list = await api.V_TokuisakiList(model.CompanyID, code);
                //yosyaList = await api.GetYosyasakiForNotConnectList(model.CompanyID, code);



                bool flgNoTokuisaki = false;

                if (select == 1 && list.Count == 0)
                {
                    v_Yosyasaki = yosyaList.First();

                    list = await api.V_TokuisakiList(model.CompanyID, null, v_Yosyasaki.名称);


                    if (list.Count == 0 && yosyaList != null)
                    {
                        flgNoTokuisaki = true;
                        list = new();
                        foreach (var target in yosyaList)
                        {
                            Dto.V_Tokuisaki_Local data = new();
                            CopyProperty(data, target);
                            data.社名 = target.名称;
                            data.住所１ = target.住所1;
                            data.住所２ = target.住所2;
                            data.回収区分 = 0;
                            data.回収サイト = 0;
                            data.回収日１ = 0;
                            data.回収日２ = 0;
                            data.回収日３ = 0;
                            list.Add(data);
                        }
                        
                    }
                    
                }

                if (list == null || list.Count == 0) { throw new Exception("トラックメイトの顧客マスタが見つかりませんでした"); }
                if (list.Count > 1) { 
                    throw new Exception("トラックメイトの顧客の取得処理が失敗しました。同じ得意先名が2件以上あります。得意先から選択してください。"); 
                }

                v_Tokuisaki = list.First();

                if (select == 0 && yosyaList.Count == 0)
                {
                    yosyaList = await api.GetYosyasakiForNotConnectList(model.CompanyID, null, v_Tokuisaki.社名);

                    if (yosyaList.Count > 1) {
                        yosyaList = yosyaList.Where(m => m.略称 == v_Tokuisaki.略称).ToList();
                        if (yosyaList.Count > 1) { throw new Exception("トラックメイトの傭車先の取得処理が失敗しました。同じ傭車先名が2件以上あります。紐づけ出来ません。"); }
                    }
                           
                    if (yosyaList.Count == 1) v_Yosyasaki = yosyaList.First();
                } else
                {
                    v_Yosyasaki = yosyaList.First();
                }


                string CompanyCode = v_Tokuisaki.コード.ToString().Substring(0, 5);

                API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CustomerData = await apiM.GetCustomerData(0, CompanyCode);

                if (model.CustomerData == null)
                {

                    List<Dto.V_Tokuisaki_Local> oya = await api.V_TokuisakiList(model.CompanyID, CompanyCode + "000");

                    if (oya.Count == 0) {
                        model.CustomerData = new();
                        model.CustomerData.Customer_ID = 0;
                        model.CustomerData.Company_ID = model.CompanyID;
                        model.CustomerData.Customer_Code = CompanyCode;
                        model.CustomerData.Customer_Name = v_Tokuisaki.社名;
                        model.CustomerData.Customer_Name_Kana = v_Tokuisaki.検索カナ;
                        model.CustomerData.Customer_Name_Abbr = v_Tokuisaki.略称;
                        model.CustomerData.PostCode = v_Tokuisaki.郵便番号;
                        model.CustomerData.Address1 = v_Tokuisaki.住所１;
                        model.CustomerData.Address2 = v_Tokuisaki.住所２;
                        model.CustomerData.Phone1 = v_Tokuisaki.電話番号;
                        model.CustomerData.Fax1 = v_Tokuisaki.FAX番号;
                        model.CustomerData.Insert_User = loguinUser.User_ID;
                    } else {
                        Dto.V_Tokuisaki_Local oyaData = oya.First();
                        model.CustomerData = new();
                        model.CustomerData.Customer_ID = 0;
                        model.CustomerData.Company_ID = model.CompanyID;
                        model.CustomerData.Customer_Code = CompanyCode;
                        model.CustomerData.Customer_Name = oyaData.社名;
                        model.CustomerData.Customer_Name_Kana = oyaData.検索カナ;
                        model.CustomerData.Customer_Name_Abbr = oyaData.略称;
                        model.CustomerData.PostCode = oyaData.郵便番号;
                        model.CustomerData.Address1 = oyaData.住所１;
                        model.CustomerData.Address2 = oyaData.住所２;
                        model.CustomerData.Phone1 = oyaData.電話番号;
                        model.CustomerData.Fax1 = oyaData.FAX番号;
                        model.CustomerData.Insert_User = loguinUser.User_ID;
                    }

                    model.CustomerBranchData.Customer_ID = 0;

                    if (v_Yosyasaki != null)
                    {
                        model.CustomerData.Yosya_Flg = 1;
                    }


                } else
                {
                    model.CustomerBranchData.Customer_ID = model.CustomerData.Customer_ID;
                }

                model.CustomerBranchData.Customer_Branch_ID = 0;
                model.CustomerBranchData.SortOrder = 0;
                model.CustomerBranchData.Oya_Branch_ID = 0;
                model.CustomerBranchData.Customer_Branch_Code = v_Tokuisaki.コード.ToString();
                model.CustomerBranchData.Customer_Branch_Name = v_Tokuisaki.社名;
                model.CustomerBranchData.Customer_Branch_Name_Kana = v_Tokuisaki.検索カナ;
                model.CustomerBranchData.Customer_Branch_Name_Abbr = v_Tokuisaki.略称;
                model.CustomerBranchData.Customer_Branch_Post = v_Tokuisaki.郵便番号;
                model.CustomerBranchData.Customer_Branch_Address1 = v_Tokuisaki.住所１;
                model.CustomerBranchData.Customer_Branch_Address2 = v_Tokuisaki.住所２;
                model.CustomerBranchData.Customer_Branch_Phone1 = v_Tokuisaki.電話番号;
                model.CustomerBranchData.Customer_Branch_Fax1 = v_Tokuisaki.FAX番号;
                model.CustomerBranchData.Shime_Day = v_Tokuisaki.締日１;
                model.CustomerBranchData.Collection_Sight = v_Tokuisaki.回収サイト;
                model.CustomerBranchData.Collection_Day = v_Tokuisaki.回収日１;

                if (v_Yosyasaki != null)
                {
                    model.CustomerBranchData.Customer_Branch_Code_Trac_Yosyasaki = v_Yosyasaki.コード;
                    model.CustomerBranchData.Shiharai_Shime_Day = v_Yosyasaki.支払日１;
                    model.CustomerBranchData.Shiharai_Sight = v_Yosyasaki.支払サイト;
                    model.CustomerBranchData.Shiharai_Day = v_Yosyasaki.支払日１;

                    if (flgNoTokuisaki == true) {
                        if (v_Yosyasaki.消費税計算区分.ToString().Length == 3)
                        {
                            model.CustomerBranchData.Tax_Fraction_Position = float.Parse(v_Yosyasaki.消費税計算区分.ToString().Substring(1, 1));
                            model.CustomerBranchData.Tax_Fraction_Kubun = int.Parse(v_Yosyasaki.消費税計算区分.ToString().Substring(2, 1));
                        }
                    }
                } 

                if (flgNoTokuisaki == false)
                {
                    model.CustomerBranchData.Customer_Branch_Code_Trac_Tokuisaki = v_Tokuisaki.コード;
                }
                




                if (v_Tokuisaki.消費税計算区分.ToString().Length == 3) { 
                    model.CustomerBranchData.Tax_Fraction_Position = float.Parse(v_Tokuisaki.消費税計算区分.ToString().Substring(1,1));
                    model.CustomerBranchData.Tax_Fraction_Kubun = int.Parse(v_Tokuisaki.消費税計算区分.ToString().Substring(2, 1));
                }


                //if (v_Tokuisaki.コード.Substring(v_Tokuisaki.コード.Length -3, 3) == "000")
                //{
                //    model.CustomerData.Customer_ID_Oya = 0;
                //} else
                //{
                //    List<Dto.M_Customer_Local> m_Customer = await apiM.GetCustomerList(model.CompanyID, null, v_Tokuisaki.コード.Substring(0, v_Tokuisaki.コード.Length - 3) + "000");
                //    if ((m_Customer != null && m_Customer.Count == 1))
                //    {
                //        Dto.M_Customer_Local oya = m_Customer.First();
                //        model.CustomerData.Customer_Code_Oya = oya.Customer_Code;
                //        model.CustomerData.Customer_ID_Oya = oya.Customer_ID;
                //        model.Customer_Name_Oya = oya.Customer_Name;
                //    }
                //}

                Dto.M_Customer_Tantou_Local tantou = new()
                {
                    Customer_ID = 0,
                    Company_ID = model.CompanyID,
                    Tantou_Name = v_Tokuisaki.社名,
                    Tantou_Name_Abbr = v_Tokuisaki.略称,
                    Mail_Address1 = "none",
                    PostCode = v_Tokuisaki.郵便番号,
                    Address1 = v_Tokuisaki.住所１,
                    Address2 = v_Tokuisaki.住所２,
                    Phone1 = v_Tokuisaki.電話番号,
                    Fax1 = v_Tokuisaki.FAX番号,
                    Del_Flg = false,
                };
                model.CustomerTantouList.Add(tantou);

                model.TaxFractionKubunSelectList = new SelectListItemEx[] {
                        new SelectListItemEx() { Value="0", Text="切り捨て" },
                        new SelectListItemEx() { Value="1", Text="四捨五入" },
                        new SelectListItemEx() { Value="2", Text="切り上げ" },
                    };

                return await PartialViewAsJson("TracmateLinkModal", model, true);

            } catch(Exception ex)
            {
                return Json(new { partialView = "", message = ex.Message });
            }
            
        }

        /// <summary>
        /// M_Customerマスタのデータ登録
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<IActionResult> TracmateLinkRegExec(Models.CustomerTruckmeteModalDto dto)
        {

            string errorMessage = "";

            try
            {
                dto.CustomerData.Yosya_Flg = (dto.YosyaFlg == true) ? 1 : 0;

                if (CustomerTracmateVaridationCheck(dto, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCustomerTracmateData(dto);

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
        /// <param name="M_Customer"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CustomerTracmateVaridationCheck(Models.CustomerTruckmeteModalDto dto, out string errorMessage)
        {
            if (dto.CustomerData.Customer_Name == null) { errorMessage = "入力エラー。担当者名が空白です。"; return true; }
            if (dto.CustomerData.Customer_Code == null) { errorMessage = "入力エラー。顧客コードが空白です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Name == null) { errorMessage = "入力エラー。支店名が空白です。"; return true; }
            if (dto.CustomerBranchData.Customer_Branch_Code == null) { errorMessage = "入力エラー。支店コードが空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion トラックメイト連携

        #region CostomerTantouList
        public async Task<IActionResult> CostomerTantouList(int CustomerID)
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            if (CustomerID == 0) { return null; }

            Models.CustomerTantouDto model = new()
            {
                CompanyID = loguinUser.Company_ID,
                CustomerID = CustomerID,
                CustomerData = new(),
                CustomerTantouList = new(),
            };

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            model.CustomerData = await api.GetCustomerData(CustomerID);
            model.CustomerTantouList = await api.GetCustomerTantouList(loguinUser.Company_ID, CustomerID);

            return await PartialViewAsJson("CostomerTantouList", model, true);
        }

        public async Task<IActionResult> CostomerTantouModal(int CustomerID, int TantouID)
        {
            // ログインユーザー取得
            Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

            if (CustomerID == 0) { return null; }

            Models.CustomerTantouDto model = new()
            {
                CompanyID = loguinUser.Company_ID,
                CustomerID = CustomerID,
                TantouID = TantouID,
                CustomerData = new(),
                CustomerTantouData = new(),
                CustomerTantouList = new(),
            };

            API.WebApp.MasterDataApi api = new(_mapApiSettiong);
            model.CustomerData = await api.GetCustomerData(CustomerID);

            if (TantouID == 0)
            {
                model.CustomerTantouData.Tantou_ID = 0;
                model.CustomerTantouData.Company_ID = loguinUser.Company_ID;
                model.CustomerTantouData.Customer_ID = CustomerID;
                model.Title = "新規登録";
                model.BtnCaption = "設定の保存";
            } else
            {
                model.CustomerTantouList = await api.GetCustomerTantouList(loguinUser.Company_ID, CustomerID);
                model.CustomerTantouData = model.CustomerTantouList.FirstOrDefault(m => m.Tantou_ID == TantouID);
                model.Title = "修正";
                model.BtnCaption = "設定の保存";
            }

            return await PartialViewAsJson("CostomerTantouModal", model, true);
        }

        /// <summary>
        /// M_Customerマスタのデータ登録
        /// </summary>
        /// <param name="M_Customer"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerTantouRegExec(Models.CustomerTantouDto dto)
        {

            string errorMessage = "";

            try
            {
                if (CustomerTantouVaridationCheck(dto, out errorMessage))
                {
                    return Json(new { t_Anken = "", errorMessage = errorMessage });
                }

                API.WebApp.MasterDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.InsertUpdateCustomerTantouData(dto.CustomerTantouData);

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
        /// <param name="M_Customer"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool CustomerTantouVaridationCheck(Models.CustomerTantouDto dto, out string errorMessage)
        {
 
            if (dto.CustomerTantouData.Tantou_Name == null) { errorMessage = "入力エラー。担当者名が空白です。"; return true; }
            if (dto.CustomerTantouData.Tantou_Name_Abbr == null) { errorMessage = "入力エラー。担当者名(メールの宛名)が空白です。"; return true; }
            if (dto.CustomerTantouData.Mail_Address1 == null) { errorMessage = "入力エラー。メールアドレスが空白です。"; return true; }

            errorMessage = "";
            return false;
        }
        #endregion CostomerTantouList


    }
}
