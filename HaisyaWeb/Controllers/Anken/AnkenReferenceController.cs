using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
using HaisyaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static HaisyaWeb.Common.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HaisyaWeb.Models.AnkenModel;
using HaisyaWeb.Dto;
using HaisyaWeb.Common;

namespace HaisyaWeb.Controllers.Anken
{
    [Authorize]
    public class AnkenReferenceController : BaseController
    {
        private readonly ILogger<AnkenReferenceController> _logger;

        private const string SessionCopyAnken = "_objCopyAnkenInfo";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="viewRenderService"></param>
        /// <param name="mapApiSetting"></param>
        public AnkenReferenceController(ILogger<AnkenReferenceController> logger, IViewRenderService viewRenderService,
            IOptions<MapApiSettings> mapApiSetting, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
            _signInManager = signInManager;
        }

        /// <summary>
        /// 案件履歴画面（モーダル）を返却する
        /// </summary>
        /// <param name="ankenID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(int ankenID, int ankenDisplayID)
        {
            try
            {
                if (ankenID == 0) { throw new Exception("パラメーターエラー：ankenID"); }

                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo("HaisyaController", "Haisya");

                AnkenReferenceModel model = new()
                {
                    Company_ID = loguinUser.Company_ID,
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                    AnkenDisplayID = ankenDisplayID,
                    Report_Serch_Kubun = new(),
                };

                using API.WebApp.AnkenDataApi api = new(_mapApiSettiong);
                List<V_AnkenDataList_Local> list = await api.GetAnkenDataList(null, null, null, loguinUser.Company_ID, 0, 0, 0, 0, 0, ankenID)
                    ?? throw new Exception("パラメーターエラー：ankenID＝" + ankenID.ToString());
                model.AnkenData = list.FirstOrDefault();
                model.AnkenPointList = await api.GetAnkenPointList(model.AnkenData.Anken_ID, model.AnkenData.Anken_Order);
                model.HaisyaKubunSelectList = await SearchCommonService.GetCodeDataSelectList(_mapApiSettiong, 22, false);

                using API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);
                model.HaisyaDataList = await apiH.GetHaisyaDataList(loguinUser.Company_ID, null, null, null, 0, ankenID, 0, 0, ankenDisplayID);
                //配車データが無い場合は、空を作成
                if (model.HaisyaDataList == null || model.HaisyaDataList.Count == 0)
                {
                    model.HaisyaDataList = new();

                    Dto.T_Anken_Display_Local AnkenDisplay = await api.GetAnkenDisplayData(ankenDisplayID);

                    Dto.V_HaisyaDataList_Local v_Haisya = new()
                    {
                        Anken_ID = ankenID,
                        AnkenDisplay_ID = ankenDisplayID,
                        Display2 = AnkenDisplay?.Display2,
                    };

                    model.HaisyaDataList.Add(v_Haisya);
                }

                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                model.CodeList = await apiM.M_Code_DataList(0);
                model.CompanyUserList = await apiM.GetCompanyUserList(loguinUser.Company_ID);
                model.CompanyUserGroupList = await apiM.GetCompanyUserGroupList(loguinUser.Company_ID, Service.UserGroupLists.ALL);

                model.Report_Serch_Kubun = await GetReportSearchKubun();

                return await PartialViewAsJson("AnkenReferenceModal", model);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 傭車乗務員登録画面
        /// </summary>
        /// <param name="ankenDisplayID"></param>
        /// <param name="yosyaBranchId"></param>
        /// <returns></returns>
        public async Task<IActionResult> JsonSenzokuDriverRegModal(int ankenDisplayID, int yosyaBranchId)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                // 権限データ
                List<Dto.M_Role_Local> roleList = await GetRoleInfo("HaisyaController", "Haisya");

                AnkenReferenceYosyaRegModel model = new()
                {
                    UserID = loguinUser.User_ID,
                    CompanyID = loguinUser.Company_ID,
                    AnkenDisplayID = ankenDisplayID,
                    EditEnabled = roleList.Count != 0 && roleList.First().Enabled,
                    BtnCaption = "設定の保存",
                    Yosya = new(),
                    TantouSelectList = await SearchCommonService.GetUserGroupSelect(_mapApiSettiong, loguinUser.Company_ID, UserGroupLists.Haisya),
                };

                API.WebApp.HaisyaDataApi apiH = new(_mapApiSettiong);

                if (yosyaBranchId == 0)
                {
                    model.Title = "新規登録";
                    model.Yosya.Haisya_ID = 0;
                    model.Yosya.Yosya_Sort = 0;
                    model.Yosya.Yosya_Count = 1;
                }
                else
                {
                    model.Title = "修正";
                    List<V_HaisyaDataList_Local> haisyaList = await apiH.GetHaisyaDataList(loguinUser.Company_ID, null, null, null, 0, 0, 0, 0, ankenDisplayID);
                    V_HaisyaDataList_Local haisya = haisyaList.FirstOrDefault();

                    model.YosyaBranchName = haisya.Driver_Branch_Name;
                    model.YosyaTantouName = haisya.Driver_Yosya_Tantou_Name;
                    model.YosyaDriverName = haisya.Driver_Name;
                    model.YosyaDriverSyaryoName = haisya.Syasyu + haisya.Kata + "  " + haisya.FULL_SYABAN;

                    model.Yosya = await apiH.GetHaisyaYosyaData((int)haisya.Haisya_ID);
                }

                return await PartialViewAsJson("YosyaRegModal", model, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 【Json】配車された傭車ドライバー情報を登録する
        /// </summary>
        /// <param name="AnkenDisplayID"></param>
        /// <param name="YosyaDriverID"></param>
        /// <param name="YosyaDriverSyaryoID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> JsonRegisterYosyaDriver(AnkenReferenceYosyaRegModel param)
        {
            try
            {
                // ログインユーザー取得
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                //更新用Dtoの作成
                HaisyaModel.HaisyaYosyaDriverRegisterDto dto = new()
                {
                    loginUser = loguinUser,
                    AnkenDisplayID = param.AnkenDisplayID,
                    HaisyaYosya = param.Yosya,
                };

                using API.WebApp.HaisyaDataApi api = new(_mapApiSettiong);
                Dto.MsterDataCommonResultValDto_Local result = await api.RegisterYosyaDriver(dto);

                List<Dto.V_HaisyaDataList_Local> haisyaDataList = await api.GetHaisyaDataList(loguinUser.Company_ID, null, null, null, 0, 0, 0, 0, param.AnkenDisplayID);


                return Json(new { haisyaData = haisyaDataList.FirstOrDefault(), retrunFlg = result.RetrunFlg, errorMessage = result.ErrrMessage });
            }
            catch (Exception ex)
            {
                return Json(new { retrunFlg = false, errorMessage = ex.Message });
            }
        }

        /// <summary>
        /// レポート情報取得
        /// </summary>
        /// <returns>M_Report_Serch_Kubun_Local（null返却時はボタン押下不可）</returns>
        private async Task<M_Report_Serch_Kubun_Local> GetReportSearchKubun()
        {
            M_Report_Serch_Kubun_Local reportSerchKubun = null;
            try
            {
                using API.WebApp.MasterDataApi apiM = new(_mapApiSettiong);
                M_Report_Serch_Local reportSerch = await apiM.GetReportSearch((int)ReportType.UNKO_SHIJI_LIST);
                if (reportSerch?.Report_Serch_ID > 0) 
                {
                    List<M_Report_Serch_Kubun_Local> reportSerchKubunList = await apiM.GetReportSearchKubunList(reportSerch.Report_Serch_ID);
                    reportSerchKubun = reportSerchKubunList.Find(m => m.Report_Html == SystemConstants.ReportCommon.ReportHtmlType.TransportInstructionsSheet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            return reportSerchKubun;
        }
    }
}
