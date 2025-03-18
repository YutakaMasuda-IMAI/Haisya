using HaisyaWeb.Dto;
using HaisyaWeb.Models;
using HaisyaWeb.Models.DB;
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
using static HaisyaWeb.Models.AccidentListModel;

namespace HaisyaWeb.Controllers
{
    /// <summary>
    /// 事故コントローラー
    /// </summary>
    public class AccidentController : BaseController
    {
        public AccidentController(ILogger<AccidentController> logger,
            SignInManager<ApplicationUser> signInManager, IViewRenderService viewRenderService, IOptions<MapApiSettings> mapApiSetting)
        {
            _signInManager = signInManager;
            _viewRenderService = viewRenderService;
            _mapApiSettiong = mapApiSetting.Value;
        }
        
        /// <summary>
        /// 事故情報の返却
        /// </summary>
        /// <param name="jikoId"></param>
        /// <returns>AccidentModel</returns>
        public async Task<IActionResult> AccidentInputIndex(int jikoId, SearchModelForAccidentList param)
        {
            try
            {
                // ログインユーザーの情報を取得
                V_LoginUser_Local loguinUser = await GetLoginUser();
                // AccidentDataApiを使用して、指定された事故IDとユーザーIDに基づいて事故情報を取得
                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);

                AccidentModel_Local AccidentModel = await api.GetAccident(jikoId, loguinUser.User_ID);
                // ログインユーザーのID、会社ID、ユーザー名をAccidentModelに設定   
                AccidentModel.Login_User_ID = loguinUser.User_ID;
                AccidentModel.Company_ID = loguinUser.Company_ID;
                AccidentModel.Login_User_Name = loguinUser.User_Name;

                // JikoKubunリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.JikoKubunDto = AccidentModel.JikoKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data, // データベースからのコード値
                    Text = item.Code_Name  // コードの表示名
                }).ToList();
                // WeatherKubunリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.WeatherKubunDto = AccidentModel.WeatherKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // JikoTypeKubunリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.JikoTypeKubunDto = AccidentModel.JikoTypeKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // ListKubunリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.ListKubunDto = AccidentModel.ListKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // WorkKubunリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.WorkKubunDto = AccidentModel.WorkKubun.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // WorkFlowListリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.WorkFlowListDto = AccidentModel.WorkFlowList.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                // UserGroupDataリストをSelectListItem形式に変換して、ビューに渡すためにAccidentModelに設定
                AccidentModel.UserGroupDataDto = AccidentModel.UserGroupData.Select(item => new SelectListItem
                {
                    Value = item.Code_Data,
                    Text = item.Code_Name
                }).ToList();
                AccidentModel.Search = param;
                return View("../Accident/AccidentInputIndex", AccidentModel);
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                return Error(ex, Layout.MainLayout);
            }
        }

        /// <summary>
        /// 事故情報の返却
        /// </summary>
        /// <param name="jikoId"></param>
        /// <returns>List<WorkFlowList>></returns>
        public async Task<List<WebApplication.Model.WorkFlow>> GetJikoWorkFlowList(int jikoId)
        {
            try
            {
                // ログインユーザーの情報を取得
                V_LoginUser_Local loguinUser = await GetLoginUser();
                // AccidentDataApiを使用して、指定された事故IDとユーザーIDに基づいて事故情報を取得
                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);

                AccidentModel_Local AccidentModel = await api.GetAccident(jikoId, loguinUser.User_ID);
                return AccidentModel.JikoWorkFlowList;
            }
            // 処理エラー用のviewを返します
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<WebApplication.Model.WorkFlow>();
            }
        }

        /// <summary>
        /// Jiko_Statusの更新
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeStatus([FromBody] Dto.ChangeStatus combinedData)
        {
            string errorMessage = null;
            try
            {
                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.ChangeStatus(combinedData);
                return Json(new { data = result, errorMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// JSON：ワークフロー情報の返却
        /// </summary>
        /// <param name="Jiko_ID"></param>
        /// <param name="Jiko_WorkFlow_Base_ID"></param>
        /// <returns>PartialViewAsJson</returns>
        public async Task<IActionResult> GetWorkflowList(int Jiko_ID, int Jiko_WorkFlow_Base_ID)
        {
            try
            {
                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                int User_ID = loguinUser.User_ID;

                WorkFlowList model = await api.GetWorkflowList(Jiko_ID, Jiko_WorkFlow_Base_ID, User_ID);
                model.Jiko_ID = Jiko_ID;
                model.Jiko_WorkFlow_Base_ID = Jiko_WorkFlow_Base_ID;
                string html = "../Accident/WorkflowList";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 差し戻しの登録モーダル画面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RemandModal(int Jiko_WorkFlow_Status_ID, int Jiko_ID = 0)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();

                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);

                Dto.RemandDataModel model = await api.GetRemandData(loguinUser.User_ID, Jiko_WorkFlow_Status_ID);
                model.Jiko_WorkFlow_Status_ID = Jiko_WorkFlow_Status_ID;
                model.Jiko_ID = Jiko_ID;

                string html = "../Accident/RemandModal";

                return await PartialViewAsJson(html, model, true);
            }
            catch (Exception ex)
            {
                return await JsonError(ex);
            }
        }

        /// <summary>
        /// 事故の起案
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostAccident([FromBody] PostAccident combinedData)
        {
            try
            {
                Dto.V_LoginUser_Local loguinUser = await GetLoginUser();
                combinedData.User_ID = loguinUser.User_ID;

                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostAccident(combinedData);

                return Json(new { result.Jiko_ID });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 差し戻しの登録
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PostRemand(int Jiko_WorkFlow_ID, string Reject_Reason, int Jiko_WorkFlow_Status_ID)
        {
            string errorMessage = null;
            try
            {
                Dto.V_LoginUser_Local loginUser = await GetLoginUser();
                Dto.PostRemand model = new PostRemand
                {
                    User_ID = loginUser.User_ID,
                    Reject_Reason = Reject_Reason,
                    Jiko_WorkFlow_ID = Jiko_WorkFlow_ID,
                    Jiko_WorkFlow_Status_ID = Jiko_WorkFlow_Status_ID
                };

                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostRemand(model);
                return Json(new { data = result, errorMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }

        /// <summary>
        /// 承認の登録
        /// </summary>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        public async Task<IActionResult> PostApproval(int Jiko_WorkFlow_Status_ID)
        {
            string errorMessage = null;
            try
            {
                Dto.V_LoginUser_Local loginUser = await GetLoginUser();
                Dto.PostRemand model = new PostRemand
                {
                    User_ID = loginUser.User_ID,
                    Jiko_WorkFlow_Status_ID = Jiko_WorkFlow_Status_ID
                };

                using API.WebApp.AccidentDataApi api = new(_mapApiSettiong);
                MsterDataCommonResultValDto_Local result = await api.PostApproval(model);

                return Json(new { data = result, errorMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { partialView = "", errorMessage = e.Message });
            }
        }
    }
}